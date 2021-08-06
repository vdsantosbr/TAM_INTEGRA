using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Configuracao
    {
        public string ID_PROJETO { get; set; }
        public string ID_TIPO_DOCUMENTO { get; set; }
        public string PROJETO { get; set; }
        public string Tipo_documento { get; set; }
        public string CODTMV { get; set; }
        public string CODIGO { get; set; }
        public string MOVIMENTO { get; set; }
        public string  IDPRD { get; set; }
        public string TIPO { get; set; }
        public string DESCRICAO { get; set; }
        public List<Configuracao> lstConfiguracao { get; set; }
    }
}
