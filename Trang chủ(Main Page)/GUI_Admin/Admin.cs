using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Guna.UI2.WinForms;

namespace Trang_chủ_Main_Page_
{
    public partial class Admin : Form
    {
        bool menuExpand_3=false;
        private bool isEditing = false; // Kiểm tra có đang sửa không

        public Admin()
        {
            InitializeComponent();
           
        }

        private void container(object _form)
        {
            if (guna2Panel3.Controls.Count > 0)
                guna2Panel3.Controls.Clear();
            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            guna2Panel3.Controls.Add(fm);
            guna2Panel3.Tag = fm;
            fm.Show();

        }



        private void guna2ComboBoxVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRole = cb_role.SelectedItem.ToString();
            string role = "";

            if (selectedRole.Contains("Quản lý Kho")) role = "Kho";
            else if (selectedRole.Contains("Quản lý Nhân Sự/Tài Chính")) role = "TCNS";


            if (!string.IsNullOrEmpty(role))
            {
                container(new FormAddQL(role));
            }
            else if (selectedRole.Contains("Nhân Viên"))
            {
                container(new FormAddNV());
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void btn_DSNhanVien_Click(object sender, EventArgs e)
        {
            menuTransition_2.Start();
            // Kiểm tra nếu form đã mở rồi thì không mở nữa
            //foreach (Form frm in Application.OpenForms)
            //{
            //    if (frm is DSNhanVien)
            //    {
            //        frm.Activate(); // Đưa form lên foreground
            //        return;
            //    }
            //}
            //DSNhanVien ds = new DSNhanVien();
            //ds.Show();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
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
        private void Admin_Load(object sender, EventArgs e)
        {
            LoadEmployeeList();
            btnLuu.Enabled = false; // Nút Lưu bị vô hiệu hóa ban đầu
        }

        private void menuTransition_2_Tick(object sender, EventArgs e)
        {
            if (menuExpand_3 == false)
            {
                guna2Panel1.Height += 25;
                if (guna2Panel1.Height >= 578)
                {
                    lbl_role.ForeColor =Color.FromArgb(255, 251, 234);
                    lbl_role_Add.ForeColor =Color.FromArgb(255, 251, 234);
                    cb_role.ForeColor =Color.FromArgb(255, 251, 234);
                    cb_role.FillColor = Color.FromArgb(255, 251, 234);
                  
                    menuTransition_2.Stop();
                    menuExpand_3 = true;
                }
            }
            else
            {
                guna2Panel1.Height -= 50;
                if (guna2Panel1.Height <= 0)
                {
                    lbl_role.ForeColor = Color.Black;
                    lbl_role_Add.ForeColor = Color.Black;
                    cb_role.ForeColor = Color.FromArgb(68, 88, 112);
                    cb_role.FillColor = Color.White;
                   
                    menuTransition_2.Stop();
                    menuExpand_3 = false;
                }
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        // Sự kiện nhấn nút Lưu
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
    }
}
