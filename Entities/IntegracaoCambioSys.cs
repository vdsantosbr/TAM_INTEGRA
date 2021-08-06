using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace Entities
{
    public class IntegracaoCambioSys
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Fechamento { get; set; }
        public DateTime? Dt_contabilizacao { get; set; }
        public string Situacao { get; set; }
        public string Responsavel { get; set; }
        public int Id_integracao { get; set; }
        public DateTime? Data { get; set; }
        public string Nome { get; set; }
        public DateTime? Dt_transacao { get; set; }
        public int Qtd_registros { get; set; }
        public string Ativar_RadioBox { get; set; }
        public string Valor_RadioBox { get; set; }
        public double?  Vc_Efetiva_Ganho { get; set; }
        public double? Vc_Efetiva_Perda { get; set; }
        public double? Vc_Nao_Efetiva_Ganho { get; set; }
        public double? Vc_Nao_Efetiva_Perda { get; set; }
        public List<IntegracaoCambioSys> lstGrid { get; set; }
        public List<IntegracaoCambioSys> lstRegistroIntegracao { get; set; }
    }
}
