using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatementRelatorioReceberPagar
    {
        public int Id { get; set; }
        public string Conta { get; set; }
        public DateTime Conciliacao { get; set; }
        public string Situacao { get; set; }
        public string Classificacao { get; set; }
        public string SO_Ref { get; set; }
        public string Invoice { get; set; }
        public DateTime Doc_Date { get; set; }
        public DateTime Net_Due_Dt { get; set; }
        public decimal Amount { get; set; }
        public decimal RMValor { get; set; }
        public decimal Diferenca { get; set; }
        public string Tipo_Processo { get; set; }
        public string Deposito_Especial { get; set; }
        public string Canal { get; set; }
        public int Arrear { get; set; }
        public string Documento_No { get; set; }
        public string Text { get; set; }
        public string Departamento { get; set; }
        public string Status { get; set; }
        public string Situacao_Analise { get; set; }
    }
}
