using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class RecebimentoNota
    {
        public string Situacao { get; set; }        
        public int ID_AVALARA { get; set; }
        public int ORDEM { get; set; }
        public string NOME { get; set; }
        public string CGCCFO { get; set; }
        public string NF_EMISSAO { get; set; }
        public string NF_NUMERO { get; set; }
        public string NF_VALOR { get; set; }
        public string PEDIDO_NUMERO { get; set; }
        public string PEDIDO_EMISSAO { get; set; }
        public string PEDIDO_REQUISITANTE { get; set; }
        public string PEDIDO_VALOR { get; set; }
        public Int32 IDMOV { get; set; }
        public string CODTMV { get; set; }
        public string CFOP { get; set; }
        public string TIPONFE { get; set; }
        public string SITUACAO { get; set; }
        public string XMOTIVO { get; set; }
        public string  SIGLANOTA { get; set; }
    }

    public class RecebimentoNotaVinculada
    {
        public int IdAvalara { get; set; }
        public int CodColigada { get; set; }
        public int idmov { get; set; }
        public int? Nseqitmmov { get; set; }
        public int nItem { get; set; }
        public string NotaFiscal { get; set; }
        public int Idprd { get; set; }
        public decimal QuantidadeReceber { get; set; }
        public string NCM { get; set; }
        public string TipoMovimento { get; set; }
        public string ClasseFinanceira { get; set; }
        public string TipoPagamento { get; set; }
        public string itemCFOP { get; set; }

        public string itemDEPARA { get; set; }
        public string NumpedidoRM { get; set; }
       
    }

    public class RecebimentoNotaRM
    {
        public int ID_AVALARA { get; set; }
        public int ORDEM { get; set; }
        public string NOME { get; set; }
        public string CNPJ { get; set; }
        public DateTime NF_EMISSAO { get; set; }
        public string NF_NUMERO { get; set; }
        public string NF_VALOR { get; set; }
        public string PEDIDO_NUMERO { get; set; }
        public string PEDIDO_NUMERO_SOMENTE { get; set; }
        public DateTime PEDIDO_EMISSAO { get; set; }
        public string PEDIDO_REQUISITANTE { get; set; }
        public decimal PEDIDO_VALOR { get; set; }
        public decimal PEDIDO_PRECO { get; set; }

        public decimal QUANTIDADE { get; set; }
        public Int32 IDMOV { get; set; }
        public string CODTMV { get; set; }
        public string CFOP { get; set; }
        public string STATUS { get; set; }
        public string PEDIDO_NUMERO_EXIBICAO { get; set; }

    }
}
