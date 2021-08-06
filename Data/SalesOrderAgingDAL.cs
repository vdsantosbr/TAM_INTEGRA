using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    public class SalesOrderAgingDAL
    {
        public List<SalesOrderAging> ListaAging()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<SalesOrderAging>("EXEC STO_SALL_FIN_SALESORDER_AGING").ToList();
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
