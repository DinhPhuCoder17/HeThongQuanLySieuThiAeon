using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trang_chu_Main_Page_.DSHangHoa_Dat;

namespace Trang_chủ_Main_Page_
{
    public partial class GoiYNhapHangBangAI : Form
    {
        private bool isEdited = false;             
        private DataGridViewRow rowEdited = null;    
        private object[] originalRowValues = null;    
        BLL_predictor bLL_Predictor = new BLL_predictor();
        BindingSource bindingSource = new BindingSource();

        public GoiYNhapHangBangAI()
        {
            List<DTO_Hanghoa> dsHH = BLLQuanLyKho.Instance.XemDSTonKho();
            InitializeComponent();

            cmbTGTTHH.Items.Clear();
            cmbTGTTHH.Items.Add("1 tuần");
            cmbTGTTHH.Items.Add("1 tháng");
            cmbTGTTHH.SelectedIndex = 0;
            cmbTGTTHH.SelectedIndexChanged += cmbTGTTHH_SelectedIndexChanged;

            BLL_predictor.Predict(dgvGoiYNhapHang, "week");


            var unique = dsHH.GroupBy(dd => dd.DanhMuc)
                     .Select(g => g.First())
                     .ToList();
            var tatCa = new DTO_Hanghoa
            {
                DanhMuc = "Tất cả"
            };

            unique.Insert(0, tatCa);

            cmbLocTheoDanhMuc_GoiYNhapHang.DataSource = unique;
            cmbLocTheoDanhMuc_GoiYNhapHang.DisplayMember = "DanhMuc";
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
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("The 'Quantity Ordered' column cannot be left blank!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Cột Số lượng đặt không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }
            if (!int.TryParse(newValue, out int soLuong))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("The 'Quantity Ordered' column must only contain integers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Cột Số lượng đặt chỉ được nhập số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

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

            if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                MessageBox.Show("Changes saved successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                MessageBox.Show("Lưu thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please select the row to delete!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void txt_AI_SearchBar_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtAI.Text.Trim().ToLower();

            foreach (DataGridViewRow row in dgvGoiYNhapHang.Rows)
            {
                if (row.IsNewRow) continue; // Bỏ qua hàng trống cuối cùng

                bool match = false;
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    object cellValue = row.Cells[i].Value;
                    if (cellValue != null && cellValue.ToString().ToLower().Contains(keyword))
                    {
                        match = true;
                        break;
                    }
                }
                row.Visible = match;
            }
        }



        private void cmbSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = cmbLocTheoDanhMuc_GoiYNhapHang.Text.ToLower();

            foreach (DataGridViewRow row in dgvGoiYNhapHang.Rows)
            {
                if (row.IsNewRow) continue; // Bỏ qua hàng trống cuối cùng

                string danhMuc = row.Cells["DanhMuc"].Value?.ToString().ToLower() ?? "";
                row.Visible = (selectedCategory == "tất cả" || danhMuc == selectedCategory);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
