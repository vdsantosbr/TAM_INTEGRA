using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ImportacaoProdutoDAL
    {
        public List<ImportacaoProduto> ListaProduto(int id_integracao = 0,string codTMV = null, string numeroMov = null, string partNumber = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? 0 : id_integracao);
                SqlParameter pCodTMV = new SqlParameter("@codTMV", (codTMV == null) ? (object)DBNull.Value : codTMV);
                SqlParameter PNumeroMov = new SqlParameter("@numeroMov", (numeroMov == null) ? (object)DBNull.Value : numeroMov);
                SqlParameter pPartNumber = new SqlParameter("@partNumber", (partNumber == null) ? (object)DBNull.Value : partNumber);

                var linha = db.Database.SqlQuery<ImportacaoProduto>("EXEC STO_S_RM_IMPORTSYS_PRODUTO @id_integracao, @codTMV, @numeroMov, @partNumber", pIdIntegracao, pCodTMV, PNumeroMov, pPartNumber).ToList();
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
