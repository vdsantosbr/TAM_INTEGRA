using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CompraFollowUp
    {
        public string Pedido { get; set; }
        //public int  Item { get; set; }
        public string PN { get; set; }
        //public int Qtd { get; set; }
        public string Aplicacao { get; set; }
        public string Status_Compra { get; set; }
        public string Prazo_Entrega { get; set; }
        public int Idmov { get; set; }
        public int NSEQITMMOV { get; set; }
        public int Idprd { get; set; }
        public string Aprovacao { get; set; }
        //public DateTime Dt_Necessidade { get; set; }
        //public DateTime Dt_Prazo { get; set; }
        //public string Descricao { get; set; }
        public int Id_status_compra { get; set; }
        public string Desc_status_compra { get; set; }
        public string PDC_NUMERO { get; set; }
        public int PDC_ITEM { get; set; }
        public string PDC_CODIGO { get; set; }
        public string Descricao { get; set; }
        public double? PDC_QUANTIDADE { get; set; }
        public double? NF_ITE_QTDE { get; set; }
        public double? PDC_QTD_PENDENTE { get; set; }
        public DateTime? NEC_EMISSAO { get; set; }
        public DateTime? PDC_EMISSAO { get; set; }
        public string PDC_STATUS { get; set; }
        public string NF_COD_PROCESSO { get; set; }
        public string NF_ITE_NM_NUM_INVOICE { get; set; }
        public string NF_NUM_CONHECIMENTO { get; set; }
        public string TIPO_IMPORTACAO { get; set; }
        public string OBSERVACAO { get; set; }
        public string REQUISITANTE { get; set; }
        public string COMPRADOR { get; set; }
        public string FORNECEDOR { get; set; }
        public string INVOICE { get; set; }
        public string PROCESSO { get; set; }
        public string CONHECIMENTO { get; set; }

        public List<CompraFollowUp> LstDados { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public string ckDatas { get; set; }
        public string Status_Pedido { get; set; }
        public String HOUSE { get; set; }
        public string ORDEM { get; set; }
        public string EXPORTADOR { get; set; }
        public double QUANTIDADE { get; set; }
        public string NCM { get; set; }
        public DateTime? DT_LIBERACAO { get; set; }
        public string STR_DT_LIBERACAO { get; set; }
        public string NUM_FATURA { get; set; }
        public string PART_NUMBER { get; set; }
        public DateTime? NFE_EMISSAO { get; set; }
        public string NFE_NUMERO { get; set; }

        //public CompraFollowUp (int id_status_compra= 0, string desc_status_compra = "")
        //{
        //    this.Id_status_compra = id_status_compra;
        //    this.Desc_status_compra = desc_status_compra;
        //}
    }
}
