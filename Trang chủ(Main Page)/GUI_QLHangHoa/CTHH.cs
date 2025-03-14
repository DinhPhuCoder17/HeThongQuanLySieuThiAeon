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
    public partial class CTHH : Form
    {
        public CTHH()
        {
            InitializeComponent();
        }

        private void CTHH_Load(object sender, EventArgs e)
        {
            // Xóa dữ liệu cũ (nếu cần)
            dgvCTHH.Rows.Clear();

            // Thêm 10 dòng dữ liệu mẫu
            for (int i = 1; i <= 10; i++)
            {
                dgvCTHH.Rows.Add(
                    DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy"),  // Ngày Sản Xuất (giả lập ngày trước đó)
                    10 + i,                                          // Số Lượng
                    "Nhà Cung Cấp " + i,                            // Nhà Cung Cấp
                    DateTime.Now.AddMonths(6).ToString("dd/MM/yyyy"), // Hạn Sử Dụng (6 tháng sau)
                    "Nhà Cung Cấp " + i                             // Nhà Cung Cấp (trùng với cột trước)
                );
            }


        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
