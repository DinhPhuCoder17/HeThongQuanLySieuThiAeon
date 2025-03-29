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
    public partial class FormAddQL : Form
    {
        private string role;
        public FormAddQL(string selectedRole)
        {
            InitializeComponent();
            this.role = selectedRole;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            string userName = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;
            string hoTen = txtHoTen.Text;
            string cccd = txtCCCD.Text;
            DateTime ngaySinh = guna2DateTimePicker1.Value;
            string gioiTinh = cmbGioiTinh.SelectedItem.ToString();
            string diaChi = txtDiaChi.Text;
            string sdt = txtSDT.Text;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) ||
    cmbGioiTinh.SelectedIndex == -1 || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BLL_Nhanvien.Instance.AddEmployee(hoTen, cccd, ngaySinh, gioiTinh, diaChi, sdt, userName, password, role))
            {
                MessageBox.Show("Thêm nhân viên thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm nhân viên!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
