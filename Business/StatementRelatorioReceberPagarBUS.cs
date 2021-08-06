using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;


namespace Business
{
    public class StatementRelatorioReceberPagarBUS
    {
        StatementRelatorioReceberPagarDAL dal = new StatementRelatorioReceberPagarDAL();
        public List<StatementRelatorioReceberPagar> RelatorioExportacaoDados(string id_conciliacao)
        {
            List<StatementRelatorioReceberPagar> lst = dal.RelatorioExportacaoDados(id_conciliacao);
            return lst;
        }
    }
}
