using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K2
{
    public partial class Settings : Form
    {
        public float eps;
        public float q1, q2;
        public string mode;

        public Settings()
        {
            InitializeComponent(); 
        }

        public void Func(string mstr)
        {
            string[] tmp = mstr.Split(' ');
            if (!float.TryParse(tmp[0], out eps)) eps = 0.05f;
            if (!float.TryParse(tmp[1], out q1)) q1 = 0;
            if (!float.TryParse(tmp[2], out q2)) q2 = 10;
            mode = tmp[3];
            textBoxEps.Text = eps.ToString();
            textBoxQ1.Text = q1.ToString();
            textBoxQ2.Text = q2.ToString();
            listBoxMode.Text = mode;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(textBoxEps.Text, out eps)) { eps = 0.05f; MessageBox.Show("Неверно задан eps. Применено значение по умолчанию"); }
            if (!float.TryParse(textBoxQ1.Text, out q1)) { q1 = 0; MessageBox.Show("Неверно задан q1. Применено значение по умолчанию"); }
            if (!float.TryParse(textBoxQ2.Text, out q2)) { q2 = 10; MessageBox.Show("Неверно задан q2. Применено значение по умолчанию"); }
            mode = listBoxMode.Text;
            string txt = eps.ToString() + " " + q1.ToString() + " " + q2.ToString() + " " + mode;
            System.IO.File.WriteAllText("../../settings.txt", txt);
            textBoxQ1.Text = q1.ToString();
            textBoxQ2.Text = q2.ToString();
            listBoxMode.Text = mode;
            Close();
        }
    }
}
