using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chu_Main_Page_.DSHangHoa_Dat
{
    public partial class HangHoaNCC : UserControl
    {
        public int SoLuong
        {
            get
            {
                int result;
                if (int.TryParse(txtSoLuong.Text, out result) && result > 0)
                    return result;
                else
                    return 1;
            }
        }

        public event EventHandler OnSelect = null;
        private double cost;
        public HangHoaNCC()
        {
            InitializeComponent();
            this.imgImage.Paint += new System.Windows.Forms.PaintEventHandler(this.imgImage_Paint);

        }

        public string Title //label1
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public double Cost //label4
        {
            get => cost;
            set
            {
                cost = value;
                label4.Text = cost.ToString("C2");
            }
        }

        public string Supplier //label3
        {
            get => label3.Text;
            set => label3.Text = value;
        }
        public Image Icon
        {
            get => imgImage.Image;
            set => imgImage.Image = value;
        }


        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            OnSelect?.Invoke(this, e);
        }
        private void imgImage_Paint(object sender, PaintEventArgs e)
        {
            int radius = 15; // Bán kính bo tròn cho 4 góc là 5
            GraphicsPath gp = new GraphicsPath();

            // Góc trên trái
            gp.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            // Góc trên phải
            gp.AddArc(imgImage.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            // Góc dưới phải
            gp.AddArc(imgImage.Width - radius * 2, imgImage.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            // Góc dưới trái
            gp.AddArc(0, imgImage.Height - radius * 2, radius * 2, radius * 2, 90, 90);

            gp.CloseFigure(); // Đóng đường dẫn thành một hình liên tục
            imgImage.Region = new Region(gp); // Áp dụng vùng bo tròn cho PictureBox
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
