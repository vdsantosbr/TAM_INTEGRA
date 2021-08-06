using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ComposicaoVariacao
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Id_Fechamento { get; set; }
        public int Id_Integracao { get; set; }
        public string Id_Lancamento { get; set; }
        public string Id_Entidade { get; set; }
        public string Fechamento { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Data_contabilizacao { get; set; }
        public DateTime? Data_emissao { get; set; }
        public DateTime? Data_transacao { get; set; }
        public DateTime? Data_contabil { get; set; }
        public DateTime? Dt_Emissao_Doc { get; set; }
        public DateTime? Dt_Contabil { get; set; }
        public DateTime? Dt_Transacao { get; set; }
        public string Situacao { get; set; }
        public string Validacao { get; set; }
        public string Validacao_Complemento { get; set; }
        public string Responsavel { get; set; }
        public string Mensagem { get; set; }
        public string Qualificacao { get; set; }
        public string Tipo { get; set; }
        public string Referencia1 { get; set; }
        public string Conta_debito { get; set; }
        public string Conta_credito { get; set; }
        public double? Valor { get; set; }
        public string Cod_processo { get; set; }
        public int CodFilial { get; set; }
        public int Filial { get; set; }
        public string CodTMV { get; set; }
        public string Mvto { get; set; }
        public string Numero { get; set; }
        public string NumeroMov { get; set; }
        public string Historico { get; set; }
        public string Comentario { get; set; }
        public List<ComposicaoVariacao> lstGrid { get; set; }
        public List<ComposicaoVariacao> lstRegistros { get; set; }
        public List<ComposicaoVariacao> lstRegistrosContabilizacao { get; set; }
        public int Cod_vc_composicao_situacao { get; set; }
        public string Des_vc_composicao_situacao { get; set; }
        public string Cod_vc_composicao_qualificacao  { get; set; }
        public string Des_vc_composicao_qualificacao { get; set; }
        public int Cod_vc_composicao_tipo_vc { get; set; }
        public string Des_vc_composicao_tipo_vc { get; set; }
        public int Cod_vc_composicao_tipo_fatura { get; set; }
        public string tipo_fatura { get; set; }
        public string Des_vc_composicao_tipo_fatura { get; set; }
        public string Descricao { get; set; }
        public double? VC_EFETIVA_GANHO { get; set; }
        public double? VC_EFETIVA_PERDA { get; set; }
        public double? VC_NAO_EFETIVA_GANHO { get; set; }
        public double? VC_NAO_EFETIVA_PERDA { get; set; }
        public double? VC_REVERSAO_NAO_EFETIVA_GANHO { get; set; }
        public double? VC_REVERSAO_NAO_EFETIVA_PERDA { get; set; }
        public string CHAVE_LANCAMENTO { get; set; }
        public string Titulo { get; set; }
        public int FAT_TIPO_FATURA { get; set; }
    }
}
    