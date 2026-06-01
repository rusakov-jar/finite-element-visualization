namespace K2
{
    partial class ViewResultForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openGLDrawingArea = new SharpGL.OpenGLControl();
            ((System.ComponentModel.ISupportInitialize)(this.openGLDrawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLDrawingArea
            // 
            this.openGLDrawingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLDrawingArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.openGLDrawingArea.DrawFPS = false;
            this.openGLDrawingArea.Location = new System.Drawing.Point(330, 42);
            this.openGLDrawingArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.openGLDrawingArea.Name = "openGLDrawingArea";
            this.openGLDrawingArea.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLDrawingArea.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLDrawingArea.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLDrawingArea.Size = new System.Drawing.Size(424, 341);
            this.openGLDrawingArea.TabIndex = 0;
            // 
            // ViewResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.openGLDrawingArea);
            this.Name = "ViewResultForm";
            this.Text = "ViewResultForm";
            ((System.ComponentModel.ISupportInitialize)(this.openGLDrawingArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLDrawingArea;
    }
}