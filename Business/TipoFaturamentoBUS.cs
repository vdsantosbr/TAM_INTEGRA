using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Business
{
    public class TipoFaturamentoBUS
    {
        TipoFaturamentoDAL dal = new TipoFaturamentoDAL();

        public List<TipoFaturamento> ListaTipoFaturamento()
        {
            List<TipoFaturamento> situacao = dal.ListaTipoFaturamento();
            return situacao;
        }
    }
}
