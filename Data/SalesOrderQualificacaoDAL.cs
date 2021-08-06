using Entities;
using System.Collections.Generic;
using System.Linq;
namespace Data
{
    public class SalesOrderQualificacaoDAL
    {
        public List<SalesOrderQualificacao> ListaQualificacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<SalesOrderQualificacao>("EXEC STO_SALL_FIN_SALESORDER_QUALIFICACAO").ToList();
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
