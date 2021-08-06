using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatementClassificacao
    {
        public int Id_Classificacao { get; set; }
        public string Classificacao { get; set; }
        public string Descricao { get; set; }
        public int Id_Situacao { get; set; }
        public string Situacao { get; set; }
        public string Visivel { get; set; }
        public List<StatementClassificacao> lstClassificacoes { get; set; }
        public string situacaoFiltro { get; set; }
    }
}
