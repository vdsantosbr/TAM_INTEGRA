using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroGuiaDeclaracao
    {
        public string NF_COD_PROCESSO { get; set; }
        public string TIPO_DECLARACAO { get; set; }
        public string NF_NUMERO_DI { get; set; }
        public double NF_TAXA_DOLAR_REGISTRO_DI { get; set; }
        public DateTime NF_DATA_REGISTRO_DI { get; set; }
        public string NF_NUM_CONHECIMENTO { get; set; }
        public DateTime NF_DATA_CONHECIMENTO { get; set; }
    }
}
