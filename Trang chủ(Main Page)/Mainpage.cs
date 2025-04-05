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
using Guna.UI2.WinForms;
using DTO;
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
            timer1.Interval = 1000; // 1 giây
            timer1.Tick += timer1_Tick;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                //vietnamese
                guna2ComboBox2.SelectedIndex = 0;
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                //english
                guna2ComboBox2.SelectedIndex = 1;
            }
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        // ------------------------- Phần Đăng Nhập -----------------------------
        //
        private int soLanNhap = 3; //Số lần nhập sai tối đa
        private int countdown = 30; //Thời gian chờ 30s
        public static DTO_User CurrentUser { get; set; }
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

            // Gọi BLL để lấy Mã nhân viên và Role
            string role = BLL_Account.Instance.GetRole(username, password);
            string maNV = BLL_Account.Instance.GetMaNV(username, password);

            string xoaValue = BLL_Account.Instance.CheckXoa(username, password);
            if (xoaValue != null)
            {
                MessageBox.Show("Tài khoản đã dừng hoạt động!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                // Kiểm tra nếu role hoặc mã nhân viên null thì không tiếp tục
                if (role == null || maNV == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                soLanNhap--;
                if (soLanNhap > 0)
                {
                    lbWarning.Text = "Sai mật khẩu hoặc tên tài khoản! Còn " + soLanNhap + " lần nhập.";
                }
                else
                {
                    lbWarning.Text = "Bạn đã nhập sai quá 3 lần. Vui lòng đợi 30s...";
                    guna2GradientButton2.Enabled = false;
                    StartLockTimer();
                }
                return;  // Dừng lại nếu thông tin đăng nhập không hợp lệ
            }

            // Gọi BLL để lấy thông tin nhân viên từ mã nhân viên
            DataTable dtNhanVien = BLL_Nhanvien.Instance.GetEmployeeById(maNV);
            if (dtNhanVien.Rows.Count > 0)
            {
                string hoTen = dtNhanVien.Rows[0]["Hoten"].ToString();
                string ngaySinh = DateTime.Parse((dtNhanVien.Rows[0]["Ngaysinh"]).ToString()).ToString("dd/MM/yyyy");
                string gioiTinh = dtNhanVien.Rows[0]["Gioitinh"].ToString();
                string diaChi = dtNhanVien.Rows[0]["Diachi"].ToString();
                string sdt = dtNhanVien.Rows[0]["Sodienthoai"].ToString();

                // Gán vào CurrentUser
                Mainpage.CurrentUser = new DTO_User(maNV, hoTen, ngaySinh, gioiTinh, diaChi, sdt, role);
            }

            soLanNhap = 3; // Đặt lại soLanNhap nếu đăng nhập thành công
            lbWarning.Text = ""; // Xóa cảnh báo nếu có

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

        // Hàm bắt đầu khóa đăng nhập
        private void StartLockTimer()
        {
            countdown = 30;
            timer1.Start();
        }
        //Hàm xử lý đếm ngược
        private void timer1_Tick(object sender, EventArgs e)
        {
            countdown--;
            lbWarning.Text = "Bạn đã nhập sai quá 3 lần. Vui lòng đợi " + countdown + "s...";

            if (countdown <= 0)
            {
                timer1.Stop();
                guna2GradientButton2.Enabled = true;
                soLanNhap = 3; // Reset số lần nhập sai
                lbWarning.Text = ""; // Xóa cảnh báo
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
