using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2
{
    internal class Gauss
    {
        public static double[] Solve(double[,] matrix, double[] rightPart)
        {
            int n = matrix.GetLength(0);

            // Создаем расширенную матрицу
            double[][] augmentedMatrix = new double[n][];
            for (int i = 0; i < n; i++)
            {
                augmentedMatrix[i] = new double[n + 1];
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i][j] = matrix[i,j];
                }
                augmentedMatrix[i][n] = rightPart[i];
            }

            // Прямой ход метода Гаусса
            for (int i = 0; i < n; i++)
            {
                // Проверка диагонального элемента на ноль
                if (Math.Abs(augmentedMatrix[i][i]) < 1e-10)
                    throw new InvalidOperationException($"Нулевой диагональный элемент на шаге {i}. Матрица вырождена.");

                // Нормализация текущей строки
                double pivot = augmentedMatrix[i][i];
                for (int j = i; j <= n; j++)
                {
                    augmentedMatrix[i][j] /= pivot;
                }

                // Исключение переменной из остальных строк
                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        double factor = augmentedMatrix[k][i];
                        for (int j = i; j <= n; j++)
                        {
                            augmentedMatrix[k][j] -= factor * augmentedMatrix[i][j];
                        }
                    }
                }
            }

            // Получаем решение
            double[] solution = new double[n];
            for (int i = 0; i < n; i++)
            {
                solution[i] = augmentedMatrix[i][n];
            }

            return solution;
        }
    }
}