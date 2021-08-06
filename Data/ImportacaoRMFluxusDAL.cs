using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.Data;

namespace Data
{
    public class ImportacaoRMFluxusDAL
    {
        int retorno = 0;
        public List<ImportacaoRMFluxus> Lista(int ano = 0, int mes = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ano", (ano > 0) ? ano : 0);
                SqlParameter pMes = new SqlParameter("@mes", (mes > 0) ? mes : 0);

                var linha = db.Database.SqlQuery<ImportacaoRMFluxus>("EXEC STO_S_FIN_RM_FLUXUS @ANO, @MES", pAno, pMes).ToList();
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

        public bool GerarImagem()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<ImportacaoRMFluxus>("EXEC STO_I_FIN_RM_FLUXUS").ToList();
                if(retorno == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
