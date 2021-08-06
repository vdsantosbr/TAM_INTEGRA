using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StatementConciliacaoDAL
    {
        public List<StatementConciliacao> Analise(int ano = 0, int mes = 0, string id_conta = null, string po = null, string invoice = null,
            string so_ref = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ano", (ano == 0) ? 0 : ano);
                SqlParameter pMes = new SqlParameter("@mes", (mes == 0) ? 0 : mes);
                SqlParameter pIdConta = new SqlParameter("@id_conta", (id_conta == null) ? string.Empty : id_conta);
                SqlParameter pPo = new SqlParameter("@po", (po == null) ? string.Empty : po);
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? string.Empty : invoice);
                SqlParameter pSoRef = new SqlParameter("@so_ref", (so_ref == null) ? string.Empty : so_ref);

                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_CONCILIACAO_ANALISE @ano, @mes, @id_conta, @po, @invoice, @so_ref", pAno, pMes, pIdConta, pPo, pInvoice, pSoRef).ToList();
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

        public List<StatementConciliacao> AnaliseItem(string id_conciliacao = null, string id_situacao = null, string id_classificacao = null, string id_departamento = null, string situacao = null, string PO = null, string invoice = null, string so_ref = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConciliacao = new SqlParameter("@id_conciliacao", (id_conciliacao == null) ? (object)DBNull.Value : id_conciliacao);
                SqlParameter pIdSituacao = new SqlParameter("@id_situacao", (id_situacao == null) ? (object)DBNull.Value : id_situacao);
                SqlParameter pIdClassificacao = new SqlParameter("@id_classificacao", (id_classificacao == null) ? (object)DBNull.Value : id_classificacao);
                SqlParameter pIdDepartamento = new SqlParameter("@id_departamento", (id_departamento == null) ? (object)DBNull.Value : id_departamento);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);
                SqlParameter pPO = new SqlParameter("@PO", (PO == null) ? (object)DBNull.Value : PO);
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pSORef = new SqlParameter("@so_ref", (so_ref == null) ? (object)DBNull.Value : so_ref);
                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_CONCILIACAO_ANALISE_ITEM @id_conciliacao, @id_situacao, @id_classificacao, @id_departamento, @situacao, @PO, @Invoice, @SO_Ref", pIdConciliacao, pIdSituacao, pIdClassificacao, pIdDepartamento, pSituacao, pPO, pInvoice, pSORef).ToList();
                    return linha;
                }
                catch (Exception e)
                {
                    var m = e.Message;
                    return null;
                }

            }
                
        }

        public List<StatementConciliacao> ConciliacaoItem(string id_conciliacao_item = null)
        {
            using (DatabaseContext db = new DatabaseContext())
           {
                SqlParameter pIdConciliacaoItem = new SqlParameter("@id_conciliacao_item", (id_conciliacao_item == null) ? (object)DBNull.Value : id_conciliacao_item);

                var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_CONCILIACAO_ITEM @id_conciliacao_item", pIdConciliacaoItem).ToList();
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

        public List<StatementConciliacao> PopulaHistorico(string invoice = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);

                var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_STATEMENT_HISTORICO @invoice", pInvoice).ToList();
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
        public List<StatementConciliacao> ConciliacaoItem(int id_conciliacao_item = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId_conciliacao_item = new SqlParameter("@id_conciliacao_item", (id_conciliacao_item == 0) ? (object)DBNull.Value : id_conciliacao_item);
                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_CONCILIACAO_ITEM @id_conciliacao_item", pId_conciliacao_item).ToList();
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
                    var m = e.Message;
                    return null;
                }
              
               
              
            }
        }

        public List<StatementConciliacao> Situacao(string descricao = null, string situacao = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDescricao = new SqlParameter("@descricao", (descricao == null) ? (object)DBNull.Value : descricao);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_SITUACAO @descricao, @situacao", pDescricao, pSituacao).ToList();
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

        public List<StatementConciliacao> updateConciliacaoItem(int id_conciliacao, int id_conciliacao_item, int id_classificacao, int id_departamento,
            int id_pessoa, string invoice, string situacao_analise)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConciliacao = new SqlParameter("@id_conciliacao", (id_conciliacao == 0) ? (object)DBNull.Value : id_conciliacao);
                SqlParameter pIdConciliacaoItem = new SqlParameter("@id_conciliacao_item", (id_conciliacao_item == 0) ? (object)DBNull.Value : id_conciliacao_item);
                SqlParameter pIdClassificacao = new SqlParameter("@id_classificacao", (id_classificacao == 0) ? (object)DBNull.Value : id_classificacao);
                SqlParameter pIdDepartamento = new SqlParameter("@id_departamento", (id_departamento == 0) ? (object)DBNull.Value : id_departamento);
                SqlParameter pIdPessoa = new SqlParameter("@id_pessoa", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pSituacaoAnalise = new SqlParameter("@situacao_Analise", (situacao_analise == null) ? (object)DBNull.Value : situacao_analise);

                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_I_FIN_CONCILIACAO_ANALISE @id_conciliacao, @id_conciliacao_item, @id_Classificacao, @id_departamento, @id_pessoa, @invoice, @situacao_analise", pIdConciliacao, pIdConciliacaoItem, pIdClassificacao, pIdDepartamento, pIdPessoa, pInvoice, pSituacaoAnalise).ToList();
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
                    var r = e.Message;
                    return null;
                }
               
            }
        }

        public List<StatementConciliacao> insertObservacao(int id_conciliacao, int id_conciliacao_item, string invoice, string comentarios, int id_pessoa, int id_obs = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConciliacao = new SqlParameter("@id_conciliacao", (id_conciliacao == 0) ? (object)DBNull.Value : id_conciliacao);
                SqlParameter pIdConciliacaoItem = new SqlParameter("@id_conciliacao_item", (id_conciliacao_item == 0) ? (object)DBNull.Value : id_conciliacao_item);
                SqlParameter pIdPessoa = new SqlParameter("@id_pessoa", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pComentarios = new SqlParameter("@descricao", (comentarios == null) ? (object)DBNull.Value : comentarios);

                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_I_FIN_OBS @id_conciliacao, @id_conciliacao_item, @id_pessoa, @invoice, @descricao", pIdConciliacao, pIdConciliacaoItem, pIdPessoa, pInvoice, pComentarios).ToList();
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
                    var r = e.Message;
                    return null;
                }

             
            }
        }
        public List<StatementConciliacao> selectObservacao(string invoice)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);
                //SqlParameter pIdConciliacaoItem = new SqlParameter("@id_conciliacao_item", (id_conciliacao_item == 0) ? (object)DBNull.Value : id_conciliacao_item);

              try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_OBS @invoice", pInvoice).ToList();
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

        public List<StatementConciliacao> PedidoCompra(string invoice)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);

                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_RM_COMPRA @invoice", pInvoice).ToList();
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
                    var r = e.Message;
                    return null;
                }

            }
        }
        public List<StatementConciliacao> Invoice(string invoice)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);

                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_RM_INVOICE @invoice", pInvoice).ToList();
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
                    var r = e.Message;
                    return null;
                }

            }
        }
        public List<StatementConciliacao> Financeiro(string invoice)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);

                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_RM_FATURA @invoice", pInvoice).ToList();
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
                    var r = e.Message;
                    return null;
                }

            }
        }
        public List<StatementConciliacao> Historico(string invoice)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);

                try
                {
                    var linha = db.Database.SqlQuery<StatementConciliacao>("EXEC STO_S_FIN_STATEMENT_HISTORICO @invoice", pInvoice).ToList();
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
                    var r = e.Message;
                    return null;
                }

            }
        }
    }
}
