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
using System.Text.RegularExpressions;
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
                DialogResult dialogResult = DialogResult.None;


                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    dialogResult = MessageBox.Show(
                        "Bạn có chắc chắn muốn xóa hàng hóa này?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    dialogResult = MessageBox.Show(
                        "Are you sure you want to delete this product?",
                        "Delete Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                }




                if (dialogResult == DialogResult.Yes)
                {
                    string barcode = dgvDSTonKho.SelectedRows[0].Cells["colBarcode"].Value.ToString();
                    bool isDeleted = BLLQuanLyKho.Instance.XoaHangHoa(barcode);

                    if (isDeleted)
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Hàng hóa đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                        {
                            MessageBox.Show("The product has been successfully deleted!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LoadData();
                        HighlightHansudungLessThan15Percent();
                    }
                    else
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Xóa hàng hóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                        {
                            MessageBox.Show("Failed to delete the product!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                else
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Hành động xóa đã bị hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("The delete action has been canceled!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please select a row to delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void btnSua_DSTonKho_Click(object sender, EventArgs e)
        {
            if (!isEdited)
            {
                if (dgvDSTonKho.SelectedRows.Count > 0)
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Bạn chỉ được sửa các thông tin trừ Barcode, mã nhà cung cấp và Mã hàng hóa!",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("You can only edit information except Barcode, Supplier Code and Product Code!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }

                    isEdited = true;
                    dgvDSTonKho.ReadOnly = false;

                    foreach (DataGridViewColumn col in dgvDSTonKho.Columns)
                    {
                        // Không cho phép sửa cột Barcode, Mã hàng hóa và mã nhà cung cấp (colNhaCC)
                        if (col.Name == "colBarcode" || col.Name == "colMaHang" || col.Name == "colNhaCC")
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

                    // Khóa combobox và search textbox khi đang sửa
                    cmb_SapXepDM_DSTK.Enabled = false;     // Vô hiệu hóa combobox
                    txtSearchDSTonKho.Enabled = false;       // Vô hiệu hóa search textbox

                    // Đăng ký sự kiện CellValidating để kiểm tra cột colSoLuong và các cột khác nếu cần
                    dgvDSTonKho.CellValidating += dgvDSTonKho_CellValidating;

                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        btnSua_DSTonKho.Text = "Hủy";
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        btnSua_DSTonKho.Text = "Cancel";
                    }

                    cellClick = false;
                }
                else
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Vui lòng chọn dòng cần sửa!",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Please select a row to edit!",
                                        "Notification",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
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
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    btnSua_DSTonKho.Text = "Sửa";
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    btnSua_DSTonKho.Text = "Edit";
                }

                // Mở lại combobox và search textbox khi hủy sửa
                cmb_SapXepDM_DSTK.Enabled = true;     // Kích hoạt lại combobox
                txtSearchDSTonKho.Enabled = true;       // Kích hoạt lại search textbox
                
                // Gỡ bỏ đăng ký sự kiện CellValidating
                dgvDSTonKho.CellValidating -= dgvDSTonKho_CellValidating;

                // Cập nhật lại giao diện của DataGridView để khôi phục hiển thị
                dgvDSTonKho.EndEdit();
                LoadData();


                HighlightHansudungLessThan15Percent();
            }
        }


        // Sự kiện CellValidating dùng để kiểm tra
        private void dgvDSTonKho_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = dgvDSTonKho.Columns[e.ColumnIndex].Name;

            // Chỉ xử lý nếu dòng đang sửa là dòng hiện tại
            if (dgvDSTonKho.CurrentRow != null && dgvDSTonKho.CurrentRow == rowEdited)
            {
                // 1. Kiểm tra colSoLuong: không được vượt quá số lượng gốc
                if (columnName == "colSoLuong")
                {
                    int originalQuantity = 0;
                    int newQuantity = 0;

                    if (int.TryParse(originalRowValues[e.ColumnIndex].ToString(), out originalQuantity))
                    {
                        if (int.TryParse(e.FormattedValue.ToString(), out newQuantity))
                        {
                            if (newQuantity > originalQuantity)
                            {
                                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                                {
                                    MessageBox.Show("Số lượng không được vượt quá số lượng hiện tại!",
                                                    "Thông báo",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);

                                }
                                else
                                {
                                    MessageBox.Show("Quantity cannot be greater than the current amount!",
                                                    "Notification",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                }
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                            {
                                MessageBox.Show("Vui lòng nhập số hợp lệ!",
                                                "Thông báo",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid number!",
                                                "Notification",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                            }
                            e.Cancel = true;
                        }
                    }
                }

                // 2. Kiểm tra colGiaBan và colGiaNhap: phải là số thực > 0
                else if (columnName == "colGiaBan" || columnName == "colGiaNhap")
                {
                    decimal newPrice = 0;
                    if (!decimal.TryParse(e.FormattedValue.ToString(), out newPrice) || newPrice <= 0)
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("Giá bán và giá nhập phải là số thập phân lớn hơn 0!",
                                            "Thông báo",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Sale price and purchase price must be a decimal number greater than 0!",
                                            "Notification",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                        e.Cancel = true;
                    }
                }

                // 3. Kiểm tra colTHSD: phải là số nguyên > 0
                else if (columnName == "colTHSD")
                {
                    int newTHSD = 0;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newTHSD) || newTHSD <= 0)
                    {
                        if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                        {
                            MessageBox.Show("THSD phải là số nguyên lớn hơn 0!",
                                            "Thông báo",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("THSD must be an integer greater than 0!",
                                            "Notification",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                        e.Cancel = true;
                    }
                }
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
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Cập nhật hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Product updated successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    List<DTO_Hanghoa> danhSachTonKho = BLLQuanLyKho.Instance.XemDSTonKho();
                    dgvDSTonKho.DataSource = danhSachTonKho;

                }
                else
                {
                    if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                    {
                        MessageBox.Show("Cập nhật hàng hóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        MessageBox.Show("Failed to update the product!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

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
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    btnSua_DSTonKho.Text = "Sửa";
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    btnSua_DSTonKho.Text = "Edit";
                }
                // Mở lại combobox và search textbox khi hủy sửa
                cmb_SapXepDM_DSTK.Enabled = true;     // Kích hoạt lại combobox
                txtSearchDSTonKho.Enabled = true;       // Kích hoạt lại search textbox
                HighlightHansudungLessThan15Percent();
            }
        }

        // ---------------- Phần của Quang ----------------------
        bool menuExpand_3 = false;
        //Check Password hợp lệ
        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private void tm_InforChanges_Tick_1(object sender, EventArgs e)
        {
            if (menuExpand_3 == false)
            {
                pn_infoChanges.Height += 20;
                if (pn_infoChanges.Height >= 280)
                {
                    tm_InforChanges.Stop();
                    menuExpand_3 = true;
                }
            }
            else
            {
                pn_infoChanges.Height -= 20;
                if (pn_infoChanges.Height <= 0)
                {
                    tm_InforChanges.Stop();
                    menuExpand_3 = false;
                }
            }
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            tm_InforChanges.Start();
            lb_Ma.Text = Mainpage.CurrentUser.MaNhanvien;
            lb_Hoten.Text = Mainpage.CurrentUser.Hoten;
            lb_Ngaysinh.Text = Mainpage.CurrentUser.Ngaysinh;
            lb_Gioitinh.Text = Mainpage.CurrentUser.Gioitinh;
            lb_sdt.Text = Mainpage.CurrentUser.Sodienthoai;
        }

        private void btn_Xacnhan_Click_1(object sender, EventArgs e)
        {
            string oldPassword = tb_OldPassword.Text;
            string Password = tb_mk1.Text;
            string Repassword = tb_mk2.Text;
            if (string.IsNullOrEmpty(oldPassword))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please enter the old password!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please enter the new password!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (string.IsNullOrEmpty(Repassword))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Vui lòng nhập xác nhận mật khẩu mới!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Please enter the new password confirmation!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (!Password.Equals(Repassword))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Mật khẩu không trùng khớp!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Password does not match!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return;
            }
            if (!IsValidPassword(Password))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Mật khẩu không hợp lệ! Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Invalid password! Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (BLL_Nhanvien.Instance.UpdatePassword(Mainpage.CurrentUser.MaNhanvien, oldPassword, Password))
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Password changed successfully!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "vi-VN")
                {
                    MessageBox.Show("Lỗi khi đổi mật khẩu!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    MessageBox.Show("Error changing password!", "NOTIFICATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }
    }

}
