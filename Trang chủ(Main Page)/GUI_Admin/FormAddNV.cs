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
using BLL;
namespace Trang_chủ_Main_Page_
{
    public partial class FormAddNV : Form
    {
        
        public FormAddNV()
        {
            InitializeComponent();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void btnTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text;
            string cccd = txtCCCD.Text;
            DateTime ngaySinh = guna2DateTimePicker1.Value;
            string gioiTinh = null;
            string diaChi = txtDiaChi.Text;
            string sdt = txtSDT.Text;
            string rolenv = txtVaiTro.Text;

            //Bắt đầu check nhập dữ liệu
            if(cmbGioiTinh.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                gioiTinh = cmbGioiTinh.SelectedItem.ToString();
            }
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (!IsValidRoleNV(rolenv))
            {
                MessageBox.Show("Vai trò không khớp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Kết thúc check nhập dữ liệu
            if (BLL_Nhanvien.Instance.IsCCCDExist(cccd))
            {
                MessageBox.Show("CCCD đã tồn tại!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (BLL_Nhanvien.Instance.IsPhoneExist(sdt))
            {
                MessageBox.Show("Số điện thoại đã tồn tại!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (BLL_Nhanvien.Instance.AddEmployee(hoTen, cccd, ngaySinh, gioiTinh, diaChi, sdt, rolenv, "","",""))
            {
                MessageBox.Show("Thêm nhân viên thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm nhân viên!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Check Họ tên hợp lệ
        public static bool IsValidFullName(string fullName)
        {
            string pattern = @"^([A-ZĐ][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]+)(\s+[A-ZĐ][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]+){0,49}$";
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
            string pattern = @"^0\d{9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        //Check địa chỉ hợp lệ
        public static bool IsValidAddress(string address)
        {
            string pattern = @"^[a-zA-ZĐđàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹ0-9,./\- ]{5,500}$";
            return Regex.IsMatch(address, pattern);
        }

        //Check vai trò
        public static bool IsValidRoleNV(string address)
        {
            string pattern = @"^[a-zA-ZĐđàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹ0-9,.\- ]{5,500}$";
            return Regex.IsMatch(address, pattern);
        }
    }
}
