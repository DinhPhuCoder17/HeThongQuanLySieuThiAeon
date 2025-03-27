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
            dgvDanhSachDatHang.Columns["MaHangHoa"].Visible = false;
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
        public void AddItem(string MaHH, string name, string Supplier, double cost, byte[] icon)
        {
            var c = new HangHoaNCC()
            {
                MaHH = MaHH,
                Title = name,
                Supplier = Supplier,
                Cost = cost,
                Icon = icon
            };

            flowLayoutPanel1.Controls.Add(c);

            c.OnSelect += (ss, ee) =>
            {
                var us1 = (HangHoaNCC)ss;
                int addQuantity = us1.SoLuong;

                foreach (DataGridViewRow item in dgvDanhSachDatHang.Rows)
                {
                    if (item.Cells.Count > 0 && item.Cells[0].Value != null &&
                        item.Cells[0].Value.ToString() == us1.label2.Text) 
                    {
                        int currentQuantity = int.Parse(item.Cells[3].Value.ToString());
                        item.Cells[3].Value = currentQuantity + addQuantity;
                        double costValue = double.Parse(item.Cells[4].Value.ToString().Replace("đ", "").Replace(",", "").Trim());
                        item.Cells[5].Value = ((currentQuantity + addQuantity) * costValue).ToString("#,##0") + "đ";
                        CalculateTotal();
                        return;
                    }
                }

                dgvDanhSachDatHang.Rows.Add(new object[]
                {
            us1.label2.Text,              
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
            BLLQuanLyKho bll = new BLLQuanLyKho();
            List<DTO_Hanghoa> listHangHoa = bll.hangHoa_NhapHang();

            foreach (DTO_Hanghoa hh in listHangHoa)
            {
                AddItem(hh.MaHangHoa, hh.TenHangHoa, hh.NhaCC, hh.GiaNhap, hh.HinhAnh);
            }
        }
        public void AddItem_Dgv(string maHang, string tenHang, string tenNCC, int soLuong, double giaGoc)
        {
            foreach (DataGridViewRow item in dgvDanhSachDatHang.Rows)
            {
                if (item.Cells.Count > 0 && item.Cells[0].Value != null &&
                    item.Cells[0].Value.ToString() == maHang)
                {
                    int currentQuantity = int.Parse(item.Cells[3].Value.ToString());
                    int newQuantity = currentQuantity + soLuong;
                    item.Cells[3].Value = newQuantity;

                    double unitPrice = double.Parse(
                        item.Cells[4].Value.ToString()
                            .Replace("đ", "")
                            .Replace(",", "")
                            .Trim()
                    );

                    item.Cells[5].Value = (newQuantity * unitPrice).ToString("#,##0") + " đ";

                    CalculateTotal();
                    return;
                }
            }

            double thanhTien = soLuong * giaGoc;
            dgvDanhSachDatHang.Rows.Add(
                 maHang,                                  
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
       /* private void btnLuuDonHang_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đặt đơn hàng này?", "Xác nhận",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool isSuccess = orderBLL.datHang(tongGiaTri, tongSoLuong);

                if (isSuccess)
                    MessageBox.Show("Cập nhật tổng số lượng và tổng tiền thành công!");
                else
                    MessageBox.Show("Cập nhật thất bại, vui lòng kiểm tra lại!");
            }
        }*/
        public List<DTO_HH_HDNH> GetHangHoaFromDGV()
        {
            List<DTO_HH_HDNH> list = new List<DTO_HH_HDNH>();

            foreach (DataGridViewRow row in dgvDanhSachDatHang.Rows)
            {
                if (row.Cells["MaHangHoa"].Value != null)
                {
                    string maHang = row.Cells["MaHangHoa"].Value.ToString();

                    string tenHang = row.Cells["TenHangHoa"].Value != null
                                     ? row.Cells["TenHangHoa"].Value.ToString()
                                     : "";

                    int soLuongDat = 0;
                    if (row.Cells["SoLuong"].Value != null
                        && int.TryParse(row.Cells["SoLuong"].Value.ToString(), out int tempSoLuong))
                    {
                        soLuongDat = tempSoLuong;
                    }

                    float giaNhap = 0f;
                    if (row.Cells["DonGia"].Value != null)
                    {
                        string donGiaStr = row.Cells["DonGia"].Value
                            .ToString()
                            .Replace("đ", "")
                            .Replace(",", "")
                            .Trim();

                        if (float.TryParse(donGiaStr, out float tempDonGia))
                        {
                            giaNhap = tempDonGia;
                        }
                    }

                    DTO_Hanghoa dtoHangHoa = new DTO_Hanghoa
                    {
                        MaHangHoa = maHang,
                        TenHangHoa = tenHang,
                        GiaNhap = giaNhap
                    };

                    DTO_HH_HDNH dtoCT = new DTO_HH_HDNH
                    {
                        HangHoa = dtoHangHoa,
                        SoLuongDat = soLuongDat,
                        SoLuongNhan = 0,                
                        NgayNhan = DateTime.Now,        
                        HanThanhToan = DateTime.Now,    
                        NSX = DateTime.Now,            
                        HSD = "2025-12-31"              
                    };

                    list.Add(dtoCT);
                }
            }

            return list;
        }





    }
}
