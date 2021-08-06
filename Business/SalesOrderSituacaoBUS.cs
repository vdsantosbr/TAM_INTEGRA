﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class SalesOrderSituacaoBUS
    {
        SalesOrderSituacaoDAL dal = new SalesOrderSituacaoDAL();

        public List<SalesOrderSituacao> ListaSituacao()
        {
            List<SalesOrderSituacao> lst = dal.ListaSituacao();
            return lst;
        }
    }
}
