using BLL;
using DTO;
using iTextSharp.text.pdf;
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

        private bool isEdited = false;
        private object[] originalRowValues; 
        private DataGridViewRow rowEdited; 
        private bool cellClick = true;

        public DSTonKho()
        {
            List<DTO_Hanghoa> danhSachTonKho = BLLQuanLyKho.Instance.XemDSTonKho();
            List<DTO_Hanghoa> dsHH = BLLQuanLyKho.Instance.XemDSTonKho();
            InitializeComponent();
            dgvDSTonKho.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

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
            dgvDSTonKho.SelectionChanged += dgvDSTonKho_SelectionChanged;
            cmb_SapXepDM_DSTK.SelectedIndexChanged += cmbSelectedIndexChanged;
            HighlightHansudungLessThan15Percent();

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cellClick == false)
            {
                return;
            }
            else
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
            
        }
        public void HighlightHansudungLessThan15Percent()
        {
            foreach (DataGridViewRow row in dgvDSTonKho.Rows)
            {

                string mahh = row.Cells["colMahang"].Value.ToString(); 

                DataTable dtCTHH = BLLQuanLyKho.Instance.XemCTHH(mahh);

                if (dtCTHH.Rows.Count > 0)
                {
                    int thsd = Convert.ToInt32(row.Cells["colTHSD"].Value);

                    foreach (DataRow dr in dtCTHH.Rows)
                    {
                       
                        DateTime hansudungDate = Convert.ToDateTime(dr["Hansudung"]);
                        int remainingDays = (hansudungDate - DateTime.Now).Days;  // Số ngày còn lại

                        // Kiểm tra điều kiện 15% của THSD
                        if (thsd > 30 && remainingDays < (0.15 * thsd))
                        {
                            // Tô màu vàng cho cột Hansudung trong dgvDSTonKho
                            row.Cells["colTHSD"].Style.BackColor = Color.Yellow;
                        }
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
                HighlightHansudungLessThan15Percent();
            }
            else
            {
                bindingSource.DataSource = BLLQuanLyKho.Instance.XemDSTonKho();
                HighlightHansudungLessThan15Percent();
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
                HighlightHansudungLessThan15Percent();

            }
            else
            {
                var filterList = danhSachTonKho
                    .Where(hh => hh.DanhMuc == selectedCategory)
                    .ToList();
                dgvDSTonKho.DataSource = filterList;
                HighlightHansudungLessThan15Percent();
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

        private void btnXoa_DSTonKho_Click(object sender, EventArgs e)
        {
            if (dgvDSTonKho.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa hàng hóa này?", 
                    "Xác nhận xóa", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question 
                );

                if (dialogResult == DialogResult.Yes)
                {
                    string barcode = dgvDSTonKho.SelectedRows[0].Cells["colBarcode"].Value.ToString();
                    bool isDeleted = BLLQuanLyKho.Instance.XoaHangHoa(barcode);

                    if (isDeleted)
                    {
                        MessageBox.Show("Hàng hóa đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        HighlightHansudungLessThan15Percent();
                    }
                    else
                    {
                        MessageBox.Show("Xóa hàng hóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Hành động xóa đã bị hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_DSTonKho_Click(object sender, EventArgs e)
        {
            if (!isEdited)
            {
                if (dgvDSTonKho.SelectedRows.Count > 0)
                {
                    MessageBox.Show("Bạn chỉ được sửa các thông tin trừ Barcode và Mã hàng hóa!",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    isEdited = true;
                    dgvDSTonKho.ReadOnly = false;

                    foreach (DataGridViewColumn col in dgvDSTonKho.Columns)
                    {
                        if (col.Name == "colBarcode" || col.Name == "colMaHang")
                            col.ReadOnly = true; 
                        else
                            col.ReadOnly = false; 
                    }

                    rowEdited = dgvDSTonKho.SelectedRows[0];
                    originalRowValues = new object[rowEdited.Cells.Count];
                    for (int i = 0; i < rowEdited.Cells.Count; i++)
                    {
                        originalRowValues[i] = rowEdited.Cells[i].Value;
                    }
                    rowEdited.DefaultCellStyle.BackColor = Color.DarkGray;
                    rowEdited.DefaultCellStyle.SelectionBackColor = Color.Gray;
                    btnXoa_DSTonKho.Enabled = false;
                    dgvDSTonKho.SelectionChanged -= dgvDSTonKho_SelectionChanged;
                    btnSua_DSTonKho.Text = "Hủy";
                    cellClick = false;
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dòng cần sửa!",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            else
            {
                isEdited = false;
                dgvDSTonKho.ReadOnly = true;

                foreach (DataGridViewColumn col in dgvDSTonKho.Columns)
                {
                    col.ReadOnly = true;
                }

                btnXoa_DSTonKho.Enabled = true;
                cellClick = true;

                if (originalRowValues != null)
                {
                    for (int i = 0; i < rowEdited.Cells.Count; i++)
                    {
                        rowEdited.Cells[i].Value = originalRowValues[i];
                    }
                }
                rowEdited.DefaultCellStyle.BackColor = Color.White;
                rowEdited.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 69, 0);
                btnSua_DSTonKho.Text = "Sửa";
                HighlightHansudungLessThan15Percent();
            }
        }
        private void dgvDSTonKho_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDSTonKho.SelectedRows.Count > 0)
            {
                string barcode = dgvDSTonKho.SelectedRows[0].Cells["colBarcode"].Value.ToString();


            }
        }

        private void btnLuu_DSTonKho_Click_1(object sender, EventArgs e)
        {
            if (isEdited)
            {
                string barcode = rowEdited.Cells["colBarcode"].Value.ToString();
                string tenHangHoa = rowEdited.Cells["colTenHang"].Value.ToString();
                string danhMuc = rowEdited.Cells["colDanhMuc"].Value.ToString();
                int soLuong = Convert.ToInt32(rowEdited.Cells["colSoLuong"].Value);
                float giaNhap = Convert.ToSingle(rowEdited.Cells["colGiaNhap"].Value);
                float giaBan = Convert.ToSingle(rowEdited.Cells["colGiaBan"].Value);
                string nhaCC = rowEdited.Cells["colNhaCC"].Value.ToString();
                int thsd = Convert.ToInt32(rowEdited.Cells["colTHSD"].Value);

                bool isUpdated = BLLQuanLyKho.Instance.SuaHangHoa(barcode, tenHangHoa, danhMuc, giaNhap, giaBan, thsd, nhaCC, soLuong);

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<DTO_Hanghoa> danhSachTonKho = BLLQuanLyKho.Instance.XemDSTonKho();
                    dgvDSTonKho.DataSource = danhSachTonKho;

                }
                else
                {
                    MessageBox.Show("Cập nhật hàng hóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                isEdited = false;
                dgvDSTonKho.ReadOnly = true;

                foreach (DataGridViewColumn col in dgvDSTonKho.Columns)
                {
                    col.ReadOnly = true;
                }

                btnXoa_DSTonKho.Enabled = true;
                dgvDSTonKho.SelectionChanged += dgvDSTonKho_SelectionChanged;
                if (originalRowValues != null)
                {
                    for (int i = 0; i < rowEdited.Cells.Count; i++)
                    {
                        rowEdited.Cells[i].Value = originalRowValues[i];
                    }
                }

                rowEdited.DefaultCellStyle.BackColor = Color.White;
                rowEdited.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 69, 0);
                cellClick = true;
                btnSua_DSTonKho.Text = "Sửa";
                HighlightHansudungLessThan15Percent();
            }
        }

    }


}
