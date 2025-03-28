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
    public partial class ThemMatHang : Form
    {
        public ThemMatHang()
        {
            InitializeComponent();
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn ảnh";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    // Tải ảnh lên PictureBox
                    guna2PictureBox1.Image = Image.FromFile(imagePath);
                    guna2PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    guna2PictureBox1.BorderStyle = BorderStyle.FixedSingle;
                    // (Tùy chọn) Thực hiện đăng tải ảnh lên server hoặc lưu vào cơ sở dữ liệu

                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
