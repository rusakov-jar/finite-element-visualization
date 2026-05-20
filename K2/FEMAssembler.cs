using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2
{
    internal class FEMAssembler
    {
        public double[] globalB;

        public SparseMatrix sparseMatrix;
        private int Nx, Ny;


        double[,] m_local;
        double[,] g_localA, g_localB, g_local;
        double[] b_local;
        // buildLocal заполняет переменные локальных матриц и вектора правой части одного элемента на основе вектора f
        // порядок обхода снизу вверх слева направо, лн, пн, лв, пв
        // порядок глобальной нумерации тоже снизу вверх, слева направо.

        public FEMAssembler(int Nx, int Ny)
        {
            this.Nx = Nx;
            this.Ny = Ny;
            sparseMatrix = BuildPortrait(Nx, Ny);
            globalB = new double[Nx * Ny];
        }

        public struct SparseMatrix
        {
            public double[] di;   // диагональ, размер N
            public double[] al;   // внедиагональные нижнего треугольника, размер k
            public int[] ia;      // начала строк, размер N+1
            public int[] ja;      // номера столбцов, размер k
        }

        public static SparseMatrix BuildPortrait(int Nx, int Ny)
        {
            int N = Nx * Ny;
            var jaList = new List<int>();
            int[] ia = new int[N + 1];

            ia[0] = 0;

            for (int row = 0; row < N; row++)
            {
                int i = row / Nx;  // индекс по Y (снизу вверх)
                int j = row % Nx;  // индекс по X (слева направо)

                var neighbors = new List<int>();

                // Четыре смещения для соседей с меньшим номером
                (int di, int dj)[] offsets = { (-1, 0), (0, -1), (-1, -1), (-1, 1) };

                foreach (var (di, dj) in offsets)
                {
                    int ni = i + di;
                    int nj = j + dj;

                    if (ni >= 0 && ni < Ny && nj >= 0 && nj < Nx)
                    {
                        int col = ni * Nx + nj;
                        if (col < row)
                            neighbors.Add(col);
                    }
                }

                neighbors.Sort();
                jaList.AddRange(neighbors);
                ia[row + 1] = jaList.Count;
            }

            int k = jaList.Count;

            return new SparseMatrix
            {
                di = new double[N],
                al = new double[k],
                ia = ia,
                ja = jaList.ToArray()
            };
        }

        int GetAlIndex(int row, int col, int Nx, int[] ia)
        {
            int i = row / Nx;
            int j = row % Nx;
            int offset = 0;

            // Нижний левый
            if (i > 0 && j > 0)
            {
                if (col == row - Nx - 1) return ia[row] + offset;
                offset++;
            }
            // Нижний
            if (i > 0)
            {
                if (col == row - Nx) return ia[row] + offset;
                offset++;
            }
            // Нижний правый
            if (i > 0 && j < Nx - 1)
            {
                if (col == row - Nx + 1) return ia[row] + offset;
                offset++;
            }
            // Левый
            if (j > 0)
            {
                if (col == row - 1) return ia[row] + offset;
                offset++;
            }

            throw new Exception("Недопустимая пара (row, col)");
        }

        public void buildLocal(double hx, double hy, double lambda, double gamma, double[] f)
        {
            // порядок обхода лн, пн, лв, пв
            double k = gamma * hx * hy / 36;
            m_local = new double[,]{
                { k * 4, k * 2, k * 2, k * 1},
                { k * 2, k * 4, k * 1, k * 2},
                { k * 2, k * 1, k * 4, k * 2},
                { k * 1, k * 2, k * 2, k * 4}
            };
            k = lambda * hy / (6 * hx);
            g_localA = new double[,] {
                { k *  2, k * -2, k *  1, k * -1},
                { k * -2, k *  2, k * -1, k *  1},
                { k *  1, k * -1, k *  2, k * -2},
                { k * -1, k *  1, k * -2, k *  2}};
            k = lambda * hx / (6 * hy);
            g_localB = new double[,] {
                { k * 2, k * 1, k * -2, k * -1},
                { k * 1, k * 2, k * -1, k * -2},
                { k *-2, k *-1, k *  2, k *  1},
                { k *-1, k *-2, k *  1, k *  2}};

            g_local = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    g_local[i, j] = g_localA[i, j] + g_localB[i, j];
                }
            }
            k = hx * hy / 36;
            b_local = new double[]{
                 k*4*f[0] + k*2*f[1] + k*2*f[2] + k*1*f[3],
                 k*2*f[0] + k*4*f[1] + k*1*f[2] + k*2*f[3],
                 k*2*f[0] + k*1*f[1] + k*4*f[2] + k*2*f[3],
                 k*1*f[0] + k*2*f[1] + k*2*f[2] + k*4*f[3]
            };
        }

        public void addToGlobal(int low_left, int low_right, int top_left, int top_right)
        {
            int [] dict = new int[] 
            {
                low_left,
                low_right,
                top_left,
                top_right
            };
            for (int i = 0; i < 4; i++)
            {
                int gi = dict[i];
                for (int j = 0; j < 4; j++)
                {
                    int gj = dict[j];
                    double val = g_local[i, j] + m_local[i, j];

                    if (gi == gj)
                    {
                        sparseMatrix.di[gi] += val;
                    }
                    else if (gi > gj)
                    {
                        int idx = GetAlIndex(gi, gj, Nx, sparseMatrix.ia);
                        sparseMatrix.al[idx] += val;
                    }
                    else // gi < gj
                    {
                        int idx = GetAlIndex(gj, gi, Nx, sparseMatrix.ia);
                        sparseMatrix.al[idx] += val;
                    }
                }
                globalB[gi] += b_local[i];
            }
        }
        public void applyBoundaryCondition(Dictionary<int, double> dict)
        {
            foreach (int node in dict.Keys)
            {
                double value = dict[node];
                int i = node / Nx;
                int j = node % Nx;

                // 1. Обрабатываем нижний треугольник (соседи col < node)
                // Проходим по всем возможным соседям с меньшим номером
                // и обнуляем их, модифицируя правую часть
                int pos = sparseMatrix.ia[node];

                // Нижний левый
                if (i > 0 && j > 0)
                {
                    int col = node - Nx - 1;
                    int idx = pos;
                    globalB[col] -= sparseMatrix.al[idx] * value;
                    sparseMatrix.al[idx] = 0;
                    pos++;
                }
                // Нижний
                if (i > 0)
                {
                    int col = node - Nx;
                    int idx = pos;
                    globalB[col] -= sparseMatrix.al[idx] * value;
                    sparseMatrix.al[idx] = 0;
                    pos++;
                }
                // Нижний правый
                if (i > 0 && j < Nx - 1)
                {
                    int col = node - Nx + 1;
                    int idx = pos;
                    globalB[col] -= sparseMatrix.al[idx] * value;
                    sparseMatrix.al[idx] = 0;
                    pos++;
                }
                // Левый
                if (j > 0)
                {
                    int col = node - 1;
                    int idx = pos;
                    globalB[col] -= sparseMatrix.al[idx] * value;
                    sparseMatrix.al[idx] = 0;
                    pos++;
                }

                // 2. Обрабатываем верхний треугольник (строки row > node, где node — сосед)
                // node является соседом для строк row, которые выше и/или правее
                // Перебираем возможные строки, у которых node может быть соседом

                // Как нижний левый для строки (node + Nx + 1)
                if (i < Ny - 1 && j < Nx - 1)
                {
                    int row = node + Nx + 1;
                    int idx = GetAlIndex(row, node, Nx, sparseMatrix.ia);
                    globalB[row] -= sparseMatrix.al[idx] * value;
                    sparseMatrix.al[idx] = 0;
                }
                // Как нижний для строки (node + Nx)
                if (i < Ny - 1)
                {
                    int row = node + Nx;
                    int idx = GetAlIndex(row, node, Nx, sparseMatrix.ia);
                    globalB[row] -= sparseMatrix.al[idx] * value;
                    sparseMatrix.al[idx] = 0;
                }
                // Как нижний правый для строки (node + Nx - 1)
                if (i < Ny - 1 && j > 0)
                {
                    int row = node + Nx - 1;
                    int idx = GetAlIndex(row, node, Nx, sparseMatrix.ia);
                    globalB[row] -= sparseMatrix.al[idx] * value;
                    sparseMatrix.al[idx] = 0;
                }
                // Как левый для строки (node + 1)
                if (j < Nx - 1)
                {
                    int row = node + 1;
                    int idx = GetAlIndex(row, node, Nx, sparseMatrix.ia);
                    globalB[row] -= sparseMatrix.al[idx] * value;
                    sparseMatrix.al[idx] = 0;
                }

                // 3. Обнуляем диагональ и правую часть для самого узла
                sparseMatrix.di[node] = 1;
                globalB[node] = value;
            }
        }
        public double[,] ToDense()
        {
            int N = Nx * Ny;
            double[,] dense = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                dense[i, i] = sparseMatrix.di[i];

                for (int k = sparseMatrix.ia[i]; k < sparseMatrix.ia[i + 1]; k++)
                {
                    int j = sparseMatrix.ja[k];
                    double val = sparseMatrix.al[k];
                    dense[i, j] = val;
                    dense[j, i] = val; // симметрия
                }
            }

            return dense;
        }
    }
}