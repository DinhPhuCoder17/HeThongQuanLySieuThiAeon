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
    public partial class GoiYNhapHangBangAI : Form
    {
        public GoiYNhapHangBangAI()
        {
            InitializeComponent();
            LoadData();
        }
        /*    private void LoadData()
            {
             *//*   foreach (DataGridViewColumn column in dgvGoiYNhapHang.Columns)
                {
                    column.HeaderText = "";
                *//*}*//*
                List<Product> products = Database.GetProducts();
                dgvGoiYNhapHang.DataSource = products;  // Đưa dữ liệu vào dgvGoiYNhapHang*/
        /*     dgvGoiYNhapHang.DataSource = bLL_QuanlyTCNS.xemDSKH();
             dgvGoiYNhapHang.Columns["Sodienthoai"].HeaderText = "Số điện thoại";
             dgvGoiYNhapHang.Columns["Hoten"].HeaderText = "Họ tên";
             dgvGoiYNhapHang.Columns["Diachi"].HeaderText = "Địa chỉ";
             dgvGoiYNhapHang.Columns["Diemthuong"].HeaderText = "Điểm thưởng";
             dgvGoiYNhapHang.Columns["Gioitinh"].HeaderText = "Giới tính";
             dgvGoiYNhapHang.Columns["Hang"].HeaderText = "Hạng";*/

        /*foreach (DataGridViewColumn column in dgvGoiYNhapHang.Columns)
        {
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }*//*
    }*/
        private void LoadData()
        {
            BLL_predictor.Predict(dgvGoiYNhapHang);
        }

        private void guna2GradientTileButton4_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GoiYNhapHangBangAI_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            DatHang datHangForm = Application.OpenForms.OfType<DatHang>().FirstOrDefault();

            if (datHangForm != null)
            {
                foreach (DataGridViewRow row in dgvGoiYNhapHang.SelectedRows)
                {
                    string maHH = row.Cells["Column9"].Value.ToString();
                    string tenHang = row.Cells["Column11"].Value.ToString();
                    string tenNCC = row.Cells["Column5"].Value.ToString();
                    int soLuong = Convert.ToInt32(row.Cells["Column12"].Value);
                    double giaGoc = Convert.ToDouble(row.Cells["Column13"].Value);

                    datHangForm.AddItem_Dgv(maHH, tenHang, tenNCC, soLuong, giaGoc);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu.");
            }

            this.Close();
        }


    }
}
