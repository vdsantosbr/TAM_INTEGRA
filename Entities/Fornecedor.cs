using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Fornecedor
    {
        public string CodCFO { get; set; }
        public string Nome { get; set; }
        public string FornecedorNome { get; set; }
        public string Situacao { get; set; }
        public string Qualificacao { get; set; }
        public string Tipo { get; set; }
        public int ID_INTEGRACAO_SERVIDOR { get; set; }
        public int id_integracao_processo { get; set; }
        public int id_integracao { get; set; }
    }
}
