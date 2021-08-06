using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ContaContabil
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Fechamento { get; set; }
        public DateTime? Data_contabilizacao { get; set; }
        public DateTime? Data_Registro { get; set; }
        public string Situacao { get; set; }
        public string Responsavel { get; set; }
        public string Tipo_Variacao { get; set; }
        public int ID { get; set; }
        public string Qualificacao { get; set; }
        public string Tipo_Conta_Contabil { get; set; }
        public string Conta_Contabil { get; set; }
        public string Descricao { get; set; }
        public List<Conta> lstFechamentoAtual { get; set; }
        public List<Conta> lstContas { get; set; }
    }
}
