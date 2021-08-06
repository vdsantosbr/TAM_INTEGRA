using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data
{
    public class StatementGestaodeCreditoDAL
    {
        public List<StatementGestaoCredito> ListaQualificacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_SALL_FIN_SALESORDER_QUALIFICACAO").ToList();
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

        public List<StatementGestaoCredito> ListaComposicao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_SALL_FIN_SALESORDER_COMPOSICAO").ToList();
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
        public List<StatementGestaoCredito> ListaSaldo()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_SALL_FIN_SALESORDER_SALDO").ToList();
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
        public List<StatementGestaoCredito> ListaAging()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_SALL_FIN_SALESORDER_AGING").ToList();
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

        public List<StatementGestaoCredito> ListaSituacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_SALL_FIN_SALESORDER_SITUACAO").ToList();
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
        public List<StatementGestaoCredito> ListaSalesOrder(string id_conta = null, string qualificacao_so = null, string qualificacao_invoice = null,
            string qualificacao_saldo = null, string aging = null, string situacao = null, string referencia = null, string invoice = null, string num_pedido = null,
            string prefixo = null, string num_di = null, int id_os = 0, int id_pf = 0, string num_faturamento = null, string num_processo = null, int id_salesorder_lote = 0)

        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConta = new SqlParameter("@id_conta", (id_conta == null) ? (object)DBNull.Value : id_conta);
                SqlParameter pqualificacao_so = new SqlParameter("@id_qualificacao", (qualificacao_so == null) ? (object)DBNull.Value : qualificacao_so);
                SqlParameter pqualificacao_invoice = new SqlParameter("@qualificacao_invoice", (qualificacao_invoice == null) ? (object)DBNull.Value : qualificacao_invoice);
                SqlParameter pqualificacao_saldo = new SqlParameter("@qualificacao_saldo", (qualificacao_saldo == null) ? (object)DBNull.Value : qualificacao_saldo);
                SqlParameter paging = new SqlParameter("@aging", (aging == null) ? (object)DBNull.Value : aging);
                SqlParameter psituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);
                SqlParameter preferencia = new SqlParameter("@referencia", (referencia == null) ? (object)DBNull.Value : referencia);
                SqlParameter pinvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pnum_pedido = new SqlParameter("@num_pedido", (num_pedido == null) ? (object)DBNull.Value : num_pedido);
                SqlParameter pprefixo = new SqlParameter("@prefixo", (prefixo == null) ? (object)DBNull.Value : prefixo);
                SqlParameter pnum_di = new SqlParameter("@num_di", (num_di == null) ? (object)DBNull.Value : num_di);
                SqlParameter pid_os = new SqlParameter("@id_os", (id_os == 0) ? (object)DBNull.Value : id_os);
                SqlParameter pid_pf = new SqlParameter("@id_pf", (id_pf == 0) ? (object)DBNull.Value : id_pf);
                SqlParameter pnum_faturamento = new SqlParameter("@num_faturamento", (num_faturamento == null) ? (object)DBNull.Value : num_faturamento);
                SqlParameter pnum_processo = new SqlParameter("@num_processo", (num_processo == null) ? (object)DBNull.Value : num_processo);
                SqlParameter pid_salesorder_lote = new SqlParameter("@id_salesorder_lote", (id_salesorder_lote == 0) ? (object)DBNull.Value : id_salesorder_lote);

                try
                {
                    var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_S_FIN_SALESORDER @id_conta, @id_qualificacao, @qualificacao_invoice, @qualificacao_saldo, @aging, @situacao, @referencia, @invoice, @num_pedido, @prefixo, @num_di, @id_os, @id_pf, @num_faturamento, @num_processo, @ID_SALESORDER_LOTE",
                    pIdConta, pqualificacao_so, pqualificacao_invoice, pqualificacao_saldo, paging, psituacao, preferencia, pinvoice, pnum_pedido, pprefixo, pnum_di, pid_os, pid_pf, pnum_faturamento, pnum_processo, pid_salesorder_lote).ToList();

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
                    var ex = e.Message;
                    return null;
                }

            }
        }
        public List<StatementGestaoCredito> ListaSalesOrderAnalise(int id_salesorder = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pid_salesorder = new SqlParameter("@id_salesorder", (id_salesorder == 0) ? (object)DBNull.Value : id_salesorder);
                try
                {
                    var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_S_FIN_SALESORDER_Analise @id_salesorder", pid_salesorder).ToList();
                
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
                    var ex = e.Message;
                    return null;
                }

            }
        }

    public List<StatementGestaoCredito> ListaSalesOrderInvoice(int id_salesorder = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pid_salesorder = new SqlParameter("@id_salesorder", (id_salesorder == 0) ? (object)DBNull.Value : id_salesorder);

               
                try
                {
                    var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_S_FIN_SALESORDER_INVOICE @id_salesorder", pid_salesorder).ToList();
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
                    var ex = e.Message;
                    return null;
                }

            }
        }

    public List<StatementGestaoCredito> SalvarComentario(int id_salesorder = 0, int id_pessoa = 0, string comentarios = null, int id_qualificacao = 0)
    {
        using (DatabaseContext db = new DatabaseContext())
        {
            SqlParameter pid_salesorder = new SqlParameter("@id_salesorder", (id_salesorder == 0) ? (object)DBNull.Value : id_salesorder);
            SqlParameter pid_pessoa = new SqlParameter("@id_pessoa", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);
            SqlParameter pdescricao = new SqlParameter("@descricao", (comentarios == null) ? (object)DBNull.Value : comentarios);
            SqlParameter pid_qualificacao = new SqlParameter("@id_qualificacao", (id_qualificacao == 0) ? (object)DBNull.Value : id_qualificacao);

                try
            {
                var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_U_FIN_SALESORDER @id_salesorder, @id_qualificacao, @descricao, @id_pessoa", pid_salesorder, pid_qualificacao, pdescricao, pid_pessoa).ToList();
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
                var ex = e.Message;
                return null;
            }

        }
    }
        public List<StatementGestaoCredito> ListaComentarios(int id_salesorder = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pid_salesorder = new SqlParameter("@id_salesorder", (id_salesorder == 0) ? (object)DBNull.Value : id_salesorder);

                try
                {
                    var linha = db.Database.SqlQuery<StatementGestaoCredito>("EXEC STO_S_FIN_SALESORDER_COMENTARIO @id_salesorder", pid_salesorder).ToList();
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
                    var ex = e.Message;
                    return null;
                }

            }
        }
    }
}
    
  
