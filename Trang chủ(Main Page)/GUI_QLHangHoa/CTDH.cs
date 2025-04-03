using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using Microsoft.Office.Interop.Excel;
using Trang_chu_Main_Page_.GUI_QLHangHoa;

namespace Trang_chủ_Main_Page_
{
    public partial class CTDH : Form
    {
        private String Trangthai;
        public CTDH()
        {
            InitializeComponent();
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            string language = Thread.CurrentThread.CurrentUICulture.Name;

            string message = language == "vi-VN"
                ? "Khi ấn hủy, đơn hàng sẽ bị xóa và hoàn lại đồ cho nhà cung cấp."
                : "When you cancel, the order will be deleted and the items will be returned to the supplier.";

            string title = language == "vi-VN" ? "Xác nhận hủy đơn hàng" : "Confirm Order Cancellation";

            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }

        private bool kiemTraOTrong()
        {
            foreach (DataGridViewRow row in dgvCTDH.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString() == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void nhapVaoKho_Click(object sender, EventArgs e)
        {
            string language = Thread.CurrentThread.CurrentUICulture.Name;

            string message = language == "vi-VN"
                ? "Khi ấn xác nhận, hàng hóa sẽ được nhập vào kho và không thể hủy."
                : "Once confirmed, the goods will be entered into the warehouse and cannot be canceled.";

            string title = language == "vi-VN" ? "Xác nhận nhập hàng" : "Confirm Goods Entry";

            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                if (kiemTraOTrong())
                {
                    string status = language == "vi-VN" ? "Đã Xử Lý" : "Processed";

                    foreach (DataGridViewRow row in dgvCTDH.Rows)
                    {
                        if (int.Parse(row.Cells[4].Value.ToString()) != int.Parse(row.Cells[5].Value.ToString()))
                        {
                            status = language == "vi-VN" ? "Chờ Xử Lý Bổ Sung" : "Pending Additional Processing";
                        }
                    }

                    DTO_HDNhapHang hDNhapHang = new DTO_HDNhapHang()
                    {
                        soHD = lbMaDH.Text,
                        soLuong = 0,
                        trangThai = status,
                        ngayDat = DateTime.Now,
                        tongTien = 0,
                        CT_HDNH = new List<DTO_HH_HDNH>()
                    };

                    foreach (DataGridViewRow row in dgvCTDH.Rows)
                    {
                        DTO_HH_HDNH dto_HH_HDNH = new DTO_HH_HDNH()
                        {
                            HangHoa = new DTO_Hanghoa() { MaHangHoa = row.Cells[1].Value.ToString() },
                            SoLuongDat = int.Parse(row.Cells[4].Value.ToString()),
                            SoLuongNhan = int.Parse(row.Cells[5].Value.ToString()),
                            NgayNhan = DateTime.Parse(row.Cells[3].Value.ToString()),
                            NSX = DateTime.Parse(row.Cells[6].Value.ToString()),
                            HSD = DateTime.Parse(row.Cells[7].Value.ToString()),
                            TrangThai = language == "vi-VN" ? "Đã Nhập Kho" : "Warehouse Entered"
                        };

                        hDNhapHang.CT_HDNH.Add(dto_HH_HDNH);
                    }

                    if (BLLQuanLyKho.Instance.nhapKho(hDNhapHang))
                    {
                        string successMessage = language == "vi-VN" ? "Nhập hàng thành công" : "Goods entry successful";
                        string successTitle = language == "vi-VN" ? "Thông báo" : "Notification";

                        MessageBox.Show(successMessage, successTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnNhapVaoKho.Enabled = false;
                        btnKhieuNai.Enabled = false;
                        btnXacNhanDuTatCa.Enabled = false;
                        dgvCTDH.ReadOnly = true;

                        dgvCTDH.CellEndEdit -= dgvCTDH_CellEndEdit;
                        dgvCTDH.CellValidating -= dgvCTDH_CellValidating;

                        this.Close();
                    }
                    else
                    {
                        string errorMessage = language == "vi-VN" ? "Nhập hàng thất bại" : "Goods entry failed";
                        string errorTitle = language == "vi-VN" ? "Lỗi" : "Error";

                        MessageBox.Show(errorMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    string warningMessage = language == "vi-VN" ? "Vui lòng điền đầy đủ thông tin" : "Please fill in all the required information";
                    string warningTitle = language == "vi-VN" ? "Lỗi" : "Error";

                    MessageBox.Show(warningMessage, warningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void btnXacNhanDuTatCa_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvCTDH.Rows)
            {
                row.Cells[5].Value = row.Cells[4].Value;
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2GradientButton3_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }



        private void CTDH_Load(object sender, EventArgs e)
        {
            string language = Thread.CurrentThread.CurrentUICulture.Name;

            if (language == "vi-VN") 
            {
                dgvCTDH.Columns[0].HeaderText = "Số hóa đơn";
                dgvCTDH.Columns[1].HeaderText = "Mã hàng hóa";
                dgvCTDH.Columns[2].HeaderText = "Tên hàng hóa";
                dgvCTDH.Columns[3].HeaderText = "Ngày nhập";
                dgvCTDH.Columns[4].HeaderText = "Số lượng đặt";
                dgvCTDH.Columns[5].HeaderText = "Số lượng nhận";
                dgvCTDH.Columns[6].HeaderText = "Ngày sản xuất";
                dgvCTDH.Columns[7].HeaderText = "Hạn sử dụng";
                dgvCTDH.Columns[8].HeaderText = "Thành tiền";
            }
            else 
            {
                dgvCTDH.Columns[0].HeaderText = "Invoice Number";
                dgvCTDH.Columns[1].HeaderText = "Product Code";
                dgvCTDH.Columns[2].HeaderText = "Product Name";
                dgvCTDH.Columns[3].HeaderText = "Import Date";
                dgvCTDH.Columns[4].HeaderText = "Ordered Quantity";
                dgvCTDH.Columns[5].HeaderText = "Received Quantity";
                dgvCTDH.Columns[6].HeaderText = "Manufacturing Date";
                dgvCTDH.Columns[7].HeaderText = "Expiration Date";
                dgvCTDH.Columns[8].HeaderText = "Total Price";
            }

            foreach (DataGridViewRow row in dgvCTDH.Rows)
            {
                if (string.IsNullOrEmpty(row.Cells[3].Value?.ToString()))
                {
                    row.Cells[3].Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }

            foreach (DataGridViewColumn column in dgvCTDH.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Resizable = DataGridViewTriState.False;
                column.ReadOnly = true;
            }

            dgvCTDH.Columns[5].ReadOnly = false;
            dgvCTDH.Columns[6].ReadOnly = false;

            if (Trangthai == "Nhập Kho Một Phần" || Trangthai == "Partial Warehouse Import")
            {
                foreach (DataGridViewRow row in dgvCTDH.Rows)
                {
                    if (int.Parse(row.Cells[4].Value.ToString()) != int.Parse(row.Cells[5].Value.ToString()))
                    {
                        row.Cells[5].Style.BackColor = Color.DarkRed;
                        row.Cells[5].Style.ForeColor = Color.White;
                        row.Cells[5].Style.SelectionBackColor = Color.DarkRed;
                        row.Cells[5].Style.SelectionForeColor = Color.White;
                    }
                }
            }
        }


        public void loadCTHDGridview(String soHD, String Trangthai)
        {
            dgvCTDH.DataSource = BLLQuanLyKho.Instance.xemCTDHBySohd(soHD);
            dgvCTDH.Columns["THSD"].Visible = false;
            dgvCTDH.Columns["Sohd"].Visible = false;
            this.Trangthai = Trangthai;
        }

        private void btnKhieuNai_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = BLLQuanLyKho.Instance.xemDSKN(lbMaDH.Text);
            foreach (DataRow row in dt.Select("Soluongnhan = Soluongdat"))
            {
                dt.Rows.Remove(row);
            }


            KhieuNai kN = new KhieuNai();
            kN.giveDataGridView(dt);
            kN.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void UpdateMaDH(string maDH, String TrangThaiDHLon)
        {
            string soHD = maDH;
            btnKhieuNai.Enabled = false;
            lbMaDH.Text = maDH;
            if (TrangThaiDHLon == "Chờ Xác Nhận" || TrangThaiDHLon == "Đang Vận Chuyển" || TrangThaiDHLon == "Đã Xử Lý" || TrangThaiDHLon == "Đã Nhập Một Phần")
            {
                btnNhapVaoKho.Enabled = false;
                btnXacNhanDuTatCa.Enabled = false;
                btnKhieuNai.Enabled = false;
                dgvCTDH.ReadOnly = true;

                dgvCTDH.CellEndEdit -= dgvCTDH_CellEndEdit;
                dgvCTDH.CellValidating -= dgvCTDH_CellValidating;

            }

            if(TrangThaiDHLon == "Chờ Xử Lý Bổ Sung")
            {
                btnNhapVaoKho.Enabled = false;
                btnKhieuNai.Enabled = true;
                btnXacNhanDuTatCa.Enabled = false;
                dgvCTDH.ReadOnly = true;
                dgvCTDH.CellEndEdit += dgvCTDH_CellEndEdit;
                dgvCTDH.CellValidating += dgvCTDH_CellValidating;
            }

        }

        private void dgvCTDH_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    dgvCTDH.CellValueChanged -= dgvCTDH_CellValueChanged; // Tạm thời ngừng sự kiện CellValueChanged
            //    dgvCTDH.Rows[e.RowIndex].Cells[6].Value = DateTime.Now.AddDays(int.Parse(dgvCTDH.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString("yyyy/MM/dd");
            //    dgvCTDH.CellValueChanged += dgvCTDH_CellValueChanged; // Kích hoạt lại sự kiện CellValueChanged
            //}
            //catch
            //{
            //    MessageBox.Show("Có lỗi xảy ra khi cập nhật hạn sử dụng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }


        private void dgvCTDH_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string input = e.FormattedValue.ToString().Trim(); // Lấy giá trị người dùng nhập vào

            // ✅ Nếu ô trống, không cần kiểm tra gì thêm
            if (string.IsNullOrEmpty(input))
            {
                return;
            }


            if (e.ColumnIndex == 5)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int result) || result < 0 )
                {
                    string language = Thread.CurrentThread.CurrentUICulture.Name;

                    if (language == "vi-VN")
                    {
                        MessageBox.Show("Số lượng không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else 
                    {
                        MessageBox.Show("Invalid quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    e.Cancel = true; // Ngăn không cho rời khỏi ô nếu nhập sai

                }
            }


            if (e.ColumnIndex == 6)
            {
                string formats = "dd/MM/yyyy";
                CultureInfo culture = new CultureInfo("vi-VN"); // Định dạng ngày của Việt Nam
                dgvCTDH.Columns["Ngaysanxuat"].ValueType = typeof(String);

                if (!DateTime.TryParse(input.ToString(), out DateTime result)) // Kiểm tra có đúng định dạng ngày không
                {
                    string language = Thread.CurrentThread.CurrentUICulture.Name;

                    if (language == "vi-VN")
                    {
                        MessageBox.Show("Vui lòng nhập đúng định dạng ngày (dd/MM/yyyy hoặc yyyy-MM-dd)!",
                                        "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else 
                    {
                        MessageBox.Show("Please enter the correct date format (dd/MM/yyyy or yyyy-MM-dd)!",
                                        "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    e.Cancel = true; // Ngăn không cho rời khỏi ô nếu nhập sai

                }
            }
        }

        private void dgvCTDH_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvCTDH.Rows[e.RowIndex].Cells[6].Value.ToString() != "")
            {
                try
                {
                    dgvCTDH.Rows[e.RowIndex].Cells[7].Value = DateTime.Parse(dgvCTDH.Rows[e.RowIndex].Cells[6].Value.ToString()).AddDays(int.Parse(dgvCTDH.Rows[e.RowIndex].Cells[9].Value.ToString())).ToString("yyyy/MM/dd");
                }
                catch
                {
                    string language = Thread.CurrentThread.CurrentUICulture.Name;

                    if (language == "vi-VN")
                    {
                        MessageBox.Show("Có lỗi xảy ra khi cập nhật hạn sử dụng",
                                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else // Mặc định tiếng Anh
                    {
                        MessageBox.Show("An error occurred while updating the expiration date",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        private void dgvCTDH_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.Exception is FormatException)
            {
                e.Cancel = true;

            }
        }
    }
}
