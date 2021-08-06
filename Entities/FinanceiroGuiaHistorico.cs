using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroGuiaHistorico
    {
        public string REGISTRO { get; set; }
        public string IMPORTSYS { get; set; }
        public string DOCUMENTO { get; set; }
        public double VALOR { get; set; }
        public int ID_INTEGRACAO { get; set; }
        public DateTime DATA { get; set; }
        public int ID_INT { get; set; }
        public string CODTMV { get; set; }
        public string CHAVEORIGEM_INT { get; set; }
        public string EMISSAO { get; set; }
        public string RECEBIMENTO { get; set; }
    }
}
