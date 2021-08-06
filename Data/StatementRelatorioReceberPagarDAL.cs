using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Data
{
    public class StatementRelatorioReceberPagarDAL
    {
        public List<StatementRelatorioReceberPagar> RelatorioExportacaoDados(string id_conciliacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConciliacao = new SqlParameter("@SELECT_CONCILIACAO ", (id_conciliacao == null) ? (object)DBNull.Value : id_conciliacao);

                var linha = db.Database.SqlQuery<StatementRelatorioReceberPagar>("EXEC STO_REL_CONCILIACAO_RECEBER_PAGAR @SELECT_CONCILIACAO", pIdConciliacao).ToList();
                try
                {
                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }
            }
        }
    }
}
