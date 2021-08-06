using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class SalesOrderAgingBUS
    {
        SalesOrderAgingDAL dal = new SalesOrderAgingDAL();
        public List<SalesOrderAging> ListaAging()
        {
            List<SalesOrderAging> lst = dal.ListaAging();
            return lst;
        }
    }
}
