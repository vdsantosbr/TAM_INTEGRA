using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StatementDepartamentoDAL
    {
        public List<StatementDepartamento> Lista(string situacao = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<StatementDepartamento>("EXEC STO_S_DEPARTAMENTO @situacao", pSituacao).ToList();
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
