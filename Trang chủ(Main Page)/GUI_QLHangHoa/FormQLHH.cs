using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trang_chu_Main_Page_.GUI_QLHangHoa;

namespace Trang_chủ_Main_Page_
{
  
    public partial class Form1 : Form
    {
        private Guna.UI2.WinForms.Guna2GradientTileButton selectedButton = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnDSTonKho.PerformClick();
        }
       
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {

        }


        private void guna2GradientTileButton2_Click(object sender, EventArgs e)
        {

        }
        private void ButtonNhapHang_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2GradientTileButton clickedButton = sender as Guna.UI2.WinForms.Guna2GradientTileButton;
            if (clickedButton == null) return;
            if (selectedButton != null && selectedButton != clickedButton)
            {
                selectedButton.FillColor = Color.White;
                selectedButton.FillColor2 = Color.White;
            }
            clickedButton.FillColor = Color.FromArgb(255, 128, 0);
            clickedButton.FillColor2 = Color.FromArgb(255, 128, 0);
            selectedButton = clickedButton;

            //container(new NhapHang());
        }

        private void ButtonAdmin_Click(object  sender, EventArgs e)
        {
            container(new Admin());
        }

     


        private void ButtonDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Bạn có chắc chắn muốn đăng xuất?", 
            "Xác nhận đăng xuất",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Question 
            );

            if(result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnDSTonKho_Click(object sender, EventArgs e)
        {
            container(new DSTonKho());
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
        private void btnDatHang_Click(object sender, EventArgs e)
        {
            container(new DatHang());
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            container(new NhapHang());
            BLLQuanLyKho bll_QuanLyKho = new BLLQuanLyKho();
            bll_QuanLyKho.AutoUpdateTrangThaiNhapHang();
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            container(new QuanLyNhaCungCap());
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            container(new QuanLyNhaCungCap());
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            KhieuNai kN = new KhieuNai();
            kN.Show();
        }
    }
}