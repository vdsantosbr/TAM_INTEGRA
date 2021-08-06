using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class FinanceiroProcessoBUS
    {
        FinanceiroProcessoDAL dal = new FinanceiroProcessoDAL();

        public List<FinanceiroProcesso> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string numProcesso, string numDI)
        {
            List<FinanceiroProcesso> lst = dal.Filtro(dataInicioDT, dataTerminoDT, numProcesso, numDI);
            return lst;
        }
        public List<FinanceiroProcesso> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<FinanceiroProcesso> lst = dal.Informe(id_integracao, sp_id, sp_id_despesa_processo).ToList();
            return lst;
        }

        public List<FinanceiroProcesso> Reprocessar(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<FinanceiroProcesso> lst = dal.Reprocessamento(id_integracao, sp_id, sp_id_despesa_processo);
            return lst;
        }
        public List<FinanceiroProcesso> TiposDespesas()
        {
            List<FinanceiroProcesso> lst = dal.TiposDespesas();
            return lst;
        }
        public List<FinanceiroProcesso> Declaracao()
        {
            List<FinanceiroProcesso> lst = dal.Declaracao();
            return lst;
        }
        public List<FinanceiroProcesso> Guia(string codProcesso = null)
        {
            List<FinanceiroProcesso> lst = dal.Guia(codProcesso);
            return lst;
        }
        public List<FinanceiroGuiaFatura> InformeFatura(string codProcesso = null)
        {
            List<FinanceiroGuiaFatura> lst = dal.InformeFatura(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaImpostos> InformeImpostos(string codProcesso = null)
        {
            List<FinanceiroGuiaImpostos> lst = dal.InformeImpostos(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaDespesas> InformeDespesas(string codProcesso = null)
        {
            List<FinanceiroGuiaDespesas> lst = dal.InformeDespesas(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaEstoque> InformeEstoque(string codProcesso = null)
        {
            List<FinanceiroGuiaEstoque> lst = dal.InformeEstoque(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaEstoque> InformeEstoqueLACCTB(string codProcesso = null)
        {
            List<FinanceiroGuiaEstoque> lst = dal.InformeEstoqueLACCTB(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaHistorico> InformeHistorico(string codProcesso = null)
        {
            List<FinanceiroGuiaHistorico> lst = dal.InformeHistorico(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaDeclaracao> InformeDeclaracao(string codProcesso = null)
        {
            List<FinanceiroGuiaDeclaracao> lst = dal.InformeDeclaracao(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaFinanceiro> InformeFinanceiro(string codProcesso = null)
        {
            List<FinanceiroGuiaFinanceiro> lst = dal.InformeFinanceiro(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaNotaCompl> InformeNotacompl(string codProcesso = null)
        {
            List<FinanceiroGuiaNotaCompl> lst = dal.InformeNotaCompl(codProcesso).ToList();
            return lst;
        }
        public List<FinanceiroGuiaFaturaPDC> InformeFaturaPDC(string codProcesso = null)
        {
            List<FinanceiroGuiaFaturaPDC> lst = dal.InformeFaturaPDC(codProcesso).ToList();
            return lst;
        }
    }
}
