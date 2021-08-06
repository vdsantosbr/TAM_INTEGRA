using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class FinanceiroDespesasBUS
    {
        FinanceiroDespesasDAL dal = new FinanceiroDespesasDAL();

        public List<FinanceiroDespesas> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, int faturamento, string codCredorDespesa, string codigoCredorDespesa, string processo, string situacao)
        {
            List<FinanceiroDespesas> lst = dal.Filtro(dataInicioDT, dataTerminoDT, faturamento, codCredorDespesa, codigoCredorDespesa, processo, situacao);
            return lst;
        }
        public List<FinanceiroDespesas> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<FinanceiroDespesas> lst = dal.Informe(id_integracao, sp_id, sp_id_despesa_processo).ToList();
            return lst;
        }

        public List<FinanceiroDespesas> Reprocessar(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<FinanceiroDespesas> lst = dal.Reprocessamento(id_integracao, sp_id, sp_id_despesa_processo);
            return lst;
        }

        public List<FinanceiroDespesas> Excluir(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<FinanceiroDespesas> lst = dal.Excluir(id_integracao, sp_id, sp_id_despesa_processo);
            return lst;
        }
        public List<FinanceiroDespesas> TiposDespesas()
        {
            List<FinanceiroDespesas> lst = dal.TiposDespesas();
            return lst;
        }
        public List<FinanceiroDespesas> Credor()
        {
            List<FinanceiroDespesas> lst = dal.Credor();
            return lst;
        }
    }
}
