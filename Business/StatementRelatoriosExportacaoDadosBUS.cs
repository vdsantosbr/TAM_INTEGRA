using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Business
{
    public class StatementRelatoriosExportacaoDadosBUS
    {
        StatementRelatorioExportacaoDadosDAL dal = new StatementRelatorioExportacaoDadosDAL();
        public List<StatementRelatorioExportacaoDados> RelatorioExportacaoDados(string id_conciliacao)
        {
            List<StatementRelatorioExportacaoDados> lst = dal.RelatorioExportacaoDados(id_conciliacao);
            return lst;
        }
    }
}
