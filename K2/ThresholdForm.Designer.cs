namespace K2
{
    partial class ThresholdForm
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
            this.components = new System.ComponentModel.Container();
            this.dgvThresholds = new System.Windows.Forms.DataGridView();
            this.btnApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.thresholdFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colorThresholdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThresholds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdFormBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorThresholdBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvThresholds
            // 
            this.dgvThresholds.AllowUserToAddRows = false;
            this.dgvThresholds.AllowUserToDeleteRows = false;
            this.dgvThresholds.AllowUserToResizeColumns = false;
            this.dgvThresholds.AllowUserToResizeRows = false;
            this.dgvThresholds.AutoGenerateColumns = false;
            this.dgvThresholds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThresholds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.valueDataGridViewTextBoxColumn,
            this.colorDataGridViewTextBoxColumn});
            this.dgvThresholds.DataSource = this.colorThresholdBindingSource;
            this.dgvThresholds.Location = new System.Drawing.Point(42, 12);
            this.dgvThresholds.Name = "dgvThresholds";
            this.dgvThresholds.RowHeadersWidth = 51;
            this.dgvThresholds.RowTemplate.Height = 24;
            this.dgvThresholds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThresholds.Size = new System.Drawing.Size(305, 204);
            this.dgvThresholds.TabIndex = 0;
            this.dgvThresholds.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThresholds_CellDoubleClick);
            this.dgvThresholds.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThresholds_CellEndEdit);
            this.dgvThresholds.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvThresholds_CellFormatting);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(42, 393);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(133, 46);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Принять";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(226, 393);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(133, 46);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(63, 295);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(112, 32);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(226, 295);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(112, 32);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Удалить";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // thresholdFormBindingSource
            // 
            this.thresholdFormBindingSource.DataSource = typeof(K2.ThresholdForm);
            // 
            // colorThresholdBindingSource
            // 
            this.colorThresholdBindingSource.DataSource = typeof(K2.ColorThreshold);
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Значение";
            this.valueDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Width = 125;
            // 
            // colorDataGridViewTextBoxColumn
            // 
            this.colorDataGridViewTextBoxColumn.DataPropertyName = "Color";
            this.colorDataGridViewTextBoxColumn.HeaderText = "Цвет";
            this.colorDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.colorDataGridViewTextBoxColumn.Name = "colorDataGridViewTextBoxColumn";
            this.colorDataGridViewTextBoxColumn.Width = 125;
            // 
            // ThresholdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 451);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.dgvThresholds);
            this.Name = "ThresholdForm";
            this.Text = "ThresholdForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvThresholds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdFormBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorThresholdBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvThresholds;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colorDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource colorThresholdBindingSource;
        private System.Windows.Forms.BindingSource thresholdFormBindingSource;
    }
}