using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class StatementConciliacaoBUS
    {
        StatementConciliacaoDAL dal = new StatementConciliacaoDAL();
        public List<StatementConciliacao> Analise(int ano = 0, int mes = 0, string id_conta = null, string po = null, string invoice = null,
            string so_ref = null)
        {
            return dal.Analise(ano, mes, id_conta, po, invoice, so_ref);
        }

        public List<StatementConciliacao> AnaliseItem(string id_conciliacao = null, string id_situacao = null, string id_classificacao = null, string id_departamento = null, string situacao = null, string PO = null, string invoice = null, string so_ref = null)
        {
            return dal.AnaliseItem(id_conciliacao, id_situacao, id_classificacao, id_departamento, situacao, PO, invoice,so_ref);
        }

        public List<StatementConciliacao> ConciliacaoItem(string id_conciliacao_item = null)
        {
            return dal.ConciliacaoItem(id_conciliacao_item);
        }

        public List<StatementConciliacao> Situacao(string descricao = null, string situacao = null)
        {
            return dal.Situacao(descricao, situacao);
        }
        public List<StatementConciliacao> PopulaHistorico(string invoice = null)
        {
            return dal.PopulaHistorico(invoice);
        }
        public List<StatementConciliacao> ConciliacaoItem(int id_conciliacao_item = 0)
        {
            return dal.ConciliacaoItem(id_conciliacao_item);
        }
        public List<StatementConciliacao> updateConciliacaoItem(int id_conciliacao, int id_conciliacao_item, int id_classificacao, int id_departamento,
            int id_pessoa, string invoice, string situacao_analise)
        {
            return dal.updateConciliacaoItem(id_conciliacao, id_conciliacao_item, id_classificacao, id_departamento, id_pessoa, invoice, situacao_analise);
        }
        public List<StatementConciliacao> insertObservacao(int id_conciliacao, int id_conciliacao_item, string invoice, string comentarios, int id_pessoa, int id_obs = 0)
        {
            return dal.insertObservacao(id_conciliacao, id_conciliacao_item, invoice, comentarios, id_pessoa, 0);
        }
        public List<StatementConciliacao> selectObservacao(string invoice)
        {
            return dal.selectObservacao(invoice);
        }
        public List<StatementConciliacao> PedidoCompra(string invoice)
        {
            return dal.PedidoCompra(invoice);
        }
        public List<StatementConciliacao> Financeiro(string invoice)
        {
            return dal.Financeiro(invoice);
        }
        public List<StatementConciliacao> Invoice(string invoice)
        {
            return dal.Invoice(invoice);
        }
        public List<StatementConciliacao> Historico(string invoice)
        {
            return dal.Historico(invoice);
        }
    }
}
