using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FinanceiroCambioSYS
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Arquivo { get; set; }
        public string Tipo_Arquivo { get; set; }
        public string Documento { get; set; }
        public string Numero { get; set; }
        public string Status { get; set; }
        public string Situacao { get; set; }
        public int Id_Integracao { get; set; }
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<FinanceiroCambioSYS> lstItens { get; set; }
        public string Modal { get; set; }
        public List<FinanceiroCambioSYS> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<FinanceiroCambioSYS> lstReprocessamento { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public string Guia { get; set; }
        public string Nome { get; set; }
        public string Texto { get; set; }
        public int? ID_INTEGRACAO_TXT { get; set; }
        public int ARQUIVO_INCONSISTENCIA { get; set; }
        public string Nome_Arquivo { get; set; }
        public string Mensagem { get; set; }
    }
}
