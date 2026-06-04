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
            this.tableLayoutPanelLegendValues = new System.Windows.Forms.TableLayoutPanel();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMed = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3DSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxZeroY = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.checkBoxGreenBox = new System.Windows.Forms.CheckBox();
            this.checkBoxNet = new System.Windows.Forms.CheckBox();
            this.checkBoxSolution = new System.Windows.Forms.CheckBox();
            this.checkBoxY0 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.openGLDrawingArea)).BeginInit();
            this.tableLayoutPanelLegendValues.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.panelLegend.Size = new System.Drawing.Size(29, 344);
            this.panelLegend.TabIndex = 1;
            this.panelLegend.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLegend_Paint);
            // 
            // tableLayoutPanelLegendValues
            // 
            this.tableLayoutPanelLegendValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelLegendValues.ColumnCount = 1;
            this.tableLayoutPanelLegendValues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLegendValues.Controls.Add(this.labelMin, 0, 2);
            this.tableLayoutPanelLegendValues.Controls.Add(this.labelMed, 0, 1);
            this.tableLayoutPanelLegendValues.Controls.Add(this.labelMax, 0, 0);
            this.tableLayoutPanelLegendValues.Location = new System.Drawing.Point(700, 39);
            this.tableLayoutPanelLegendValues.Name = "tableLayoutPanelLegendValues";
            this.tableLayoutPanelLegendValues.RowCount = 3;
            this.tableLayoutPanelLegendValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelLegendValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanelLegendValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelLegendValues.Size = new System.Drawing.Size(88, 344);
            this.tableLayoutPanelLegendValues.TabIndex = 2;
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelMin.Location = new System.Drawing.Point(3, 328);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(82, 16);
            this.labelMin.TabIndex = 2;
            this.labelMin.Text = "labelMin";
            this.labelMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMed
            // 
            this.labelMed.AutoSize = true;
            this.labelMed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMed.Location = new System.Drawing.Point(3, 34);
            this.labelMed.Name = "labelMed";
            this.labelMed.Size = new System.Drawing.Size(82, 275);
            this.labelMed.TabIndex = 1;
            this.labelMed.Text = "labelMed";
            this.labelMed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelMax.Location = new System.Drawing.Point(3, 0);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(82, 16);
            this.labelMax.TabIndex = 0;
            this.labelMax.Text = "labelMax";
            this.labelMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.checkBoxY0);
            this.panel1.Controls.Add(this.checkBoxSolution);
            this.panel1.Controls.Add(this.checkBoxNet);
            this.panel1.Controls.Add(this.checkBoxGreenBox);
            this.panel1.Controls.Add(this.textBoxZeroY);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 344);
            this.panel1.TabIndex = 0;
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
            // textBoxZeroY
            // 
            this.textBoxZeroY.Location = new System.Drawing.Point(112, 4);
            this.textBoxZeroY.Name = "textBoxZeroY";
            this.textBoxZeroY.Size = new System.Drawing.Size(100, 22);
            this.textBoxZeroY.TabIndex = 1;
            this.textBoxZeroY.Text = "0";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(12, 413);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(88, 23);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
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
            // ViewResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 453);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.button3DSettings);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanelLegendValues);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.openGLDrawingArea);
            this.Name = "ViewResultForm";
            this.Text = "ViewResultForm";
            ((System.ComponentModel.ISupportInitialize)(this.openGLDrawingArea)).EndInit();
            this.tableLayoutPanelLegendValues.ResumeLayout(false);
            this.tableLayoutPanelLegendValues.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLDrawingArea;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLegendValues;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMed;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3DSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxSolution;
        private System.Windows.Forms.CheckBox checkBoxNet;
        private System.Windows.Forms.CheckBox checkBoxGreenBox;
        private System.Windows.Forms.TextBox textBoxZeroY;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox checkBoxY0;
    }
}