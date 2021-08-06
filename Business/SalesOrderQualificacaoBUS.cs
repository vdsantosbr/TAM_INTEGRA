using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class SalesOrderQualificacaoBUS
    {
        SalesOrderQualificacaoDAL dal = new SalesOrderQualificacaoDAL();

        public List<SalesOrderQualificacao> ListaQualificacao()
        {
            List<SalesOrderQualificacao> lst = dal.ListaQualificacao();
            return lst;
        }
    }
}
