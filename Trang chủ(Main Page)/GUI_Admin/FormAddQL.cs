using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (!IsValidUsername(userName))
            {
                MessageBox.Show("Tên tài khoản không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidPassword(password))
            {
                MessageBox.Show("Mật khẩu không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidFullName(hoTen))
            {
                MessageBox.Show("Họ tên không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (!IsValidCCCD(cccd))
            {
                MessageBox.Show("CCCD không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (!IsValidBirthDate(ngaySinh))
            {
                MessageBox.Show("Tuổi không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (!IsValidAddress(diaChi))
            {
                MessageBox.Show("Địa chỉ không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (!IsValidPhoneNumber(sdt))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (BLL_Nhanvien.Instance.AddEmployee(hoTen, cccd, ngaySinh, gioiTinh, diaChi, sdt, role, userName, password, role))
            {
                MessageBox.Show("Thêm nhân viên thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm nhân viên!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Check Username hợp lệ
        public static bool IsValidUsername(string username)
        {
            string pattern = "^[a-zA-Z0-9]{6,20}$";
            return Regex.IsMatch(username, pattern);
        }
        //Check Password hợp lệ
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, pattern);
        }
        //Check Họ tên hợp lệ
        public static bool IsValidFullName(string fullName)
        {
            string pattern = @"^([A-ZĐ][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]+)(\s[A-ZĐ][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]+){1,49}$";
            return Regex.IsMatch(fullName, pattern);
        }
        //Check tuổi
        public static bool IsValidBirthDate(DateTime birthDate)
        {
            int year = birthDate.Year;
            int currentYear = DateTime.Now.Year;
            int age = currentYear - year;
            return year >= 1900 && year <= currentYear && age >= 18;
        }
        //Check CCCD hợp lệ
        public static bool IsValidCCCD(string cccd)
        {
            string pattern = @"^\d{12}$";
            return Regex.IsMatch(cccd, pattern);
        }
        //Check SĐT hợp lệ
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        //Check địa chỉ hợp lệ
        public static bool IsValidAddress(string address)
        {
            string pattern = @"^[a-zA-ZĐđàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹ0-9,.\- ]{5,500}$";
            return Regex.IsMatch(address, pattern);
        }
    }
}
