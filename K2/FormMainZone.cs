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
    public partial class FormMainZone : Form
    {
        public FormMainZone()
        {
            InitializeComponent();
        }
        public float x1, x2, y1, y2;

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public bool correct;


        private void button1_Click(object sender, EventArgs e)
        {
            correct = true;
            if (!float.TryParse(textBoxX1.Text, out x1))
            {
                correct = false;
                string msg = "Некорректный Х1";
                MessageBox.Show(msg);
            }
            if (!float.TryParse(textBoxX2.Text, out x2))
            {
                correct = false;
                string msg = "Некорректный Х2";
                MessageBox.Show(msg);
            }
            if (!float.TryParse(textBoxY1.Text, out y1))
            {
                correct = false;
                string msg = "Некорректный Y1";
                MessageBox.Show(msg);
            }
            if (!float.TryParse(textBoxY2.Text, out y2))
            {
                correct = false;
                string msg = "Некорректный Y2";
                MessageBox.Show(msg);
            }
            if (correct)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
