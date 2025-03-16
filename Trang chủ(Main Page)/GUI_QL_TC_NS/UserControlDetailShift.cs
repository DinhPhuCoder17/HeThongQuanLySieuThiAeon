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
    public partial class UserControlDetailShift : UserControl
    {
        public UserControlDetailShift()
        {
            InitializeComponent();
            lb_nameShift.AutoSize = false; // Tắt AutoSize để canh thủ công
            lb_nameShift.TextAlignment = ContentAlignment.MiddleCenter; // Canh nội dung của label
            lb_nameShift.Dock = DockStyle.Fill; // Dàn đều trong panel
            lb_nameShift.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            lb_timeDetail.AutoSize = false; // Tắt AutoSize để canh thủ công
            lb_timeDetail.TextAlignment = ContentAlignment.MiddleCenter; // Canh nội dung của label
            lb_timeDetail.Dock = DockStyle.Fill; // Dàn đều trong panel
            int x = (panel1.Width - lb_nameShift.Width) / 2;   // Căn giữa theo chiều ngang
            int y = (panel1.Height - lb_nameShift.Height) / 2;

            //lb_nameShift.Location = new Point(x, y + 100);

        }
    }
}
