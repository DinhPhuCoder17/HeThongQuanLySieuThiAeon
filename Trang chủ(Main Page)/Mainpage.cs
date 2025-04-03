using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Trang_chu_Main_Page_.Properties;
namespace Trang_chủ_Main_Page_
{
    public partial class Mainpage : Form
    {
        public static int pageSelection = 1;
        public Mainpage()
        {
            //string password = "123";
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            //System.Diagnostics.Debug.WriteLine("Hashed Password: " + hashedPassword);
            string savedLanguage = Trang_chu_Main_Page_.Properties.Settings.Default.UserLanguage;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(savedLanguage);
            InitializeComponent();
            LoadUserLanguage();
            BLLQuanLyKho.Instance.AutoUpdateTrangThaiNhapHang();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }
        private int soLanNhap = 3; //Số lần nhập sai tối đa
        private int countdown = 30; //Thời gian chờ 30s

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
                soLanNhap = 3; //Đặt lại soLanNhap nếu đăng nhập thành công
                lblWarning.Text = ""; //Xóa cảnh báo nếu có

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
                soLanNhap--;
                if(soLanNhap > 0)
                {
                    lblWarning.Text = "Sai mật khẩu hoặc tên tài khoản! Còn " + soLanNhap + " lần nhập.";
                }
                else
                {
                    lblWarning.Text = "Bạn đã nhập sai quá 3 lần. Vui lòng đợi 30s...";
                    guna2GradientButton2.Enabled = false;
                    StartLockTimer();
                }
            }
        }
        // Hàm bắt đầu khóa đăng nhập
        private void StartLockTimer()
        {
            countdown = 30;
            timer1.Interval = 1000; // 1 giây
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        //Hàm xử lý đếm ngược
        private void timer1_Tick(object sender, EventArgs e)
        {
            countdown--;
            lblWarning.Text = "Bạn đã nhập sai quá 3 lần. Vui lòng đợi " + countdown + "s...";

            if (countdown <= 0)
            {
                timer1.Stop();
                guna2GradientButton2.Enabled = true;
                lblWarning.Text = ""; // Xóa cảnh báo
                soLanNhap = 3; // Reset số lần nhập sai
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

        private void guna2CirclePictureBox1_Click_1(object sender, EventArgs e)
        {

        }


        // (Code vá của Liemp)
        //private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        //{
        //    if (int.TryParse(guna2TextBox1.Text, out int value))
        //    {
        //        pageSelection = value; // Lưu giá trị từ TextBox
        //    }
        //}
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = "vi-VN"; // Mặc định là Tiếng Việt

            switch (guna2ComboBox2.SelectedIndex)
            {
                case 0:
                    selectedLanguage = "vi-VN"; // Tiếng Việt
                    break;
                case 1:
                    selectedLanguage = "en-US"; // Tiếng Anh
                    break;
            }

            // Lưu lại ngôn ngữ vào Settings
            Trang_chu_Main_Page_.Properties.Settings.Default.UserLanguage = selectedLanguage;
            Trang_chu_Main_Page_.Properties.Settings.Default.Save();

            // Đổi ngôn ngữ ngay lập tức
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);

            // Cập nhật lại Form
            this.Controls.Clear();
            InitializeComponent();
        }
        private void LoadUserLanguage()
        {
            string savedLanguage = Trang_chu_Main_Page_.Properties.Settings.Default.UserLanguage;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(savedLanguage);

            // Đặt lại comboBox theo ngôn ngữ đã lưu
            if (savedLanguage == "vi-VN")
                guna2ComboBox2.SelectedIndex = 0;
            else if (savedLanguage == "en-US")
                guna2ComboBox2.SelectedIndex = 1;
        }



    }
}
