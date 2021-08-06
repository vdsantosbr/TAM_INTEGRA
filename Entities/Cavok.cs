using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cavok
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Documento { get; set; }
        public string Numero { get; set; }
        public string Status { get; set; }
        public string Situacao { get; set; }
        public int Id_Integracao { get; set; }
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public List<Cavok> lstItens { get; set; }
        public string Modal { get; set; }
        public List<Cavok> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<Cavok> lstReprocessamento{ get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public string FATURAMENTO { get; set; }
        public int FATURA { get; set; }
        public string TIPO { get; set; }
        public string CLIENTE { get; set; }
        public string CONSIDERACOES { get; set; }
        public string HANDLING { get; set; }
        public string FORMA_PAGAMENTO { get; set; }
        public string MOEDA { get; set; }
        public double VALOR { get; set; }
        public Double COTACAO { get; set; }
        public double VALOR_TOTAL { get; set; }
        public double VALOR_TOTAL_REAIS { get; set; }
        public int ITEM { get; set; }
        public string COMENTARIO { get; set; }
        public string FILIAL { get; set; }
        public string BASE { get; set; }
        public string MOVIMENTO { get; set; }
        public string COR { get; set; }

    }
}
