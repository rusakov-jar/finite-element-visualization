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
            this.labelMax = new System.Windows.Forms.Label();
            this.labelMed = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLDrawingArea)).BeginInit();
            this.tableLayoutPanelLegendValues.SuspendLayout();
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
            this.openGLDrawingArea.Size = new System.Drawing.Size(424, 341);
            this.openGLDrawingArea.TabIndex = 0;
            // 
            // panelLegend
            // 
            this.panelLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLegend.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelLegend.Location = new System.Drawing.Point(665, 39);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(29, 341);
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
            this.tableLayoutPanelLegendValues.Size = new System.Drawing.Size(88, 341);
            this.tableLayoutPanelLegendValues.TabIndex = 2;
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
            // labelMed
            // 
            this.labelMed.AutoSize = true;
            this.labelMed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMed.Location = new System.Drawing.Point(3, 34);
            this.labelMed.Name = "labelMed";
            this.labelMed.Size = new System.Drawing.Size(82, 272);
            this.labelMed.TabIndex = 1;
            this.labelMed.Text = "labelMed";
            this.labelMed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelMin.Location = new System.Drawing.Point(3, 325);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(82, 16);
            this.labelMin.TabIndex = 2;
            this.labelMin.Text = "labelMin";
            this.labelMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ViewResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanelLegendValues);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.openGLDrawingArea);
            this.Name = "ViewResultForm";
            this.Text = "ViewResultForm";
            ((System.ComponentModel.ISupportInitialize)(this.openGLDrawingArea)).EndInit();
            this.tableLayoutPanelLegendValues.ResumeLayout(false);
            this.tableLayoutPanelLegendValues.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLDrawingArea;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLegendValues;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMed;
        private System.Windows.Forms.Label labelMax;
    }
}