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
            this.panelLegend = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxY0 = new System.Windows.Forms.CheckBox();
            this.checkBoxSolution = new System.Windows.Forms.CheckBox();
            this.checkBoxNet = new System.Windows.Forms.CheckBox();
            this.checkBoxGreenBox = new System.Windows.Forms.CheckBox();
            this.textBoxZeroY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3DSettings = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.tabControl3D = new System.Windows.Forms.TabControl();
            this.tabPageVeiw = new System.Windows.Forms.TabPage();
            this.tabPageCamera = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCamPosX = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxCamPosY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCamPosZ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxCamLookAtZ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCamLookAtY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCamLookAtX = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonSetCam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGLDrawingArea)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl3D.SuspendLayout();
            this.tabPageVeiw.SuspendLayout();
            this.tabPageCamera.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLDrawingArea
            // 
            this.openGLDrawingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLDrawingArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.openGLDrawingArea.DrawFPS = false;
            this.openGLDrawingArea.Location = new System.Drawing.Point(234, 39);
            this.openGLDrawingArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.openGLDrawingArea.Name = "openGLDrawingArea";
            this.openGLDrawingArea.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLDrawingArea.RenderContextType = SharpGL.RenderContextType.FBO;
            this.openGLDrawingArea.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLDrawingArea.Size = new System.Drawing.Size(424, 344);
            this.openGLDrawingArea.TabIndex = 0;
            // 
            // panelLegend
            // 
            this.panelLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLegend.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelLegend.Location = new System.Drawing.Point(665, 39);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(38, 344);
            this.panelLegend.TabIndex = 1;
            this.panelLegend.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLegend_Paint);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.checkBoxY0);
            this.panel1.Controls.Add(this.buttonUpdate);
            this.panel1.Controls.Add(this.checkBoxSolution);
            this.panel1.Controls.Add(this.checkBoxNet);
            this.panel1.Controls.Add(this.checkBoxGreenBox);
            this.panel1.Controls.Add(this.textBoxZeroY);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 296);
            this.panel1.TabIndex = 0;
            // 
            // checkBoxY0
            // 
            this.checkBoxY0.AutoSize = true;
            this.checkBoxY0.Checked = true;
            this.checkBoxY0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxY0.Location = new System.Drawing.Point(4, 112);
            this.checkBoxY0.Name = "checkBoxY0";
            this.checkBoxY0.Size = new System.Drawing.Size(38, 20);
            this.checkBoxY0.TabIndex = 5;
            this.checkBoxY0.Text = "Y";
            this.checkBoxY0.UseVisualStyleBackColor = true;
            // 
            // checkBoxSolution
            // 
            this.checkBoxSolution.AutoSize = true;
            this.checkBoxSolution.Checked = true;
            this.checkBoxSolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSolution.Location = new System.Drawing.Point(4, 86);
            this.checkBoxSolution.Name = "checkBoxSolution";
            this.checkBoxSolution.Size = new System.Drawing.Size(114, 20);
            this.checkBoxSolution.TabIndex = 4;
            this.checkBoxSolution.Text = "Поверхность";
            this.checkBoxSolution.UseVisualStyleBackColor = true;
            // 
            // checkBoxNet
            // 
            this.checkBoxNet.AutoSize = true;
            this.checkBoxNet.Checked = true;
            this.checkBoxNet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNet.Location = new System.Drawing.Point(3, 60);
            this.checkBoxNet.Name = "checkBoxNet";
            this.checkBoxNet.Size = new System.Drawing.Size(68, 20);
            this.checkBoxNet.TabIndex = 3;
            this.checkBoxNet.Text = "Сетка";
            this.checkBoxNet.UseVisualStyleBackColor = true;
            // 
            // checkBoxGreenBox
            // 
            this.checkBoxGreenBox.AutoSize = true;
            this.checkBoxGreenBox.Checked = true;
            this.checkBoxGreenBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGreenBox.Location = new System.Drawing.Point(4, 34);
            this.checkBoxGreenBox.Name = "checkBoxGreenBox";
            this.checkBoxGreenBox.Size = new System.Drawing.Size(84, 20);
            this.checkBoxGreenBox.TabIndex = 2;
            this.checkBoxGreenBox.Text = "Коробка";
            this.checkBoxGreenBox.UseVisualStyleBackColor = true;
            // 
            // textBoxZeroY
            // 
            this.textBoxZeroY.Location = new System.Drawing.Point(89, 4);
            this.textBoxZeroY.Name = "textBoxZeroY";
            this.textBoxZeroY.Size = new System.Drawing.Size(100, 22);
            this.textBoxZeroY.TabIndex = 1;
            this.textBoxZeroY.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "y = ";
            // 
            // button3DSettings
            // 
            this.button3DSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3DSettings.Location = new System.Drawing.Point(686, 413);
            this.button3DSettings.Name = "button3DSettings";
            this.button3DSettings.Size = new System.Drawing.Size(98, 28);
            this.button3DSettings.TabIndex = 3;
            this.button3DSettings.Text = "Настройки";
            this.button3DSettings.UseVisualStyleBackColor = true;
            this.button3DSettings.Click += new System.EventHandler(this.button3DSettings_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(7, 270);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(88, 23);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // tabControl3D
            // 
            this.tabControl3D.Controls.Add(this.tabPageVeiw);
            this.tabControl3D.Controls.Add(this.tabPageCamera);
            this.tabControl3D.Location = new System.Drawing.Point(12, 39);
            this.tabControl3D.Name = "tabControl3D";
            this.tabControl3D.SelectedIndex = 0;
            this.tabControl3D.Size = new System.Drawing.Size(215, 344);
            this.tabControl3D.TabIndex = 6;
            // 
            // tabPageVeiw
            // 
            this.tabPageVeiw.Controls.Add(this.panel1);
            this.tabPageVeiw.Location = new System.Drawing.Point(4, 25);
            this.tabPageVeiw.Name = "tabPageVeiw";
            this.tabPageVeiw.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVeiw.Size = new System.Drawing.Size(207, 315);
            this.tabPageVeiw.TabIndex = 0;
            this.tabPageVeiw.Text = "Отрисовка";
            this.tabPageVeiw.UseVisualStyleBackColor = true;
            // 
            // tabPageCamera
            // 
            this.tabPageCamera.Controls.Add(this.buttonSetCam);
            this.tabPageCamera.Controls.Add(this.groupBox2);
            this.tabPageCamera.Controls.Add(this.groupBox1);
            this.tabPageCamera.Location = new System.Drawing.Point(4, 25);
            this.tabPageCamera.Name = "tabPageCamera";
            this.tabPageCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCamera.Size = new System.Drawing.Size(207, 315);
            this.tabPageCamera.TabIndex = 1;
            this.tabPageCamera.Text = "Камера";
            this.tabPageCamera.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "X";
            // 
            // textBoxCamPosX
            // 
            this.textBoxCamPosX.Location = new System.Drawing.Point(98, 21);
            this.textBoxCamPosX.Name = "textBoxCamPosX";
            this.textBoxCamPosX.Size = new System.Drawing.Size(100, 22);
            this.textBoxCamPosX.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxCamPosZ);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxCamPosY);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxCamPosX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 109);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Позиция камеры";
            // 
            // textBoxCamPosY
            // 
            this.textBoxCamPosY.Location = new System.Drawing.Point(98, 49);
            this.textBoxCamPosY.Name = "textBoxCamPosY";
            this.textBoxCamPosY.Size = new System.Drawing.Size(100, 22);
            this.textBoxCamPosY.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            // 
            // textBoxCamPosZ
            // 
            this.textBoxCamPosZ.Location = new System.Drawing.Point(98, 77);
            this.textBoxCamPosZ.Name = "textBoxCamPosZ";
            this.textBoxCamPosZ.Size = new System.Drawing.Size(100, 22);
            this.textBoxCamPosZ.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Z";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxCamLookAtZ);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxCamLookAtY);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxCamLookAtX);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(3, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 109);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Направление камеры";
            // 
            // textBoxCamLookAtZ
            // 
            this.textBoxCamLookAtZ.Location = new System.Drawing.Point(98, 77);
            this.textBoxCamLookAtZ.Name = "textBoxCamLookAtZ";
            this.textBoxCamLookAtZ.Size = new System.Drawing.Size(100, 22);
            this.textBoxCamLookAtZ.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Z";
            // 
            // textBoxCamLookAtY
            // 
            this.textBoxCamLookAtY.Location = new System.Drawing.Point(98, 49);
            this.textBoxCamLookAtY.Name = "textBoxCamLookAtY";
            this.textBoxCamLookAtY.Size = new System.Drawing.Size(100, 22);
            this.textBoxCamLookAtY.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Y";
            // 
            // textBoxCamLookAtX
            // 
            this.textBoxCamLookAtX.Location = new System.Drawing.Point(98, 21);
            this.textBoxCamLookAtX.Name = "textBoxCamLookAtX";
            this.textBoxCamLookAtX.Size = new System.Drawing.Size(100, 22);
            this.textBoxCamLookAtX.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "X";
            // 
            // buttonSetCam
            // 
            this.buttonSetCam.Location = new System.Drawing.Point(10, 234);
            this.buttonSetCam.Name = "buttonSetCam";
            this.buttonSetCam.Size = new System.Drawing.Size(75, 23);
            this.buttonSetCam.TabIndex = 4;
            this.buttonSetCam.Text = "Принять";
            this.buttonSetCam.UseVisualStyleBackColor = true;
            this.buttonSetCam.Click += new System.EventHandler(this.buttonSetCam_Click);
            // 
            // ViewResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 453);
            this.Controls.Add(this.tabControl3D);
            this.Controls.Add(this.button3DSettings);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.openGLDrawingArea);
            this.Name = "ViewResultForm";
            this.Text = "ViewResultForm";
            this.Resize += new System.EventHandler(this.ViewResultForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.openGLDrawingArea)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl3D.ResumeLayout(false);
            this.tabPageVeiw.ResumeLayout(false);
            this.tabPageCamera.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLDrawingArea;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3DSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxSolution;
        private System.Windows.Forms.CheckBox checkBoxNet;
        private System.Windows.Forms.CheckBox checkBoxGreenBox;
        private System.Windows.Forms.TextBox textBoxZeroY;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox checkBoxY0;
        private System.Windows.Forms.TabControl tabControl3D;
        private System.Windows.Forms.TabPage tabPageVeiw;
        private System.Windows.Forms.TabPage tabPageCamera;
        private System.Windows.Forms.TextBox textBoxCamPosX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxCamPosZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCamPosY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxCamLookAtZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCamLookAtY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCamLookAtX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSetCam;
    }
}