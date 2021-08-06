using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroFaturas
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Documento { get; set; }
        public string Numero { get; set; }
        public string Status { get; set; }
        public string Situacao { get; set; }
        public int Id_Integracao { get; set; }
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public List<FinanceiroFaturas> lstItens { get; set; }
        public string Modal { get; set; }
        public List<FinanceiroFaturas> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<FinanceiroFaturas> lstReprocessamento { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public DateTime FAT_DATA_FATURA { get; set; }
        public string FAT_FATURA_ID { get; set; }
        public string FAT_COD_EXPORTADOR { get; set; }
        public string FAT_NUM_FATURA { get; set; }
        public string FAT_TIPO_FATURA { get; set; }
        public string FAT_COD_PROCESSO { get; set; }
        public string FAT_NUM_DI { get; set; }
        public string FAT_COND_PAGTO { get; set; }
        public string FAT_COD_MOEDA { get; set; }
        public double FAT_VMCV_TOTAL { get; set; }
        public string Item { get; set; }
        public string Comentario { get; set; }
        public List<string> lstMoedas { get; set; }
        public String OBSERVACAO { get; set; }
        public int SP_COD_CREDOR_DESPESA { get; set; }
        public string SP_COD_DESPESA { get; set; }
        public int SP_CODIGO_CREDOR_DESPESA { get; set; }
        public string CREDOR { get; set; }
        public string EXPORTADOR { get; set; }
        public string FAT_EMBARQUE_NUM { get; set; }
        public string FAT_NUMERO_CONHECIMENTO { get; set; }
        public string FAT_USERNAME { get; set; }
        public DateTime DATA_VENCIMENTO { get; set; }
        public string FAT_TIPO_CONHEC_PROCESSO { get; set; }
        public string PROCESSO { get; set; }
        public string TIPO { get; set; }
        public string ID_INT { get; set; }
        public string CODTMV_INT { get; set; }
        public string CHAVEORIGEM_INT { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string MOVIMENTO { get; set; }
        public string COR { get; set; }
        public string CONSIDERAÇÕES { get; set; }
        public string COMPLEMENTO { get; set; }
        public string EXIBIR_REPROCESSAR { get; set; }
        public string EXIBIR_EXCLUIR { get; set; }
        public string Exibir_Check { get; set; }
    }

}
