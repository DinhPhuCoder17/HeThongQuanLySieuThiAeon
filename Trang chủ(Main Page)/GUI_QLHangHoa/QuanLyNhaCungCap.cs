using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chu_Main_Page_.GUI_QLHangHoa
{
    public partial class QuanLyNhaCungCap : Form
    {
        bool pn_supplier_Add_Expand=false;
        public QuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void QuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            // Đảm bảo có đủ dòng trong DataGridView
            while (dgvNhaCungCap.Rows.Count < 10)
            {
                dgvNhaCungCap.Rows.Add();
            }

            // Dữ liệu mẫu cho 10 dòng
            string[,] data = new string[,]
            {
    { "NCC001", "Công ty ABC", "Hà Nội", "123456789", "0987654321" },
    { "NCC002", "Công ty XYZ", "TP HCM", "987654321", "0976543210" },
    { "NCC003", "Công ty DEF", "Đà Nẵng", "564738291", "0965432109" },
    { "NCC004", "Công ty GHI", "Hải Phòng", "192837465", "0954321098" },
    { "NCC005", "Công ty JKL", "Cần Thơ", "827364551", "0943210987" },
    { "NCC006", "Công ty MNO", "Bình Dương", "918273645", "0932109876" },
    { "NCC007", "Công ty PQR", "Nha Trang", "364527189", "0921098765" },
    { "NCC008", "Công ty STU", "Huế", "736452189", "0910987654" },
    { "NCC009", "Công ty VWX", "Quảng Ninh", "918273645", "0909876543" },
    { "NCC010", "Công ty YZ", "Vũng Tàu", "827364519", "0898765432" }
            };

            // Gán dữ liệu vào từng dòng của DataGridView
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++) // 5 cột
                {
                    dgvNhaCungCap.Rows[i].Cells[j].Value = data[i, j];
                }
            }

        }

        private void t_Supplier_Add_Tick(object sender, EventArgs e)
        {
            if (pn_supplier_Add_Expand == false)
            {
                pn_Supplier_Add.Height += 20;
                if (pn_Supplier_Add.Height >= 300)
                {
                    t_Supplier_Add.Stop();
                    pn_supplier_Add_Expand = true;
                }
            }
            else
            {
                pn_Supplier_Add.Height -= 20;
                if (pn_Supplier_Add.Height <= 0)
                {
                    t_Supplier_Add.Stop();
                    pn_supplier_Add_Expand = false;
                }
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            t_Supplier_Add.Stop();
        }
    }
}
