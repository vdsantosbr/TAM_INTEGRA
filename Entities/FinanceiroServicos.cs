using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroServicos
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Documento { get; set; }
        public string Status { get; set; }
        public string Situacao { get; set; }
        public int? Id_Integracao { get; set; }
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<FinanceiroServicos> lstItens { get; set; }
        public string Modal { get; set; }
        public List<FinanceiroServicos> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<FinanceiroServicos> lstReprocessamento { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }

        public int IDLAN { get; set; }
        public int IDMOV { get; set; }
        public string NOMEFANTASIA { get; set; }
        public string INVOICE { get; set; }
        public string TITULO { get; set; }
        public DateTime DATAEMISSAO { get; set; }
        public DateTime DATAVENCIMENTO { get; set; }
        public string FORNECEDOR { get; set; }
        public string MOEDA { get; set; }
        public decimal? VALORORIGINAL { get; set; }
        public string CCUSTO { get; set; }
        public string HISTORICOLONGO { get; set; }
        public string ORDEM_SERVICO { get; set; }
        public string Comentario { get; set; }
        public string DOC_ORIGEM { get; set; }
        public string CODTMV { get; set; }
        public string STATUS_PEDIDO_RM { get; set; }
        public string APROVACAO { get; set; }
        public string STATUSLANCAMENTO { get; set; }
        public string FILIAL { get; set; }
        public int? ID_LANCAMENTO { get; set; }
        public DateTime?  DT_EMISSAO_DOC { get; set; }
        public DateTime? DT_TRANSACAO { get; set; }
        public DateTime? DT_CONTABIL { get; set; }
        public decimal? VALOR_ME_RM { get; set; }
        public string TAXA_CONVERSAO { get; set; }
        public decimal? VALOR_MN_RM { get; set; }
        public string TEXTO_COMPLEMENTAR { get; set; }
        public string COR { get; set; }
        public string OBSERVACAO { get; set; }
    }
}
