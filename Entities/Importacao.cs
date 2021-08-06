using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Estoque
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Documento { get; set; }
        public string Situacao { get; set; }
        public int Id_Integracao { get; set; }
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public List<Estoque> lstItens { get; set; }
        public string Modal { get; set; }
        public List<Estoque> lstInforme { get; set; }
        public string DescricaoInforme { get; set; }
        public List<Estoque> lstReprocessamento { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public string Item { get; set; }
        public string Comentario { get; set; }
        public String OBSERVACAO { get; set; }
        public string CONSIDERAÇÕES { get; set; }
        public string COR { get; set; }
        public string ID_BrokerSys { get; set; }
        public string NUMNF_BrokerSys { get; set; }
        public string iNTEGRACAO { get; set; }
        public string EMISSAO { get; set; }
        public string RECFISICO { get; set; }
        public double VR_FATURA { get; set; }
        public double TAXA_DI { get; set; }
        public double II { get; set; }
        public double COFINS { get; set; }
        public double IPI { get; set; }
        public double PIS { get; set; }
        public double FreteInter { get; set; }
        public double Seguro { get; set; }
        public double VR_Produto { get; set; }
        public double ICMS { get; set; }
        public double VR_Nota { get; set; }
        public double DespesasCompl { get; set; }
        public double VR_RecFisico { get; set; }
        public string NF_COD_PROCESSO { get; set; }
        public string  Exibir_Emissao { get; set; }
        public string Exibir_Fisico { get; set; }
        public string Exibir_Cancelar { get; set; }
        public string ID_NE { get; set; }
        public string NUMNF_NE { get; set; }
        public string ID_ND { get; set; }
        public string NUMNF_ND { get; set; }
        public string FISICO { get; set; }
        public int TOTAL_PROCESSO { get; set; }
        public Int64 RKPROCESSO { get; set; }
        public string TIPO_NF  { get; set; }
        public Int64 RK_TPNF{ get; set; }
        public Int32 RK_TPNF_ND { get; set; }
        public int ID_TR_BS_OUT_NF { get; set; }
        public string NF_TPNOTA { get; set; }
        public int QtdLinhasProcesso { get; set; }
    }
}
