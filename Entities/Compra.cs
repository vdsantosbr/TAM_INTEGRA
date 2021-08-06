using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Compra
    {
        public int Id { get; set; }
        public int Id_Integracao { get; set; }
        public string Codigo { get; set; }
        public string USUARIOCRIACAO { get; set; }
        public string Descricao { get; set; }
        public DateTime? data_Inicio { get; set; }
        public DateTime? data_Termino { get; set; }
        public DateTime dataEmissao { get; set; }
        public string dataEmissaoStr { get; set; }
        public string strDataInicio{get;set;}
        public string strDataFim { get; set; }
        public List<Compra> ListaComprador { get; set; }
        public string CodTmv { get; set; }

        public string NUMEROMOV { get; set; }
        public string Numero { get; set; }
        public string Situacao { get; set; }
        public string Aprovacao { get; set; }
        public string Status_Pedido_RM { get; set; }
        public Decimal ValorLiquido { get; set; }
        public string Fornecedor { get; set; }
        public string Comprador { get; set; }
        public int idMov { get; set; }
        public string tipoMovimento { get; set; }
        public string Comentario { get; set; }

    }
}
