using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class SalesOrderComposicaoBUS
    {
        SalesOrderComposicaoDAL dal = new SalesOrderComposicaoDAL();

        public List<SalesOrderComposicao> ListaComposicao()
        {
            List<SalesOrderComposicao> lst = dal.ListaComposicao();
            return lst;
        }
    }
}
