using SharpGL;
using SharpGL.SceneGraph.Raytracing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace K2
{
    public partial class ViewResultForm : Form
    {
        Form1 _mainForm;

        double cameraX = 0.0f;
        double cameraY = 80.0f;
        double cameraZ = 0.0f;

        double targetX = 50.0f;
        double targetY = 0.0f;
        double targetZ = 50.0f;

        double upX = 0.0f;
        double upY = 1.0f;
        double upZ = 0.0f;

        // Углы для вращения мышью
        double yaw = 45.0f;    // поворот влево/вправо
        double pitch = -45.0f;   // наклон вверх/вниз

        // Управление мышью
        bool mouseCaptured = false;
        System.Drawing.Point lastMousePos;
        double mouseSensitivity = 0.3f;

        bool keyW = false;
        bool keyA = false;
        bool keyS = false;
        bool keyD = false;
        bool keyQ = false;
        bool keyE = false;

        // Скорость перемещения
        double moveSpeed = 30.0f;

        DateTime lastFrameTime;

        // Настройки нормализации высоты
        double targetMinHeight = 0.0f;   // минимальная высота после нормализации
        double targetMaxHeight = 100.0f;  // максимальная высота после нормализации

        bool no_normalize = false;
        // настройки отображения
        bool show_green_box = true, show_surface = true, show_net = true, show_zero_y = true;
        double x_size = 100.0f, z_size = 100.0f;
        int x_zero_lines_num = 10, z_zero_lines_num = 10;
        double y_zero = 0;

        private List<Label> legendLabels = new List<Label>();
        private List<ColorThreshold> thresholds = new List<ColorThreshold>()
        {
            new ColorThreshold { Value = 0, Color = Color.Blue },
            new ColorThreshold { Value = 100, Color = Color.LimeGreen },
            new ColorThreshold { Value = 200, Color = Color.Red }
        };

        public ViewResultForm(Form1 mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;

            openGLDrawingArea.OpenGLInitialized += openGLDrawingArea_OpenGLInitialized;
            openGLDrawingArea.OpenGLDraw += openGLDrawingArea_OpenGLDraw;
            openGLDrawingArea.Resized += openGLDrawingArea_Resized;

            openGLDrawingArea.MouseDown += OpenGLDrawingArea_MouseDown;
            openGLDrawingArea.MouseMove += OpenGLDrawingArea_MouseMove;
            openGLDrawingArea.MouseUp += OpenGLDrawingArea_MouseUp;// Подписка на клавиатуру

            double sol_min = _mainForm.solution.Min();
            double sol_max = _mainForm.solution.Max();
            thresholds = new List<ColorThreshold>() {
            new ColorThreshold { Value = sol_min, Color = Color.Blue },
            new ColorThreshold { Value = (sol_max + sol_min) / 2, Color = Color.LimeGreen },
            new ColorThreshold { Value = sol_max, Color = Color.Red }
            };
            UpdateLegendLabels();


            this.KeyDown += ViewResultForm_KeyDown;
            this.KeyUp += ViewResultForm_KeyUp;

            // Разрешаем форме получать события клавиатуры
            this.KeyPreview = true;

            // Для расчёта времени между кадрами
            lastFrameTime = DateTime.Now;

            this.Resize += (s, e) => panelLegend.Invalidate();
            this.Load += (s, e) => openGLDrawingArea_Resized(openGLDrawingArea, EventArgs.Empty);
        }

        private void openGLDrawingArea_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLDrawingArea.OpenGL;
            gl.ClearColor(0.2f, 0.2f, 0.3f, 1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Disable(OpenGL.GL_LIGHTING);

        }

        private void openGLDrawingArea_Resized(object sender, EventArgs e)
        {
            OpenGL gl = openGLDrawingArea.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            double aspect = (double)openGLDrawingArea.Width / (double)openGLDrawingArea.Height;
            gl.Perspective(45.0f, aspect, 0.1f, 500.0f);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLDrawingArea_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Расчёт времени между кадрами
            DateTime now = DateTime.Now;
            double deltaTime = (double)(now - lastFrameTime).TotalSeconds;
            if (deltaTime > 0.1f) deltaTime = 0.1f;
            lastFrameTime = now;

            UpdateCameraPosition(deltaTime);

            UpdateCameraDirection();

            OpenGL gl = openGLDrawingArea.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.LookAt(cameraX, cameraY, cameraZ,
              targetX, targetY, targetZ,
              upX, upY, upZ);

            DrawHeightmap(gl);
        }

        private void DrawHeightmap(OpenGL gl)
        {
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            if (show_green_box)
            {
                gl.Begin(OpenGL.GL_QUADS);
                gl.Color(0.2f, 0.8f, 0.3f);

                gl.Vertex(0.0f, -0.001f, 0.0f);
                gl.Vertex(0.0f, -0.001f, z_size);
                gl.Vertex(x_size, -0.001f, z_size);
                gl.Vertex(x_size, -0.001f, 0.0f);
                gl.End();


                int x_strip_size = _mainForm.global_net_x.Count;
                int z_strip_size = _mainForm.global_net_y.Count;
                double min_y = _mainForm.solution.Min(), max_y = _mainForm.solution.Max();
                gl.Begin(OpenGL.GL_QUADS);
                for (int i = 0; i < x_strip_size - 1; i++)
                {
                    double x1 = ScaleX(_mainForm.global_net_x[i]);
                    double x2 = ScaleX(_mainForm.global_net_x[i + 1]);
                    double z = ScaleZ(_mainForm.global_net_y[0]);

                    double y1 = (double)NormalizeHeight(_mainForm.solution[i], min_y, max_y);
                    double y2 = (double)NormalizeHeight(_mainForm.solution[i + 1], min_y, max_y);

                    gl.Vertex(x1, 0, z);
                    gl.Vertex(x2, 0, z);
                    gl.Vertex(x2, y2, z);
                    gl.Vertex(x1, y1, z);
                }
                gl.End();
                gl.Begin(OpenGL.GL_QUADS);
                for (int j = 0; j < z_strip_size - 1; j++)
                {
                    double z1 = ScaleZ(_mainForm.global_net_y[j]);
                    double z2 = ScaleZ(_mainForm.global_net_y[j + 1]);

                    double y1 = (double)NormalizeHeight(_mainForm.solution[j * x_strip_size + 0], min_y, max_y);
                    double y2 = (double)NormalizeHeight(_mainForm.solution[(j + 1) * x_strip_size + 0], min_y, max_y);

                    gl.Vertex(0.0f, 0.0f, z1);
                    gl.Vertex(0.0f, 0.0f, z2);
                    gl.Vertex(0.0f, y2, z2);
                    gl.Vertex(0.0f, y1, z1);
                }
                gl.End();
                gl.Begin(OpenGL.GL_QUADS);
                for (int j = 0; j < z_strip_size - 1; j++)
                {
                    double z1 = ScaleZ(_mainForm.global_net_y[j]);
                    double z2 = ScaleZ(_mainForm.global_net_y[j + 1]);
                    double x = ScaleX(_mainForm.global_net_x[x_strip_size - 1]);

                    double y1 = (double)NormalizeHeight(_mainForm.solution[j * x_strip_size + (x_strip_size - 1)], min_y, max_y);
                    double y2 = (double)NormalizeHeight(_mainForm.solution[(j + 1) * x_strip_size + (x_strip_size - 1)], min_y, max_y);

                    gl.Vertex(x, 0.0f, z1);
                    gl.Vertex(x, 0.0f, z2);
                    gl.Vertex(x, y2, z2);
                    gl.Vertex(x, y1, z1);
                }
                gl.End();
                gl.Begin(OpenGL.GL_QUADS);
                for (int i = 0; i < x_strip_size - 1; i++)
                {
                    double x1 = ScaleX(_mainForm.global_net_x[i]);
                    double x2 = ScaleX(_mainForm.global_net_x[i + 1]);
                    double z = ScaleZ(_mainForm.global_net_y[z_strip_size - 1]);

                    double y1 = (double)NormalizeHeight(_mainForm.solution[(z_strip_size - 1) * x_strip_size + i], min_y, max_y);
                    double y2 = (double)NormalizeHeight(_mainForm.solution[(z_strip_size - 1) * x_strip_size + (i + 1)], min_y, max_y);

                    gl.Vertex(x1, 0.0f, z);
                    gl.Vertex(x2, 0.0f, z);
                    gl.Vertex(x2, y2, z);
                    gl.Vertex(x1, y1, z);
                }
                gl.End();
            }
            DrawRealSurface(gl);
        }

        private void UpdateCameraDirection()
        {
            // Перевод градусов в радианы
            double yawRad = yaw * Math.PI / 180.0;
            double pitchRad = pitch * Math.PI / 180.0;

            // Вектор направления взгляда
            double frontX = (Math.Cos(yawRad) * Math.Cos(pitchRad));
            double frontY = (Math.Sin(pitchRad));
            double frontZ = (Math.Sin(yawRad) * Math.Cos(pitchRad));

            targetX = cameraX + frontX;
            targetY = cameraY + frontY;
            targetZ = cameraZ + frontZ;
        }

        private void OpenGLDrawingArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseCaptured = true;
                lastMousePos = e.Location;
                openGLDrawingArea.Cursor = Cursors.Cross;
            }
        }

        private void OpenGLDrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseCaptured)
            {
                double deltaX = e.X - lastMousePos.X;
                double deltaY = e.Y - lastMousePos.Y;

                yaw += deltaX * mouseSensitivity;
                pitch -= deltaY * mouseSensitivity;

                if (pitch > 89.0f) pitch = 89.0f;
                if (pitch < -89.0f) pitch = -89.0f;

                lastMousePos = e.Location;
                openGLDrawingArea.Invalidate();
            }
        }

        private void OpenGLDrawingArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseCaptured = false;
                openGLDrawingArea.Cursor = Cursors.Default;
            }
        }

        private void UpdateCameraPosition(double deltaTime)
        {
            // Вычисляем векторы направления камеры
            double frontX = targetX - cameraX;
            double frontY = targetY - cameraY;
            double frontZ = targetZ - cameraZ;

            // Нормализуем вектор направления
            double len = Math.Sqrt(frontX * frontX + frontY * frontY + frontZ * frontZ);
            if (len > 0.0001f)
            {
                frontX /= len;
                frontY /= len;
                frontZ /= len;
            }

            // Вычисляем правый вектор (перпендикулярно направлению и вектору вверх)
            double rightX = frontY * upZ - frontZ * upY;
            double rightY = frontZ * upX - frontX * upZ;
            double rightZ = frontX * upY - frontY * upX;

            // Нормализуем правый вектор
            len = Math.Sqrt(rightX * rightX + rightY * rightY + rightZ * rightZ);
            if (len > 0.0001f)
            {
                rightX /= len;
                rightY /= len;
                rightZ /= len;
            }

            // Перемещение
            double velocity = moveSpeed * deltaTime;

            if (keyW) // Вперёд
            {
                cameraX += frontX * velocity;
                cameraY += frontY * velocity;
                cameraZ += frontZ * velocity;
            }
            if (keyS) // Назад
            {
                cameraX -= frontX * velocity;
                cameraY -= frontY * velocity;
                cameraZ -= frontZ * velocity;
            }
            if (keyD) // Вправо
            {
                cameraX += rightX * velocity;
                cameraY += rightY * velocity;
                cameraZ += rightZ * velocity;
            }
            if (keyA) // Влево
            {
                cameraX -= rightX * velocity;
                cameraY -= rightY * velocity;
                cameraZ -= rightZ * velocity;
            }
            if (keyQ) // Подъём
            {
                cameraY += velocity;
            }
            if (keyE) // Опускание
            {
                cameraY -= velocity;
            }
        }

        private void ViewResultForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: keyW = true; break;
                case Keys.A: keyA = true; break;
                case Keys.S: keyS = true; break;
                case Keys.D: keyD = true; break;
                case Keys.Q: keyQ = true; break;
                case Keys.E: keyE = true; break;
            }
        }

        private void ViewResultForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: keyW = false; break;
                case Keys.A: keyA = false; break;
                case Keys.S: keyS = false; break;
                case Keys.D: keyD = false; break;
                case Keys.Q: keyQ = false; break;
                case Keys.E: keyE = false; break;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            show_green_box = checkBoxGreenBox.Checked;
            show_net = checkBoxNet.Checked;
            show_surface = checkBoxSolution.Checked;
            show_zero_y = checkBoxY0.Checked;
            if (!double.TryParse(textBoxZeroY.Text, out y_zero)) { y_zero = 0; textBoxZeroY.Text = "0"; }
            panel1.Invalidate();
            panelLegend.Invalidate();
        }

        private void DrawRealSurface(OpenGL gl)
        {
            var xNodes = _mainForm.global_net_x;
            var zNodes = _mainForm.global_net_y;
            var flatSolution = _mainForm.solution;

            if (xNodes == null || zNodes == null || flatSolution == null) return;

            int nx = xNodes.Count;
            int nz = zNodes.Count;

            double GetRawHeight(int i, int j) => flatSolution[i * nx + j];

            // Находим min и max высоты
            double rawMinY = flatSolution.Min(), rawMaxY = flatSolution.Max();

            if (rawMinY != 0)
            {
                if (Math.Abs(rawMaxY / rawMinY - 1) < 0.0001f) no_normalize = true;
            }

            if (show_surface)
            {
                // Рисуем залитые квадраты
                gl.Begin(OpenGL.GL_QUADS);
                for (int i = 0; i < nz - 1; i++)
                {
                    double z1 = ScaleZ(zNodes[i]);
                    double z2 = ScaleZ(zNodes[i + 1]);

                    for (int j = 0; j < nx - 1; j++)
                    {
                        double x1 = ScaleX(xNodes[j]);
                        double x2 = ScaleX(xNodes[j + 1]);

                        double rawY11 = GetRawHeight(i, j);
                        double rawY12 = GetRawHeight(i, j + 1);
                        double rawY21 = GetRawHeight(i + 1, j);
                        double rawY22 = GetRawHeight(i + 1, j + 1);

                        double y11 = NormalizeHeight(rawY11, rawMinY, rawMaxY);
                        double y12 = NormalizeHeight(rawY12, rawMinY, rawMaxY);
                        double y21 = NormalizeHeight(rawY21, rawMinY, rawMaxY);
                        double y22 = NormalizeHeight(rawY22, rawMinY, rawMaxY);


                        Color color = GetColorByAbsoluteValue(rawY11);
                        gl.Color(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
                        gl.Vertex(x1, y11, z1);

                        color = GetColorByAbsoluteValue(rawY21);
                        gl.Color(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
                        gl.Vertex(x1, y21, z2);

                        color = GetColorByAbsoluteValue(rawY22);
                        gl.Color(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
                        gl.Vertex(x2, y22, z2);

                        color = GetColorByAbsoluteValue(rawY12);
                        gl.Color(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
                        gl.Vertex(x2, y12, z1);
                    }
                }
                gl.End();
            }
            // отрисовка сетки
            if (show_net)
            {
                //gl.Disable(OpenGL.GL_DEPTH_TEST);
                gl.Color(0.0f, 0.0f, 0.0f);
                gl.LineWidth(2.0f);

                gl.Begin(OpenGL.GL_LINES);

                // Линии вдоль X (горизонтальные)
                for (int i = 0; i < nz; i++)
                {
                    double z = ScaleZ(zNodes[i]);
                    for (int j = 0; j < nx - 1; j++)
                    {
                        double x1 = ScaleX(xNodes[j]);
                        double x2 = ScaleX(xNodes[j + 1]);
                        double y1 = NormalizeHeight(GetRawHeight(i, j), rawMinY, rawMaxY) + 0.001f * targetMaxHeight;
                        double y2 = NormalizeHeight(GetRawHeight(i, j + 1), rawMinY, rawMaxY) + 0.001f * targetMaxHeight;

                        gl.Vertex(x1, y1, z);
                        gl.Vertex(x2, y2, z);
                    }
                }

                // Линии вдоль Z (вертикальные)
                for (int j = 0; j < nx; j++)
                {
                    double x = ScaleX(xNodes[j]);
                    for (int i = 0; i < nz - 1; i++)
                    {
                        double z1 = ScaleZ(zNodes[i]);
                        double z2 = ScaleZ(zNodes[i + 1]);
                        double y1 = NormalizeHeight(GetRawHeight(i, j), rawMinY, rawMaxY) + 0.001f * targetMaxHeight;
                        double y2 = NormalizeHeight(GetRawHeight(i + 1, j), rawMinY, rawMaxY) + 0.001f * targetMaxHeight;

                        gl.Vertex(x, y1, z1);
                        gl.Vertex(x, y2, z2);
                    }
                }
                gl.End();
            }
            if (show_zero_y)
            {
                double x_step = x_size / x_zero_lines_num, z_step = z_size / z_zero_lines_num;
                gl.Begin(OpenGL.GL_LINES);
                for (int i = 0; i <= x_zero_lines_num; i++)
                {
                    gl.Vertex(x_step * i, NormalizeHeight(y_zero, rawMinY, rawMaxY), 0);
                    gl.Vertex(x_step * i, NormalizeHeight(y_zero, rawMinY, rawMaxY), z_size);
                }
                for (int i = 0; i <= z_zero_lines_num; i++)
                {
                    gl.Vertex(0, NormalizeHeight(y_zero, rawMinY, rawMaxY), z_step * i);
                    gl.Vertex(x_size, NormalizeHeight(y_zero, rawMinY, rawMaxY), z_step * i);
                }
                gl.End();
            }
            //gl.Enable(OpenGL.GL_DEPTH_TEST);
        }

        // Функция нормализации
        double NormalizeHeight(double rawVal, double min, double max)
        {
            double t;
            if (no_normalize)
            {
                t = 0.5f;
            }
            else
            {
                t = (rawVal - min) / (max - min);
            }
            return targetMinHeight + t * (targetMaxHeight - targetMinHeight);
        }

        private void panelLegend_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = panelLegend.Width;
            int height = panelLegend.Height;

            var sorted = thresholds.OrderBy(t => t.Value).ToList();

            // Проход по интервалам между порогами
            for (int idx = 0; idx < sorted.Count - 1; idx++)
            {
                double startVal = sorted[idx].Value;
                double endVal = sorted[idx + 1].Value;
                Color startColor = sorted[idx].Color;
                Color endColor = sorted[idx + 1].Color;

                // Вычисляем Y-координаты для начала и конца интервала
                double tStart = (startVal - thresholds[0].Value) / (thresholds.Last().Value - thresholds[0].Value);
                double tEnd = (endVal - thresholds[0].Value) / (thresholds.Last().Value - thresholds[0].Value);

                int yStart = (int)(height * (tStart));
                int yEnd = (int)(height * (tEnd));


                for (int y = yStart; y <= yEnd; y++)
                {
                    double t = (double)(y - yStart) / (yEnd - yStart);
                    Color color = InterpolateColor(startColor, endColor, t);
                    using (Pen pen = new Pen(color))
                    {
                        g.DrawLine(pen, 0, height - y, width - 1, height - y);
                    }
                }
            }

            // Рисуем маркеры на границах порогов
            DrawThresholdMarkers(g, width, height, sorted);

            // Рамка и подписи
            g.DrawRectangle(Pens.Black, 0, 0, width - 1, height - 1);
        }

        private void button3DSettings_Click(object sender, EventArgs e)
        {
            using (ThresholdForm dlg = new ThresholdForm(thresholds))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    thresholds = dlg.Thresholds;
                    panelLegend.Invalidate();
                    UpdateLegendLabels();
                    openGLDrawingArea.Invalidate();
                }
            }
        }

        private double ScaleX(double x)
        {
            double minX = _mainForm.global_net_x.Min();
            double maxX = _mainForm.global_net_x.Max();
            return (x - minX) / (maxX - minX) * x_size;
        }

        private void buttonSetCam_Click(object sender, EventArgs e)
        {
            double cam_x, cam_y, cam_z, look_at_x, look_at_y, look_at_z;
            bool input_correct = true;
            if (!double.TryParse(textBoxCamPosX.Text.Replace('.', ','), out cam_x))
                input_correct = false;
            if (!double.TryParse(textBoxCamPosY.Text.Replace('.', ','), out cam_y))
                input_correct = false;
            if (!double.TryParse(textBoxCamPosZ.Text.Replace('.', ','), out cam_z))
                input_correct = false;
            if (!double.TryParse(textBoxCamLookAtX.Text.Replace('.', ','), out look_at_x))
                input_correct = false;
            if (!double.TryParse(textBoxCamLookAtY.Text.Replace('.', ','), out look_at_y))
                input_correct = false;
            if (!double.TryParse(textBoxCamLookAtZ.Text.Replace('.', ','), out look_at_z))
                input_correct = false;
            if (!input_correct)
            {
                MessageBox.Show("Ошибка ввода");
            }
            else
            {
                UpdateCameraFromInput(cam_x, cam_y, cam_z, look_at_x, look_at_y, look_at_z);
            }
        }

        private double ScaleZ(double z)
        {
            double minZ = _mainForm.global_net_y.Min();
            double maxZ = _mainForm.global_net_y.Max();
            return (z - minZ) / (maxZ - minZ) * z_size;
        }

        private Color GetColorByAbsoluteValue(double value)
        {
            if (thresholds.Count == 0) return Color.Black;

            // Сортируем пороги по возрастанию
            var sorted = thresholds.OrderBy(t => t.Value).ToList();

            // Если значение меньше минимального порога
            if (value <= sorted[0].Value)
                return sorted[0].Color;

            // Если значение больше максимального порога
            if (value >= sorted[sorted.Count - 1].Value)
                return sorted[sorted.Count - 1].Color;

            // Находим интервал, содержащий value
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                if (value >= sorted[i].Value && value <= sorted[i + 1].Value)
                {
                    double t = (value - sorted[i].Value) / (sorted[i + 1].Value - sorted[i].Value);
                    return InterpolateColor(sorted[i].Color, sorted[i + 1].Color, t);
                }
            }

            return Color.Black;
        }

        private void ViewResultForm_Resize(object sender, EventArgs e)
        {
            PositionLegendLabels();
            panelLegend.Invalidate();
        }

        private Color InterpolateColor(Color a, Color b, double t)
        {
            // Ограничиваем t в пределах [0, 1]
            if (t < 0) t = 0;
            if (t > 1) t = 1;

            int r = (int)(a.R + (b.R - a.R) * t);
            int g = (int)(a.G + (b.G - a.G) * t);
            int bVal = (int)(a.B + (b.B - a.B) * t);

            return Color.FromArgb(r, g, bVal);
        }
        private void DrawThresholdMarkers(Graphics g, int width, int height, List<ColorThreshold> sortedThresholds)
        {
            if (sortedThresholds == null || sortedThresholds.Count == 0) return;

            double minValue = sortedThresholds.First().Value;
            double maxValue = sortedThresholds.Last().Value;
            double range = maxValue - minValue;

            if (range < 0.0001f) range = 1;

            using (Font font = new Font("Arial", 8, FontStyle.Bold))
            using (Pen markerPen = new Pen(Color.Black, 1))
            using (Brush textBrush = new SolidBrush(Color.Black))
            {
                foreach (var threshold in sortedThresholds)
                {
                    // Вычисляем Y-координату маркера (0 = верх, height = низ)
                    double t = (threshold.Value - minValue) / range;
                    int y = (int)(height * (1 - t));

                    // Ограничиваем, чтобы не вылезать за пределы
                    y = Math.Max(0, Math.Min(height, y));

                    // Рисуем горизонтальную линию-маркер
                    g.DrawLine(markerPen, width - 15, y, width - 5, y);
                }
            }
        }

        private void UpdateCameraFromInput(double input_cameraX, double input_cameraY, double input_cameraZ, double input_targetX, double input_targetY, double input_targetZ)
        {
            double dx = (input_targetX - input_cameraX);
            double dy = (input_targetY - input_cameraY);
            double dz = (input_targetZ - input_cameraZ);

            yaw = Math.Atan2(dz, dx) * 180.0 / Math.PI;

            double horizontalDistance = Math.Sqrt(dx * dx + dz * dz);
            pitch = Math.Atan2(dy, horizontalDistance) * 180.0 / Math.PI;

            if (pitch > 89.0f) pitch = 89.0f;
            if (pitch < -89.0f) pitch = -89.0f;
            cameraX = input_cameraX;
            cameraY = input_cameraY;
            cameraZ = input_cameraZ;
        }

        private void UpdateLegendLabels()
        {
            var sorted = thresholds.OrderBy(t => t.Value).ToList();
            if (sorted.Count == 0) return;

            // Удаляем старые Label
            foreach (var label in legendLabels)
            {
                this.Controls.Remove(label);
                label.Dispose();
            }
            legendLabels.Clear();

            // Создаём новые Label для каждого порога
            foreach (var threshold in sorted)
            {
                Label label = new Label();
                label.AutoSize = true;
                label.Font = new Font("Arial", 8, FontStyle.Bold);
                label.TextAlign = ContentAlignment.MiddleRight;
                label.BackColor = Color.White;
                label.Text = threshold.Value.ToString("F1");

                this.Controls.Add(label);
                legendLabels.Add(label);
            }

            // Позиционируем Label
            PositionLegendLabels();
        }

        private void PositionLegendLabels()
        {
            if (legendLabels.Count == 0) return;

            var sorted = thresholds.OrderBy(t => t.Value).ToList();
            if (sorted.Count != legendLabels.Count) return;

            int legendRight = panelLegend.Left + panelLegend.Width;
            int labelX = legendRight + 5;  // отступ справа от панели

            double minVal = sorted.First().Value;
            double maxVal = sorted.Last().Value;
            double range = maxVal - minVal;
            if (range < 0.0001) range = 1;

            for (int i = 0; i < legendLabels.Count; i++)
            {
                double t = (sorted[i].Value - minVal) / range;
                int y = (int)(panelLegend.Height * (1 - t));
                y = panelLegend.Top + y;

                Label label = legendLabels[i];
                label.Text = sorted[i].Value.ToString("F1");
                label.Left = labelX;
                label.Top = y - label.Height / 2;
            }
        }
    }

    public class ColorThreshold
    {
        public double Value { get; set; }
        public Color Color { get; set; }
    }
}