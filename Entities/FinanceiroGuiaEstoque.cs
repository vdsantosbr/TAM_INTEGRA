using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroGuiaEstoque
    {
        public string BROKERSYS { get; set; }
        public string EMISSAO { get; set; }
        public string RECFISICO { get; set; }
        public double VR_FATURA { get; set; }
        public string TAXA_DI { get; set; }
        public double COFINS { get; set; }
        public double II { get; set; }
        public double IPI { get; set; }
        public double PIS { get; set; }
        public double FRETEINTER { get; set; }
        public double SEGURO { get; set; }
        public double VR_PRODUTO { get; set; }
        public double ICMS { get; set; }
        public double VR_NOTA { get; set; }
        public double DESPESA_COMPL { get; set; }
        public double VR_REC_FISICO { get; set; }
        public int ID_INTEGRACAO { get; set; }
        public string SITUACAO { get; set; }
        public string NF_ID { get; set; }
        public int IDMOV_EMISSAONF { get; set; }
        public int IDMOV_RECFISICO { get; set; }
        public string OPERACAO { get; set; }
        public string CONTA { get; set; }
        public double CREDITO { get; set; }
        public double DEBITO { get; set; }
        public string DESPESA { get; set; }
    }
}
