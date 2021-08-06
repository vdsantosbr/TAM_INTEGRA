using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PedidoVenda
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
        public List<PedidoVenda> LstPedidoVenda { get; set; }
        public List<PedidoVenda> LstItem { get; set; }
        public List<PedidoVenda> LstAprovacao { get; set; }
        public List<PedidoVenda> LstJustificativa { get; set; }
        public string Numero { get; set; }
        public string Situacao { get; set; }
        public string Aprovacao { get; set; }
        public string Status_Pedido_RM { get; set; }
        public Decimal ValorLiquido { get; set; }
        public string Comprador { get; set; }

        public string Pedido { get; set; }
        public string Identificador { get; set; }
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
        public int Id_Integracao_Servidor { get; set; }
        public int Id_Integracao_Processo { get; set; }
        public string CodCFO { get; set; }
        public string Nome { get; set; }
        public int Id_Posicao { get; set; }
        public bool Selecao { get; set; }
        public string CodTMV { get; set; }
        public string FornecedorNome { get; set; }
        public string Movimento { get; set; }
        public List<PedidoVenda> pdvLst  { get; set; }
        public List<PedidoVenda> lstPedidoCompra { get; set; }
    }
}
