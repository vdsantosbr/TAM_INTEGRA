using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class Recebimento_Compra_item_Avalara
    {
        public string PEDIDO_NCM { get; set; }
        public string PEDIDO_DESCRICAO { get; set; }
        public decimal? PEDIDO_QUANTIDADE { get; set; }
        public decimal PEDIDO_PRECO { get; set; }
        
    }

    public class Recebimento_item_Avalara
    {
        public string ID_AVALARA { get; set; }
        public string PEDIDO_NUMERO { get; set; }
        public string PEDIDO_NCM { get; set; }
        public string PEDIDO_DESCRICAO { get; set; }
        public decimal PEDIDO_QUANTIDADE { get; set; }
        public decimal PEDIDO_PRECO { get; set; }
        public decimal NF_CFOP { get; set; }

        public int COLIGADA { get; set; }
        public int IDMOV { get; set; }

        public int NSEQITMMOV { get; set; }
        public int IDPRD { get; set; }

    }

    public class Recebimento_CFOP_Avalara
    {
        public string CODNAT { get; set; }
    }

    public class Recebimento_NCM_Avalara
    {
        public string DESCRICAO { get; set; }
        public string CODNCM { get; set; }
    }
}
