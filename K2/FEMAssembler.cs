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
        // порядок обхода снизу вверх слева направо
        // порядок глобальной нумерации тоже снизу вверх, слева направо.

        public FEMAssembler(int Nx, int Ny, int b_type)
        {
            this.Nx = Nx;
            this.Ny = Ny;
            this.basis_type = b_type;
            sparse_matrix = BuildPortrait(Nx, Ny, b_type + 1);
            globalB = new double[Nx * Ny];
        }

        public struct SparseMatrix
        {
            public double[] di;   // диагональ, размер N
            public double[] al;   // внедиагональные нижнего треугольника, размер k
            public int[] ia;      // начала строк, размер N+1
            public int[] ja;      // номера столбцов, размер k
        }

        public static SparseMatrix BuildPortrait(int nx, int ny, int nodesPerElementAxis)
        {
            int N = nx * ny;

            var jaSet = new List<int>[N];
            for (int i = 0; i < N; i++)
                jaSet[i] = new List<int>();

            // Количество элементов по осям
            int xElementsNum = (nx - 1) / (nodesPerElementAxis - 1);
            int yElementsNum = (ny - 1) / (nodesPerElementAxis - 1);

            // Буфер для глобальных индексов узлов одного элемента
            int nodesPerElement = nodesPerElementAxis * nodesPerElementAxis;
            int[] elementNodes = new int[nodesPerElement];

            // Цикл по элементам сетки
            for (int elementRow = 0; elementRow < yElementsNum; elementRow++)
            {
                for (int elementCol = 0; elementCol < xElementsNum; elementCol++)
                {
                    // Вычисляем глобальные координаты левого нижнего узла элемента
                    int globalNodeRow = elementRow * (nodesPerElementAxis - 1);
                    int globalNodeCol = elementCol * (nodesPerElementAxis - 1);
                    int globalCornerNodeNum = globalNodeRow * nx + globalNodeCol;

                    // Собираем глобальные индексы всех узлов элемента
                    int idx = 0;
                    for (int localNodeRow = 0; localNodeRow < nodesPerElementAxis; localNodeRow++)
                    {
                        for (int localNodeCol = 0; localNodeCol < nodesPerElementAxis; localNodeCol++)
                        {
                            elementNodes[idx++] = globalCornerNodeNum + localNodeRow * nx + localNodeCol;
                        }
                    }

                    // Связываем все узлы элемента (строго нижний треугольник)
                    for (int p = 0; p < nodesPerElement; p++)
                    {
                        for (int q = 0; q < nodesPerElement; q++)
                        {
                            int row = elementNodes[p];
                            int col = elementNodes[q];

                            // Добавляем в портрет только нижний треугольник (row > col)
                            if (row > col)
                            {
                                jaSet[row].Add(col);
                            }
                        }
                    }
                }
            }

            // Формируем ia и ja
            int[] ia = new int[N + 1];
            List<int> jaList = new List<int>();

            for (int row = 0; row < N; row++)
            {
                ia[row] = jaList.Count;

                // Сортировка и удаление дубликатов
                var uniqueSortedCols = jaSet[row].Distinct().OrderBy(c => c);

                foreach (int col in uniqueSortedCols)
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

        int GetAlIndex(int row, int col, int[] ia, int[] ja)
        {
            for (int k = ia[row]; k < ia[row + 1]; k++)
                if (ja[k] == col) return k;
            return -1;
        }

        public void buildLocal(double hx, double hy, double lambda, double gamma, double[] f)
        {
            if (basis_type == 1)
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
            else if(basis_type == 2)
            {
                BuildLocalBiquadraticMatrix(hx, hy, lambda, gamma, f);
            }
            else if(basis_type == 3)
            {
                BuildLocalBicubicMatrix(hx, hy, lambda, gamma, f);
            }
        }

        public void addToGlobal(int[] dict)
        {
            int d_len = dict.Length;
            for (int i = 0; i < d_len; i++)
            {
                int gi = dict[i];
                for (int j = 0; j < d_len; j++)
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
                        int idx = GetAlIndex(gi, gj, sparse_matrix.ia, sparse_matrix.ja);
                        sparse_matrix.al[idx] += val;
                    }
                }
                globalB[gi] += b_local[i];
            }
        }

        public void applyBoundaryCondition(Dictionary<int, double> dict)
        {
            int N = this.Nx * this.Ny;

            foreach (var kvp in dict)
            {
                int node = kvp.Key;
                double value = kvp.Value;

                // Зануляем нижний треугольник для текущей строки (col < node)
                for (int k = sparse_matrix.ia[node]; k < sparse_matrix.ia[node + 1]; k++)
                {
                    int col = sparse_matrix.ja[k];

                    if (!dict.Keys.Contains(col))
                    {
                        globalB[col] -= sparse_matrix.al[k] * value;
                    }
                    sparse_matrix.al[k] = 0.0;
                }

                // Выставляем значение на диагональ
                sparse_matrix.di[node] = 1.0;
                globalB[node] = value;
            }

            // Зануляем столбцы (Верхний треугольник, где row > col)
            for (int row = 0; row < N; row++)
            {
                bool isRowBoundary = dict.ContainsKey(row);
                if (isRowBoundary)
                    continue;

                for (int k = sparse_matrix.ia[row]; k < sparse_matrix.ia[row + 1]; k++)
                {
                    int col = sparse_matrix.ja[k]; 

                    // Если столбец (col) является граничным узлом Дирихле
                    if (dict.TryGetValue(col, out double boundaryValue))
                    {
                        // переносим известное значение в правую часть
                        globalB[row] -= sparse_matrix.al[k] * boundaryValue;

                        // В любом случае зануляем этот элемент матрицы для сохранения симметрии
                        sparse_matrix.al[k] = 0.0;
                    }
                }
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

        void BuildLocalBiquadraticMatrix(double hx, double hy, double lambda, double gamma, double[] f)
        {
            // Одномерные матрицы 3×3
            double factor = 1.0 / (3.0 * hx);
            double[,] Gx = new double[,]
            {
                {factor *  7, factor * -8, factor *  1},
                {factor * -8, factor * 16, factor * -8},
                {factor *  1, factor * -8, factor *  7}
            };
            factor = 1.0 / (3.0 * hy);
            double[,] Gy = new double[,]
            {
                {factor *  7, factor * -8, factor *  1},
                {factor * -8, factor * 16, factor * -8},
                {factor *  1, factor * -8, factor *  7}
            };
            factor = hx / 30.0;
            double[,] Mx = new double[,]
            {
                {factor *  4, factor *  2, factor * -1},
                {factor *  2, factor * 16, factor *  2},
                {factor * -1, factor *  2, factor *  4}
            };
            factor = hy / 30.0;
            double[,] My = new double[,]
            {
                {factor *  4, factor *  2, factor * -1},
                {factor *  2, factor * 16, factor *  2},
                {factor * -1, factor *  2, factor *  4}
            };

            g_local = new double[9, 9];
            m_local = new double[9, 9];
            b_local = new double[9];

            for (int i = 0; i < 9; i++)
            {
                int iy = i / 3, ix = i % 3;
                b_local[i] = 0;
                for (int j = 0; j < 9; j++)
                {
                    int jy = j / 3, jx = j % 3;
                    double M = Mx[ix, jx] * My[iy, jy];
                    g_local[i, j] = lambda * (Gx[ix, jx] * My[iy, jy] + Mx[ix, jx] * Gy[iy, jy]);
                    m_local[i, j] = gamma * M;

                    b_local[i] += M * f[j];
                }
            }
        }

        void BuildLocalBicubicMatrix(double hx, double hy, double lambda, double gamma, double[] f)
        {
            // Одномерные матрицы 4x4
            double factor = 1.0 / (40.0 * hx);
            double[,] Gx = new double[,]
            {
                { factor *  148, factor * -189, factor *   54, factor *  -13},
                { factor * -189, factor *  432, factor * -297, factor *   54},
                { factor *   54, factor * -297, factor *  432, factor * -189},
                { factor *  -13, factor *   54, factor * -189, factor *  148}
            };
            factor = 1.0 / (40.0 * hy);
            double[,] Gy = new double[,]
            {
                { factor *  148, factor * -189, factor *   54, factor *  -13},
                { factor * -189, factor *  432, factor * -297, factor *   54},
                { factor *   54, factor * -297, factor *  432, factor * -189},
                { factor *  -13, factor *   54, factor * -189, factor *  148}
            };
            factor = hx / 1680.0;
            double[,] Mx = new double[,]
            {
                { factor * 128, factor *  99, factor * -36, factor *  19},
                { factor *  99, factor * 648, factor * -81, factor * -36},
                { factor * -36, factor * -81, factor * 648, factor *  99},
                { factor *  19, factor * -36, factor *  99, factor * 128}
            };
            factor = hy / 1680.0;
            double[,] My = new double[,]
            {
                { factor * 128, factor *  99, factor * -36, factor *  19},
                { factor *  99, factor * 648, factor * -81, factor * -36},
                { factor * -36, factor * -81, factor * 648, factor *  99},
                { factor *  19, factor * -36, factor *  99, factor * 128}
            };

            g_local = new double[16, 16];
            m_local = new double[16, 16];
            b_local = new double[16];

            for (int i = 0; i < 16; i++)
            {
                int iy = i / 4, ix = i % 4;
                b_local[i] = 0;
                for (int j = 0; j < 16; j++)
                {
                    int jy = j / 4, jx = j % 4;
                    double M = Mx[ix, jx] * My[iy, jy];
                    g_local[i, j] = lambda * (Gx[ix, jx] * My[iy, jy] + Mx[ix, jx] * Gy[iy, jy]);
                    m_local[i, j] = gamma * M;

                    b_local[i] += M * f[j];
                }
            }
        }
    }
}