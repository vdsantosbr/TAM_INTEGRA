using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Data
{
    public class FinanceiroProFormaDAL
    {
        public List<FinanceiroProForma> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string codTMV, string numeroMov, string invoice, string documento, string situacao, int id_integracao = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@DATA_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@DATA_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                SqlParameter pCodTMV = new SqlParameter("@CODTMV", (codTMV == null) ? (object)DBNull.Value : codTMV);
                SqlParameter pNumeroMov = new SqlParameter("@NUMEROMOV", (numeroMov == null) ? (object)DBNull.Value : numeroMov);
                SqlParameter pInvoice = new SqlParameter("@INVOICE", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pDocumento = new SqlParameter("@DOCUMENTO", (documento == null) ? (object)DBNull.Value : documento);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", (situacao == null) ? (object)DBNull.Value : situacao);
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);

                var linha = db.Database.SqlQuery<FinanceiroProForma>("EXEC STO_S_RM_SERVICO_SOFTWAY @DATA_INICIO, @DATA_TERMINO,@CODTMV, @NUMEROMOV, @INVOICE, @DOCUMENTO, @SITUACAO, @ID_INTEGRACAO", pDataInicio, pDataFim, pCodTMV, pNumeroMov, pInvoice, pDocumento, pSituacao, pIdIntegracao).ToList();

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
                catch(Exception e)
                {
                    var erro = e.Message;
                    return null;
                }
            }
        }

        public List<FinanceiroServicos> Informe(int id_integracao = 0, int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? 0 : id_integracao);
                SqlParameter pIdMov = new SqlParameter("@idMov", (idMov == 0) ? (object)DBNull.Value : idMov);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroServicos>("EXEC STO_S_RM_SERVICO_SOFTWAY_Info @ID_INTEGRACAO, @IDMOV", pIdIntegracao, pIdMov).ToList();

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

        public List<FinanceiroServicos> Reprocessamento(int id_integracao = 0, int idMov = 0, int idLan = 0, int id_pessoa = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdMov = new SqlParameter("@idMov", (idMov == 0) ? (object)DBNull.Value : idMov);
                SqlParameter pIdLan = new SqlParameter("@idLan", (idLan == 0) ? (object)DBNull.Value : idLan);
                SqlParameter pIdPessoa = new SqlParameter("@id_pessoa", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);
                SqlParameter pAcao = new SqlParameter("@ACAO", "R");

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroServicos>("EXEC STO_S_RM_REPROCESSAR_SERVICO_SOFTWAY @ID_INTEGRACAO, @IDMOV, @IDLAN, @ACAO, @id_pessoa", pIdIntegracao, pIdMov, pIdLan, pAcao, pIdPessoa).ToList();

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
                    List<FinanceiroServicos> lst = new List<FinanceiroServicos>();
                    lst.Add(new FinanceiroServicos
                    {
                        Comentario = e.Message,
                        OBSERVACAO = "Erro"
                    });
                    return lst.ToList();
                }

            }
        }

        public List<FinanceiroServicos> Excluir(int id_integracao = 0, int idMov = 0, int idLan = 0, int id_pessoa = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdMov = new SqlParameter("@idMov", (idMov == 0) ? (object)DBNull.Value : idMov);
                SqlParameter pIdLan = new SqlParameter("@idLan", (idLan == 0) ? (object)DBNull.Value : idLan);
                SqlParameter pIdPessoa = new SqlParameter("@id_pessoa", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);
                SqlParameter pAcao = new SqlParameter("@ACAO", "E");

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroServicos>("EXEC STO_S_RM_REPROCESSAR_SERVICO_SOFTWAY @ID_INTEGRACAO, @IDMOV, @IDLAN, @ACAO, @id_pessoa", pIdIntegracao, pIdMov, pIdLan, pAcao, pIdPessoa).ToList();

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
                    List<FinanceiroServicos> lst = new List<FinanceiroServicos>();
                    lst.Add(new FinanceiroServicos
                    {
                        Comentario = e.Message,
                        OBSERVACAO = "Erro"
                    });
                    return lst.ToList();
                }

            }
        }

        public int Integracao_Processo(string formulario)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pFormulario = new SqlParameter("@FORMULARIO", (formulario == null) ? (object)DBNull.Value : formulario);

                try
                {
                    var linha = db.Database.SqlQuery<int>("EXEC STO_S_FORMULARIO_PROCESSO @FORMULARIO", pFormulario).Single();

                    if (linha != 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception e)
                {
                    return 0;
                }

            }
        }

        public List<TipoFaturamento> ListaTipoFaturamento()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<TipoFaturamento>("EXEC STO_S_RM_MOVIMENTO_SERVICO_SOFTWAY").ToList();

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
