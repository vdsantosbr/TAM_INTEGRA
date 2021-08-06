using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ImportacaoSolPgto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Documento { get; set; }
        public string Situacao { get; set; }
        public int Id_Integracao { get; set; }
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public List<ImportacaoSolPgto> lstItens { get; set; }
        public string Modal { get; set; }
        public List<ImportacaoSolPgto> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<ImportacaoSolPgto> lstReprocessamento { get; set; }
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
        public double TAXA_DI { get; set; }
        public double II { get; set; }
        public double COFINS { get; set; }
        public double IPI { get; set; }
        public double PIS { get; set; }
        public double FreteInter { get; set; }
        public double Seguro { get; set; }
        public double VR_Produto { get; set; }
        public double ICMS { get; set; }
        public double VR_Nota { get; set; }
        public double DespesasCompl { get; set; }
        public double VR_RecFisico { get; set; }
        public string NF_COD_PROCESSO { get; set; }
        public string SP_ID { get; set; }
        public string SPD_ID_DESPESA_PROCESSO { get; set; }
        public string SP_COD_PROCESSO { get; set; }
        public DateTime SP_DATA_LIBERACAO { get; set; }
        public DateTime SP_DATA_VENCIMENTO { get; set; }
        public string SP_CODIGO_CREDOR_DESPESA { get; set; }
        public string SP_COD_DESPESA { get; set; }
        public string SP_NUM_DOCUMENTO { get; set; }
        public string SP_COD_MOEDA { get; set; }
        public double SP_VR_A_PAGAR_DESPESA { get; set; }
        public string RECEBIMENTO { get; set; }
        public string REF_FIN { get; set; }
        public string STATUS_FIN { get; set; }
        public string Exibir_Check { get; set; }
    }
}
