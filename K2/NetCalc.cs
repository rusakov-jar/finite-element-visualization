using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K2
{
    public class NetCalc
    {
        public NetCalc(double q1_p, double q2_p) {
            q1 = q1_p;
            q2 = q2_p;
        }
        public double q1, q2;

        public static double ScaleCalc(double x_size, double y_size, int panel_x_size, int panel_y_size)
        {
            if (x_size < 0 || y_size < 0)
            {
                return 1;
            }
            double x_scale = panel_x_size / x_size;
            double y_scale = panel_y_size / y_size;
            if (x_scale < y_scale)
            {
                return x_scale;
            }
            else
            {
                return y_scale;
            }
        }

        public List<double> MakeSplit(double a, double b, int n) // равномерное разбиение через n
        {
            n--;
            List<double> res = new List<double>();
            double step = (b - a) / n;
            for (int i = 0; i < n; i++)
            {
                res.Add(a + step * i);
            }
            res.Add(b);
            return res;
        }

        public List<double> MakeSplit(double a, double b, double h, double eps, string mode_str) // равномерное разбиение через h
        {
            List<double> res = new List<double>();
            int n = (int)((b - a) / h);
            double step = (b - a) / n;
            if( (step - h) / h < 1 - eps && (step - h) / h > eps)
            {
                n = (int)Math.Round((b - a) / h);//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                step = (b - a) / n;
            }
            if ((step - h) / h >= 1 - eps)
            {
                n++;
                step = (b - a) / n;
            }
            for (int i = 0; i < n; i++)
            {
                res.Add(a + step * i);
            }
            res.Add(b);
            return res;
        }

        public List<double> MakeSplit(double a, double b, int n, double h0, double q, int mode, double eps, string mode_str)
        {
            List<double> res = new List<double> ();
            double S = Math.Abs(b - a);
            if (mode == 1)// h0, n
            {
                q = Method_Dihotomia(q1, q2, eps, h0, S, n);
                n--;
            }
            if (mode == 2) // h0, q
            {
                double arg1 = 1 - (1 - q) * S / h0;
                n = Round((double)Math.Log(arg1, q), eps);
                if (mode_str == "h")
                {
                    h0 = S * (1 - q) / (1 - (double)Math.Pow(q, n));
                }
                else if (mode_str == "q")
                {
                    q = Method_Dihotomia(q1, q2, eps, h0, S, n);
                    n--;
                }
            }
            if (mode == 3) // n, q
            {
                n--;
                h0 = S * (1 - q) / (1 - (double)Math.Pow(q, n));
            }
            double h_curr = h0;
            double Prev = a;
            for (int i = 0; i < n; i++)
            {
                res.Add(Prev);
                Prev = Prev + h_curr;
                h_curr *= q;
            }
            res.Add(b);
            return res;
        }

        public int Round(double x, double eps)
        {
            int x2 = (int)x;
            if(Math.Abs(x - x2) < eps)
            {
                return x2;
            }
            else if (Math.Abs(x - (x2 + 1)) < eps)
            {
                return x2 + 1;
            }
            else
            {
                return (int)Math.Round(x);
            }
        }
        public double Method_Dihotomia(double a, double b, double eps, double h0, double S, int n)
        {
            n--;
            double x;
            double f, fa, fb;
            x = (a + b) / 2;
            f = (double)(S - h0 * (1.0 - Math.Pow(x, n)) / (1.0 - x));
            while (Math.Abs(f) > eps)     
            {
                x = (a + b) / 2;
                f = (double)(S-h0 * (1.0 - Math.Pow(x, n)) / (1.0 - x));
                fa = (double)(S - h0 * (1.0 - Math.Pow(a, n)) / (1.0 - a));
                fb = (double)(S - h0 * (1.0 - Math.Pow(b, n)) / (1.0 - b));
                if (Math.Abs(f) < eps)
                {
                    return x;
                }
                if (fa*f > 0)
                {
                    a = x;
                }
                else if(fb * f > 0)
                {
                    b = x;
                }
            }
            return (a + b) / 2;
        }
        public SmallNet makeNet(List<double> x_split, List<double> y_split) 
        {
            SmallNet res = new SmallNet ();
            res.nodes = new PointF[x_split.Count, y_split.Count];
            res.net_size.Width = (float)(x_split[x_split.Count - 1] - x_split[0]);
            res.net_size.Height = (float)(y_split[y_split.Count - 1] - y_split[0]);
            double x_step1 = x_split[1] - x_split[0];
            double x_step2 = x_split[x_split.Count - 1] - x_split[x_split.Count - 2];
            double y_step1 = y_split[1] - y_split[0];
            double y_step2 = y_split[y_split.Count - 1] - y_split[y_split.Count - 2];
            res.max_x_step = x_step2;
            res.min_x_step = x_step1;
            if(x_step1 > x_step2) { res.max_x_step = x_step1; res.min_x_step = x_step2; }
            res.max_y_step = y_step2;
            res.min_y_step = y_step1;
            if (y_step1 > y_step2) { res.max_y_step = y_step1; res.min_y_step = y_step2; }
            for (int j = 0; j < y_split.Count; j++)
            {
                for(int i = 0;i < x_split.Count; i++)
                {
                    res.nodes[i, j].X = (float)x_split[i];
                    res.nodes[i, j].Y = (float)y_split[j];
                }
            }
            res.nx = res.nodes.GetLength(0);
            res.ny = res.nodes.GetLength(1);
            res.qx = 1;
            if (res.nx > 2)
            {
                res.qx = (res.nodes[2, 0].X - res.nodes[1, 0].X) / x_step1;
            }
            res.qy = 1;
            if (res.ny > 2)
            {
                res.qy = (res.nodes[0, 2].Y - res.nodes[0, 1].Y) / y_step1;
            }
            res.h0X = x_split[1] - x_split[0]; ;
            res.h0Y = y_split[1] - y_split[0]; ;
            return res;
        }
    }
    public class SmallNet
    {
        public PointF[,] nodes;
        public SizeF net_size = new SizeF(-1,-1);
        public Pen pen = new Pen(Color.Black);
        public double min_x_step;
        public double min_y_step;
        public double max_x_step;
        public double max_y_step;
        public double h0X, h0Y, qx,qy;
        public int nx, ny;
    }
}