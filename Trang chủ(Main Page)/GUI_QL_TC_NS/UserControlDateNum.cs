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
    public partial class UserControlDateNum : UserControl
    {
        public UserControlDateNum()
        {
            InitializeComponent();
        }

        public void setDay(String day)
        {
            lb_dateNumber.Text = day;
        }
    }
}
