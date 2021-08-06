using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroGuiaImpostos
    {
        public string ImportSys { get; set; }
        public string REC_NUCLEUS { get; set; }
        public string doc_fluxus { get; set; }
        public string EXPORTADOR { get; set; }
        public string CREDOR { get; set; }
        public DateTime VENCIMENTO { get; set; }
        public string strVENCIMENTO { get; set; }
        public string DESCRICAO { get; set; }
        public string MOEDA { get; set; }
        public double VR_PREVISTO { get; set; }
        public double VR_ADIANTADO { get; set; }
        public double VR_REAL { get; set; }
        public double VR_A_PAGAR { get; set; }
        public string SP_NUM_DOCUMENTO { get; set; }
        public DateTime DATA_CADASTRO { get; set; }
        public string strDataCadastro { get; set; }
        public DateTime DATA_LIBERACAO { get; set; }
        public string strDataLiberacao { get; set; }
        public int ID_INTEGRACAO { get; set; }
        public string SITUACAO { get; set; }
        public int NUCLEUS_ID { get; set; }
        public int FLUXUS_ID { get; set; }

    }
}
