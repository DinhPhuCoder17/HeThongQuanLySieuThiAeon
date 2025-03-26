using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;

namespace Trang_chủ_Main_Page_
{
    public partial class Mainpage : Form
    {
        public static int pageSelection = 1;

        DTO_Account account = new DTO_Account();
        public Mainpage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private BLL_Account bll_account = new BLL_Account();
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string userName = guna2TextBox1.Text;
            string password = guna2TextBox2.Text;

            if(bll_account.Login (userName, password))
            {
                loading load = new loading();
                load.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sike! That's the wrong number!");
            }

        }

        private void lblTk_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(guna2TextBox1.Text, out int value))
            {
                pageSelection = value; // Lưu giá trị từ TextBox
            }
        }

    }
}
