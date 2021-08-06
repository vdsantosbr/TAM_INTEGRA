using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoFaturamento
    {
        public int ID { get; set; }
        public string DESCRICAO { get; set; }
        public string SITUACAO { get; set; }
        public string CODTMV { get; set; }
    }
}
