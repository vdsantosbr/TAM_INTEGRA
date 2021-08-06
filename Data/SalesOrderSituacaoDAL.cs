using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    public class SalesOrderSituacaoDAL
    {
        public List<SalesOrderSituacao> ListaSituacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<SalesOrderSituacao>("EXEC STO_SALL_FIN_SALESORDER_SITUACAO").ToList();
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
