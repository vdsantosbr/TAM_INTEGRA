using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroGuiaFaturaPDC
    {
        public string FILIAL { get; set; }
        public string TIPO_IMPORTACAO { get; set; }
        public string PEDIDO_COMPRA { get; set; }
        public string IMPORTSYS { get; set; }
        public string FAT_NUM_FATURA { get; set; }
        public string REC_NUCLEUS { get; set; }
        public int IDENTIFICADOR_PDC { get; set; }
        public int IDENTIFICADOR_REC { get; set; }
        public string COND_PGTO { get; set; }
    }
}
