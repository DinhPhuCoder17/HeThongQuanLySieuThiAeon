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

namespace Trang_chủ_Main_Page_
{
    public partial class DatHang : Form
    {
        public DatHang()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.DatHang_Load);

        }

        private void CalculateTotal()
        {
            double total = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    total += double.Parse(row.Cells[4].Value.ToString());
                }
            }
            lblTotal.Text = $" {total:C}";
        }
        public void AddItem(string name, string Supplier, double cost, string icon)
        {
            var c = new HangHoaNCC()
            {
                Title = name,
                Supplier = Supplier,
                Cost = cost,
                Icon = Image.FromFile("Resources/" + icon),
            };

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
                        double costValue = double.Parse(item.Cells[3].Value.ToString().Replace("$", ""));
                        item.Cells[4].Value = ((currentQuantity + addQuantity) * costValue).ToString("C2");
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
        (addQuantity * double.Parse(us1.label4.Text.Replace("$", ""))).ToString("C2")
                });
                CalculateTotal();
            };

        }


        private void DatHang_Load(object sender, EventArgs e)
        {
            AddItem("Hàng 1", "Campuchai", 7.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 2", "Campuchai", 2012.25, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 3", "Campuchai", 9.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 1", "Campuchai", 7.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 2", "Campuchai", 2012.25, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 3", "Campuchai", 9.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 1", "Campuchai", 7.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 2", "Campuchai", 2012.25, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 3", "Campuchai", 9.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 1", "Campuchai", 7.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 2", "Campuchai", 2012.25, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            AddItem("Hàng 3", "Campuchai", 9.75, "z6338454431504_88b6a5b9be1edce907298e1dbea998ea.jpg");
            // muốn thêm ảnh vô thì phải vô Properties của ảnh rồi đổi Build action và Copy to output directory

        }
        //private Guna.UI2.WinForms.Guna2Panel guna2Panel3;

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
    }
}
