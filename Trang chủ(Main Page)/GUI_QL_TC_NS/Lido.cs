using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trang_chu_Main_Page_.GUI_QL_TC_NS
{
    public partial class Lido : Form
    {
        public Lido()
        {
            InitializeComponent();
        }

        private void Lido_Load(object sender, EventArgs e)
        {

        }
        public string Reason { get; private set; }
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Lấy lý do từ TextBox khi người dùng nhấn OK
            Reason = txtReason.Text;  // Lấy nội dung từ TextBox txtReason

            // Đóng Form và trả kết quả OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Nếu người dùng nhấn Cancel, Form sẽ đóng mà không lưu lý do
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
