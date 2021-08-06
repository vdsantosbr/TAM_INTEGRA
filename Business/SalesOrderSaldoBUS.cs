using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class SalesOrderSaldoBUS
    {
        SalesOrderSaldoDAL dal = new SalesOrderSaldoDAL();

        public List<SalesOrderSaldo> ListaComposicao()
        {
            List<SalesOrderSaldo> lst = dal.ListaSaldo();
            return lst;
        }
    }
}
