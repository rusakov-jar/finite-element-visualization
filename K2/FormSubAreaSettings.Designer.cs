namespace K2
{
    partial class FormSubAreaSettings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageSetQ = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMonoNNum = new System.Windows.Forms.NumericUpDown();
            this.tabPageSetH = new System.Windows.Forms.TabPage();
            this.textBoxMonoStepSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPageSetNoQ = new System.Windows.Forms.TabPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1H0 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageSetNoN = new System.Windows.Forms.TabPage();
            this.textBox2Q = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2H0 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3Q = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageSetQ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonoNNum)).BeginInit();
            this.tabPageSetH.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPageSetNoQ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPageSetNoN.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(307, 248);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(299, 219);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "равномерная";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPageSetQ);
            this.tabControl2.Controls.Add(this.tabPageSetH);
            this.tabControl2.Location = new System.Drawing.Point(4, 7);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(289, 206);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPageSetQ
            // 
            this.tabPageSetQ.Controls.Add(this.label1);
            this.tabPageSetQ.Controls.Add(this.numericUpDownMonoNNum);
            this.tabPageSetQ.Location = new System.Drawing.Point(4, 25);
            this.tabPageSetQ.Name = "tabPageSetQ";
            this.tabPageSetQ.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetQ.Size = new System.Drawing.Size(281, 177);
            this.tabPageSetQ.TabIndex = 0;
            this.tabPageSetQ.Text = "n";
            this.tabPageSetQ.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество узлов";
            // 
            // numericUpDownMonoNNum
            // 
            this.numericUpDownMonoNNum.Location = new System.Drawing.Point(172, 18);
            this.numericUpDownMonoNNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownMonoNNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownMonoNNum.Name = "numericUpDownMonoNNum";
            this.numericUpDownMonoNNum.Size = new System.Drawing.Size(88, 22);
            this.numericUpDownMonoNNum.TabIndex = 0;
            this.numericUpDownMonoNNum.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // tabPageSetH
            // 
            this.tabPageSetH.Controls.Add(this.textBoxMonoStepSize);
            this.tabPageSetH.Controls.Add(this.label2);
            this.tabPageSetH.Location = new System.Drawing.Point(4, 25);
            this.tabPageSetH.Name = "tabPageSetH";
            this.tabPageSetH.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetH.Size = new System.Drawing.Size(281, 177);
            this.tabPageSetH.TabIndex = 1;
            this.tabPageSetH.Text = "h";
            this.tabPageSetH.UseVisualStyleBackColor = true;
            // 
            // textBoxMonoStepSize
            // 
            this.textBoxMonoStepSize.Location = new System.Drawing.Point(148, 7);
            this.textBoxMonoStepSize.Name = "textBoxMonoStepSize";
            this.textBoxMonoStepSize.Size = new System.Drawing.Size(100, 22);
            this.textBoxMonoStepSize.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Размер шага";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(299, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "неравномерная";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl3.Controls.Add(this.tabPageSetNoQ);
            this.tabControl3.Controls.Add(this.tabPageSetNoN);
            this.tabControl3.Controls.Add(this.tabPage7);
            this.tabControl3.Location = new System.Drawing.Point(4, 7);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(292, 206);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPageSetNoQ
            // 
            this.tabPageSetNoQ.Controls.Add(this.numericUpDown1);
            this.tabPageSetNoQ.Controls.Add(this.label4);
            this.tabPageSetNoQ.Controls.Add(this.textBox1H0);
            this.tabPageSetNoQ.Controls.Add(this.label3);
            this.tabPageSetNoQ.Location = new System.Drawing.Point(4, 25);
            this.tabPageSetNoQ.Name = "tabPageSetNoQ";
            this.tabPageSetNoQ.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetNoQ.Size = new System.Drawing.Size(284, 177);
            this.tabPageSetNoQ.TabIndex = 0;
            this.tabPageSetNoQ.Text = "h0, n";
            this.tabPageSetNoQ.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(154, 52);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(88, 22);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "n";
            // 
            // textBox1H0
            // 
            this.textBox1H0.Location = new System.Drawing.Point(154, 12);
            this.textBox1H0.Name = "textBox1H0";
            this.textBox1H0.Size = new System.Drawing.Size(100, 22);
            this.textBox1H0.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "h0";
            // 
            // tabPageSetNoN
            // 
            this.tabPageSetNoN.Controls.Add(this.textBox2Q);
            this.tabPageSetNoN.Controls.Add(this.label5);
            this.tabPageSetNoN.Controls.Add(this.textBox2H0);
            this.tabPageSetNoN.Controls.Add(this.label6);
            this.tabPageSetNoN.Location = new System.Drawing.Point(4, 25);
            this.tabPageSetNoN.Name = "tabPageSetNoN";
            this.tabPageSetNoN.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetNoN.Size = new System.Drawing.Size(284, 177);
            this.tabPageSetNoN.TabIndex = 1;
            this.tabPageSetNoN.Text = "h0, q";
            this.tabPageSetNoN.UseVisualStyleBackColor = true;
            // 
            // textBox2Q
            // 
            this.textBox2Q.Location = new System.Drawing.Point(149, 46);
            this.textBox2Q.Name = "textBox2Q";
            this.textBox2Q.Size = new System.Drawing.Size(100, 22);
            this.textBox2Q.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "q";
            // 
            // textBox2H0
            // 
            this.textBox2H0.Location = new System.Drawing.Point(149, 6);
            this.textBox2H0.Name = "textBox2H0";
            this.textBox2H0.Size = new System.Drawing.Size(100, 22);
            this.textBox2H0.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "h0";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.numericUpDown3);
            this.tabPage7.Controls.Add(this.label7);
            this.tabPage7.Controls.Add(this.textBox3Q);
            this.tabPage7.Controls.Add(this.label8);
            this.tabPage7.Location = new System.Drawing.Point(4, 25);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(284, 177);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "n, q";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(159, 10);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(100, 22);
            this.numericUpDown3.TabIndex = 9;
            this.numericUpDown3.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "q";
            // 
            // textBox3Q
            // 
            this.textBox3Q.Location = new System.Drawing.Point(159, 52);
            this.textBox3Q.Name = "textBox3Q";
            this.textBox3Q.Size = new System.Drawing.Size(100, 22);
            this.textBox3Q.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "n";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSubmit.Location = new System.Drawing.Point(12, 266);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(116, 23);
            this.buttonSubmit.TabIndex = 2;
            this.buttonSubmit.Text = "Задать";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(233, 266);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(79, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormSubAreaSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 303);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSubmit);
            this.Name = "FormSubAreaSettings";
            this.Text = "FormSubAreaSettings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageSetQ.ResumeLayout(false);
            this.tabPageSetQ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonoNNum)).EndInit();
            this.tabPageSetH.ResumeLayout(false);
            this.tabPageSetH.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPageSetNoQ.ResumeLayout(false);
            this.tabPageSetNoQ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPageSetNoN.ResumeLayout(false);
            this.tabPageSetNoN.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPageSetQ;
        private System.Windows.Forms.TabPage tabPageSetH;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPageSetNoQ;
        private System.Windows.Forms.TabPage tabPageSetNoN;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownMonoNNum;
        private System.Windows.Forms.TextBox textBoxMonoStepSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1H0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2Q;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2H0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox3Q;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
    }
}