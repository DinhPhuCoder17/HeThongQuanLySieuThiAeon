using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chủ_Main_Page_
{
    public partial class GoiYNhapHangBangAI : Form
    {
        BLLQuanLyKho bll = new BLLQuanLyKho();
        private bool isEdited = false;             
        private DataGridViewRow rowEdited = null;    
        private object[] originalRowValues = null;     


        public GoiYNhapHangBangAI()
        {
            InitializeComponent();

            cmbTGTTHH.Items.Clear();
            cmbTGTTHH.Items.Add("1 tuần");
            cmbTGTTHH.Items.Add("1 tháng");
            cmbTGTTHH.SelectedIndex = 0;
            cmbTGTTHH.SelectedIndexChanged += cmbTGTTHH_SelectedIndexChanged;

            BLL_predictor.Predict(dgvGoiYNhapHang, "week");
        }

        private void cmbTGTTHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbTGTTHH.SelectedItem.ToString();

            if (selected == "1 tuần")
            {
                BLL_predictor.Predict(dgvGoiYNhapHang, "week");
            }
            else if (selected == "1 tháng")
            {
                  BLL_predictor.Predict(dgvGoiYNhapHang, "month");
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GoiYNhapHangBangAI_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            DatHang datHangForm = Application.OpenForms.OfType<DatHang>().FirstOrDefault();

            if (datHangForm != null)
            {
                foreach (DataGridViewRow row in dgvGoiYNhapHang.Rows)
                {
                    string maHH = row.Cells["Column9"].Value.ToString();
                    string tenHang = row.Cells["Column11"].Value.ToString();
                    string tenNCC = row.Cells["Column5"].Value.ToString();
                    int soLuong = Convert.ToInt32(row.Cells["Column12"].Value);
                    double giaGoc = Convert.ToDouble(row.Cells["Column13"].Value);

                    datHangForm.AddItem_Dgv(maHH, tenHang, tenNCC, soLuong, giaGoc);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu.");
            }

            this.Close();
        }

        private void dgvGoiYNhapHang_SelectionChanged(object sender, EventArgs e)
        {
            if (isEdited && rowEdited != null)
            {
                foreach (DataGridViewRow row in dgvGoiYNhapHang.Rows)
                {
                    if (row != rowEdited && row.Selected)
                    {
                        row.Selected = false;
                    }
                }
            }
            else
            {
                if (dgvGoiYNhapHang.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvGoiYNhapHang.SelectedRows[0];
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!isEdited)
            {
                if (dgvGoiYNhapHang.SelectedRows.Count > 0)
                {
                    MessageBox.Show("Bạn chỉ được sửa số lượng hàng hóa",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    isEdited = true;
                    dgvGoiYNhapHang.ReadOnly = false;

                    foreach (DataGridViewColumn col in dgvGoiYNhapHang.Columns)
                    {
                        if (col.Name == "Column12")
                            col.ReadOnly = false;
                        else
                            col.ReadOnly = true;
                    }
                    rowEdited = dgvGoiYNhapHang.SelectedRows[0];
                    originalRowValues = new object[rowEdited.Cells.Count];
                    for (int i = 0; i < rowEdited.Cells.Count; i++)
                    {
                        originalRowValues[i] = rowEdited.Cells[i].Value;
                    }
                    rowEdited.DefaultCellStyle.BackColor = Color.DarkGray;
                    rowEdited.DefaultCellStyle.SelectionBackColor = Color.Gray;
                    btnXoa.Enabled = false;
                    dgvGoiYNhapHang.SelectionChanged -= dgvGoiYNhapHang_SelectionChanged;
                    btnSua.Text = "Hủy";
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dòng cần sửa!",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            else
            {
                isEdited = false;
                dgvGoiYNhapHang.ReadOnly = true;
                foreach (DataGridViewColumn col in dgvGoiYNhapHang.Columns)
                {
                    col.ReadOnly = true;
                }
                btnXoa.Enabled = true;
                dgvGoiYNhapHang.SelectionChanged += dgvGoiYNhapHang_SelectionChanged;

                if (originalRowValues != null)
                {
                    for (int i = 0; i < rowEdited.Cells.Count; i++)
                    {
                        rowEdited.Cells[i].Value = originalRowValues[i];
                    }
                }


                rowEdited.DefaultCellStyle.BackColor = Color.White;
                rowEdited.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 69, 0);

                btnSua.Text = "Sửa";
            }
        }




        private void btnLuu_Click(object sender, EventArgs e)
        {
            dgvGoiYNhapHang.EndEdit();

            string newValue = rowEdited.Cells["Column12"].Value?.ToString().Trim();
            if (string.IsNullOrEmpty(newValue))
            {
                MessageBox.Show("Cột Số lượng đặt không được để trống!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(newValue, out int soLuong))
            {
                MessageBox.Show("Cột Số lượng đặt chỉ được nhập số nguyên!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            dgvGoiYNhapHang.ReadOnly = true;
            foreach (DataGridViewColumn col in dgvGoiYNhapHang.Columns)
            {
                col.ReadOnly = true;
            }
            btnXoa.Enabled = true;
            dgvGoiYNhapHang.SelectionChanged += dgvGoiYNhapHang_SelectionChanged;

            rowEdited.DefaultCellStyle.BackColor = Color.White;
            rowEdited.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 69, 0);

            btnSua.Text = "Sửa";
            isEdited = false;

            MessageBox.Show("Lưu thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }





        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvGoiYNhapHang.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvGoiYNhapHang.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dgvGoiYNhapHang.Rows.Remove(row);
                    }
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
