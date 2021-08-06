using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class FinanceiroServicosBUS
    {
        FincanceiroServicosDAL dal = new FincanceiroServicosDAL();

        public List<FinanceiroServicos> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string codTMV, string numeroMov, string invoice, string documento, string situacao, int id_integracao)
        {
            List<FinanceiroServicos> lst = dal.Filtro(dataInicioDT, dataTerminoDT, codTMV, numeroMov, invoice, documento, situacao, id_integracao);
            return lst;
        }
        //public List<Cavok> Informe(int id_integracao = 0, int id_fatura = 0)
        //{
        //    List<FinanceiroServicos> lst = dal.Informe(id_integracao, id_fatura).ToList();
        //    return lst;
        //}

        public List<FinanceiroServicos> Reprocessar(int id_integracao = 0, int idMov = 0, int idLan = 0, int id_pessoa = 0)
        {
            List<FinanceiroServicos> lst = dal.Reprocessamento(id_integracao, idMov, idLan, id_pessoa);
            return lst;
        }

        public List<FinanceiroServicos> Excluir(int id_integracao = 0, int idMov = 0, int idLan = 0, int id_pessoa = 0)
        {
            List<FinanceiroServicos> lst = dal.Excluir(id_integracao, idMov, idLan, id_pessoa);
            return lst;
        }
        public List<FinanceiroServicos> Informativo(int id_integracao = 0, int idMov = 0)
        {
            List<FinanceiroServicos> lst = dal.Informe(id_integracao, idMov);
            return lst;
        }
        public int Integracao_Processo(string formulario)
        {
           int lst = dal.Integracao_Processo(formulario);
            return lst;
        }
        public List<TipoFaturamento> ListaTipoFaturamento()
        {
            List<TipoFaturamento> situacao = dal.ListaTipoFaturamento();
            return situacao;
        }
    }
}
