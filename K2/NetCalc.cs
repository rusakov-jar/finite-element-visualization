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
        public NetCalc(float q1_p, float q2_p) {
            q1 = q1_p;
            q2 = q2_p;
        }
        public float q1, q2;

        public static float ScaleCalc(float x_size, float y_size, int panel_x_size, int panel_y_size)
        {
            if (x_size < 0 || y_size < 0)
            {
                return 1;
            }
            float x_scale = panel_x_size / x_size;
            float y_scale = panel_y_size / y_size;
            if (x_scale < y_scale)
            {
                return x_scale;
            }
            else
            {
                return y_scale;
            }
        }

        public List<float> MakeSplit(float a, float b, int n) // равномерное разбиение через n
        {
            n--;
            List<float> res = new List<float>();
            float step = (b - a) / n;
            for (int i = 0; i < n; i++)
            {
                res.Add(a + step * i);
            }
            res.Add(b);
            return res;
        }

        public List<float> MakeSplit(float a, float b, float h, float eps, string mode_str) // равномерное разбиение через h
        {
            List<float> res = new List<float>();
            int n = (int)((b - a) / h);
            float step = (b - a) / n;
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

        public List<float> MakeSplit(float a, float b, int n, float h0, float q, int mode, float eps, string mode_str)
        {
            List<float> res = new List<float> ();
            float S = Math.Abs(b - a);
            if (mode == 1)// h0, n
            {
                q = Method_Dihotomia(q1, q2, eps, h0, S, n);
                n--;
            }
            if (mode == 2) // h0, q
            {
                double arg1 = 1 - (1 - q) * S / h0;
                n = Round((float)Math.Log(arg1, q), eps);
                if (mode_str == "h")
                {
                    h0 = S * (1 - q) / (1 - (float)Math.Pow(q, n));
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
                h0 = S * (1 - q) / (1 - (float)Math.Pow(q, n));
            }
            float h_curr = h0;
            float Prev = a;
            for (int i = 0; i < n; i++)
            {
                res.Add(Prev);
                Prev = Prev + h_curr;
                h_curr *= q;
            }
            res.Add(b);
            return res;
        }

        public int Round(float x, float eps)
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
        public float Method_Dihotomia(float a, float b, float eps, float h0, float S, int n)
        {
            n--;
            float x;
            float f, fa, fb;
            x = (a + b) / 2;
            f = (float)(S - h0 * (1.0 - Math.Pow(x, n)) / (1.0 - x));
            while (Math.Abs(f) > eps)     
            {
                x = (a + b) / 2;
                f = (float)(S-h0 * (1.0 - Math.Pow(x, n)) / (1.0 - x));
                fa = (float)(S - h0 * (1.0 - Math.Pow(a, n)) / (1.0 - a));
                fb = (float)(S - h0 * (1.0 - Math.Pow(b, n)) / (1.0 - b));
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
        public SmallNet makeNet(List<float> x_split, List<float> y_split) 
        {
            SmallNet res = new SmallNet ();
            res.nodes = new PointF[x_split.Count, y_split.Count];
            res.net_size.Width = x_split[x_split.Count - 1] - x_split[0];
            res.net_size.Height = y_split[y_split.Count - 1] - y_split[0];
            float x_step1 = x_split[1] - x_split[0];
            float x_step2 = x_split[x_split.Count - 1] - x_split[x_split.Count - 2];
            float y_step1 = y_split[1] - y_split[0];
            float y_step2 = y_split[y_split.Count - 1] - y_split[y_split.Count - 2];
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
                    res.nodes[i, j].X = x_split[i];
                    res.nodes[i, j].Y = y_split[j];
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
        public Pen pen = new Pen(Color.Blue);
        public float min_x_step;
        public float min_y_step;
        public float max_x_step;
        public float max_y_step;
        public float h0X, h0Y, qx,qy;
        public int nx, ny;
    }
}