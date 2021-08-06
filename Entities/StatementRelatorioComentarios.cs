using System;

namespace Entities
{
    public class StatementRelatorioComentarios
    {
        public int Id { get; set; }
        public string Conta { get; set; }
        public DateTime Data_Conciliacao { get; set; }
        public string Situacao_Analise { get; set; }
        public string Situacao_Invoice { get; set; }
        public string Classificacao_Invoice { get; set; }
        public string Departamento { get; set; }
        public string Documento_No { get; set; }
        public string SO_Ref { get; set; }
        public string Invoice { get; set; }
        public DateTime Doc_Date { get; set; }
        public DateTime Net_Due_Dt { get; set; }
        public double Arrear { get; set; }
        public double Amount { get; set; }
        public double RMValor { get; set; }
        public double Diferenca { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string Comentarios { get; set; }
    }
}
