using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities
{
    public class FechamentoContabil
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Id_Fechamento { get; set; }
        public int Id_Integracao { get; set; }
        public string Fechamento { get; set; }
        public DateTime Data { get; set; }
        public DateTime Data_contabilizacao { get; set; }
        public DateTime Data_Registro { get; set; }
        public DateTime Data_Abertura { get; set; }
        public DateTime Data_Fechamento { get; set; }
        public string Situacao { get; set; }
        public string Responsavel { get; set; }
        public string Responsavel_Abertura { get; set; }
        public string Responsavel_Fechamento { get; set; }
        public string Exibir_Salvar { get; set; }
        public string Observacao { get; set; }
        public string Mensagem { get; set; }
        public List<FechamentoContabil> lstGrid { get; set; }
    }
}
