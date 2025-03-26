using Guna.UI2.WinForms;
using Trang_chu_Main_Page_.DSHangHoa_Dat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;


namespace Trang_chủ_Main_Page_
{
    public partial class DatHang : Form
    {
        public DatHang()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.DatHang_Load);
            dgvDanhSachDatHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void CalculateTotal()
        {
            double total = 0;
            foreach (DataGridViewRow row in dgvDanhSachDatHang.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    string value = row.Cells[4].Value.ToString().Replace("đ", "").Replace(",", "").Trim();
                    if (double.TryParse(value, out double price))
                    {
                        total += price;
                    }
                }
            }

            lblTotal.Text = total.ToString("#,##0") + " đ";
        }
        public void AddItem(string name, string Supplier, double cost, byte[] icon)
        {

            var c = new HangHoaNCC()
            {
                Title = name,
                Supplier = Supplier,
                Cost = cost,
                Icon = icon 
            };

           // c.Load_Data((byte[])DataProvider.Instance.ExecuteScalar("SELECT ImageData FROM Hanghoa WHERE Mahanghoa = 'HH0001'"));

            flowLayoutPanel1.Controls.Add(c);

            c.OnSelect += (ss, ee) =>
            {
                var us1 = (HangHoaNCC)ss;
                int addQuantity = us1.SoLuong; // Lấy số lượng từ TextBox trong UserControl

                foreach (DataGridViewRow item in dgvDanhSachDatHang.Rows)
                {
                    if (item.Cells.Count > 0 && item.Cells[0].Value != null &&
                        item.Cells[0].Value.ToString() == us1.label1.Text)
                    {
                        int currentQuantity = int.Parse(item.Cells[2].Value.ToString());
                        item.Cells[2].Value = currentQuantity + addQuantity;
                        double costValue = double.Parse(item.Cells[3].Value.ToString().Replace("đ", ""));
                        item.Cells[4].Value = ((currentQuantity + addQuantity) * costValue).ToString("#,##0") + "đ";
                        CalculateTotal();
                        return;
                    }
                }

                dgvDanhSachDatHang.Rows.Add(new object[]
                {
                    us1.label1.Text,
                    us1.label3.Text,
                    addQuantity,
                    us1.label4.Text,
                    (addQuantity * double.Parse(us1.label4.Text.Replace("đ", "").Replace(",", "").Trim())).ToString("#,##0") + "đ"
                });
                CalculateTotal();
            };

        }


        private void DatHang_Load(object sender, EventArgs e)
        {
            //  AddItem("Hàng 1", "Campuchai", 7.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            BLLQuanLyKho bll = new BLLQuanLyKho();
            List<DTO_Hanghoa> listHangHoa = bll.hangHoa_NhapHang();

            foreach (DTO_Hanghoa hh in listHangHoa)
            {
                // Gọi AddItem với các thuộc tính từ DTO
                AddItem(hh.TenHangHoa, hh.NhaCC, hh.GiaNhap, hh.HinhAnh);
            }
        }
        //private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        public void AddItem_Dgv(string tenHang, string tenNCC, int soLuong, double giaGoc)
        {
            // Duyệt qua danh sách để kiểm tra hàng đã tồn tại chưa
            foreach (DataGridViewRow item in dgvDanhSachDatHang.Rows)
            {
                if (item.Cells.Count > 0 && item.Cells[0].Value != null &&
                    item.Cells[0].Value.ToString() == tenHang) 
                {
                    int currentQuantity = int.Parse(item.Cells[2].Value.ToString());
                    int newQuantity = currentQuantity + soLuong;
                    item.Cells[2].Value = newQuantity;
                    double costValue = double.Parse(item.Cells[3].Value.ToString().Replace("đ", "").Replace(",", "").Trim());
                    item.Cells[4].Value = (newQuantity * costValue).ToString("#,##0") + " đ";

                    CalculateTotal();
                    return; 
                }
            }

            // Nếu hàng chưa có, thêm mới vào danh sách
            double thanhTien = soLuong * giaGoc;
            dgvDanhSachDatHang.Rows.Add(
                tenHang,
                tenNCC,
                soLuong,
                giaGoc.ToString("#,##0") + " đ", 
                thanhTien.ToString("#,##0") + " đ" 
            );

            CalculateTotal(); 
        }


        private void Btn_ThemMatHang_Click(object sender, EventArgs e)
        {
            ThemMatHang themMatHangForm = new ThemMatHang();
            themMatHangForm.Show();
        }
        private void Btn_DHBang_AI_Click(object sender, EventArgs e)
        {
            GoiYNhapHangBangAI goiYNhapHangBangAI = new GoiYNhapHangBangAI();
            //goiYNhapHangBangAI.Dock = DockStyle.Fill;
            //guna2Panel3.Controls.Add(goiYNhapHangBangAI);
            goiYNhapHangBangAI.Show();
          

        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
        private void btnThemMatHang_Click(object sender, EventArgs e)
        {
            ThemMatHang themMatHang = new ThemMatHang();
            themMatHang.Show();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DatHang_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachDatHang.SelectedRows.Count > 0) 
            {
                foreach (DataGridViewRow row in dgvDanhSachDatHang.SelectedRows)
                {
                    if (!row.IsNewRow) // Tránh xóa dòng trống cuối cùng của DataGridView
                    {
                        dgvDanhSachDatHang.Rows.Remove(row);
                    }
                }

                CalculateTotal(); 
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
