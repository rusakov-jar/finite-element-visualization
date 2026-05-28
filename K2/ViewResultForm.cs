using SharpGL;
using System;
using System.Windows.Forms;

namespace K2
{
    public partial class ViewResultForm : Form
    {
        private Form1 _mainForm;

        public ViewResultForm()
        {
            InitializeComponent();
        }

        public ViewResultForm(Form1 mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;

            openGLDrawingArea.OpenGLInitialized += openGLDrawingArea_OpenGLInitialized;
            openGLDrawingArea.OpenGLDraw += openGLDrawingArea_OpenGLDraw;
            openGLDrawingArea.Resized += openGLDrawingArea_Resized;

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
            OpenGL gl = openGLDrawingArea.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.LookAt(50.0f, 80.0f, 120.0f,
                      50.0f, 10.0f, 0.0f,
                      0.0f, 1.0f, 0.0f);

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
    }
}