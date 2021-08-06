using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ImportacaoFatura
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Documento { get; set; }
        public string Situacao { get; set; }
        public int Id_Integracao { get; set; }
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public List<ImportacaoFatura> lstItens { get; set; }
        public string Modal { get; set; }
        public List<ImportacaoFatura> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<ImportacaoFatura> lstReprocessamento { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public string Item { get; set; }
        public string Comentario { get; set; }
        public String OBSERVACAO { get; set; }
        public string CONSIDERAÇÕES { get; set; }
        public string COR { get; set; }
        public string ID_BrokerSys { get; set; }
        public string NUMNF_BrokerSys { get; set; }
        public string iNTEGRACAO { get; set; }
        public string EMISSAO { get; set; }
        public string RECFISICO { get; set; }
        public double VR_FATURA { get; set; }
        public double VR_Nota { get; set; }
        public double DespesasCompl { get; set; }
        public double VR_RecFisico { get; set; }
        public string NF_COD_PROCESSO { get; set; }
        public string FAT_FATURA_ID { get; set; }
        public string FAT_NUM_FATURA { get; set; }
        public string FAT_COD_EXPORTADOR { get; set; }
        public string FAT_COD_PROCESSO { get; set; }
        public string FAT_TIPO_FATURA { get; set; }
        public DateTime FAT_DATA_FATURA { get; set; }
        public string FAT_NUM_DI { get; set; }
        public string FAT_COND_PAGTO { get; set; }
        public string FAT_COD_MOEDA { get; set; }
        public double FAT_VMCV_TOTAL { get; set; }
        public string REC_FIN { get; set; }
        public string REF_FIN { get; set; }
        public string SP_CODIGO_CREDOR_DESPESA { get; set; }
        public string STATUS_FIN { get; set; }
        public string CREDOR { get; set; }
        public string Exibir_Check { get; set; }
    }
}
