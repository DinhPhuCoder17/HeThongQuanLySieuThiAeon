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
    public partial class financialManagement : Form
    {
        public financialManagement()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void financialManagement_Load(object sender, EventArgs e)
        {
            // Đảm bảo có đủ dòng
            while (dtg_Bill.Rows.Count < 10)
            {
                dtg_Bill.Rows.Add();
            }

            // Dữ liệu mẫu
            string[,] data = new string[,]
            {
    { "HD001", "10-03-2025 08:30", "NV01", "Mua 2 sữa Vinamilk" },
    { "HD002", "10-03-2025 09:00", "NV02", "Mua 3 Cocacola" },
    { "HD003", "10-03-2025 09:30", "NV03", "Mua 1 Pepsi, 2 Sting" },
    { "HD004", "10-03-2025 10:00", "NV01", "Mua 4 bánh Oreo" },
    { "HD005", "10-03-2025 10:30", "NV04", "Mua 5 lon RedBull" },
    { "HD006", "10-03-2025 11:00", "NV02", "Mua 3 chai Aquafina" },
    { "HD007", "10-03-2025 11:30", "NV05", "Mua 2 hộp sữa chua" },
    { "HD008", "10-03-2025 12:00", "NV03", "Mua 6 gói mì tôm" },
    { "HD009", "10-03-2025 12:30", "NV01", "Mua 1 chai nước cam" },
    { "HD010", "10-03-2025 13:00", "NV04", "Mua 2 gói bim bim Oishi" }
            };

            // Gán dữ liệu vào từng dòng của DataGridView
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++) // 4 cột
                {
                    dtg_Bill.Rows[i].Cells[j].Value = data[i, j];
                }
            }

        }
    }
}
