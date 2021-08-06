using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoMovimento
    {
        public TipoMovimento()
        {
            CODTMV = "";
            DESCRICAO = "";
            Id_Integracao_Processo = 0;
            Nome = "";
        }

        public int ID_INTEGRACAO_LAYOUT { get; set; }
        public int ID_POSICAO { get; set; }
        public string SELECAO { get; set; }
        public string CODTMV { get; set; }
        public string DESCRICAO { get; set; }
        public string tipoMovimento { get; set; }
        public int Id_Integracao_Processo { get; set; }
        public string Nome { get; set; }
    }
}
