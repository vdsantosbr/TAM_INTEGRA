using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CompraAprovacao
    {
        public int Id { get; set; }
        public int IdLiberacaoPO { get; set; }
        public int IdLiberacaoPOMotivo { get; set; }
        public int Motivo { get; set; }
        public int Id_Integracao { get; set; }
        public string Codigo { get; set; }
        public DateTime? data_Inicio { get; set; }
        public DateTime? data_Termino { get; set; }
        public string dataEmissaoStr { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public List<CompraAprovacao> LstCompraAprovacao { get; set; }
        public List<CompraAprovacao> LstItem { get; set; }
        public List<CompraAprovacao> LstAprovacao { get; set; }
        public List<CompraAprovacao> LstJustificativa { get; set; }
        public string Numero { get; set; }
        public string Situacao { get; set; }
        public string Aprovacao { get; set; }
        public string Status_Pedido_RM { get; set; }
        public Decimal ValorLiquido { get; set; }
        public string Comprador { get; set; }

        public string Pedido { get; set; }
        public string Identificador { get; set; }
        public string CodTmv { get; set; }
        public string NUMEROMOV { get; set; }
        public string Descricao { get; set; }
        public string Exibir_Editar { get; set; }
        public string Exibir_Liberar { get; set; }
        public string Exibir_Cancelar { get; set; }
        public string tipoMovimento { get; set; }
        public string Comentario { get; set; }
        public int idMov { get; set; }
        public string Filial { get; set; }
        public string CNPJ { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public string Observacao { get; set; }
        public string Fornecedor { get; set; }
        public string Moeda { get; set; }
        public decimal Valor { get; set; }
        public string Status_Aprovacao { get; set; }
        public string Status_Pedido { get; set; }
        public Int16 Item_Pedido { get; set; }
        public int Item_Importacao { get; set; }
        public string PN { get; set; }
        public decimal qtd { get; set; }
        public decimal preco { get; set; }
        public int IDSOLICITACAO { get; set; }
        public string Enviado { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataFinalizacao { get; set; }
        public string Status { get; set; }
        public string Aprovador { get; set; }
        public string Justificativa { get; set; }
        public string Responsavel { get; set; }
        public DateTime? Data_Edicao { get; set; }
        public List<Motivo> lstMotivo { get; set; }
        public string Justificativa_Edicao { get; set; }
        public string CodigoPrd { get; set; }
        public string UND { get; set; }
        public string NCM { get; set; }
    }
}
