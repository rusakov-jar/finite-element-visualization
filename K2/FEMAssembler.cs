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
        public double[,] globalMatrix;
        public double[] globalB;
        double[,] m_local;
        double[,] g_localA, g_localB, g_local;
        double[] b_local;
        // buildLocal заполняет переменные локальных матриц и вектора правой части одного элемента на основе вектора f
        // порядок обхода снизу вверх слева направо, лн, пн, лв, пв
        // порядок глобальной нумерации тоже снизу вверх, слева направо.
        public FEMAssembler(int N)
        {
            globalMatrix = new double[N,N];
            globalB = new double[N];
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
                for (int j = 0; j < 4; j++)
                {
                    globalMatrix[dict[i], dict[j]] += g_local[i, j] + m_local[i, j];
                }
                globalB[dict[i]] += b_local[i];
            }
            for(int i = 0; i < globalMatrix.GetLength(0); i++)
            {
                for (int j = 0;j < globalMatrix.GetLength(1); j++)
                {
                    Console.Write($"{globalMatrix[i, j],5}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void applyBoundaryCondition(Dictionary<int, double> dict )
        {
            foreach (int node_num in dict.Keys)
            {
                globalB[node_num] = dict[node_num];
                for (int i = 0; i < globalMatrix.GetLength(1); i++)
                {
                    globalMatrix[node_num, i] = 0;
                }
                globalMatrix[node_num, node_num] = 1;
                for (int i = 0; i < globalMatrix.GetLength(0); i++)
                {
                    if(globalMatrix[i, node_num] != 0 && i != node_num)
                    {
                        globalB[i] -= globalMatrix[i, node_num] * globalB[node_num];
                        globalMatrix[i, node_num] = 0;
                    }
                }
            }
        }
    }
}
