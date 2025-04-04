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

namespace Trang_chu_Main_Page_.GUI_Admin
{
    public partial class DynamicCreateNhanVien : Form
    {
        public bool isFillingIn = true;

        public String userName;
        public String passWord;
        public String rePassword;
        public String role;
        public DynamicCreateNhanVien()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            isFillingIn = false;
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            userName = txtUsername.Text;
            passWord = txtPassword.Text;
            rePassword = txtRePassword.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord) || string.IsNullOrEmpty(rePassword) || cbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!IsValidUsername(userName))
            {
                if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Tên tài khoản không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Username is not valid!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!IsValidPassword(passWord))
            {
                if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Mật khẩu không hợp lệ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Password is not valid!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (rePassword != passWord) 
            {
                if(Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Nhập lại mật khẩu không đúng!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Re-enter password is incorrect!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (cbRole.Text != null)
            {
                role = cbRole.Text;
            }
            this.Close();
        }
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
    }
}
