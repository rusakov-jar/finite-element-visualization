using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K2
{
    public partial class Form1 : Form
    {
        FormWindowState last_state = FormWindowState.Normal;

        List<float> LXPoints; // точки разбиения большой сетки(сырые координаты)
        List<float> LYPoints;
        List<float> global_net_x, global_net_y;
        float scale = 1;
        float initial_scale = 1;
        float orig_width;
        float orig_height;
        float orig_x1, orig_x2, orig_y1, orig_y2;
        FormSubAreaSettings f_set_sub = new FormSubAreaSettings();
        List<float>[] XSplits; // разбиения по осям(сырые координаты)
        List<float>[] YSplits;
        public string settings_str;
        public float eps, q1, q2;
        public string mode;

        private double[] solution;


        public class SelectedCell
        {
            public float top_left_x = -1; // координаты узла для отрисовки(без поправки на масштаб)
            public float top_left_y = -1;
            public float bottom_right_x = -1;
            public float bottom_right_y = -1;
            public int cell_x_num = -1; //координаты узла по номеру(индексы для subareas)
            public int cell_y_num = -1;
        }

        SelectedCell SelCell;
        SmallNet[,] subareas;

        public Form1()
        {
            InitializeComponent();
            panel1.Paint += panel1_Paint;
            settings_str = System.IO.File.ReadAllText("../../settings.txt");
            string[] split_strs = settings_str.Split(' ');
            eps = float.Parse(split_strs[0]);
            q1 = float.Parse(split_strs[1]);
            q2 = float.Parse(split_strs[2]);
            mode = split_strs[3];
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            if (solution != null)
            {
                DrawSolution(g);
            }
            if (LXPoints != null && LYPoints != null && panel1.Visible)
            {
                int width = panel1.Size.Width;
                int height = panel1.Size.Height;
                // отрисовка внешнего разбиения 
                for (int i = 0; i < LXPoints.Count; i++)
                {
                    g.DrawLine(Pens.Black, (LXPoints[i] - LXPoints[0]) * scale, 0, (LXPoints[i] - LXPoints[0]) * scale, height);
                }
                for (int i = 0; i < LYPoints.Count; i++)
                {
                    g.DrawLine(Pens.Black, 0, height - ((LYPoints[i] - LYPoints[0]) * scale), width, height - (LYPoints[i] - LYPoints[0]) * scale);
                }
                // отрисовка выделения области
                if (SelCell != null)
                {
                    Pen SelCellBorder = new Pen(Color.Red);
                    SelCellBorder.Width += 5;
                    float x1 = (SelCell.top_left_x - LXPoints[0]) * scale - 2;
                    float y1 = (SelCell.top_left_y - LYPoints[0]) * scale - 2;
                    y1 = height - y1;
                    float x2 = (SelCell.bottom_right_x - LXPoints[0]) * scale - 2;
                    float y2 = (SelCell.bottom_right_y - LYPoints[0]) * scale - 2;
                    y2 = height - y2;
                    if (radioButtonX.Checked)
                    {
                        y1 = 0;
                        y2 = panel1.Height;
                    }
                    if (radioButtonY.Checked)
                    {
                        x1 = 0;
                        x2 = panel1.Width;
                    }
                    g.DrawLine(SelCellBorder, x1, y1, x2, y1);
                    g.DrawLine(SelCellBorder, x1, y2, x2, y2);
                    g.DrawLine(SelCellBorder, x1, y1, x1, y2);
                    g.DrawLine(SelCellBorder, x2, y1, x2, y2);
                }
                // цикл отрисовки по подобластям
                for (int i = 0; i < subareas.GetLength(0); i++)
                {
                    for (int j = 0; j < subareas.GetLength(1); j++)
                    {
                        if (subareas[i, j] == null)
                        {
                            continue;
                        }
                        Pen subarea_pen = subareas[i, j].pen;
                        SmallNet cur_net = subareas[i, j];
                        for (int i1 = 0; i1 < cur_net.nodes.GetLength(0); i1++)
                        {
                            for (int j1 = 0; j1 < cur_net.nodes.GetLength(1); j1++)
                            {
                                //draw nodes
                            }
                        }
                        for (int i1 = 0; i1 < cur_net.nodes.GetLength(0); i1++)
                        {
                            float x1 = (cur_net.nodes[i1, 0].X - LXPoints[0]) * scale;
                            float x2 = x1;
                            float y1 = (cur_net.nodes[i1, 0].Y - LYPoints[0]) * scale;
                            float y2 = y1 + cur_net.net_size.Height * scale;
                            y1 = height - y1;
                            y2 = height - y2;
                            g.DrawLine(subarea_pen, x1, y1, x2, y2);
                        }
                        for (int j1 = 0; j1 < cur_net.nodes.GetLength(1); j1++)
                        {
                            float y1 = (cur_net.nodes[0, j1].Y - LYPoints[0]) * scale;
                            float y2 = y1;
                            float x1 = (cur_net.nodes[0, j1].X - LXPoints[0]) * scale;
                            float x2 = x1 + cur_net.net_size.Width * scale;
                            y1 = height - y1;
                            y2 = height - y2;
                            g.DrawLine(subarea_pen, x1, y1, x2, y2);
                        }
                    }
                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel1.Width > 0 && panel1.Height > 0 && panel1.Visible)
            {
                Point scrollPosition = panel1.AutoScrollPosition;
                int absoluteX = e.X - scrollPosition.X;
                int absoluteY = e.Y - scrollPosition.Y;
                absoluteY = panel1.Height - absoluteY;
                SelCell = new SelectedCell();
                int i, j;
                for (i = 0; i < LXPoints.Count; i++)
                {
                    if ((LXPoints.ElementAt(i) - LXPoints[0]) * scale > absoluteX)
                    {
                        i--;
                        break;
                    }
                }
                SelCell.top_left_x = LXPoints.ElementAt(i);
                for (j = 0; j < LYPoints.Count; j++)
                {
                    if ((LYPoints.ElementAt(j) - LYPoints[0]) * scale > absoluteY)
                    {
                        j--;
                        break;
                    }
                }
                SelCell.cell_x_num = i;
                SelCell.cell_y_num = j;
                SelCell.top_left_y = LYPoints.ElementAt(j);
                SelCell.bottom_right_x = LXPoints.ElementAt(i + 1);
                SelCell.bottom_right_y = LYPoints.ElementAt(j + 1);
                set_stat_fields();
                SubAreaActivate();
                if(e.Button == MouseButtons.Right)
                {
                    SelCell = null;
                    SubAreaDeactivate();
                }
                panel1.Invalidate();
                panel1.Update();
            }
        }
        private void set_stat_fields()
        {
            if (SelCell == null || !radioButtonView.Checked)
            {
                textBoxXNodesNum.Text = "-";
                textBoxYNodesNum.Text = "-";
                textBoxTotalElements.Text = "-";
                textBoxTotalNodes.Text = "-";

                textBoxMaxXStep.Text = "-";
                textBoxMinXStep.Text = "-";
                textBoxMaxYStep.Text = "-";
                textBoxMinYStep.Text = "-";

                textBoxh0X.Text = "-";
                textBoxh0Y.Text = "-";
                textBoxQX.Text = "-";
                textBoxQY.Text = "-";
                textBoxNX.Text = "-";
                textBoxNY.Text = "-";
                return;
            }
            int i = SelCell.cell_x_num;
            int j = SelCell.cell_y_num;
            if (subareas[i, j] != null && radioButtonView.Checked)
            {
                int x_nodes_num = subareas[i, j].nodes.GetLength(0);
                int y_nodes_num = subareas[i, j].nodes.GetLength(1);
                textBoxXNodesNum.Text = x_nodes_num.ToString();
                textBoxYNodesNum.Text = y_nodes_num.ToString();
                textBoxTotalElements.Text = ((x_nodes_num - 1) * (y_nodes_num - 1)).ToString();
                textBoxTotalNodes.Text = (x_nodes_num * y_nodes_num).ToString();

                textBoxMaxXStep.Text = subareas[i, j].max_x_step.ToString();
                textBoxMinXStep.Text = subareas[i, j].min_x_step.ToString();
                textBoxMaxYStep.Text = subareas[i, j].max_y_step.ToString();
                textBoxMinYStep.Text = subareas[i, j].min_y_step.ToString();

                textBoxh0X.Text = subareas[i, j].h0X.ToString();
                textBoxh0Y.Text = subareas[i, j].h0Y.ToString();
                textBoxQX.Text = subareas[i, j].qx.ToString();
                textBoxQY.Text = subareas[i, j].qy.ToString();
                textBoxNX.Text = subareas[i, j].nx.ToString();
                textBoxNY.Text = subareas[i, j].ny.ToString();
            }
        }

        private void inner_net_editor_activate()
        {
            listBoxX.Enabled = true;
            listBoxY.Enabled = true;
            labelMinX.Text = orig_x1.ToString();
            labelMaxX.Text = orig_x2.ToString();
            labelMinY.Text = orig_y1.ToString();
            labelMaxY.Text = orig_y2.ToString();
            textBoxAddXPoint.Enabled = true;
            textBoxAddYPoint.Enabled = true;
            button_addXpoint.Enabled = true;
            button_addYpoint.Enabled = true;
            buttonRemoveXPoint.Enabled = true;
            buttonRemoveYPoint.Enabled = true;
            textBox_Width.Text = "[" + orig_x1.ToString() + "; " + orig_x2.ToString() + "]";
            textBox_Height.Text = "[" + orig_y1.ToString() + "; " + orig_y2.ToString() + "]";

            radioButtonView.Enabled = true;
            radioButtonX.Enabled = true;
            radioButtonY.Enabled = true;
        }

        private void inner_net_editor_deactivate()
        {
            listBoxX.Enabled = false;
            listBoxY.Enabled = false;
            labelMinX.Text = "-";
            labelMaxX.Text = "-";
            labelMinY.Text = "-";
            labelMaxY.Text = "-";
            textBoxAddXPoint.Enabled = false;
            textBoxAddYPoint.Enabled = false;
            button_addXpoint.Enabled = false;
            button_addYpoint.Enabled = false;
            buttonRemoveXPoint.Enabled = false;
            buttonRemoveYPoint.Enabled = false;
            textBox_Width.Text = "-";
            textBox_Height.Text = "-";

            radioButtonView.Enabled = true;
            radioButtonX.Enabled = true;
            radioButtonY.Enabled = true;
        }

        private void SubAreaActivate()
        {
            button_clear.Enabled = true;
            button3.Enabled = true;
        }
        private void SubAreaDeactivate()
        {
            button_clear.Enabled = false;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e) // настройка подобластей
        {
            if (SelCell == null || radioButtonView.Checked)
            {
                return;
            }
            float X = SelCell.top_left_x;
            float Y = SelCell.top_left_y;
            if (f_set_sub == null)
            {
                f_set_sub = new FormSubAreaSettings();
            }
            f_set_sub.ShowDialog();
            if (f_set_sub.DialogResult != DialogResult.OK)
            {
                return;
            }
            float h0 = f_set_sub.h0;
            int n = f_set_sub.n;
            float q = f_set_sub.q;
            float a_par = SelCell.top_left_x;
            float b_par = SelCell.bottom_right_x;
            if (radioButtonY.Checked)
            {
                a_par = SelCell.top_left_y;
                b_par = SelCell.bottom_right_y;
            }
            List<float> split = new List<float>();
            NetCalc netCalc = new NetCalc(q1, q2);
            switch (f_set_sub.state)
            {
                case -1:
                    return;
                case 1: // равномерная через n
                    split = netCalc.MakeSplit(a_par, b_par, n);
                    break;
                case 2: // равномерная через h
                    split = netCalc.MakeSplit(a_par, b_par, h0, eps, mode);
                    break;
                case 3: // неравномерная h0, n
                    split = netCalc.MakeSplit(a_par, b_par, n, h0, q, 1, eps, mode);
                    break;
                case 4: // неравномерная h0, q
                    split = netCalc.MakeSplit(a_par, b_par, n, h0, q, 2, eps, mode);
                    break;
                case 5: // неравномерная q, n
                    split = netCalc.MakeSplit(a_par, b_par, n, h0, q, 3, eps, mode);
                    break;
            }
            if (radioButtonX.Checked)
            {
                XSplits[SelCell.cell_x_num] = split;
                for (int j = 0; j < YSplits.Length; j++)
                {
                    if (YSplits[j] == null)
                    {
                        YSplits[j] = new List<float>() { LYPoints[j], LYPoints[j + 1] };
                    }
                    subareas[SelCell.cell_x_num,j] = netCalc.makeNet(split, YSplits[j]);
                }
            }
            else
            {
                YSplits[SelCell.cell_y_num] = split;
                for (int i = 0; i < XSplits.Length; i++)
                {
                    if (XSplits[i] == null)
                    {
                        XSplits[i] = new List<float>() { LXPoints[i], LXPoints[i + 1] };
                    }
                    subareas[i, SelCell.cell_y_num] = netCalc.makeNet(XSplits[i], split);
                }
            }
            SelCell = null;
            panel1.Invalidate();
            panel1.Update();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            FormMainZone large_area_create_form = new FormMainZone();
            large_area_create_form.ShowDialog();
            if (large_area_create_form.DialogResult == DialogResult.OK)
            {
                if (orig_x2 < orig_x1)
                {
                    float t = orig_x2;
                    orig_x2 = orig_x1;
                    orig_x1 = t;
                }
                if (orig_y2 < orig_y1)
                {
                    float t = orig_y2;
                    orig_y2 = orig_y1;
                    orig_y1 = t;
                }
                orig_x1 = large_area_create_form.x1;
                orig_x2 = large_area_create_form.x2;
                orig_y1 = large_area_create_form.y1;
                orig_y2 = large_area_create_form.y2;
                orig_width = orig_x2 - orig_x1;
                orig_height = orig_y2 - orig_y1;
                int panelXSize = panel2.Width;
                int panelYSize = panel2.Height - 10;
                LXPoints = new List<float> { orig_x1, orig_x2 };
                LYPoints = new List<float> { orig_y1, orig_y2 };
                scale = NetCalc.ScaleCalc(orig_width, orig_height, panelXSize, panelYSize);
                initial_scale = scale;
                textBox3.Text = scale.ToString();
                panel1.Size = new Size((int)(orig_width * scale), (int)(orig_height * scale));
                panel1.Visible = true;
                subareas = new SmallNet[LXPoints.Count - 1, LYPoints.Count - 1];
                XSplits = new List<float>[1];
                XSplits[0] = new List<float> { orig_x1, orig_x2 };
                YSplits = new List<float>[1];
                YSplits[0] = new List<float> { orig_y1, orig_y2 };
                inner_net_editor_activate();
                panel1.Invalidate();
                panel1.Update();
            }
        }

        private void buttonResetAll_Click(object sender, EventArgs e)
        {
            SelCell = null;
            SubAreaDeactivate();
            subareas = null;
            XSplits = null;
            YSplits = null;
            LXPoints = null;
            LYPoints = null;
            solution = null;
            scale = 0;
            initial_scale = scale;
            panel1.Visible = false;
            listBoxX.Items.Clear();
            listBoxY.Items.Clear();
            inner_net_editor_deactivate();
            panel1.Invalidate();
            panel1.Update();
        }

        private void button_addXpoint_Click(object sender, EventArgs e)
        {
            float xval;
            bool x_valid = float.TryParse(textBoxAddXPoint.Text, out xval);
            if (x_valid && xval >= 0 && xval <= panel1.Width)
            {
                LXPoints.Add(xval);
                listBoxX.Items.Clear();
                LXPoints.Sort();
                foreach (float point in LXPoints)
                {
                    if (point == LXPoints[0] || point == LXPoints[LXPoints.Count - 1])
                    {
                        continue;
                    }
                    listBoxX.Items.Add(point);
                }
                subareas = new SmallNet[LXPoints.Count - 1, LYPoints.Count - 1];
                XSplits = new List<float>[LXPoints.Count - 1];
                panel1.Invalidate();
                panel1.Update();
            }
        }

        private void button_addYpoint_Click(object sender, EventArgs e)
        {
            float yval;
            bool y_valid = float.TryParse(textBoxAddYPoint.Text, out yval);
            if (y_valid && yval >= 0 && yval <= panel1.Height)
            {
                LYPoints.Add(yval);
                listBoxY.Items.Clear();
                LYPoints.Sort();
                foreach (float point in LYPoints)
                {
                    if (point == LYPoints[0] || point == LYPoints[LYPoints.Count - 1])
                    {
                        continue;
                    }
                    listBoxY.Items.Add(point);
                }
                subareas = new SmallNet[LXPoints.Count - 1, LYPoints.Count - 1];
                YSplits = new List<float>[LYPoints.Count - 1];
                panel1.Invalidate();
                panel1.Update();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scale = initial_scale;
            textBox3.Text = scale.ToString();
            panel1.Size = new Size((int)(orig_width * scale), (int)(orig_height * scale));
            panel1.Invalidate();
            panel1.Update();
        }

        private void radioButtonView_CheckedChanged(object sender, EventArgs e)
        {
            set_stat_fields();
            panel1.Invalidate();
            panel1.Update();
        }

        private void radioButtonY_CheckedChanged(object sender, EventArgs e)
        {
            set_stat_fields();
            panel1.Invalidate();
            panel1.Update();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Версия интерфейса 0.0.2; Автор интерфейса Халафян Михаил; Версия расчётного блока 0.0.2; Автор расчётного блока Русаков Ярослав";
            MessageBox.Show(msg);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //MessageBox.Show("-----Х-----");
            int panelXSize = panel2.Width - 5;
            int panelYSize = panel2.Height - 5;
            initial_scale = NetCalc.ScaleCalc(orig_width, orig_height, panelXSize, panelYSize);
            button4_Click(sender, e);
        }

        private void button_solve_Click(object sender, EventArgs e)
        {
            FEMSolve();
            panel1.Invalidate();
            panel1.Update();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < subareas.GetLength(0); i++)
            {
                for(int j = 0; j < subareas.GetLength(1); j++)
                {
                    subareas[i, j] = null;
                }
            }
            for (int i = 0; i < XSplits.Length; i++)
            {
                XSplits[i] = null;
            }
            for (int j = 0; j < YSplits.Length; j++)
            {
                YSplits[j] = null;
            }
            SelCell = null;
            panel1.Invalidate();
            panel1.Update();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Файлы состояния (*.k2)|*.k2|Все файлы (*.*)|*.*";
            saveDialog.DefaultExt = "k2";
            saveDialog.Title = "Сохранить состояние программы";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                SaveStateToFile(saveDialog.FileName);
            }
        }


        private void buttonSettings_Click(object sender, EventArgs e)
        {

            settings_str = System.IO.File.ReadAllText("../../settings.txt");
            Settings newForm = new Settings();
            newForm.Func(settings_str);
            newForm.Owner = this;
            newForm.ShowDialog();

            eps = newForm.eps;
            q1 = newForm.q1;
            q2 = newForm.q2;
            mode = newForm.mode;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            float new_scale;
            bool flag = float.TryParse(textBox3.Text.Replace('.', ','), out new_scale);
            if (flag)
            {
                panel1.Width = (int)(orig_width * new_scale);
                panel1.Height = (int)(orig_height * new_scale);
                scale = new_scale;
                panel1.Invalidate();
                panel1.Update();
            }
        }

        private void buttonRemoveXPoint_Click(object sender, EventArgs e)
        {
            if (listBoxX.SelectedIndex != -1)
            {
                int ind = listBoxX.SelectedIndex;
                listBoxX.Items.RemoveAt(listBoxX.SelectedIndex);
                LXPoints.RemoveAt(ind + 1);
                subareas = new SmallNet[LXPoints.Count - 1, LYPoints.Count - 1];
                XSplits = new List<float>[LXPoints.Count - 1];
                SelCell = null;
                panel1.Invalidate();
                panel1.Update();
            }
        }

        private void buttonRemoveYPoint_Click(object sender, EventArgs e)
        {
            if (listBoxY.SelectedIndex != -1)
            {
                int ind = listBoxY.SelectedIndex;
                listBoxY.Items.RemoveAt(listBoxY.SelectedIndex);
                LYPoints.RemoveAt(ind + 1);
                subareas = new SmallNet[LXPoints.Count - 1, LYPoints.Count - 1];
                YSplits = new List<float>[LYPoints.Count - 1];
                SelCell = null;
                panel1.Invalidate();
                panel1.Update();
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файлы состояния (*.k2)|*.k2|Все файлы (*.*)|*.*";
            openDialog.Title = "Загрузить состояние программы";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                LoadStateFromFile(openDialog.FileName);
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized || last_state == FormWindowState.Maximized)
            {
                // Имитируем наступление события ResizeEnd
                Form1_ResizeEnd(sender, e);
            }
            last_state = this.WindowState;
        }

        private void SaveStateToFile(string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    // 1. Сохраняем глобальные параметры
                    writer.WriteLine("[GlobalParams]");
                    writer.WriteLine($"eps={eps}");
                    writer.WriteLine($"q1={q1}");
                    writer.WriteLine($"q2={q2}");
                    writer.WriteLine($"mode={mode}");
                    writer.WriteLine($"scale={scale}");
                    writer.WriteLine($"initial_scale={initial_scale}");
                    writer.WriteLine();

                    // 2. Сохраняем размеры области
                    writer.WriteLine("[Area]");
                    writer.WriteLine($"x1={orig_x1}");
                    writer.WriteLine($"x2={orig_x2}");
                    writer.WriteLine($"y1={orig_y1}");
                    writer.WriteLine($"y2={orig_y2}");
                    writer.WriteLine($"width={orig_width}");
                    writer.WriteLine($"height={orig_height}");
                    writer.WriteLine();

                    // 3. Сохраняем основные разбиения (LXPoints, LYPoints)
                    writer.WriteLine("[LXPoints]");
                    writer.WriteLine($"count={LXPoints.Count}");
                    for (int i = 0; i < LXPoints.Count; i++)
                        writer.WriteLine($"LXPoints[{i}]={LXPoints[i]}");
                    writer.WriteLine();

                    writer.WriteLine("[LYPoints]");
                    writer.WriteLine($"count={LYPoints.Count}");
                    for (int i = 0; i < LYPoints.Count; i++)
                        writer.WriteLine($"LYPoints[{i}]={LYPoints[i]}");
                    writer.WriteLine();

                    // 4. Сохраняем XSplits
                    writer.WriteLine("[XSplits]");
                    writer.WriteLine($"count={XSplits.Length}");
                    for (int i = 0; i < XSplits.Length; i++)
                    {
                        if (XSplits[i] == null)
                        {
                            writer.WriteLine($"XSplits[{i}]=null");
                        }
                        else
                        {
                            writer.WriteLine($"XSplits[{i}]_count={XSplits[i].Count}");
                            for (int j = 0; j < XSplits[i].Count; j++)
                                writer.WriteLine($"XSplits[{i}][{j}]={XSplits[i][j]}");
                        }
                    }
                    writer.WriteLine();

                    // 5. Сохраняем YSplits
                    writer.WriteLine("[YSplits]");
                    writer.WriteLine($"count={YSplits.Length}");
                    for (int i = 0; i < YSplits.Length; i++)
                    {
                        if (YSplits[i] == null)
                        {
                            writer.WriteLine($"YSplits[{i}]=null");
                        }
                        else
                        {
                            writer.WriteLine($"YSplits[{i}]_count={YSplits[i].Count}");
                            for (int j = 0; j < YSplits[i].Count; j++)
                                writer.WriteLine($"YSplits[{i}][{j}]={YSplits[i][j]}");
                        }
                    }
                    writer.WriteLine();

                    // 6. Сохраняем подобласти (subareas)
                    writer.WriteLine("[Subareas]");
                    writer.WriteLine($"rows={subareas.GetLength(0)}");
                    writer.WriteLine($"cols={subareas.GetLength(1)}");

                    for (int i = 0; i < subareas.GetLength(0); i++)
                    {
                        for (int j = 0; j < subareas.GetLength(1); j++)
                        {
                            if (subareas[i, j] == null)
                            {
                                writer.WriteLine($"subareas[{i},{j}]=null");
                            }
                            else
                            {
                                SmallNet net = subareas[i, j];
                                writer.WriteLine($"subareas[{i},{j}]_nx={net.nx}");
                                writer.WriteLine($"subareas[{i},{j}]_ny={net.ny}");
                                writer.WriteLine($"subareas[{i},{j}]_h0X={net.h0X}");
                                writer.WriteLine($"subareas[{i},{j}]_h0Y={net.h0Y}");
                                writer.WriteLine($"subareas[{i},{j}]_qx={net.qx}");
                                writer.WriteLine($"subareas[{i},{j}]_qy={net.qy}");
                                writer.WriteLine($"subareas[{i},{j}]_width={net.net_size.Width}");
                                writer.WriteLine($"subareas[{i},{j}]_height={net.net_size.Height}");

                                // Сохраняем узлы
                                for (int ni = 0; ni < net.nodes.GetLength(0); ni++)
                                {
                                    for (int nj = 0; nj < net.nodes.GetLength(1); nj++)
                                    {
                                        writer.WriteLine($"subareas[{i},{j}]_node[{ni},{nj}]_X={net.nodes[ni, nj].X}");
                                        writer.WriteLine($"subareas[{i},{j}]_node[{ni},{nj}]_Y={net.nodes[ni, nj].Y}");
                                    }
                                }
                            }
                        }
                    }
                    writer.WriteLine();

                    // 7. Сохраняем решение (если есть)
                    writer.WriteLine("[Solution]");
                    if (solution != null)
                    {
                        writer.WriteLine($"exists=true");
                        writer.WriteLine($"size={solution.Length}");
                        for (int i = 0; i < solution.Length; i++)
                            writer.WriteLine($"solution[{i}]={solution[i]}");
                    }
                    else
                    {
                        writer.WriteLine($"exists=false");
                    }
                }

                MessageBox.Show($"Состояние сохранено в файл:\n{filename}", "Сохранение");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка");
            }
        }

        private void LoadStateFromFile(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    MessageBox.Show("Файл не найден!", "Ошибка");
                    return;
                }

                // Сбрасываем текущее состояние
                buttonResetAll_Click(null, null);

                using (StreamReader reader = new StreamReader(filename, Encoding.UTF8))
                {
                    string line;
                    string currentSection = "";

                    // Временные переменные для восстановления
                    int lxPointsCount = 0;
                    int lyPointsCount = 0;
                    int xSplitsCount = 0;
                    int ySplitsCount = 0;
                    int subareasRows = 0;
                    int subareasCols = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        // Определяем секцию
                        if (line.StartsWith("["))
                        {
                            currentSection = line.Trim('[', ']');
                            continue;
                        }

                        string[] parts = line.Split('=');
                        if (parts.Length < 2) continue;

                        string key = parts[0];
                        string value = parts[1];

                        switch (currentSection)
                        {
                            case "GlobalParams":
                                if (key == "eps") eps = float.Parse(value);
                                else if (key == "q1") q1 = float.Parse(value);
                                else if (key == "q2") q2 = float.Parse(value);
                                else if (key == "mode") mode = value;
                                else if (key == "scale") scale = float.Parse(value);
                                else if (key == "initial_scale") initial_scale = float.Parse(value);
                                break;

                            case "Area":
                                if (key == "x1") orig_x1 = float.Parse(value);
                                else if (key == "x2") orig_x2 = float.Parse(value);
                                else if (key == "y1") orig_y1 = float.Parse(value);
                                else if (key == "y2") orig_y2 = float.Parse(value);
                                else if (key == "width") orig_width = float.Parse(value);
                                else if (key == "height") orig_height = float.Parse(value);
                                break;

                            case "LXPoints":
                                if (key == "count")
                                {
                                    lxPointsCount = int.Parse(value);
                                    LXPoints = new List<float>(new float[lxPointsCount]);
                                }
                                else if (key.StartsWith("LXPoints["))
                                {
                                    int index = int.Parse(key.Substring(9, key.IndexOf(']') - 9));
                                    LXPoints[index] = float.Parse(value);
                                }
                                break;

                            case "LYPoints":
                                if (key == "count")
                                {
                                    lyPointsCount = int.Parse(value);
                                    LYPoints = new List<float>(new float[lyPointsCount]);
                                }
                                else if (key.StartsWith("LYPoints["))
                                {
                                    int index = int.Parse(key.Substring(9, key.IndexOf(']') - 9));
                                    LYPoints[index] = float.Parse(value);
                                }
                                break;

                            case "XSplits":
                                if (key == "count")
                                {
                                    xSplitsCount = int.Parse(value);
                                    XSplits = new List<float>[xSplitsCount];
                                }
                                // Обработка элементов XSplits
                                break;

                            case "YSplits":
                                if (key == "count")
                                {
                                    ySplitsCount = int.Parse(value);
                                    YSplits = new List<float>[ySplitsCount];
                                }
                                break;

                            case "Subareas":
                                if (key == "rows") subareasRows = int.Parse(value);
                                else if (key == "cols")
                                {
                                    subareasCols = int.Parse(value);
                                    subareas = new SmallNet[subareasRows, subareasCols];
                                }
                                break;

                            case "Solution":
                                if (key == "exists" && value == "true")
                                {
                                    // Загружаем решение
                                }
                                break;
                        }
                    }
                }

                // Восстанавливаем интерфейс
                panel1.Visible = true;
                panel1.Size = new Size((int)(orig_width * scale), (int)(orig_height * scale));
                textBox3.Text = scale.ToString();
                inner_net_editor_activate();

                // Обновляем списки в интерфейсе
                UpdateListsFromPoints();

                panel1.Invalidate();
                panel1.Update();

                MessageBox.Show($"Состояние загружено из файла:\n{filename}", "Загрузка");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}\n\n{ex.StackTrace}", "Ошибка");
                buttonResetAll_Click(null, null);  // Сброс в случае ошибки
            }
        }

        // Вспомогательный метод для обновления списков
        private void UpdateListsFromPoints()
        {
            if (LXPoints == null || LYPoints == null) return;

            listBoxX.Items.Clear();
            for (int i = 1; i < LXPoints.Count - 1; i++)
                listBoxX.Items.Add(LXPoints[i]);

            listBoxY.Items.Clear();
            for (int i = 1; i < LYPoints.Count - 1; i++)
                listBoxY.Items.Add(LYPoints[i]);

            labelMinX.Text = orig_x1.ToString();
            labelMaxX.Text = orig_x2.ToString();
            labelMinY.Text = orig_y1.ToString();
            labelMaxY.Text = orig_y2.ToString();
            textBox_Width.Text = $"[{orig_x1}; {orig_x2}]";
            textBox_Height.Text = $"[{orig_y1}; {orig_y2}]";
        }
        void FEMSolve()
        {
            global_net_x = new List<float>();
            for (int i = 0; i < XSplits.Count(); i++)
            {
                global_net_x.Add(LXPoints[i]);
                if ((XSplits[i] != null))
                {
                    for (int j = 1; j < XSplits[i].Count - 1; j++)
                    {
                        global_net_x.Add(XSplits[i][j]);
                    }
                }
            }
            global_net_x.Add(LXPoints.Last());

            global_net_y = new List<float>();
            for (int i = 0; i < YSplits.Count(); i++)
            {
                global_net_y.Add(LYPoints[i]);
                if ((YSplits[i] != null))
                {
                    for (int j = 1; j < YSplits[i].Count - 1; j++)
                    {
                        global_net_y.Add(YSplits[i][j]);
                    }
                }
            }
            global_net_y.Add(LYPoints.Last());

            FEMAssembler assembler = new FEMAssembler(global_net_x.Count * global_net_y.Count);
            double lambda = 6, gamma = 1;
            int f_mode = 4;

            // сборка глобальной матрицы
            for (int i = 0; i < global_net_y.Count - 1; i++)
            {
                for (int j = 0; j < global_net_x.Count - 1; j++)
                {
                    double hx = global_net_x[j+1] - global_net_x[j], hy = global_net_y[i + 1] - global_net_y[i];
                    double[] f = { FVal(global_net_x[j], global_net_y[i],lambda, gamma, f_mode), 
                                   FVal(global_net_x[j + 1], global_net_y[i], lambda, gamma, f_mode), 
                                   FVal(global_net_x[j], global_net_y[i + 1], lambda, gamma, f_mode), 
                                   FVal(global_net_x[j + 1], global_net_y[i + 1], lambda, gamma, f_mode)};
                    assembler.buildLocal(hx, hy, lambda, gamma, f);
                    int low_left = i * global_net_x.Count + j;
                    int low_right = low_left + 1;
                    int top_left = low_left + global_net_x.Count;
                    int top_right = top_left + 1;
                    assembler.addToGlobal(low_left, low_right, top_left, top_right);
                }
            }
            // применение граничных условий
            Dictionary<int, double> bounds = BuildBoundaryDictions(global_net_x, global_net_y, lambda, gamma, f_mode);
            assembler.applyBoundaryCondition(bounds);
            // Вызов решателя СЛАУ
            solution = Gauss.Solve(assembler.globalMatrix, assembler.globalB);
            // запись решения в файл
            WriteResults(f_mode);
        }
        Dictionary<int, double> BuildBoundaryDictions(List<float> x_list, List<float> y_list, double lambda, double gamma, int mode)
        {
            Dictionary<int, double> res = new Dictionary<int, double>();
            for (int i = 0; i < y_list.Count; i++)
            {
                float x = x_list[0];
                float y = y_list[i];
                int num = i * x_list.Count;
                res[num] = UVal(x, y, mode);
                x = x_list.Last();
                num += x_list.Count - 1;
                res[num] = UVal(x, y, mode);
            }
            for (int i = 0; i < x_list.Count; i++)
            {
                float x = x_list[i];
                float y = y_list[0];
                int num = i;
                res[num] = UVal(x, y, mode);
                y = y_list.Last();
                num += x_list.Count * (y_list.Count - 1);
                res[num] = UVal(x, y, mode);
            }
            return res;
        }
        private void DrawSolution(Graphics g)
        {
            if (solution == null || global_net_x == null || global_net_y == null) return;
            if (global_net_x.Count < 2 || global_net_y.Count < 2) return;

            double minVal = solution.Min();
            double maxVal = solution.Max();
            double range = maxVal - minVal;
            if (Math.Abs(range) < 1e-10) range = 1;

            // Цикл по элементам
            for (int i = 0; i < global_net_y.Count - 1; i++)
            {
                for (int j = 0; j < global_net_x.Count - 1; j++)
                {
                    // Глобальные индексы четырёх узлов
                    int n_ll = i * global_net_x.Count + j;           // левый нижний
                    int n_lr = n_ll + 1;                              // правый нижний
                    int n_ul = n_ll + global_net_x.Count;             // левый верхний
                    int n_ur = n_ul + 1;                              // правый верхний

                    // Среднее значение в элементе
                    double elementValue = (solution[n_ll] + solution[n_lr] +
                                           solution[n_ul] + solution[n_ur]) / 4.0;

                    float normalizedValue = (float)((elementValue - minVal) / range);
                    float normalized_ll, normalized_lr, normalized_ul, normalized_ur;
                    Color color_ll, color_lr, color_ul, color_ur;
                    normalized_ll = (float)((solution[n_ll] - minVal) / range);
                    normalized_lr = (float)((solution[n_lr] - minVal) / range);
                    normalized_ul = (float)((solution[n_ul] - minVal) / range);
                    normalized_ur = (float)((solution[n_ur] - minVal) / range);


                    Color color = Color.FromArgb(
                        (int)(255 * normalizedValue),
                        (int)(255 * (1 - Math.Abs(normalizedValue - 0.5) * 2)),
                        (int)(255 * (1 - normalizedValue))
                    );
                    color_ll = Color.FromArgb(
                        (int)(255 * normalized_ll),
                        (int)(255 * (1 - Math.Abs(normalized_ll - 0.5) * 2)),
                        (int)(255 * (1 - normalized_ll))
                    );
                    color_lr = Color.FromArgb(
                        (int)(255 * normalized_lr),
                        (int)(255 * (1 - Math.Abs(normalized_lr - 0.5) * 2)),
                        (int)(255 * (1 - normalized_lr))
                    );
                    color_ul = Color.FromArgb(
                        (int)(255 * normalized_ul),
                        (int)(255 * (1 - Math.Abs(normalized_ul - 0.5) * 2)),
                        (int)(255 * (1 - normalized_ul))
                    );
                    color_ur = Color.FromArgb(
                        (int)(255 * normalized_ur),
                        (int)(255 * (1 - Math.Abs(normalized_ur - 0.5) * 2)),
                        (int)(255 * (1 - normalized_ur))
                    );

                    // Координаты четырёх углов элемента
                    PointF[] points = new PointF[]
                    {
                new PointF((global_net_x[j] - LXPoints[0]) * scale,
                            panel1.Height - (global_net_y[i] - LYPoints[0]) * scale), //левый нижний 
                new PointF((global_net_x[j+1] - LXPoints[0]) * scale,
                            panel1.Height - (global_net_y[i] - LYPoints[0]) * scale), // правый нижний
                new PointF((global_net_x[j+1] - LXPoints[0]) * scale,
                            panel1.Height - (global_net_y[i+1] - LYPoints[0]) * scale)// правый верхний
                    };
                    using (var path = new GraphicsPath())
                    {
                        path.AddPolygon(points);
                        using (PathGradientBrush brush = new PathGradientBrush(path))
                        {
                            brush.SurroundColors = new Color[] { color_ll, color_lr, color_ur };
                            Color center_color = Color.FromArgb(
                                (color_ll.R + color_lr.R + color_ur.R) / 3,
                                (color_ll.G + color_lr.G + color_ur.G) / 3,
                                (color_ll.B + color_lr.B + color_ur.B) / 3
                                );
                            brush.CenterColor = center_color;
                            g.FillPolygon(brush, points);
                            g.DrawPolygon(Pens.Gray, points);
                        }
                    }
                    points = new PointF[]
                    {
                new PointF((global_net_x[j] - LXPoints[0]) * scale,
                            panel1.Height - (global_net_y[i] - LYPoints[0]) * scale), //левый нижний 
                new PointF((global_net_x[j] - LXPoints[0]) * scale,
                            panel1.Height - (global_net_y[i+1] - LYPoints[0]) * scale), // левый верхний
                new PointF((global_net_x[j+1] - LXPoints[0]) * scale,
                            panel1.Height - (global_net_y[i+1] - LYPoints[0]) * scale)// правый верхний
                    };

                    using (var path = new GraphicsPath())
                    {
                        path.AddPolygon(points);
                        using (PathGradientBrush brush = new PathGradientBrush(path))
                        {
                            brush.SurroundColors = new Color[] { color_ll, color_ul, color_ur };
                            Color center_color = Color.FromArgb(
                                (color_ll.R + color_ul.R + color_ur.R) / 3,
                                (color_ll.G + color_ul.G + color_ur.G) / 3,
                                (color_ll.B + color_ul.B + color_ur.B) / 3
                                );
                            brush.CenterColor = center_color;
                            g.FillPolygon(brush, points);
                            g.DrawPolygon(Pens.Gray, points);
                        }
                    }
                }
            }
        }

        private double FVal(float x, float y, double lambda, double gamma, int mode)
        {
            double res = 0;
            switch (mode){
                case 0: res = 1; break;
                case 1: res = (x + y) * gamma; break;
                case 2: res = -lambda * 4 + (x*x + y*y) * gamma; break;
                case 3: res = -lambda * (6 * x + 6 * y) + (Math.Pow(x, 3) + Math.Pow(y, 3)) * gamma; break;
                case 4: res = -lambda * (12 * x * x + 12 * y * y) + (Math.Pow(x, 4) + Math.Pow(y, 4)) * gamma; break;
                case -1: res = -lambda * (-Math.Sin(x) - Math.Sin(y)) + (Math.Sin(x) + Math.Sin(y)) * gamma; break;
                default: res = 0; break;
            }
            return res;
        }
        private double UVal(float x, float y, int mode)
        {
            double res = 0;
            switch (mode)
            {
                case 0: res = 1; break;
                case 1: res = (x + y); break;
                case 2: res = (x * x + y * y); break;
                case 3: res = Math.Pow(x, 3) + Math.Pow(y, 3); break;
                case 4: res = Math.Pow(x,4) + Math.Pow(y, 4); break;
                case -1: res = (Math.Sin(x) + Math.Sin(y)); break;
                default: res = 0; break;
            }
            return res;
        }

        private double VectNorm(double[] vect)
        {
            double sum = 0;
            for (int i = 0; i < vect.Length; i++)
            {
                sum += vect[i] * vect[i];
            }
            sum /= vect.Length;
            sum = Math.Sqrt(sum);
            return sum;
        }

        private void WriteResults(int f_mode)
        {
            double[] u_real_solution = new double[solution.Length], error = new double[solution.Length];
            for (int i = 0; i < global_net_y.Count; i++)
            {
                for (int j = 0; j < global_net_x.Count; j++)
                {
                    u_real_solution[i * global_net_x.Count + j] = UVal(global_net_x[j], global_net_y[i], f_mode);
                    error[i * global_net_x.Count + j] = u_real_solution[i * global_net_x.Count + j] - solution[i * global_net_x.Count + j];
                }
            }
            double u_real_norm = VectNorm(solution), err_norm = VectNorm(error);

            // Открываем файл для записи
            using (StreamWriter writer = new StreamWriter("mesh_results.txt"))
            {
                // 1. Записываем шапку таблицы
                // -15 означает выравнивание по левому краю, ширина столбца 15 символов
                writer.WriteLine($"{"Индекс",-10} | {"Функция (Точн.)",-18} | {"Решение (МКЭ)",-18} | {"Погрешность",-15}");

                // 2. Выводим строки таблицы с данными векторов
                for (int i = 0; i < solution.Length; i++)
                {
                    // F4 выводит числа с 4 знаками после запятой для аккуратности
                    writer.WriteLine($"{i,-10} | {u_real_solution[i],-18:G10} | {solution[i],-18:G10} | {error[i],-15:G8}");
                }

                writer.WriteLine(new string('-', 70)); // Разделительная линия перед итогами
                writer.WriteLine(); // Пустая строка для визуального отделения

                // 4. Записываем финальные числа в конец файла
                writer.WriteLine($"Норма вектора функции:     {u_real_norm:E6}");
                writer.WriteLine($"Норма вектора погрешности: {err_norm:E6}");
            }
        }
    }
}