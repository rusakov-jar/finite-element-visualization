using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2
{
    public static class CGSolver
    {
        public static double[] Solve(Func<double[], double[]> multiply, double[] b,
                                     double tolerance = 1e-13, int maxIter = 100000)
        {
            int N = b.Length;
            double[] x = new double[N];
            double[] r = new double[N];
            double[] p = new double[N];
            double[] Ap;

            Array.Copy(b, r, N);
            Array.Copy(r, p, N);
            double rsold = Dot(r, r);

            for (int iter = 0; iter < maxIter; iter++)
            {
                Ap = multiply(p);                // лямбда возвращает новый массив
                double alpha = rsold / Dot(p, Ap);

                for (int i = 0; i < N; i++)
                    x[i] += alpha * p[i];
                for (int i = 0; i < N; i++)
                    r[i] -= alpha * Ap[i];

                double rsnew = Dot(r, r);
                if (Math.Sqrt(rsnew) < tolerance)
                    break;

                double beta = rsnew / rsold;
                for (int i = 0; i < N; i++)
                    p[i] = r[i] + beta * p[i];
                rsold = rsnew;
            }
            return x;
        }

        private static double Dot(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
                sum += a[i] * b[i];
            return sum;
        }
    }
}
