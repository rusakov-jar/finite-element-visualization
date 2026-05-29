using SharpGL;
using System;
using System.Windows.Forms;

namespace K2
{
    public partial class ViewResultForm : Form
    {
        private Form1 _mainForm;

        private float cameraX = 0.0f;
        private float cameraY = 80.0f;
        private float cameraZ = 0.0f;

        private float targetX = 50.0f;
        private float targetY = 0.0f;
        private float targetZ = 50.0f;

        private float upX = 0.0f;
        private float upY = 1.0f;
        private float upZ = 0.0f;

        // Углы для вращения мышью
        private float yaw = 45.0f;    // поворот влево/вправо
        private float pitch = -45.0f;   // наклон вверх/вниз

        // Управление мышью
        private bool mouseCaptured = false;
        private System.Drawing.Point lastMousePos;
        private float mouseSensitivity = 0.3f;


        public ViewResultForm(Form1 mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;

            openGLDrawingArea.OpenGLInitialized += openGLDrawingArea_OpenGLInitialized;
            openGLDrawingArea.OpenGLDraw += openGLDrawingArea_OpenGLDraw;
            openGLDrawingArea.Resized += openGLDrawingArea_Resized;

            openGLDrawingArea.MouseDown += OpenGLDrawingArea_MouseDown;
            openGLDrawingArea.MouseMove += OpenGLDrawingArea_MouseMove;
            openGLDrawingArea.MouseUp += OpenGLDrawingArea_MouseUp;

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
            gl.Vertex(0.0f, 0.0f, 100.0f);     // Поменяли местами
            gl.Vertex(100.0f, 0.0f, 100.0f);
            gl.Vertex(100.0f, 0.0f, 0.0f);     // Поменяли местами
            gl.End();
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

            // Куда смотрим = позиция камеры + направление
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

                //UpdateCameraDirection();

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
    }
}