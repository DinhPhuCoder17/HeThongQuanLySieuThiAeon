using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace Trang_chu_Main_Page_.GUI_QL_TC_NS
{
    public partial class LichSuKH : Form
    {
        private readonly BLL_QuanlyTCNS bLLQuanLyKho = new BLL_QuanlyTCNS();
        public LichSuKH()
        {
            InitializeComponent();
        }

        public void load_in_Datagridview(String soDienThoai)
        {
            dtg_LichSuMH.DataSource = bLLQuanLyKho.xemLSKH(soDienThoai);
            dtg_LichSuMH.Columns["Mahoadon"].HeaderText = "Mã hóa đơn";
            dtg_LichSuMH.Columns["Thoigianban"].HeaderText = "Thời gian bán";
            dtg_LichSuMH.Columns["Manhanvien"].HeaderText = "Mã nhân viên";
            dtg_LichSuMH.Columns["Sodienthoai"].HeaderText = "Số điện thoại";
            dtg_LichSuMH.Columns["Thanhtien"].HeaderText = "Thành tiền";

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtg_LichSuMH_DoubleClick(object sender, EventArgs e)
        {
            if (dtg_LichSuMH.CurrentRow != null) // Kiểm tra có dòng nào được chọn không
            {
                string soHD = dtg_LichSuMH.CurrentRow.Cells[0].Value.ToString(); // Lấy giá trị cột đầu tiên

                CT_HDBH cT_HDBH = new CT_HDBH();
                cT_HDBH.LoadCTHDBH(soHD); // Truyền giá trị vào phương thức
                cT_HDBH.ShowDialog();
            }
        }
    }
}
