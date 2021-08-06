using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RecebimentoCombosViewModel
    {
        public List<Recebimento_Tipo_Movimento_Avalara> Combo1 { get; set; }
        public List<Recebimento_Class_Fin_Avalara> Combo2 { get; set; }
        public List<Recebimento_Forma_Pagto_Avalara> Combo3 { get; set; }

        public List<RecebimentoNota> RecebimentoNota { get; set; }
        public List<RecebimentoNotaRM> RecebimentoNotaRM { get; set; }

        public List<Recebimento_Compra_item_Avalara> Compra_item_Avalara { get; set; }

        public List<Recebimento_item_Avalara> item_Avalara { get; set; }

        public List<Recebimento_CFOP_Avalara> cfop_Avalara { get; set; }

        public bool remessa { get; set; } = false;

        public List<Recebimento_AvalaraDetalheNota> ListNotaDetalhe { get; set; }

        public List<Recebimento_NCM_Avalara> NCM_Avalara { get; set; }
        public  List<RecebimentoNotaVinculada> ListNotaVinculada { get; set; }

        public List<SelectIDMOV> SelectIDMOV { get; set; }

        public List<RecebimentoAvalaraDeParaNFSE> RecebimentoDEPARA { get; set; }

    }
}
