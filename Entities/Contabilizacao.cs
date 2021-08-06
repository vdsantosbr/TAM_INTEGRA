using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contabilizacao
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Id_Integracao { get; set; }
        public int Id_Fechamento { get; set; }
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
        public double? VC_EFETIVA_GANHO { get; set; }
        public double? VC_EFETIVA_PERDA { get; set; }
        public double? VC_NAO_EFETIVA_GANHO { get; set; }
        public double? VC_NAO_EFETIVA_PERDA { get; set; }
        public double? VC_REVERSAO_NAO_EFETIVA_GANHO { get; set; }
        public double? VC_REVERSAO_NAO_EFETIVA_PERDA { get; set; }
        public string SITUACAO_COMPOSICAO { get; set; }
        public int ID_LANCAMENTO { get; set; }
        public int ID_ENTIDADE { get; set; }
        public string REFERENCIA1 { get; set; }
        public string FLEX5 { get; set; }
        public double? VALOR_MN { get; set; }       
        public double? VALOR { get; set; }
        public string COMENTARIO { get; set; }
        public string GRUPO { get; set; }
        public DateTime? DT_CONTABIL { get; set; }
        public DateTime? DT_EMISSAO_DOC { get; set; }
        public DateTime? DT_TRANSACAO { get; set; }
        public string CHAVE_LANCAMENTO { get; set; }
        public string CONTA_CREDITO { get; set; }
        public string CONTA_DEBITO { get; set; }
        public int? RECFIN_CODFILIAL { get; set; }
        public string RECFIN_CODCFO { get; set; }
        public string RECFIN_CODTMV { get; set; }
        public string RECFIN_NUMEROMOV { get; set; }
        public string COD_PROCESSO { get; set; }
        public string HISTORICO { get; set; }
        public string EXIBIR_FORMATAR_CONTABILIZACAO { get; set; }
        public string EXIBIR_INTEGRAR_CONTABILIZACAO { get; set; }
        public string EXIBIR_EXCLUIR_CONTABILIZACAO { get; set; }
        public string VALIDACAO { get; set; }
        public string VALIDACAO_COMPLEMENTO { get; set; }
        public List<Contabilizacao> lstFechamentoAtual { get; set; }
        public List<Contabilizacao> lstGrid { get; set; }
        public List<Contabilizacao> lstRegistrosContabilizacao { get; set; }
        public List<Contabilizacao> lstRegistros { get; set; }
        public List<Contabilizacao> lstRegistrosInconsistentes { get; set; }
        public List<Contabilizacao> lstRegistrosErro { get; set; }
        public List<Conta> lstContas { get; set; }
    }
}
