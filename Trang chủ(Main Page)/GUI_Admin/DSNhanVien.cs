using BLL;
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
    public partial class DSNhanVien : Form
    {
        private bool isEditing = false; // Kiểm tra có đang sửa không

        public DSNhanVien()
        {
            InitializeComponent();
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientTileButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void LoadEmployeeList()
        {
            guna2DataGridView2.DataSource = BLL_Nhanvien.Instance.GetAllEmployees();
            if (guna2DataGridView2.DataSource != null)
            {
                guna2DataGridView2.Columns["Manhanvien"].HeaderText = "Mã nhân viên";
                guna2DataGridView2.Columns["Hoten"].HeaderText = "Họ Tên";
                guna2DataGridView2.Columns["CCCD"].HeaderText = "CCCD";
                guna2DataGridView2.Columns["Ngaysinh"].HeaderText = "Ngày sinh";
                guna2DataGridView2.Columns["Gioitinh"].HeaderText = "Giới Tính";
                guna2DataGridView2.Columns["Diachi"].HeaderText = "Địa Chỉ";
                guna2DataGridView2.Columns["Sodienthoai"].HeaderText = "Số Điện Thoại";
                guna2DataGridView2.Columns["VaiTro"].HeaderText = "Vai Trò";
            }
            guna2DataGridView2.ReadOnly = true; // Ban đầu không cho chỉnh sửa

        }


        private void DSNhanVien_Load(object sender, EventArgs e)
        {

            LoadEmployeeList();
            btnLuu.Enabled = false; // Nút Lưu bị vô hiệu hóa ban đầu

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isEditing = true;
            guna2DataGridView2.ReadOnly = false; // Cho phép chỉnh sửa
            btnLuu.Enabled = true; // Kích hoạt nút Lưu
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!isEditing) return;
            try
            {
                DataGridViewRow row = guna2DataGridView2.SelectedRows[0];
                string maNhanVien = row.Cells["Manhanvien"].Value.ToString();
                string hoTen = row.Cells["Hoten"].Value.ToString();
                string cccd = row.Cells["CCCD"].Value.ToString();
                DateTime ngaySinh = Convert.ToDateTime(row.Cells["Ngaysinh"].Value);
                string gioiTinh = row.Cells["Gioitinh"].Value.ToString();
                string diaChi = row.Cells["Diachi"].Value.ToString();
                string soDienThoai = row.Cells["Sodienthoai"].Value.ToString();

                bool result = BLL_Nhanvien.Instance.UpdateEmployee(maNhanVien, hoTen, cccd, ngaySinh, gioiTinh, diaChi, soDienThoai);

                if (result)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeList(); // Cập nhật lại danh sách
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            isEditing = false;
            guna2DataGridView2.ReadOnly = true; // Không cho chỉnh sửa nữa
            btnLuu.Enabled = false; // Vô hiệu hóa nút Lưu
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;

            string maNhanVien = guna2DataGridView2.SelectedRows[0].Cells["Manhanvien"].Value.ToString();

            bool result = BLL_Nhanvien.Instance.DeleteEmployee(maNhanVien);

            if (result)
            {
                MessageBox.Show("Nhân viên đã được xóa (ẩn).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeeList(); // Cập nhật lại danh sách
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
