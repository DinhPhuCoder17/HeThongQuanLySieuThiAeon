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
    public partial class DSTonKho : Form
    {
        public DSTonKho()
        {
            InitializeComponent();
        }

        private void DSTonKho_Load(object sender, EventArgs e)
        {

            // Thêm 10 dòng dữ liệu vào guna2DataGridView2
            guna2DataGridView2.Rows.Add("DH001", "Sản phẩm A", "Điện tử", 5, "10%", 500000, 550000);
            guna2DataGridView2.Rows.Add("DH002", "Sản phẩm B", "Gia dụng", 2, "5%", 200000, 210000);
            guna2DataGridView2.Rows.Add("DH003", "Sản phẩm C", "Thực phẩm", 10, "5%", 100000, 120000);
            guna2DataGridView2.Rows.Add("DH004", "Sản phẩm D", "Thời trang", 3, "15%", 300000, 350000);
            guna2DataGridView2.Rows.Add("DH005", "Sản phẩm E", "Sức khỏe", 7, "20%", 150000, 180000);
            guna2DataGridView2.Rows.Add("DH006", "Sản phẩm F", "Đồ chơi", 4, "25%", 250000, 300000);
            guna2DataGridView2.Rows.Add("DH007", "Sản phẩm G", "Máy móc", 1, "8%", 800000, 900000);
            guna2DataGridView2.Rows.Add("DH008", "Sản phẩm H", "Văn phòng phẩm", 6, "12%", 120000, 140000);
            guna2DataGridView2.Rows.Add("DH009", "Sản phẩm I", "Làm đẹp", 9, "18%", 180000, 200000);
            guna2DataGridView2.Rows.Add("DH010", "Sản phẩm J", "Công nghệ", 8, "22%", 220000, 250000);




        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void guna2GradientTileButton4_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2GradientTileButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
