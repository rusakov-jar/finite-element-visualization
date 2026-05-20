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
            ((System.ComponentModel.ISupportInitialize)(this.PresetNumericUpDown1)).BeginInit();
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
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "eps";
            // 
            // textBoxEps
            // 
            this.textBoxEps.Location = new System.Drawing.Point(106, 13);
            this.textBoxEps.Name = "textBoxEps";
            this.textBoxEps.Size = new System.Drawing.Size(100, 22);
            this.textBoxEps.TabIndex = 3;
            // 
            // textBoxQ1
            // 
            this.textBoxQ1.Location = new System.Drawing.Point(106, 46);
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
            this.textBoxQ2.Location = new System.Drawing.Point(106, 83);
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
            this.listBoxMode.Location = new System.Drawing.Point(106, 116);
            this.listBoxMode.Name = "listBoxMode";
            this.listBoxMode.Size = new System.Drawing.Size(100, 36);
            this.listBoxMode.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "units";
            // 
            // comboBox1
            // 
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "м",
            "см",
            "мм"});
            this.comboBox1.Location = new System.Drawing.Point(106, 180);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 13;
            // 
            // PresetNumericUpDown1
            // 
            this.PresetNumericUpDown1.Location = new System.Drawing.Point(106, 221);
            this.PresetNumericUpDown1.Name = "PresetNumericUpDown1";
            this.PresetNumericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.PresetNumericUpDown1.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Preset";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 651);
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
    }
}