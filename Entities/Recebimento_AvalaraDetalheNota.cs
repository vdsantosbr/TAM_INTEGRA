using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Recebimento_AvalaraDetalheNota
    {
        public string NUMERONOTA { get; set; }
        public string DATAEMISSAONOTA { get; set; }
        public string VALORNOTA { get; set; }
        public string CHAVENF { get; set; }
        public string EMI_IE { get; set; }
        public string EMI_INSC_MUNI { get; set; }
        public string CNAE { get; set; }
        public string EMI_CNPJ { get; set; }
        public string EMI_RAZAO { get; set; } 
        public string EMI_ENDERECO { get; set; }
        public string EMI_UF { get; set; }
        public string EMI_CEP { get; set; }
        public string EMI_NUMERO { get; set; }
        public string EMI_BAIRRO { get; set; }
        public string EMI_PAIS { get; set; }
        public string EMI_TELEFONE { get; set; }
        public string EMI_MUNICIPIO { get; set; }
        public string DEST_IE { get; set; }
        public string DEST_INSC_MUNI { get; set; }        
        public string DEST_CNPJ { get; set; }
        public string DEST_RAZAO { get; set; }
        public string DEST_ENDERECO { get; set; }
        public string DEST_UF { get; set; }
        public string DEST_CEP { get; set; }
        public string DEST_NUMERO { get; set; }
        public string DEST_BAIRRO { get; set; }
        public string DEST_PAIS { get; set; }
        public string DEST_TELEFONE { get; set; }
        public string DEST_MUNICIPIO { get; set; }
        public string CDPRODUTO { get; set; }
        public string  NUMEROITEMPEDIDO { get; set; }
        public string QCOM { get; set; }
        public string  NOMEPRODUTO { get; set; }
        public string  VALORPRODUTO { get; set; }
        public string UCOM { get; set; }
        public string NCM { get; set; }
        public string CFOP { get; set; }
        public string TIPOMOVIMENTO { get; set; }
        public string  CLASSEFINANCEIRA { get; set; }
        public string FORMAPAGAMENTO { get; set; }
        public string TIPONFE { get; set; }


    }

    public class Recebimento_AvalaraDetalheNotaPedido
    {
        public int IDMOV { get; set; }
        public string MOVIMENTO { get; set; }
        public string FILIAL { get; set; }
        public string TIPONFE { get; set; }
        public string NuMERO { get; set; }
        public DateTime EMISSAO { get; set; }
        public decimal VALOR { get; set; }

        public string MOEDA { get; set; }
        public string COMPRADOR { get; set; }

        public string CNPJ { get; set; }
        public string CODIGO { get; set; }
        public string RAZAO { get; set; }

        public string RUA { get; set; }
        public string N { get; set; }
        public string BAIRRO { get; set; }

        public string CEP { get; set; }
        public string PAIS { get; set; }
        public string ESTADO { get; set; }

        public string MUNICIPIO { get; set; }
        public string TELEFONE { get; set; }
        public string IE { get; set; }

        public string IM { get; set; }
        public string CNAE { get; set; }
        public string CHAVEACESSO { get; set; }


    }

    public class Recebimento_AvalaraDetalheNotaPedidoItens
    {

        public string CODIGO { get; set; }
        public string DESCRICAO { get; set; }

        public decimal QTDE { get; set; }
        public string UN { get; set; }

        public decimal PRECOUNITARIO { get; set; }


    }

    public class RecebimentoAvalaraDeParaNFSE
    {
        public string  DESCRICAO { get; set; }
    }

}
