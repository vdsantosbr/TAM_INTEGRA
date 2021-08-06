using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Entities
{
    public class FinanceiroGuiaFatura
    {
        public string EXPORTADOR { get; set; }
        public string FAT_NUM_FATURA { get; set; }
        public string FAT_COD_MOEDA { get; set; }
        public string FAT_COND_PAGTO { get; set; }
        public DateTime FAT_DATA_VENCIMENTO { get; set; }
        public string FAT_TAXA_DI { get; set; }
        public double FAT_VMCV_TOTAL { get; set; }
        public string NUCLEUS_MVTO { get; set; }
        public string NUCLEUS_NUM { get; set; }
        public string FLUXUS_DOC { get; set; }
        public DateTime DATA_FATURA { get; set; }
        public string STR_DATA_FATURA { get; set; }
        public string CONHECIMENTO { get; set; }
        public string TIPO_CONHECIMENTO { get; set; }
        public string FAT_INCOTERM { get; set; }
        public int ID_INTEGRACAO { get; set; }
        public string SITUACAO { get; set; }
        public DateTime DATA { get; set; }
        public string STR_DATA { get; set; }
        public int ID_INT { get; set; }
        public string CHAVEORIGEM_INT { get; set; }
        public int? NUCLEUS_ID { get; set; }
        public int FLUXUS_ID { get; set; }
        public string ImportSys { get; set; }
        public string REC_NUCLEUS { get; set; }
        public string doc_fluxus { get; set; }
        public string TIPO_FATURA { get; set; }
    }
}
