using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Data
{
    public class SalesOrderComposicaoDAL
    {
        public List<SalesOrderComposicao> ListaComposicao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<SalesOrderComposicao>("EXEC STO_SALL_FIN_SALESORDER_COMPOSICAO").ToList();
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
