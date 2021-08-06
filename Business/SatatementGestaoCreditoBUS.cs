using Data;
using Entities;
using System.Collections.Generic;

namespace Business
{
    public class SatatementGestaoCreditoBUS
    {
        StatementGestaodeCreditoDAL dal = new StatementGestaodeCreditoDAL();

        public List<StatementGestaoCredito> ListaQualificacao()
        {
            List<StatementGestaoCredito> lst = dal.ListaQualificacao();
            return lst;
        }
        public List<StatementGestaoCredito> ListaComposicao()
        {
            List<StatementGestaoCredito> lst = dal.ListaComposicao();
            return lst;
        }
        public List<StatementGestaoCredito> ListaSaldo()
        {
            List<StatementGestaoCredito> lst = dal.ListaSaldo();
            return lst;
        }
        public List<StatementGestaoCredito> ListaAging()
        {
            List<StatementGestaoCredito> lst = dal.ListaAging();
            return lst;
        }
        public List<StatementGestaoCredito> ListaSituacao()
        {
            List<StatementGestaoCredito> lst = dal.ListaSituacao();
            return lst;
        }
        public List<StatementGestaoCredito> ListaSalesOrder(string id_conta = null, string qualificacao_so = null, string qualificacao_invoice = null,
            string qualificacao_saldo = null, string aging = null, string situacao = null, string referencia = null, string invoice = null, string num_pedido = null,
            string prefixo = null, string num_di = null, int id_os = 0, int id_pf = 0, string num_faturamento = null, string num_processo = null, int id_salesorder_lote = 0)
        {
            List<StatementGestaoCredito> lst = dal.ListaSalesOrder(id_conta, qualificacao_so, qualificacao_invoice, qualificacao_saldo,
                aging, situacao, referencia, invoice, num_pedido, prefixo, num_di, id_os, id_pf, num_faturamento, num_processo, id_salesorder_lote);
            return lst;
        }

        public List<StatementGestaoCredito> ListaSalesOrderInvoice(int id_salesorder=0)
        {
            List<StatementGestaoCredito> lst = dal.ListaSalesOrderInvoice(id_salesorder);
            return lst;
        }
        public List<StatementGestaoCredito> ListaSalesOrderAnalise(int id_salesorder = 0)
        {
            List<StatementGestaoCredito> lst = dal.ListaSalesOrderAnalise(id_salesorder);
            return lst;
        }
        public List<StatementGestaoCredito> SalvarComentario(int id_salesorder = 0, int id_pessoa = 0, string comentarios = null, int id_qualificacao = 0)
        {
            List<StatementGestaoCredito> lst = dal.SalvarComentario(id_salesorder, id_pessoa, comentarios, id_qualificacao);
            return lst;
        }
        public List<StatementGestaoCredito> ListaComentarios(int id_salesorder = 0)
        {
            List<StatementGestaoCredito> lst = dal.ListaComentarios(id_salesorder);
            return lst;
        }
    }
}
