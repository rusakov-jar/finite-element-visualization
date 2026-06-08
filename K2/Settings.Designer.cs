namespace K2
{
    partial class Settings
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
            this.button_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEps = new System.Windows.Forms.TextBox();
            this.textBoxQ1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxQ2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxMode = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.PresetNumericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxShowNetLines = new System.Windows.Forms.CheckBox();
            this.checkBoxTriagLine = new System.Windows.Forms.CheckBox();
            this.checkBoxShowElementBorder = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BasisNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.checkBoxShowSolution = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PresetNumericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BasisNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(174, 607);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(173, 32);
            this.button_save.TabIndex = 1;
            this.button_save.Text = "Сохранить";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "eps";
            // 
            // textBoxEps
            // 
            this.textBoxEps.Location = new System.Drawing.Point(174, 13);
            this.textBoxEps.Name = "textBoxEps";
            this.textBoxEps.Size = new System.Drawing.Size(100, 22);
            this.textBoxEps.TabIndex = 3;
            // 
            // textBoxQ1
            // 
            this.textBoxQ1.Location = new System.Drawing.Point(174, 46);
            this.textBoxQ1.Name = "textBoxQ1";
            this.textBoxQ1.Size = new System.Drawing.Size(100, 22);
            this.textBoxQ1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "q1";
            // 
            // textBoxQ2
            // 
            this.textBoxQ2.Location = new System.Drawing.Point(174, 83);
            this.textBoxQ2.Name = "textBoxQ2";
            this.textBoxQ2.Size = new System.Drawing.Size(100, 22);
            this.textBoxQ2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "q2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "mode";
            // 
            // listBoxMode
            // 
            this.listBoxMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listBoxMode.FormattingEnabled = true;
            this.listBoxMode.ItemHeight = 16;
            this.listBoxMode.Items.AddRange(new object[] {
            "q",
            "h"});
            this.listBoxMode.Location = new System.Drawing.Point(174, 116);
            this.listBoxMode.Name = "listBoxMode";
            this.listBoxMode.Size = new System.Drawing.Size(100, 36);
            this.listBoxMode.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ед. Изм.";
            // 
            // comboBox1
            // 
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "м",
            "см",
            "мм"});
            this.comboBox1.Location = new System.Drawing.Point(174, 180);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 24);
            this.comboBox1.TabIndex = 13;
            // 
            // PresetNumericUpDown1
            // 
            this.PresetNumericUpDown1.Location = new System.Drawing.Point(174, 221);
            this.PresetNumericUpDown1.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.PresetNumericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.PresetNumericUpDown1.Name = "PresetNumericUpDown1";
            this.PresetNumericUpDown1.Size = new System.Drawing.Size(100, 22);
            this.PresetNumericUpDown1.TabIndex = 14;
            this.PresetNumericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Пресет";
            // 
            // checkBoxShowNetLines
            // 
            this.checkBoxShowNetLines.AutoSize = true;
            this.checkBoxShowNetLines.Checked = true;
            this.checkBoxShowNetLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowNetLines.Location = new System.Drawing.Point(387, 27);
            this.checkBoxShowNetLines.Name = "checkBoxShowNetLines";
            this.checkBoxShowNetLines.Size = new System.Drawing.Size(110, 20);
            this.checkBoxShowNetLines.TabIndex = 16;
            this.checkBoxShowNetLines.Text = "Линии сетки";
            this.checkBoxShowNetLines.UseVisualStyleBackColor = true;
            // 
            // checkBoxTriagLine
            // 
            this.checkBoxTriagLine.AutoSize = true;
            this.checkBoxTriagLine.Checked = true;
            this.checkBoxTriagLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTriagLine.Location = new System.Drawing.Point(387, 53);
            this.checkBoxTriagLine.Name = "checkBoxTriagLine";
            this.checkBoxTriagLine.Size = new System.Drawing.Size(112, 20);
            this.checkBoxTriagLine.TabIndex = 17;
            this.checkBoxTriagLine.Text = "Контур град.";
            this.checkBoxTriagLine.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowElementBorder
            // 
            this.checkBoxShowElementBorder.AutoSize = true;
            this.checkBoxShowElementBorder.Checked = true;
            this.checkBoxShowElementBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowElementBorder.Location = new System.Drawing.Point(387, 79);
            this.checkBoxShowElementBorder.Name = "checkBoxShowElementBorder";
            this.checkBoxShowElementBorder.Size = new System.Drawing.Size(138, 20);
            this.checkBoxShowElementBorder.TabIndex = 18;
            this.checkBoxShowElementBorder.Text = "Гран. элементов";
            this.checkBoxShowElementBorder.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "Степень баз. функций";
            // 
            // BasisNumericUpDown
            // 
            this.BasisNumericUpDown.Location = new System.Drawing.Point(174, 263);
            this.BasisNumericUpDown.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.BasisNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BasisNumericUpDown.Name = "BasisNumericUpDown";
            this.BasisNumericUpDown.Size = new System.Drawing.Size(100, 22);
            this.BasisNumericUpDown.TabIndex = 20;
            this.BasisNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxShowSolution
            // 
            this.checkBoxShowSolution.AutoSize = true;
            this.checkBoxShowSolution.Checked = true;
            this.checkBoxShowSolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowSolution.Location = new System.Drawing.Point(387, 105);
            this.checkBoxShowSolution.Name = "checkBoxShowSolution";
            this.checkBoxShowSolution.Size = new System.Drawing.Size(136, 20);
            this.checkBoxShowSolution.TabIndex = 21;
            this.checkBoxShowSolution.Text = "Грубое решение";
            this.checkBoxShowSolution.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 651);
            this.Controls.Add(this.checkBoxShowSolution);
            this.Controls.Add(this.BasisNumericUpDown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkBoxShowElementBorder);
            this.Controls.Add(this.checkBoxTriagLine);
            this.Controls.Add(this.checkBoxShowNetLines);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.PresetNumericUpDown1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBoxMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxQ2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxQ1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxEps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_save);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.PresetNumericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BasisNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxEps;
        private System.Windows.Forms.TextBox textBoxQ1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxQ2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown PresetNumericUpDown1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxShowNetLines;
        private System.Windows.Forms.CheckBox checkBoxTriagLine;
        private System.Windows.Forms.CheckBox checkBoxShowElementBorder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown BasisNumericUpDown;
        private System.Windows.Forms.CheckBox checkBoxShowSolution;
    }
}