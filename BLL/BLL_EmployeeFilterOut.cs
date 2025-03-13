using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DAL;
using System.Data.SqlClient;

namespace BLL
{

    public class BLL_EmployeeFilterOut
    {
        private readonly DAL_EmployeeFilterOut dalemployeefilterout = new DAL_EmployeeFilterOut();

        public DataTable loadEmployee()
        {
            return dalemployeefilterout.loadEmployeeList();
        }
    }
}
