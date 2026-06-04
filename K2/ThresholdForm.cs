using K2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K2
{
    public partial class ThresholdForm : Form
    {
        public List<ColorThreshold> Thresholds { get; private set; }

        private List<ColorThreshold> workingCopy;

        public ThresholdForm(List<ColorThreshold> thresholds)
        {
            InitializeComponent();

            // Копия для редактирования
            workingCopy = thresholds.Select(t => new ColorThreshold
            {
                Value = t.Value,
                Color = t.Color
            }).ToList();
            dgvThresholds.DataSource = null;
            dgvThresholds.DataSource = workingCopy;
        }

        private void dgvThresholds_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var item = workingCopy[e.RowIndex];
                e.Value = "■";
                e.CellStyle.ForeColor = item.Color;
                e.CellStyle.SelectionForeColor = item.Color;
            }
        }

        private void dgvThresholds_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                using (ColorDialog dlg = new ColorDialog())
                {
                    dlg.Color = workingCopy[e.RowIndex].Color;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        workingCopy[e.RowIndex].Color = dlg.Color;
                        dgvThresholds.Refresh();
                    }
                }
            }
        }

        private void dgvThresholds_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                float newValue;
                if (float.TryParse(dgvThresholds.Rows[e.RowIndex].Cells[0].Value?.ToString(), out newValue))
                {
                    workingCopy[e.RowIndex].Value = newValue;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            workingCopy.Add(new ColorThreshold { Value = 50, Color = System.Drawing.Color.Gray });
            dgvThresholds.DataSource = null;
            dgvThresholds.DataSource = workingCopy;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (dgvThresholds.SelectedRows.Count > 0)
            {
                int idx = dgvThresholds.SelectedRows[0].Index;
                workingCopy.RemoveAt(idx);
                dgvThresholds.DataSource = null;
                dgvThresholds.DataSource = workingCopy;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Thresholds = workingCopy.OrderBy(t => t.Value).ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
