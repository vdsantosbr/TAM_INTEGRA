using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatementGestaoCredito
    {
        public int[] contaFiltro { get; set; }
        public int[] QualificacaoFiltro { get; set; }
        public string TipoInvoiceFiltro { get; set; }
        public int[] SaldoFiltro { get; set; }
        public int Qualificacao_Saldo { get; set; }
        public string[] AgingFiltro { get; set; }
        public string salesOrderFiltro { get; set; }
        public string Aging { get; set; }
        public int[] SituacaoFiltro { get; set; }
        public int[] composicaoFiltro { get; set; }
        public int Qualificacao_invoice { get; set; }
        public string  SO_Ref { get; set; }
        public string Qualificacao { get; set; }
        public string Visivel { get; set; }
        public string Tipo { get; set; }
        public double Val_Saldo { get; set; }
        public double VAL_DEBITO { get; set; }
        public double Val_Credito { get; set; }
        public double  Val_WC { get; set; }
        public double Val_Diferenca { get; set; }
        public int Qtd_Registros { get; set; }
        public decimal Amount { get; set; }
        public decimal Valor_RM { get; set; }
        public decimal RMValor { get; set; }
        public string Codigo { get; set; }
        public decimal Valor { get; set; }
        public string Situacao { get; set; }
        public string Descricao { get; set; }
        public string Conta { get; set; }
        public DateTime Doc_Date { get; set; }
        public DateTime Net_Due_Date { get; set; }
        public int Arrear { get; set; }
        public string Classificacao { get; set; }
        public int Qualificacao_SO { get; set; }
        public string  Referencia { get; set; }
        public string Situacao_Str { get; set; }
        public string Composicao { get; set; }
        public int ID_SALESORDER { get; set; }
        public string Situacao_Invoice { get; set; }
        public string Aging_Str { get; set; }
        public string Invoice { get; set; }
        public int ID_QUALIFICACAO { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageSizeAnaliseItem { get; set; }
        public int PageNumberanaliseItem { get; set; }
        public DateTime Data { get; set; }
        public string Responsavel { get; set; }
        public List<StatementGestaoCredito> lstSalesOrder{ get; set; }
        public List<StatementGestaoCredito> lstInvoices { get; set; }
        public List<StatementGestaoCredito> lstSalesOrderAnalise { get; set; }
        public List<StatementGestaoCredito> LstComentarios { get; set; }
    }
}
