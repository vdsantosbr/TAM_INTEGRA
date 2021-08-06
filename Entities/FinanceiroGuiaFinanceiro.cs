using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroGuiaFinanceiro
    {
        public string CAMBIOSYS { get; set; }
        public string MVTO { get; set; }
        public string NUM_MVTO { get; set; }
        public string PARCEIRO { get; set; }
        public string ENTIDADE { get; set; }
        public string COMPLEMENTO1 { get; set; }
        public string COMPLEMENTO2 { get; set; }
        public string TIPO { get; set; }
        public string CTT_CAMBIO { get; set; }
        public string REFERENCIA { get; set; }
        public string DOCUMENTO { get; set; }
        public double VALOR_ME { get; set; }
        public double VALOR_MN { get; set; }
        public string OPERACAO { get; set; }
        public DateTime? DATAEMISSAO { get; set; }
        public DateTime? DATA_DOCUMENTO { get; set; }
        public string INVOICE { get; set; }
        public string NUMERODOCUMENTO { get; set; }
        public string SITUACAO { get; set; }
        public DateTime? DATAVENCIMENTO { get; set; }
        public DateTime? DATABAIXA { get; set; }
        public string MOVIMENTO { get; set; }

    }
}
