using System;
using System.Collections.Generic;
using Data;
using Entities;

namespace Business
{
    public class StatementRelatorioInfoComplementaresBUS
    {
        StatementRelatorioInfoComplementaresDAL dal = new StatementRelatorioInfoComplementaresDAL();
        public List<StatementRelatorioInfoComplementares> RelatorioExportacaoDados(string id_conciliacao)
        {
            List<StatementRelatorioInfoComplementares> lst = dal.RelatorioExportacaoDados(id_conciliacao);
            return lst;
        }
    }
}
