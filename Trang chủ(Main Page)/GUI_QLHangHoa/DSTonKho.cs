using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trang_chu_Main_Page_.GUI_QLHangHoa;

namespace Trang_chủ_Main_Page_
{
    public partial class DSTonKho : Form
    {
        private DataTable dtHangHoa;
        BindingSource bindingSource = new BindingSource();
        public DSTonKho()
        {
            List<DTO_Hanghoa> danhSachTonKho = BLLQuanLyKho.Instance.XemDSTonKho();
            List<DTO_Hanghoa> dsHH = BLLQuanLyKho.Instance.XemDSTonKho();
            InitializeComponent();

            int count = danhSachTonKho.Count(item => item.GetType() == typeof(DTO_Hanghoa));

            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
            {
                lblTongSoLuongSanPham.Text = $"Tổng số lượng hàng hóa: {count}";
            }
            else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                lblTongSoLuongSanPham.Text = $"Total number of products: {count}";
            }


            var unique = dsHH.GroupBy(dd => dd.DanhMuc)
                     .Select(g => g.First())
                     .ToList();
            var tatCa = new DTO_Hanghoa
            {
                DanhMuc = "Tất cả"
            };

            unique.Insert(0, tatCa);

            cmb_SapXepDM_DSTK.DataSource = unique;
            cmb_SapXepDM_DSTK.DisplayMember = "DanhMuc";
            ;
        }

        private void DSTonKho_Load(object sender, EventArgs e)
        {
            // Kiểm tra culture hiện tại
            string cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            string headerMaHang, headerTenHang, headerDanhMuc, headerSoLuong, headerGiaBan, headerGiaNhap, headerNhaCC, headerTHSD, headerBarcode;

            if (cultureName == "vi-VN")
            {
                headerMaHang = "Mã hàng";
                headerTenHang = "Tên hàng";
                headerDanhMuc = "Danh mục";
                headerSoLuong = "Số lượng";
                headerGiaBan = "Giá bán";
                headerGiaNhap = "Giá nhập";
                headerNhaCC = "Nhà cung cấp";
                headerTHSD = "Thời hạn sử dụng";
                headerBarcode = "Barcode";
            }
            else if (cultureName == "en-US")
            {
                headerMaHang = "Product Code";
                headerTenHang = "Product Name";
                headerDanhMuc = "Category";
                headerSoLuong = "Quantity";
                headerGiaBan = "Selling Price";
                headerGiaNhap = "Purchase Price";
                headerNhaCC = "Supplier";
                headerTHSD = "Expiry Date";
                headerBarcode = "Barcode";
            }
            else
            {
                // Mặc định dùng tiếng Anh
                headerMaHang = "Product Code";
                headerTenHang = "Product Name";
                headerDanhMuc = "Category";
                headerSoLuong = "Quantity";
                headerGiaBan = "Selling Price";
                headerGiaNhap = "Purchase Price";
                headerNhaCC = "Supplier";
                headerTHSD = "Expiry Date";
                headerBarcode = "Barcode";
            }

            dgvDSTonKho.AutoGenerateColumns = false;
            dgvDSTonKho.Columns.Clear();

            var colMaHang = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaHangHoa",
                HeaderText = headerMaHang,
                Name = "colMaHang"
            };
            dgvDSTonKho.Columns.Add(colMaHang);

            var colTenHang = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenHangHoa",
                HeaderText = headerTenHang,
                Name = "colTenHang"
            };
            dgvDSTonKho.Columns.Add(colTenHang);

            var colDanhMuc = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DanhMuc",
                HeaderText = headerDanhMuc,
                Name = "colDanhMuc"
            };
            dgvDSTonKho.Columns.Add(colDanhMuc);

            var colSoLuong = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = headerSoLuong,
                Name = "colSoLuong"
            };
            dgvDSTonKho.Columns.Add(colSoLuong);

            var colGiaBan = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaBan",
                HeaderText = headerGiaBan,
                Name = "colGiaBan"
            };
            dgvDSTonKho.Columns.Add(colGiaBan);

            var colGiaNhap = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaNhap",
                HeaderText = headerGiaNhap,
                Name = "colGiaNhap"
            };
            dgvDSTonKho.Columns.Add(colGiaNhap);

            var colNhaCC = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NhaCC",
                HeaderText = headerNhaCC,
                Name = "colNhaCC"
            };
            dgvDSTonKho.Columns.Add(colNhaCC);

            var colTHSD = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "THSD",
                HeaderText = headerTHSD,
                Name = "colTHSD"
            };
            dgvDSTonKho.Columns.Add(colTHSD);
            var colBarcode = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Barcode",
                HeaderText = headerBarcode,
                Name = "colBarcode"
            };
            dgvDSTonKho.Columns.Add(colBarcode);
            LoadData();
        }




        private void LoadData()
        {
            List<DTO_Hanghoa> danhSachTonKho = BLLQuanLyKho.Instance.XemDSTonKho();
            dgvDSTonKho.DataSource = danhSachTonKho;
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDSTonKho.Rows[e.RowIndex];

                string mahh = row.Cells["colMahang"].Value.ToString();

                DataTable dt = BLLQuanLyKho.Instance.XemCTHH(mahh);

                if (dt.Rows.Count > 0)
                {
                    CTHH formCTHH = new CTHH
                    {
                        DataCTHH = dt
                    };
                    formCTHH.ShowDialog();
                }
                else
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("No data found for product code: " + mahh);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho mã hàng: " + mahh);
                    }

                }
            }
        }




        private void txt_HH_SearchBar_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchDSTonKho.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(keyword))
            {
                var filteredList = BLLQuanLyKho.Instance.XemDSTonKho()
                    .Where(hh => hh.MaHangHoa.ToLower().Contains(keyword) ||
                                 hh.TenHangHoa.ToLower().Contains(keyword) ||
                                 hh.DanhMuc.ToLower().Contains(keyword) ||
                                 hh.NhaCC.ToLower().Contains(keyword) ||
                                 hh.SoLuong.ToString().Contains(keyword) ||
                                 hh.GiaBan.ToString().Contains(keyword) ||
                                 hh.GiaNhap.ToString().Contains(keyword) ||
                                 hh.THSD.ToString().Contains(keyword))
                    .ToList();

                bindingSource.DataSource = filteredList;
            }
            else
            {
                bindingSource.DataSource = BLLQuanLyKho.Instance.XemDSTonKho();
            }

            dgvDSTonKho.DataSource = bindingSource;
        }


        private void cmbSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = cmb_SapXepDM_DSTK.Text;
            List<DTO_Hanghoa> danhSachTonKho = BLLQuanLyKho.Instance.XemDSTonKho();

            if (selectedCategory == "Tất cả")
            {
                dgvDSTonKho.DataSource = danhSachTonKho;
            }
            else
            {
                var filterList = danhSachTonKho
                    .Where(hh => hh.DanhMuc == selectedCategory)
                    .ToList();
                dgvDSTonKho.DataSource = filterList;
            }
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

        private void btnLuu_DSTonKho_Click(object sender, EventArgs e)
        {

        }

        private void lblTongSoLuongSanPham_Click(object sender, EventArgs e)
        {

        }
    }
}
