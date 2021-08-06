using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ImportacaoRMFluxus
    {
        public string Origem { get; set; }
        public string IMAGEM_COMBO { get; set; }
        public int Id_RM_Fluxus { get; set; }
        public DateTime Data_Importacao { get; set; }
        public string Situacao { get; set; }
        public DateTime Data_Conciliacao { get; set; }
        public string IMAGEM_RM { get; set; }
    }
}
