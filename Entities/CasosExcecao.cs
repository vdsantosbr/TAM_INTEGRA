using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CasosExcecao
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Observacao { get; set; }
        public string Fechamento { get; set; }
        public DateTime? Data_contabilizacao { get; set; }
        public DateTime? Data_Registro { get; set; }
        public string Situacao { get; set; }
        public string Responsavel { get; set; }
        public int Id_Excecao { get; set; }
        public string Tipo_Variacao { get; set; }
        public string Referencia { get; set; }
        public double? Valor_Fatura { get; set; }
        public double? Valor_Vc { get; set; }
        public string Historico { get; set; }
        public string Mensagem { get; set; }
        public List<CasosExcecao> lstGrid { get; set; }
        public List<CasosExcecao> lstRegistroIntegracao { get; set; }
    }
}
