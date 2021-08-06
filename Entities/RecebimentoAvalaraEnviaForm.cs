using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RecebimentoAvalaraEnviaForm
    {
        
        public string nItem { get; set; }
        public string tipomovimento { get; set; }
        public string Classefinanceira { get; set; }
        public string FormaPagamento { get; set; }
        public string RadioNotaFiscalitem { get; set; }
        public string chekPedidos { get; set; }
        public string Coligada { get; set; }
        public string TipoMovimento { get;  set; }
        public string NCM { get; internal set; }
        public string QuantidadeReceber { get;  set; }
        public string Idprd { get; set; }
        public string NotaFiscal { get;  set; }

        public string itemDEPARA { get; set; }
        public string Nseqitmmov { get;  set; }
        public string Idmov { get;  set; }

        public string Itemcfop { get;  set; }

        public string NumpedidoRM { get; set; }
    }
}
