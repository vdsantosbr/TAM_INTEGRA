using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class FinanceiroFaturasBUS
    {
        FinanceiroFaturasDAL dal = new FinanceiroFaturasDAL();

        public List<FinanceiroFaturas> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, int faturamento, string numDI, string numInvoice, string numProcesso, string situacao)
        {
            List<FinanceiroFaturas> lst = dal.Filtro(dataInicioDT, dataTerminoDT, faturamento, numDI, numInvoice, numProcesso, situacao);
            return lst;
        }
        public List<FinanceiroFaturas> Informe(int id_integracao = 0, string id_fatura = null)
        {
            List<FinanceiroFaturas> lst = dal.Informe(id_integracao, id_fatura).ToList();
            return lst;
        }

        public List<FinanceiroFaturas> Reprocessar(int id_integracao = 0, string id_fatura = null, int id_pessoa = 0)
        {
            List<FinanceiroFaturas> lst = dal.Reprocessamento(id_integracao, id_fatura);
            return lst;
        }

        public List<FinanceiroFaturas> Excluir(int id_integracao = 0, string id_fatura = null, int id_pessoa = 0)
        {
            List<FinanceiroFaturas> lst = dal.Excluir(id_integracao, id_fatura);
            return lst;
        }
        public List<FinanceiroFaturas> TiposDespesas()
        {
            List<FinanceiroFaturas> lst = dal.TiposDespesas();
            return lst;
        }
        public List<FinanceiroFaturas> Credor()
        {
            List<FinanceiroFaturas> lst = dal.Credor();
            return lst;
        }
    }
}
