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
    public partial class DSNhanVien : Form
    {
        public DSNhanVien()
        {
            InitializeComponent();
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientTileButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DSNhanVien_Load(object sender, EventArgs e)
        {
            // Đảm bảo DataGridView có ít nhất 15 dòng
            while (guna2DataGridView2.Rows.Count < 15)
            {
                guna2DataGridView2.Rows.Add();
            }

            // Dữ liệu mẫu (15 dòng, 9 cột)
            string[,] data = new string[,]
            {
        { "1", "Nguyễn Văn B", "01-01-1990", "Nam", "Hà Nội", "001200012345", "nguyenvana@gmail.com", "0987654321", "Quản lý kho" },
        { "2", "Trần Thị B", "05-06-1992", "Nữ", "TP HCM", "001200045678", "tranthib@gmail.com", "0976543210", "Nhân viên" },
        { "3", "Lê Hoàng C", "15-09-1988", "Nam", "Đà Nẵng", "001200078912", "lehoangc@gmail.com", "0965432109", "Nhân viên" },
        { "4", "Phạm Thu D", "20-12-1995", "Nữ", "Hải Phòng", "001200034567", "phamthud@gmail.com", "0954321098", "Nhân viên" },
        { "5", "Hoàng Minh E", "02-03-1985", "Nam", "Cần Thơ", "001200056789", "hoangminhe@gmail.com", "0943210987", "Quản lý kho" },
        { "6", "Đặng Kim F", "18-07-1993", "Nữ", "Bình Dương", "001200067891", "dangkf@gmail.com", "0932109876", "Nhân viên" },
        { "7", "Võ Văn G", "30-10-1990", "Nam", "Nha Trang", "001200078912", "vovang@gmail.com", "0921098765", "Nhân viên" },
        { "8", "Bùi Hồng H", "12-04-1997", "Nữ", "Huế", "001200089123", "buihongh@gmail.com", "0910987654", "Nhân viên" },
        { "9", "Ngô Thanh I", "22-08-1986", "Nam", "Quảng Ninh", "001200091234", "ngothanhi@gmail.com", "0909876543", "Quản lý kho" },
        { "10", "Đinh Lan J", "07-11-1994", "Nữ", "Vũng Tàu", "001200012345", "dinhlanj@gmail.com", "0898765432", "Nhân viên" },
        { "11", "Nguyễn Thị K", "10-05-1991", "Nữ", "Hà Nội", "001200123456", "nguyenthik@gmail.com", "0887654321", "Nhân viên" },
        { "12", "Trương Văn L", "25-07-1987", "Nam", "Đồng Nai", "001200234567", "truongvanl@gmail.com", "0876543210", "Nhân viên" },
        { "13", "Lâm Thảo M", "13-03-1992", "Nữ", "An Giang", "001200345678", "lamthaom@gmail.com", "0865432109", "Nhân viên" },
        { "14", "Hồ Bảo N", "17-09-1984", "Nam", "Quảng Trị", "001200456789", "hobaon@gmail.com", "0854321098", "Nhân viên" },
        { "15", "Tạ Minh O", "09-11-1996", "Nam", "Bình Thuận", "001200567890", "taminho@gmail.com", "0843210987", "Quản lý kho" }
            };

            // Đổ dữ liệu vào DataGridView
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < guna2DataGridView2.Columns.Count; j++) // Số cột phù hợp với số cột của DataGridView
                {
                    if (i < data.GetLength(0) && j < data.GetLength(1)) // Kiểm tra giới hạn của mảng
                    {
                        guna2DataGridView2.Rows[i].Cells[j].Value = data[i, j];
                    }
                }
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {

        }
    }
}
