using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace Trang_chủ_Main_Page_
{
    public partial class Mainpage : Form
    {
        public static int pageSelection = 1;

        BLLQuanLyKho bll_QuanLyKho = new BLLQuanLyKho();
        public Mainpage()
        {
            InitializeComponent();
            bll_QuanLyKho.AutoUpdateTrangThaiNhapHang();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string username = guna2TextBox1.Text;
            string password = guna2TextBox2.Text;


            // Kiểm tra thông tin hợp lệ
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Gọi BLL để lấy role
            string role = BLL_Account.Instance.GetRole(username, password);

            if (role != null)
            {
                // Gán giá trị cho pageSelection dựa vào role lấy từ database
                if (role == "Admin")
                    Mainpage.pageSelection = 3;
                else if (role == "Kho")
                    Mainpage.pageSelection = 2;
                else if (role == "TCNS")
                    Mainpage.pageSelection = 1;

                // Hiển thị form loading
                loading load = new loading();
                load.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lblTk_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                guna2TextBox2.UseSystemPasswordChar = false; // Hiện mật khẩu
            }
            else
            {
                guna2TextBox2.UseSystemPasswordChar = true; // Ẩn mật khẩu
            }
        }

        // (Code vá của Liemp)
        //private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    if (int.TryParse(guna2TextBox1.Text, out int value))
        //    {
        //        pageSelection = value; // Lưu giá trị từ TextBox
        //    }
        //}

    }
}
