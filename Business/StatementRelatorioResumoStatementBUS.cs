using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;
namespace Business
{
   public  class StatementRelatorioResumoStatementBUS
    {
        StatementRelatorioResumoStatementDAL dal = new StatementRelatorioResumoStatementDAL();
        public List<StatementRelatorioResumoStatement> RelatorioExportacaoDados(string id_conciliacao)
        {
            List<StatementRelatorioResumoStatement> lst = dal.RelatorioExportacaoDados(id_conciliacao);
            return lst;
        }
    }
}
