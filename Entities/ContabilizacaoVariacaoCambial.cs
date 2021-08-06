using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ContabilizacaoVariacaoCambial
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Classificacao { get; set; }
        public string Exibir_Check { get; set; }
        public string Id_Lancamento { get; set; }
        public string Invoice { get; set; }
        public string Moeda { get; set; }
        public double? Valor_MN_RM { get; set; }
        public string Sinal { get; set; }
        public string Conta_Contabil1 { get; set; }
        public string Conta_Contrapartida { get; set; }
        public DateTime Dt_Contabil { get; set; }
        public DateTime Dt_Emissao_Doc { get; set; }
        public DateTime Dt_Transacao { get; set; }
        public string Cod_Parceiro { get; set; }
        public string Mvto_RM { get; set; }
        public string Historico { get; set; }
        public string Cod_Processo { get; set; }
        public int Id_Integracao { get; set; }
        public List<ContabilizacaoVariacaoCambial> lstVariacaoCambial { get; set; }
    }
}
