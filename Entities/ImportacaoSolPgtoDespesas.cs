using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ImportacaoSolPgtoDespesas
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<ImportacaoSolPgtoDespesas> lstItens { get; set; }
        public List<ImportacaoSolPgtoDespesas> lstDespesas { get; set; }
        public List<ImportacaoSolPgtoDespesas> lstDivDespesas { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public string NumDocumento { get; set; }
        public int Id_Tipo_Documento { get; set; }
        public string Descricao { get; set; }
        public string CodSituacao { get; set; }
        public string SituacaoDesc { get; set; }
        public int Id_SPDesp { get; set; }
        public int Id_Integracao { get; set; }
        public string Sp_ID { get; set; }
        public string Spd_Id_Despesa_Processo { get; set; }
        public string Origem { get; set; }
        public string Tipo_Documento { get; set; }
        public DateTime? Data_Emissao { get; set; }
        public string Sp_Codigo_Credor_Despesa { get; set; }
        public DateTime? Data_Vencimento { get; set; }
        public string Sp_Num_Documento { get; set; }
        public string Sp_Cod_Processo { get; set; }
        public string Fat_Num_Fatura { get; set; }
        public string Ref_Emissao { get; set; }
        public string Situacao { get; set; }
        public string Processo { get; set; }
        public string Invoice { get; set; }
        public string Status { get; set; }
        public string Filial { get; set; }
        public string Moeda { get; set; }
        public string Sp_Cod_Moeda { get; set; }
        public string Fornecedor { get; set; }
        public string Justificativa { get; set; }
        public DateTime Dt_Emissao { get; set; }
        public DateTime Dt_Vencimento { get; set; }
        public string DivExibir { get; set; }
        public string DescItem { get; set; }
        public string Desc { get; set; }
        public string CC { get; set; }
        public double Preco { get; set; }
        public double Valor { get; set; }
        public string Data { get; set; }
        public int Qtd { get; set; }
        public string Div { get; set; }
        public Int16 CodFilial { get; set; }
        public string NomeFantasia { get; set; }
        public string CodCfo { get; set; }
        public string Simbolo { get; set; }
        public string Simbolo_Descricao { get; set; }
        public string Nome { get; set; }
        public DateTime? Sp_Data_Cadastro { get; set; }
        public DateTime? Sp_Data_Vencimento { get; set; }
        public DateTime? Sp_Data_Liberacao { get; set; }
        public string NumDocDiv { get; set; }
        public string ProcessoDiv { get; set; }
        public string InvoiceDiv { get; set; }
        public Int16 FilialDiv { get; set; }
        public string DtEmissaoDiv { get; set; }
        public string DtEmissaoDivFmt { get; set; }
        public string VencimentoDiv { get; set; }
        public string VencimentoDivFmt { get; set; }
        public string DtLiberacao { get; set; }
        public double? ValorDiv { get; set; }
        public string FornecedorDiv { get; set; }
        public string DivDespesas { get; set; }
        public string HistoricoCurto { get; set; }
        public int id_tp_documento_div { get; set; }
        public string MoedaDiv { get; set; }

    }
}

