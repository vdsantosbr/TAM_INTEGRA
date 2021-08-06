using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroDespesas
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Documento { get; set; }
        public string Situacao { get; set; }
        public int Id_Integracao { get; set; }
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public List<FinanceiroDespesas> lstItens { get; set; }
        public string Modal { get; set; }
        public List<FinanceiroDespesas> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<FinanceiroDespesas> lstReprocessamento { get; set; }
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
        public string SP_COD_PROCESSO { get; set; }
        public string CADASTRO { get; set; }
        public DateTime SP_DATA_LIBERACAO { get; set; }
        public DateTime SP_DATA_VENCIMENTO { get; set; }
        public string DESPESA { get; set; }
        public string SP_COD_MOEDA { get; set; }
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
        public double SP_VR_A_PAGAR_DESPESA { get; set; }
        public int ID_DESPESA { get; set; }
        public string NOME_DESPESA { get; set; }
        public String RECEBIMENTO { get; set; }
        public String REC_FIN { get; set; }
        public String STATUS_FIN { get; set; }
        public string SP_NUM_DOCUMENTO { get; set; }
        public string EXIBIR_REPROCESSAR { get; set; }
        public string EXIBIR_EXCLUIR { get; set; }

        //public FinanceiroDespesas(string nomeDespesa, int ID_DESPESA, string SP_COD_CREDOR_DESPESA, string SP_COD_DESPESA, string SP_CODIGO_CREDOR_DESPESA, string CREDOR)
        //{
        //    this.NOME_DESPESA = nomeDespesa;
        //    this.ID_DESPESA = ID_DESPESA;
        //    this.SP_COD_CREDOR_DESPESA = SP_COD_CREDOR_DESPESA;
        //    this.SP_COD_DESPESA = SP_COD_DESPESA;
        //    this.SP_CODIGO_CREDOR_DESPESA = SP_CODIGO_CREDOR_DESPESA;
        //    this.CREDOR = CREDOR;
        //}
    }   
}
