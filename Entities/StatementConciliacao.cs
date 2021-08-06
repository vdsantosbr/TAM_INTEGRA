using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatementConciliacao
    {
        public int Id_Conciliacao { get; set; }
        public int Data_Inicial { get; set; }
        public int Data_Final { get; set; }
  public int ID_CLASSIFICACAO { get; set; }
        public int[] ID_CLASSIFICACAO2 { get; set; }
        public string Situacao_Analise { get; set; }
        public string Id_Situacao_Analise { get; set; }
        public int[] Id_Situacao_Analise2 { get; set; }
        public string Conta { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public string SituacaoAnalise { get; set; }
        public string Classificacao { get; set; }        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Id_Situacao { get; set; }
        public int Id_Departamento { get; set; }
        public int[] Id_Departamento2 { get; set; }
      
        public string Departamento { get; set; }
        public string Analise { get; set; }
        public string Documento_No { get; set; }
        public string  Tipo { get; set; }
        public DateTime Data_Imagem_RM { get; set; }
        public DateTime Data_Statement { get; set; }
        public DateTime Data_Base { get; set; }
        public DateTime Imagem_RM { get; set; }
        public DateTime Data_Conciliacao { get; set; }
        public DateTime Doc_Date { get; set; }
        public DateTime Net_Due_Date { get; set; }
        public int QTD_REGISTRO { get; set; }
        public decimal Amount { get; set; }
        public decimal Valor_RM { get; set; }
        public decimal Diferenca { get; set; }
        public decimal RMValor { get; set; }
        public string SO_REF { get; set; }
        public string Invoice { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageSizeAnaliseItem { get; set; }
        public int PageNumberanaliseItem { get; set; }
        public string PO_Number { get; set; }
        public int Id_Conciliacao_Item { get; set; }
        public string[] btnFiltroAnalise { get; set; }
        public string SITUACAO_CONCILIACAO_ANTERIOR { get; set; }
        public string SITUACAO_ATUAL { get; set; }
        public int modalShow { get; set; }
        public string Comentarios { get; set; }
        public DateTime Data_Obs { get; set; }
        public DateTime Data { get; set; }
        public string Responsavel { get; set; }
        public Int16 Filial { get; set; }
        public DateTime Emissao { get; set; }
        public string Movimento { get; set; }
        public string Serie { get; set; }
        public Decimal VALORLIQUIDOORIG { get; set; }
        public string Status { get; set; }
        public string NomeFantasia { get; set; }
        public string Status_Aprovacao { get; set; }
        public string  DI { get; set; }
        public string Processo { get; set; }
        public string House { get; set; }
        public string CTT_CAMBIO { get; set; }
        public DateTime DataBaixa { get; set; }
        public string Moeda { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorBaixado { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string CodCxa { get; set; }
        public string Text { get; set; }
        public string PO { get; set; }
        public decimal Amount_retrieved { get; set; }
        public decimal Balance { get; set; }
        public int Arrear { get; set; }
        public DateTime? Paid_date { get; set; }
        public string Doc_type { get; set; }
        public string Currency { get; set; }
        public string Aircraft { get; set; }
        public string Suffix { get; set; }
        public string Check { get; set; }
        public string pedidoCompraFiltro { get; set; }
        public List<StatementConciliacao> lstAnalise { get;set;}
        public List<StatementConciliacao> lstAnaliseItem { get; set; }
        public List<StatementConciliacao> lstPopulaHistorico { get; set; }
        public List<StatementConciliacao> lstAnaliseConciliacao { get; set; }
        public List<StatementConciliacao> lstConciliacaoItem { get; set; }
        public List<StatementConciliacao> lstObservacao { get; set; }
        public List<StatementConciliacao> lstPedidoCompra { get; set; }
        public List<StatementConciliacao> lstInvoice { get; set; }
        public List<StatementConciliacao> lstFinanceiro { get; set; }
        public List<StatementConciliacao> lstHistorico { get; set; }
    }
}
