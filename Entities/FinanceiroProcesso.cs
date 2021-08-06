using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroProcesso
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Documento { get; set; }
        public string Situacao { get; set; }
        public int Id_Integracao { get; set; }
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public List<FinanceiroProcesso> lstItens { get; set; }
        public string Modal { get; set; }
        public List<FinanceiroProcesso> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<FinanceiroProcesso> lstReprocessamento { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public string Item { get; set; }
        public string Comentario { get; set; }
        public List<string> lstMoedas { get; set; }
        public String OBSERVACAO { get; set; }
        public string SP_COD_CREDOR_DESPESA { get; set; }
        public string SP_COD_DESPESA { get; set; }
        public string SP_CODIGO_CREDOR_DESPESA { get; set; }
        public string CREDOR { get; set; }
        public string SP_ID { get; set; }
        public string SPD_ID_DESPESA_PROCESSO { get; set; }
        public string PROCESSO { get; set; }
        public string CADASTRO { get; set; }
        public string LIBERACAO { get; set; }
        public string VENCIMENTO { get; set; }
        public string DESPESA { get; set; }
        public string MOEDA { get; set; }
        public decimal VALOR { get; set; }
        public string TIPO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string ID_INT { get; set; }
        public string CODTMV_INT { get; set; }
        public string CHAVEORIGEM_INT { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string MOVIMENTO { get; set; }
        public string CONSIDERAÇÕES { get; set; }
        public string COR { get; set; }
        public string PROCESSO_INTEGRACAO { get; set; }
        public string NUM_MOVIMENTO { get; set; }
        public string SERIE { get; set; }
        public string VR_PREVISTO_DESPESA { get; set; }
        public string VR_ADIANTADO_DESPESA { get; set; }
        public string VR_REAL_DESPESA { get; set; }
        public string VR_A_PAGAR_DESPESA { get; set; }
        public DateTime DATA_INTEGRACAO { get; set; }
        public string COD_PROCESSO { get; set; }
        public string NUMERO_DI { get; set; }
        public string DATA_REGISTRO_DI { get; set; }
        public string NF_TIPO_DEC_SISCOMEX { get; set; }
        public string TIPO_DECLARACAO { get; set; }
        public int DECLARACAO { get; set; }
    }
}
