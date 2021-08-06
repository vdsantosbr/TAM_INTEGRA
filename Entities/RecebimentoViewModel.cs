using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RecebimentoViewModel
    {
        public List<RecebimentoNota> recebimentoNota { get; set; }
        public List<RecebimentoContadores> Contadores { get; set; }
        public List<NomeClienteNota> NomeCliente { get; set; }
        public List<RecebimentoNotaRM> recebimentoNotaRM { get; set; }

        public List<RecebimentoNotaVinculada> ListNotaVinculada { get; set; }

        public List<Recebimento_AvalaraDetalheNotaPedidoItens> DetalhePedidosItens { get; set; }

        public List<Recebimento_AvalaraDetalheNotaPedido> DetalhePedidos { get; set; }


    }

    public class NomeClienteNota
    {
        public string NomeCliente { get; set; }
        public int ID_AVALARA { get; set; }
        public string CGCCFO { get; set; }
    }

}



