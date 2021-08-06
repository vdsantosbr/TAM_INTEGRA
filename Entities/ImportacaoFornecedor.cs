using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ImportacaoFornecedor
    {
        public ImportacaoFornecedor()
        {
            codtmv = "";
            Descricao = "";
            Id_Integracao_Processo = 0;
            Nome = "";
            SITUACAODESC = "";
            CODSITUACAO = 0;
        }
        public int Id_Integracao_Layout { get; set; }
        public int int_posicao { get; set; }
        public string Selecao { get; set; }
        public string NomeFantasia { get; set; }
        public string CGCCFO { get; set; }
        public string INSCRESTADUAL { get; set; }
        public string INSCRMUNICIPAL { get; set; }
        public DateTime? DATACRIACAO { get; set; }
        public DateTime? DATAULTALTERACAO { get; set; }

        public int Id_Fornecedor { get; set; }
        public string Fornecedor { get; set; }

        [Required(ErrorMessage = "Selecione o Fornecedor")]
        [Column("fornecedorNome")]
        [Display(Name = "Fornecedor")]
        public string fornecedorNome { get; set; }
        public int Id_Documento { get; set; }
        [Column("documento")]
        [Display(Name = "Documento")]
        public string Documento { get; set; }
        public string NOMETMV { get; set; }
        public string Situacao { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public int Id_Integracao_Servidor { get; set; }
        public int Id_Integracao_Processo { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public List<ImportacaoFornecedor> lstDocumentos { get; set; }
        public List<ImportacaoFornecedor> lstItensPedido { get; set; }
        public int Identificacao { get; set; }
        public string Integracao { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string Status_Pedido_RM { get; set; }
        public decimal ValorLiquido { get; set; }
        public string Aprovacao { get; set; }
        public string NUMEROMOV { get; set; }
        public int Id_Integracao { get; set; }
        public int idMov { get; set; }
        public string codtmv { get; set; }
        public string codcfo { get; set; }
        public string Pais { get; set; }
        public string Descricao { get; set; }
        public string Filial { get; set; }
        public string Qualificacao { get; set; }
        public string SerialNumber { get; set; }
        public string Chave { get; set; }
        public int Quantidade { get; set; }
        public string NUMNOFABRIC { get; set; }
        public string Status { get; set; }
        public string Moeda { get; set; }
        public decimal Valor { get; set; }
        public string Condicao_Pagamento { get; set; }
        public string Comprador { get; set; }
        public int Idprd { get; set; }
        public int NSEQITMMOV { get; set; }
        public Int16 NUMEROSEQUENCIAL { get; set; }
        public string NF_NUMNF { get; set; }
        public string NF_ITE_NUMITE { get; set; }
        public string NF_ITE_NM_ADICAO { get; set; }
        public string NF_ITE_NM_ITEM_ADICAO { get; set; }
        public List<ImportacaoFornecedor> lstItensDocumento { get; set; }
        public List<ImportacaoFornecedor> lstItens { get; set; }
        public string PERMITE_EDICAO { get; set; }
        public string PERMITE_EXCLUSAO { get; set; }
        public string numeroMovimento { get; set; }
        public string movimento { get; set; }
        public int CODSITUACAO { get; set; }
        public string SITUACAODESC { get; set; }
        public int idcfo { get; set; }
    }
}
