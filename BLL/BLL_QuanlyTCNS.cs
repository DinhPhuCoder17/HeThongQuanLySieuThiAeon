using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BLL_QuanlyTCNS
    {

        private readonly DAL_QuanlyTCNS dAL_QuanlyTCNS = new DAL_QuanlyTCNS();
        public DataTable xemDSNV()
        {
            return dAL_QuanlyTCNS.xemDSNV();
        }

        public DataTable xemDSKH()
        {
            return dAL_QuanlyTCNS.xemDSKH();
        }

    }
}
