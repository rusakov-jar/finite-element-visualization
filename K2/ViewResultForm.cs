using SharpGL;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace K2
{
    public partial class ViewResultForm : Form
    {
        Form1 _mainForm;

        float cameraX = 0.0f;
        float cameraY = 80.0f;
        float cameraZ = 0.0f;

        float targetX = 50.0f;
        float targetY = 0.0f;
        float targetZ = 50.0f;

        float upX = 0.0f;
        float upY = 1.0f;
        float upZ = 0.0f;

        // Углы для вращения мышью
        float yaw = 45.0f;    // поворот влево/вправо
        float pitch = -45.0f;   // наклон вверх/вниз

        // Управление мышью
        bool mouseCaptured = false;
        System.Drawing.Point lastMousePos;
        float mouseSensitivity = 0.3f;

        bool keyW = false;
        bool keyA = false;
        bool keyS = false;
        bool keyD = false;
        bool keyQ = false;
        bool keyE = false;

        // Скорость перемещения
        float moveSpeed = 30.0f;

        // Для плавного движения (deltaTime)
        DateTime lastFrameTime;

        // Настройки нормализации высоты
        float targetMinHeight = 0.0f;   // минимальная высота после нормализации
        float targetMaxHeight = 100.0f;  // максимальная высота после нормализации

        bool no_normalize = false;


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

            float aspect = (float)openGLDrawingArea.Width / (float)openGLDrawingArea.Height;
            gl.Perspective(45.0f, aspect, 0.1f, 500.0f);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void openGLDrawingArea_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Расчёт времени между кадрами
            DateTime now = DateTime.Now;
            float deltaTime = (float)(now - lastFrameTime).TotalSeconds;
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
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(0.2f, 0.8f, 0.3f);

            gl.Vertex(0.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 100.0f);
            gl.Vertex(100.0f, 0.0f, 100.0f);
            gl.Vertex(100.0f, 0.0f, 0.0f);
            gl.End();
            DrawRealSurface(gl);
        }

        private void UpdateCameraDirection()
        {
            // Перевод градусов в радианы
            float yawRad = yaw * (float)Math.PI / 180.0f;
            float pitchRad = pitch * (float)Math.PI / 180.0f;

            // Вектор направления взгляда
            float frontX = (float)(Math.Cos(yawRad) * Math.Cos(pitchRad));
            float frontY = (float)(Math.Sin(pitchRad));
            float frontZ = (float)(Math.Sin(yawRad) * Math.Cos(pitchRad));

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
                float deltaX = e.X - lastMousePos.X;
                float deltaY = e.Y - lastMousePos.Y;

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

        private void UpdateCameraPosition(float deltaTime)
        {
            // Вычисляем векторы направления камеры
            float frontX = targetX - cameraX;
            float frontY = targetY - cameraY;
            float frontZ = targetZ - cameraZ;

            // Нормализуем вектор направления
            float len = (float)Math.Sqrt(frontX * frontX + frontY * frontY + frontZ * frontZ);
            if (len > 0.0001f)
            {
                frontX /= len;
                frontY /= len;
                frontZ /= len;
            }

            // Вычисляем правый вектор (перпендикулярно направлению и вектору вверх)
            float rightX = frontY * upZ - frontZ * upY;
            float rightY = frontZ * upX - frontX * upZ;
            float rightZ = frontX * upY - frontY * upX;

            // Нормализуем правый вектор
            len = (float)Math.Sqrt(rightX * rightX + rightY * rightY + rightZ * rightZ);
            if (len > 0.0001f)
            {
                rightX /= len;
                rightY /= len;
                rightZ /= len;
            }

            // Перемещение
            float velocity = moveSpeed * deltaTime;

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

        private void DrawRealSurface(OpenGL gl)
        {
            var xNodes = _mainForm.global_net_x;
            var zNodes = _mainForm.global_net_y;
            var flatSolution = _mainForm.solution;

            if (xNodes == null || zNodes == null || flatSolution == null) return;

            int nx = xNodes.Count;
            int nz = zNodes.Count;

            float GetRawHeight(int i, int j) => (float)flatSolution[i * nx + j];

            // Находим min и max высоты
            float rawMinY = (float)flatSolution.Min(), rawMaxY = (float)flatSolution.Max();

            if (rawMinY != 0)
            {
                if (Math.Abs(rawMaxY / rawMinY - 1) < 0.0001f) no_normalize = true;
            }


            // Рисуем залитые квадраты
            gl.Begin(OpenGL.GL_QUADS);
            for (int i = 0; i < nz - 1; i++)
            {
                float z1 = ScaleZ(zNodes[i]);
                float z2 = ScaleZ(zNodes[i + 1]);

                for (int j = 0; j < nx - 1; j++)
                {
                    float x1 = ScaleX(xNodes[j]);
                    float x2 = ScaleX(xNodes[j + 1]);

                    float rawY11 = GetRawHeight(i, j);
                    float rawY12 = GetRawHeight(i, j + 1);
                    float rawY21 = GetRawHeight(i + 1, j);
                    float rawY22 = GetRawHeight(i + 1, j + 1);

                    double y11 = NormalizeHeight(rawY11, rawMinY, rawMaxY);
                    double y12 = NormalizeHeight(rawY12, rawMinY, rawMaxY);
                    double y21 = NormalizeHeight(rawY21, rawMinY, rawMaxY);
                    double y22 = NormalizeHeight(rawY22, rawMinY, rawMaxY);


                    float t = (rawY11 - rawMinY) / (rawMaxY - rawMinY);
                    gl.Color(t, 1 - Math.Abs(t - 0.5f) * 2, 1.0f - t);
                    gl.Vertex(x1, y11, z1);

                    t = (rawY21 - rawMinY) / (rawMaxY - rawMinY);
                    gl.Color(t, 1 - Math.Abs(t - 0.5f) * 2, 1.0f - t);
                    gl.Vertex(x1, y21, z2);

                    t = (rawY22 - rawMinY) / (rawMaxY - rawMinY);
                    gl.Color(t, 1 - Math.Abs(t - 0.5f) * 2, 1.0f - t);
                    gl.Vertex(x2, y22, z2);

                    t = (rawY12 - rawMinY) / (rawMaxY - rawMinY);
                    gl.Color(t, 1 - Math.Abs(t - 0.5f) * 2, 1.0f - t);
                    gl.Vertex(x2, y12, z1);
                }
            }
            gl.End();

            // отрисовка сетки

            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.Color(0.0f, 0.0f, 0.0f);
            gl.LineWidth(2.0f);

            gl.Begin(OpenGL.GL_LINES);

            // Линии вдоль X (горизонтальные)
            for (int i = 0; i < nz; i++)
            {
                float z = ScaleZ(zNodes[i]);
                for (int j = 0; j < nx - 1; j++)
                {
                    float x1 = ScaleX(xNodes[j]);
                    float x2 = ScaleX(xNodes[j + 1]);
                    float y1 = (float)NormalizeHeight(GetRawHeight(i, j), rawMinY, rawMaxY);
                    float y2 = (float)NormalizeHeight(GetRawHeight(i, j + 1), rawMinY, rawMaxY);

                    gl.Vertex(x1, y1, z);
                    gl.Vertex(x2, y2, z);
                }
            }

            // Линии вдоль Z (вертикальные)
            for (int j = 0; j < nx; j++)
            {
                float x = ScaleX(xNodes[j]);
                for (int i = 0; i < nz - 1; i++)
                {
                    float z1 = ScaleZ(zNodes[i]);
                    float z2 = ScaleZ(zNodes[i + 1]);
                    float y1 = (float)NormalizeHeight(GetRawHeight(i, j), rawMinY, rawMaxY);
                    float y2 = (float)NormalizeHeight(GetRawHeight(i + 1, j), rawMinY, rawMaxY);

                    gl.Vertex(x, y1, z1);
                    gl.Vertex(x, y2, z2);
                }
            }
            gl.End();

            gl.Enable(OpenGL.GL_DEPTH_TEST);
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

            // Рисуем градиентную полосу
            for (int y = 0; y < height; y++)
            {
                float t = 1.0f - (float)y / height; // от max (верх) к min (низ)
                int r = (int)(255 * t);
                int gVal = (int)(255 * (1 - Math.Abs(t - 0.5f) * 2));
                int b = (int)(255 * (1 - t));
                using (Pen pen = new Pen(Color.FromArgb(r, gVal, b)))
                {
                    g.DrawLine(pen, 0, y, width, y);
                }
            }

            // Рисуем рамку
            g.DrawRectangle(Pens.Black, 0, 0, width - 1, height - 1);

            // Подписываем min и max значения
            float minValue = (float)_mainForm.solution.Min();
            float maxValue = (float)_mainForm.solution.Max();
            labelMax.Text = maxValue.ToString();
            labelMin.Text = minValue.ToString();
            labelMed.Text = ((maxValue + minValue) / 2).ToString();
        }

        private float ScaleX(float x)
        {
            float minX = _mainForm.global_net_x.Min();
            float maxX = _mainForm.global_net_x.Max();
            return (x - minX) / (maxX - minX) * 100.0f;
        }

        private float ScaleZ(float z)
        {
            float minZ = _mainForm.global_net_y.Min();
            float maxZ = _mainForm.global_net_y.Max();
            return (z - minZ) / (maxZ - minZ) * 100.0f;
        }
    }
}