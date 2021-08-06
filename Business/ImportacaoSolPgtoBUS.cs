using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class ImportacaoSolPgtoBUS
    {
        ImportacaoSolPgtoDAL dal = new ImportacaoSolPgtoDAL();

        public List<ImportacaoSolPgto> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT,string tipoDespesa, string codCredorDespesa, string processo, string situacao)
        {
            List<ImportacaoSolPgto> lst = dal.Filtro(dataInicioDT, dataTerminoDT, tipoDespesa, codCredorDespesa, processo, situacao);
            return lst;
        }
        public List<ImportacaoSolPgto> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<ImportacaoSolPgto> lst = dal.Informe(id_integracao, sp_id, sp_id_despesa_processo).ToList();
            return lst;
        }

        public List<ImportacaoSolPgto> Reprocessar(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            List<ImportacaoSolPgto> lst = dal.Reprocessar(id_integracao, id_brokersys, numnf_brokersys);
            return lst;
        }
        public List<ImportacaoSolPgto> TiposDespesas()
        {
            List<ImportacaoSolPgto> lst = dal.TiposDespesas();
            return lst;
        }
        public List<ImportacaoSolPgto> Credor()
        {
            List<ImportacaoSolPgto> lst = dal.Credor();
            return lst;
        }
    }
}
