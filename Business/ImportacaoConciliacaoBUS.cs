using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class ImportacaoConciliacaoBUS
    {
        ImportacaoConciliacaoDAL dal = new ImportacaoConciliacaoDAL();

        public List<ImportacaoConciliacao> Lista(int ano, int mes)
        {
            List<ImportacaoConciliacao> lst = dal.Lista(ano, mes);
            return lst;
        }

        public string AgendarConciliacao(int idStatement, int idRMFluxus, int id_pessoa)
        {
            return dal.AgendarConciliacao(idStatement, idRMFluxus, id_pessoa);
        }

        public string ExecutarConciliacao()
        {
            return dal.ExecutarConciliacao();
        }
    }
}
