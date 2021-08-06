using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatementContas
    {
        public int Id_Conta{ get; set; }
        public string Conta { get; set; }
        public string Descricao { get; set; }
        public int Id_Situacao { get; set; }
        public string Situacao { get; set; }
        public List<StatementContas> lstContas { get; set; }
        public string situacaoFiltro { get; set; }
        public string Observacao { get; set; }
    }
}
