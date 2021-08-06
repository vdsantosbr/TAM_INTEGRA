using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Entities
{
    public class Home
    {
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public DateTime Data { get; set; }
        public string numeroPO { get; set; }
        public int Id_Integracao { get; set; }
        public string PO { get; set; }
        public int POQtd { get; set; }
        public int EDIQtd { get; set; }
        public int InvoiceQtd { get; set; }
        public int ProcessoQtd { get; set; }
        public int NFEstoqueQtd { get; set; }
        public int InconsistenciaQtd { get; set; }
        public string EDI { get; set; }
        public string Invoice { get; set; }
        public string Processo { get; set; }
        public string NFEstoque { get; set; }
        public string Inconsistencia { get; set; }
        public List<Home> lstWorkflow { get; set; }
    }
}
