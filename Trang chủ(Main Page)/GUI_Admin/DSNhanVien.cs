using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
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
                else // Mặc định là tiếng Anh
                {
                    guna2DataGridView2.Columns["Manhanvien"].HeaderText = "Employee ID";
                    guna2DataGridView2.Columns["Hoten"].HeaderText = "Full Name";
                    guna2DataGridView2.Columns["CCCD"].HeaderText = "ID Card";
                    guna2DataGridView2.Columns["Ngaysinh"].HeaderText = "Date of Birth";
                    guna2DataGridView2.Columns["Gioitinh"].HeaderText = "Gender";
                    guna2DataGridView2.Columns["Diachi"].HeaderText = "Address";
                    guna2DataGridView2.Columns["Sodienthoai"].HeaderText = "Phone Number";
                    guna2DataGridView2.Columns["VaiTro"].HeaderText = "Role";
                }

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


        private DataGridViewRow selectedRow = null; // Lưu dòng đang chỉnh sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.CurrentRow == null)
            {
                string message = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Vui lòng chọn một nhân viên để sửa." : "Please select an employee to edit.";
                string title = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Thông báo" : "Notification";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isEditing)
            {
                string message = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Bạn đang chỉnh sửa một dòng. Hãy lưu hoặc hủy trước khi chọn dòng khác." : "You are editing a row. Please save or cancel before selecting another row.";
                string title = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Thông báo" : "Notification";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //isEditing = true;

            //selectedRow = guna2DataGridView2.CurrentRow; // Lưu dòng đang sửa
            //guna2DataGridView2.ReadOnly = true;

            //// Khóa tất cả các dòng trước khi mở dòng được chọn
            //foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        cell.ReadOnly = true; // Chỉ mở khóa ô của dòng đang chỉnh sửa
            //    }
            //}

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
                string vaiTro = row.Cells["Vaitro"].Value.ToString();

                bool result = BLL_Nhanvien.Instance.UpdateEmployee(maNhanVien, hoTen, cccd, ngaySinh, gioiTinh, diaChi, soDienThoai, vaiTro);

                string successMessage, errorMessage, successTitle, errorTitle;

                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    // Tiếng Việt
                    successMessage = "Cập nhật thành công!";
                    successTitle = "Thông báo";
                    errorMessage = "Cập nhật thất bại!";
                    errorTitle = "Lỗi";
                }
                else
                {
                    // Tiếng Anh
                    successMessage = "Update successful!";
                    successTitle = "Notification";
                    errorMessage = "Update failed!";
                    errorTitle = "Error";
                }

                if (result)
                {
                    MessageBox.Show(successMessage, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeList(); // Cập nhật lại danh sách
                }
                else
                {
                    MessageBox.Show(errorMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Lỗi: " + ex.Message : "Error: " + ex.Message;
                string errorTitle = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Lỗi" : "Error";
                MessageBox.Show(errorMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            isEditing = false;
            guna2DataGridView2.ReadOnly = true; // Không cho chỉnh sửa nữa
            btnLuu.Enabled = false; // Vô hiệu hóa nút Lưu
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView2.SelectedRows.Count == 0)
            {
                string message = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Vui lòng chọn một nhân viên để xóa." : "Please select an employee to delete.";
                string title = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Thông báo" : "Notification";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmMessage = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Bạn có chắc chắn muốn xóa nhân viên này?" : "Are you sure you want to delete this employee?";
            string confirmTitle = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Xác nhận" : "Confirmation";

            DialogResult dialog = MessageBox.Show(confirmMessage, confirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;

            string maNhanVien = guna2DataGridView2.SelectedRows[0].Cells["Manhanvien"].Value.ToString();

            bool result = BLL_Nhanvien.Instance.DeleteEmployee(maNhanVien);

            string successMessage = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Nhân viên đã được xóa." : "The employee has been deleted.";
            string successTitle = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Thông báo" : "Notification";
            string errorMessage = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Xóa thất bại!" : "Deletion failed!";
            string errorTitle = (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN") ? "Lỗi" : "Error";

            if (result)
            {
                MessageBox.Show(successMessage, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeeList(); // Cập nhật lại danh sách
            }
            else
            {
                MessageBox.Show(errorMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void txt_searchBar_TextChanged(object sender, EventArgs e)
        //{
        //    if (txt_searchBar.Text == null)
        //    {
        //        DSNhanVien_Load(sender, e);
        //    }
        //    else
        //    {
        //        guna2DataGridView2.DataSource = BLL_Nhanvien.Instance.timKiemNV(txt_searchBar.Text);
        //    }
        //}
    }
}
