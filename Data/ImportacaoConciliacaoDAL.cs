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
    public class ImportacaoConciliacaoDAL
    {
        public List<ImportacaoConciliacao> Lista(int ano = 0, int mes = 0)
        {
            using(DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ano", (ano == 0) ? (object)DBNull.Value : ano);
                SqlParameter pMes = new SqlParameter("@mes", (mes == 0) ? (object)DBNull.Value : mes);

                var linha = db.Database.SqlQuery<ImportacaoConciliacao>("EXEC STO_S_FIN_CONCILIACAO @ANO, @MES", pAno, pMes).ToList();
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

        public string AgendarConciliacao(int idStatement, int idRMFluxus, int id_pessoa)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdStatement = new SqlParameter("@id_statement", (idStatement == 0) ? (object)DBNull.Value : idStatement);
                SqlParameter pidRMFluxus = new SqlParameter("@id_RM_Fluxus", (idRMFluxus == 0) ? (object)DBNull.Value : idRMFluxus);
                SqlParameter pId_Pessoa = new SqlParameter("@id_pessoa", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoConciliacao>("EXEC STO_I_FIN_CONCILIACAO @id_statement, @id_RM_Fluxus, @ID_PESSOA", pIdStatement, pidRMFluxus, pId_Pessoa).ToList();
                    return "Conciliação agendada com sucesso";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
              
            }
        }

        public string ExecutarConciliacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoConciliacao>("EXEC STO_I_FIN_CONCILIACAO").ToList();
                    return "Conciliação executada com sucesso";
                }
                catch (Exception e)
                {
                    return e.Message;
                }

            }
        }
    }
}
