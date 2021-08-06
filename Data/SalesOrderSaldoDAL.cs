using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    public class SalesOrderSaldoDAL
    {
        public List<SalesOrderSaldo> ListaSaldo()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<SalesOrderSaldo>("EXEC STO_SALL_FIN_SALESORDER_SALDO").ToList();
                if (linha.Count > 0)
                {
                    return linha;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
