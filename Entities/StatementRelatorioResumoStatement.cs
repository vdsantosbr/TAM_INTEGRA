using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatementRelatorioResumoStatement
    {
        public string Grupo { get; set; }
        public string Descricao { get; set; }
        public int Qtd_invoice { get; set; }
        public double Amount { get; set; }
        public double Saldo { get; set; }
    }
}
