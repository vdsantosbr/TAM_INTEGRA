using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatementRelatorios
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Conta { get; set; }
        public string Relatorio { get; set; }
        public List<StatementConciliacao> lstAnalise { get; set; }
        public List<StatementRelatorioExportacaoDados> lstRelatorio { get; set; }
    }
}
