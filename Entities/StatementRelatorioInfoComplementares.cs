using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatementRelatorioInfoComplementares
    {
        public int Id_Conciliacao { get; set; }
        public int Id_Conciliacao_Item { get; set; }
        public string Conta { get; set; }
        public string Invoice { get; set; }
        public string Situacao { get; set; }
        public string Classificacao { get; set; }
        public string Documento_No { get; set; }
        public string SO_Ref { get; set; }
        public DateTime Doc_Date { get; set; }
        public DateTime Net_Due_Date { get; set; }
        public decimal Amount { get; set; }
        public decimal RMValor { get; set; }
        public decimal Diferenca { get; set; }
        public int Arrear { get; set; }
        public string Status { get; set; }
        public string Num_DI { get; set; }
        public string Num_Processo { get; set; }
        public string Num_House { get; set; }
        public string Tipo_Processo { get; set; }
        public int Deposito_Especial { get; set; }
        public int Canal { get; set; }
        public string Pedido_Compra { get; set; }
        public string Aprovacao { get; set; }
        public string Titulo_Financeiro { get; set; }
        public string CTT_Cambio { get; set; }
        public DateTime Data_Baixa { get; set; }
        public string Status_Titulo { get; set; }
        public string Text { get; set; }
        public string Historico { get; set; }
        public string Departamento { get; set; }
        public string Situacao_Analise { get; set; }
        public DateTime Data_Atualizacao { get; set; }
        public DateTime Data_Statement { get; set; }
        public DateTime Data_Conciliacao { get; set; }
        public string NFNUMNF { get; set; }
        public string NF_NUMERO_DI { get; set; }
        public string Nf_Cod_Processo { get; set; }
        public string NF_Num_conhecimeno { get; set; }
        public string Obsercacao { get; set; }
    }
}
