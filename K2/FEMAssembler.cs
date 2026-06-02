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

        public SparseMatrix sparse_matrix;
        private int Nx, Ny, basis_type;


        double[,] m_local;
        double[,] g_localA, g_localB, g_local;
        double[] b_local;
        // buildLocal заполняет переменные локальных матриц и вектора правой части одного элемента на основе вектора f
        // порядок обхода снизу вверх слева направо, лн, пн, лв, пв
        // порядок глобальной нумерации тоже снизу вверх, слева направо.

        public FEMAssembler(int Nx, int Ny, int b_type = 1)
        {
            this.Nx = Nx;
            this.Ny = Ny;
            this.basis_type = b_type;
            sparse_matrix = BuildPortrait(Nx, Ny);
            globalB = new double[Nx * Ny];
        }

        public struct SparseMatrix
        {
            public double[] di;   // диагональ, размер N
            public double[] al;   // внедиагональные нижнего треугольника, размер k
            public int[] ia;      // начала строк, размер N+1
            public int[] ja;      // номера столбцов, размер k
        }

        public static SparseMatrix BuildPortrait(int nx, int ny, int nodesPerElementX = 2, int nodesPerElementY = 2)
        {
            int N = nx * ny;
            var jaSet = new SortedSet<int>[N];
            for (int i = 0; i < N; i++)
                jaSet[i] = new SortedSet<int>();

            // Добавление связи (только нижний треугольник)
            void AddLink(int row, int col)
            {
                if (row == col) return;
                if (col < row)
                    jaSet[row].Add(col);
                else
                    jaSet[col].Add(row);
            }

            // Цикл по элементам
            // Элемент размера nodesPerElementX × nodesPerElementY узлов
            for (int i = 0; i <= ny - nodesPerElementY; i++)
            {
                for (int j = 0; j <= nx - nodesPerElementX; j++)
                {
                    // Собираем глобальные индексы всех узлов элемента
                    int[] elementNodes = new int[nodesPerElementX * nodesPerElementY];
                    int idx = 0;
                    for (int di = 0; di < nodesPerElementY; di++)
                        for (int dj = 0; dj < nodesPerElementX; dj++)
                            elementNodes[idx++] = (i + di) * nx + (j + dj);

                    // Добавляем связи между всеми парами узлов
                    for (int p = 0; p < elementNodes.Length; p++)
                        for (int q = p + 1; q < elementNodes.Length; q++)
                            AddLink(elementNodes[p], elementNodes[q]);
                }
            }

            // Формируем ia и ja из jaSet
            int[] ia = new int[N + 1];
            List<int> jaList = new List<int>();

            for (int row = 0; row < N; row++)
            {
                ia[row] = jaList.Count;
                foreach (int col in jaSet[row])
                    jaList.Add(col);
            }
            ia[N] = jaList.Count;

            return new SparseMatrix
            {
                di = new double[N],
                al = new double[jaList.Count],
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
                    if (gi < gj) continue; // пропускаем верхний треугольник

                    double val = g_local[i, j] + m_local[i, j];

                    if (gi == gj)
                    {
                        sparse_matrix.di[gi] += val;
                    }
                    else // gi > gj
                    {
                        int idx = GetAlIndex(gi, gj, Nx, sparse_matrix.ia);
                        sparse_matrix.al[idx] += val;
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
                int pos = sparse_matrix.ia[node];

                // Нижний левый
                if (i > 0 && j > 0)
                {
                    int col = node - Nx - 1;
                    int idx = pos;
                    globalB[col] -= sparse_matrix.al[idx] * value;
                    sparse_matrix.al[idx] = 0;
                    pos++;
                }
                // Нижний
                if (i > 0)
                {
                    int col = node - Nx;
                    int idx = pos;
                    globalB[col] -= sparse_matrix.al[idx] * value;
                    sparse_matrix.al[idx] = 0;
                    pos++;
                }
                // Нижний правый
                if (i > 0 && j < Nx - 1)
                {
                    int col = node - Nx + 1;
                    int idx = pos;
                    globalB[col] -= sparse_matrix.al[idx] * value;
                    sparse_matrix.al[idx] = 0;
                    pos++;
                }
                // Левый
                if (j > 0)
                {
                    int col = node - 1;
                    int idx = pos;
                    globalB[col] -= sparse_matrix.al[idx] * value;
                    sparse_matrix.al[idx] = 0;
                    pos++;
                }

                // 2. Обрабатываем верхний треугольник (строки row > node, где node — сосед)
                // node является соседом для строк row, которые выше и/или правее
                // Перебираем возможные строки, у которых node может быть соседом

                // Как нижний левый для строки (node + Nx + 1)
                if (i < Ny - 1 && j < Nx - 1)
                {
                    int row = node + Nx + 1;
                    int idx = GetAlIndex(row, node, Nx, sparse_matrix.ia);
                    globalB[row] -= sparse_matrix.al[idx] * value;
                    sparse_matrix.al[idx] = 0;
                }
                // Как нижний для строки (node + Nx)
                if (i < Ny - 1)
                {
                    int row = node + Nx;
                    int idx = GetAlIndex(row, node, Nx, sparse_matrix.ia);
                    globalB[row] -= sparse_matrix.al[idx] * value;
                    sparse_matrix.al[idx] = 0;
                }
                // Как нижний правый для строки (node + Nx - 1)
                if (i < Ny - 1 && j > 0)
                {
                    int row = node + Nx - 1;
                    int idx = GetAlIndex(row, node, Nx, sparse_matrix.ia);
                    globalB[row] -= sparse_matrix.al[idx] * value;
                    sparse_matrix.al[idx] = 0;
                }
                // Как левый для строки (node + 1)
                if (j < Nx - 1)
                {
                    int row = node + 1;
                    int idx = GetAlIndex(row, node, Nx, sparse_matrix.ia);
                    globalB[row] -= sparse_matrix.al[idx] * value;
                    sparse_matrix.al[idx] = 0;
                }

                // 3. Обнуляем диагональ и правую часть для самого узла
                sparse_matrix.di[node] = 1;
                globalB[node] = value;
            }
        }

        public void Multiply(double[] x, double[] y)
        {
            int N = Nx * Ny;
            Array.Clear(y, 0, N);
            for (int i = 0; i < N; i++)
                y[i] += sparse_matrix.di[i] * x[i];
            for (int i = 0; i < N; i++)
            {
                for (int k = sparse_matrix.ia[i]; k < sparse_matrix.ia[i + 1]; k++)
                {
                    int j = sparse_matrix.ja[k];
                    double a_ij = sparse_matrix.al[k];
                    y[i] += a_ij * x[j];
                    y[j] += a_ij * x[i];
                }
            }
        }
    }
}