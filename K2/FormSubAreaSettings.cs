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
    public partial class FormSubAreaSettings : Form
    {
        public FormSubAreaSettings()
        {
            InitializeComponent();
        }
        public float h0 = 0;
        public int n = 0;
        public float q = 0;
        bool mono = true;
        public int state = -1;
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            state = -1;
            DialogResult = DialogResult.Cancel;
            if (tabControl1.SelectedTab.Text == "равномерная")
            {
                mono = true;
                switch (tabControl2.SelectedTab.Text)
                {
                    case "n":
                        n = (int)numericUpDownMonoNNum.Value;
                        state = 1;
                        break;
                    case "h":
                        if (!float.TryParse(textBoxMonoStepSize.Text, out h0))
                        {
                            MessageBox.Show("Некорректное значение шага");
                            return;
                        }
                        state = 2;
                        break;
                }
            }
            else
            {
                mono = false;
                switch (tabControl3.SelectedTab.Text)
                {
                    case "h0, n":
                        if (!float.TryParse(textBox1H0.Text, out h0))
                        {
                            MessageBox.Show("Некорректное значение шага");
                            return;
                        }
                        n = (int)numericUpDown1.Value;
                        state = 3;
                        break;
                    case "h0, q":
                        if (!float.TryParse(textBox2H0.Text, out h0))
                        {
                            MessageBox.Show("Некорректное значение шага");
                            return;
                        }
                        if (!float.TryParse(textBox2Q.Text, out q))
                        {
                            MessageBox.Show("Некорректное значение q");
                            return;
                        }
                        state = 4;
                        break;
                    case "n, q":
                        if (!float.TryParse(textBox3Q.Text, out q))
                        {
                            MessageBox.Show("Некорректное значение q");
                            return;
                        }
                        n = (int)numericUpDown3.Value;
                        state = 5;
                        break;
                }
            }
            DialogResult = DialogResult.OK;
            Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            state = -1;
            Hide();
        }
    }
}
