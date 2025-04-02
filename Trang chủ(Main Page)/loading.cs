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
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Timer_Click(object sender, EventArgs e)
        {

            if (guna2CircleProgressBar1.Value == 100) // Khi progress bar đầy 100%
            {
                timer1.Stop();

                // Kiểm tra role để mở đúng trang
                if (Mainpage.pageSelection == 1)
                {
                    EmployeeMainPage employee = new EmployeeMainPage();
                    employee.Show();
                }
                else if (Mainpage.pageSelection == 2)
                {
                    Form1 formQLHH = new Form1();
                    formQLHH.Show();
                }
                else if (Mainpage.pageSelection == 3)
                {
                    Admin admin = new Admin();
                    admin.Show();
                }

                this.Hide();
            }

            // Cập nhật progress bar
            guna2CircleProgressBar1.Value += 1;
            lbl_load_num.Text = guna2CircleProgressBar1.Value.ToString() + "%";
        }

        private void loading_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            timer1.Start();
        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
