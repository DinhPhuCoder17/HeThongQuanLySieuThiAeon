using BLL;
using DTO;
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
        private DataTable dtHangHoa;
        BindingSource bindingSource = new BindingSource();
        public DSTonKho()
        {
            List<DTO_Hanghoa> danhSachTonKho = BLLQuanLyKho.Instance.XemDSTonKho();
            List<DTO_Hanghoa> dsHH = BLLQuanLyKho.Instance.XemDSTonKho();
            InitializeComponent();

            int count = danhSachTonKho.Count(item => item.GetType() == typeof(DTO_Hanghoa));
            lblTongSoLuongSanPham.Text = $"Tổng số lượng hàng hóa: {count}";

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

            dgvDSTonKho.AutoGenerateColumns = false;
            dgvDSTonKho.Columns.Clear();

            var colMaHang = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaHangHoa",
                HeaderText = "Mã hàng",
                Name = "colMaHang"
            };
            dgvDSTonKho.Columns.Add(colMaHang);
            var colTenHang = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenHangHoa",
                HeaderText = "Tên hàng",
                Name = "colTenHang"
            };
            dgvDSTonKho.Columns.Add(colTenHang);
            var colDanhMuc = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DanhMuc",
                HeaderText = "Danh mục",
                Name = "colDanhMuc"
            };
            dgvDSTonKho.Columns.Add(colDanhMuc);
            var colSoLuong = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "Số lượng",
                Name = "colSoLuong"
            };
            dgvDSTonKho.Columns.Add(colSoLuong);
            var colGiaBan = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaBan",
                HeaderText = "Giá bán",
                Name = "colGiaBan"
            };
            dgvDSTonKho.Columns.Add(colGiaBan);
            var colGiaNhap = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaNhap",
                HeaderText = "Giá Nhập",
                Name = "colGiaNhap"
            };
            dgvDSTonKho.Columns.Add(colGiaNhap);
            var colNhaCC = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NhaCC",
                HeaderText = "Nhà Cung Cấp",
                Name = "colNhaCC"
            };
            dgvDSTonKho.Columns.Add(colNhaCC);
            var colTHSD = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "THSD",
                HeaderText = "Thời Hạn Sử Dụng",
                Name = "colTHSD"
            };
            dgvDSTonKho.Columns.Add(colTHSD);
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSTonKho.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDSTonKho.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dgvDSTonKho.Rows.Remove(row);
                    }
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
