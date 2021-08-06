using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Data
{
    public class CavokDAL
    {
        public List<Cavok> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, int faturamento, string numeroMov, string situacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@DATA_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@DATA_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                SqlParameter pTipoFaturamento = new SqlParameter("@TIPO_FATURAMENTO", (faturamento == 0) ? (object)DBNull.Value : faturamento);
                SqlParameter pNumeroMov = new SqlParameter("@NUMEROMOV", (numeroMov == null) ? (object)DBNull.Value : numeroMov);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<Cavok>("EXEC STO_S_CAVOK_DOCUMENTO @DATA_INICIO, @DATA_TERMINO,@TIPO_FATURAMENTO, @NUMEROMOV, @SITUACAO", pDataInicio, pDataFim, pTipoFaturamento, pNumeroMov, pSituacao).ToList();

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

        public List<Cavok> Informe(int id_integracao = 0, int id_fatura = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdFatura = new SqlParameter("@id_fatura", (id_fatura == 0) ? (object)DBNull.Value : id_fatura);

                try
                {
                    var linha = db.Database.SqlQuery<Cavok>("EXEC STO_S_CAVOK_DOCUMENTO_Info @ID_INTEGRACAO, @ID_FATURA", pIdIntegracao, pIdFatura).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception e)
                {
                    var erro = e.Message;
                    return null;
                }
                
            }
        }

        public List<Cavok> Reprocessamento(int id_integracao = 0, int id_fatura = 0, int id_pessoa = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdFatura = new SqlParameter("@id_fatura", (id_fatura == 0) ? (object)DBNull.Value : id_fatura);
                SqlParameter pIdPessoa = new SqlParameter("@id_pessoa", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);

                try
                {
                    var linha = db.Database.SqlQuery<Cavok>("EXEC STO_U_CAVOK_REPROCESSAR @ID_INTEGRACAO, @ID_FATURA, @id_pessoa", pIdIntegracao, pIdFatura, pIdPessoa).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception e)
                {
                    var erro = e.Message;
                    return null;
                }
                
            }
        }
    }
}
