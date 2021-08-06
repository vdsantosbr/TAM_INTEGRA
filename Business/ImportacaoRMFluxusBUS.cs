using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class ImportacaoRMFluxusBUS
    {
        ImportacaoRMFluxusDAL dal = new ImportacaoRMFluxusDAL();

        public List<ImportacaoRMFluxus> Lista(int ano, int mes)
        {
            List<ImportacaoRMFluxus> lst = dal.Lista(ano, mes);
            return lst;
        }

        public bool GerarImagem()
        {
            return dal.GerarImagem();

        }
    }
}
