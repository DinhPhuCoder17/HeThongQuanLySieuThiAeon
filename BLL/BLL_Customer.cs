using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BLL_Customer
    {
        private readonly DAL_Customer dAL_Customer = new DAL_Customer();

        public DataTable loadCustomer()
        {
            return dAL_Customer.loadCustomer();
        }
    }
}
