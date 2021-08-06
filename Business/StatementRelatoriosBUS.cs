using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Business
{
    public class StatementRelatoriosBUS
    {
        StatementRelatoriosDAL dal = new StatementRelatoriosDAL();

        public List<StatementRelatorios> RelatorioExportacaoDados(string id_conciliacao)
        {
            List<StatementRelatorios> lst = dal.RelatorioExportacaoDados(id_conciliacao);
            return lst;
        }
    }
}
