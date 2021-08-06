using Entities;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TipoMovimentoBUS
    {
        TipoMovimentoDAL dal = null;

        public TipoMovimentoBUS()
        {
            dal = new TipoMovimentoDAL();
        }

        public List<Entities.TipoMovimento> Lista(string formulario, int id_integracao)
        {
            List<Entities.TipoMovimento> lst = dal.Lista(formulario, id_integracao).ToList();
            return lst;
        }
    }
}
