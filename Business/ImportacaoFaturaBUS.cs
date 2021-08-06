using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class ImportacaoFaturaBUS
    {
        ImportacaoFaturaDAL dal = new ImportacaoFaturaDAL();

        public List<ImportacaoFatura> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string tipoFatura, string numProcesso, string numDI, string numInvoice, string situacao)
        {
            List<ImportacaoFatura> lst = dal.Filtro(dataInicioDT, dataTerminoDT, tipoFatura, numProcesso, numDI, numInvoice, situacao);
            return lst;
        }
        public List<ImportacaoFatura> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<ImportacaoFatura> lst = dal.Informe(id_integracao, sp_id, sp_id_despesa_processo).ToList();
            return lst;
        }

        public List<ImportacaoFatura> Cancelar(int id_integracao = 0, string fat_fatura_id = null)
        {
            List<ImportacaoFatura> lst = dal.Cancelar(id_integracao, fat_fatura_id).ToList();
            return lst;
        }

        public List<ImportacaoFatura> Reprocessar(int id_integracao = 0, string id_fatura = null, int id_pessoa = 0)
        {
            List<ImportacaoFatura> lst = dal.Reprocessamento(id_integracao, id_fatura);
            return lst;
        }
        public List<ImportacaoFatura> TiposDespesas()
        {
            List<ImportacaoFatura> lst = dal.TiposDespesas();
            return lst;
        }
        public List<ImportacaoFatura> Credor()
        {
            List<ImportacaoFatura> lst = dal.Credor();
            return lst;
        }
    }
}
