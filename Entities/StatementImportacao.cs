using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Entities
{
    public class StatementImportacao
    {
        public int Ano { get; set; }
        public string Conta_Combo { get; set; }
        public int Mes { get; set; }
        public int ContaFiltro { get; set; }
        public string dataBaseFiltro { get; set; }
        public string Origem { get; set; }
        public DateTime? Importacao { get; set; }
        public DateTime? Conciliacao { get; set; }
        public string SituacaoConciliacao { get; set; }
        public string Conta { get; set; }
        public DateTime? Statement { get; set; }
        public int QtdeRegistros { get; set; }
        public DateTime? RMFluxus { get; set; }
        public DateTime? Conciliação { get; set; }
        public string Tipo { get; set; }
        public string Processamento { get; set; }
        public string SituacaoRM { get; set; }
        public string DocumentoNo { get; set; }
        public string SORef { get; set; }
        public string StatementFiltro { get; set; }
        public string RMFluxusFiltro { get; set; }
        public DateTime? DataImportacao { get; set; }
        public DateTime? DataBase { get; set; }
        public float AmountstatementDetail { get; set; }
       
        public string SituacaoStatement { get; set; }
        public List<StatementImportacao> lstConciliacao { get; set; }

        /* statement */
        public int Id_Conta { get; set; }
        public DateTime? Data_Base { get; set; }
        public DateTime? Paid_Date { get; set; }
        public DateTime? Data_Importacao { get; set; }
        public int Qtd_Pendencia { get; set; }
        public int Qtd_Registro { get; set; }
        public string Situacao { get; set; }
        public string Currency { get; set; }
        public string Aircraft { get; set; }
        public int Id_Statement { get; set; }
        public int idStatementModal { get; set; }
        public string Po_Number { get; set; }
        public int MyProperty { get; set; }
        public string Documento_No{ get; set; }
        public string So_Ref { get; set; }
        public string Invoice { get; set; }
        public DateTime? Doc_Date { get; set; }
        public DateTime? NET_DUE_DT { get; set; }
        public decimal Amount { get; set; }
        public decimal AMOUNT_REVEIVED { get; set; }
        public decimal Balance { get; set; }
        public int Arrear { get; set; }
        public string Doc_Type { get; set; }
        public string Suffix { get; set; }
        public string Check { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public string IMAGEM_RM { get; set; }
        /* statement detail */
    }
}
