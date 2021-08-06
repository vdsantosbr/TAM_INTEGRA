using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;


namespace Business
{
    public class StatementRelatorioComentariosBUS
    {
        StatementRelatorioComentariosDAL dal = new StatementRelatorioComentariosDAL();
        public List<StatementRelatorioComentarios> RelatorioExportacaoDados(string id_conciliacao)
        {
            List<StatementRelatorioComentarios> lst = dal.RelatorioExportacaoDados(id_conciliacao);
            return lst;
        }
    }
}
