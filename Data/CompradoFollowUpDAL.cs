using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class CompradoFollowUpDAL
    {
        private DatabaseContext db = new DatabaseContext();
        public List<CompraFollowUp> Filtro(DateTime? data_Inicio, DateTime? data_Termino, string pedido = "", string aplicacao = "", string pn = "", string invoice = "", string conhecimento = "", string status_compra = "", string status_pedido = "", string processo = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@data_Inicio", (data_Inicio == null) ? (object)DBNull.Value : data_Inicio);
                SqlParameter pDataTermino = new SqlParameter("@data_Termino", (data_Termino == null) ? (object)DBNull.Value : data_Termino);
                SqlParameter pPedido = new SqlParameter("@PEDIDO", (pedido == "") ? "" : pedido);
                SqlParameter pAplicacao = new SqlParameter("@APLICACAO", (aplicacao == "") ? "" : aplicacao);
                SqlParameter pPN = new SqlParameter("@PN", (pn == "") ? "" : pn);
                SqlParameter pInvoice = new SqlParameter("@INVOICE", (invoice == "") ? "" : invoice);
                SqlParameter pConchecimento = new SqlParameter("@CONHECIMENTO", (conhecimento == "") ? "" : conhecimento);
                SqlParameter pStatusCompra = new SqlParameter("@STATUS_COMPRA", (status_compra == "") ? "" : status_compra);
                SqlParameter pStatusPedido = new SqlParameter("@STATUS_PEDIDO", (status_pedido == "") ? "" : status_pedido);
                SqlParameter pProcesso = new SqlParameter("@PROCESSO", (processo == "") ? "" : processo);


                try
                {
                    var linha = db.Database.SqlQuery<CompraFollowUp>("STO_S_FOLLOWUP_Filtro @data_Inicio, @data_Termino, @PEDIDO, @APLICACAO, @PN, @INVOICE, @CONHECIMENTO, @STATUS_COMPRA, @STATUS_PEDIDO, @PROCESSO", pDataInicio, pDataTermino, pPedido, pAplicacao, pPN, pInvoice, pConchecimento, pStatusCompra, pStatusPedido, pProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraFollowUp>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraFollowUp>();
                }
            }
        }
        public List<CompraFollowUp> Excel(DateTime? data_Inicio, DateTime? data_Termino, string pedido = "", string aplicacao = "", string pn = "", string invoice = "", string conhecimento = "", string status_compra = "", string status_pedido = "", string processo = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@data_Inicio", (data_Inicio == null) ? (object)DBNull.Value : data_Inicio);
                SqlParameter pDataTermino = new SqlParameter("@data_Termino", (data_Termino == null) ? (object)DBNull.Value : data_Termino);
                SqlParameter pPedido = new SqlParameter("@PEDIDO", (pedido == "") ? "" : pedido);
                SqlParameter pAplicacao = new SqlParameter("@APLICACAO", (aplicacao == "") ? "" : aplicacao);
                SqlParameter pPN = new SqlParameter("@PN", (pn == "") ? "" : pn);
                SqlParameter pInvoice = new SqlParameter("@INVOICE", (invoice == "") ? "" : invoice);
                SqlParameter pConchecimento = new SqlParameter("@CONHECIMENTO", (conhecimento == "") ? "" : conhecimento);
                SqlParameter pStatusCompra = new SqlParameter("@STATUS_COMPRA", (status_compra == "") ? "" : status_compra);
                SqlParameter pStatusPedido = new SqlParameter("@STATUS_PEDIDO", (status_pedido == "") ? "" : status_pedido);
                SqlParameter pProcesso = new SqlParameter("@PROCESSO", (processo == "") ? "" : processo);


                try
                {
                    var linha = db.Database.SqlQuery<CompraFollowUp>("STO_S_FOLLOWUP  @data_Inicio, @data_Termino, @PEDIDO, @APLICACAO, @PN, @INVOICE, @CONHECIMENTO, @STATUS_COMPRA, @STATUS_PEDIDO, @PROCESSO", pDataInicio, pDataTermino, pPedido, pAplicacao, pPN, pInvoice, pConchecimento, pStatusCompra, pStatusPedido, pProcesso).ToList();
                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraFollowUp>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraFollowUp>();
                }
            }
        }
        public List<CompraFollowUp> Informativo(int idmov = 0, int nseqitmov = 0, int idprd = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pIdmov = new SqlParameter("@IDMOV", (idmov == 0) ? 0 : idmov);
                SqlParameter pNseqitmov = new SqlParameter("@NSEQITMMOV", (nseqitmov == 0) ? 0 : nseqitmov);
                SqlParameter pIdprd = new SqlParameter("@IDPRD", (idprd == 0) ? 0 : idprd);

                try
                {
                    var linha = db.Database.SqlQuery<CompraFollowUp>("STO_S_FOLLOWUP_Info @IDMOV, @NSEQITMMOV, @IDPRD", pIdmov, pNseqitmov, pIdprd).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraFollowUp>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraFollowUp>();
                }
            }
        }
        public List<CompraFollowUp> Editar(int idmov = 0, int nseqitmov = 0, int idprd = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pIdmov = new SqlParameter("@IDMOV", (idmov == 0) ? 0 : idmov);
                SqlParameter pNseqitmov = new SqlParameter("@NSEQITMMOV", (nseqitmov == 0) ? 0 : nseqitmov);
                SqlParameter pIdprd = new SqlParameter("@IDPRD", (idprd == 0) ? 0 : idprd);

                try
                {
                    var linha = db.Database.SqlQuery<CompraFollowUp>("STO_S_FOLLOWUP_Info @IDMOV, @NSEQITMMOV, @IDPRD", pIdmov, pNseqitmov, pIdprd).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraFollowUp>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraFollowUp>();
                }
            }
        }
        public List<CompraFollowUp> Salvar(int idmov = 0, int nseqitmov = 0, int idprd = 0, string status = "", DateTime? prazo = null, string observacao = "", string house = "", string processo = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pIdmov = new SqlParameter("@IDMOV", (idmov == 0) ? 0 : idmov);
                SqlParameter pNseqitmov = new SqlParameter("@NSEQITMMOV", (nseqitmov == 0) ? 0 : nseqitmov);
                SqlParameter pIdprd = new SqlParameter("@IDPRD", (idprd == 0) ? 0 : idprd);
                SqlParameter pStatus = new SqlParameter("@STATUS_COMPRA", (status == "") ? "" : status);
                //SqlParameter pPrazo = new SqlParameter("@PRAZO_ENTREGA", (prazo == null) ? DateTime.MinValue : prazo);
                SqlParameter pHouse = new SqlParameter("@HOUSE", (house == "") ? "" : house);
                SqlParameter pProcesso = new SqlParameter("@PROCESSO", (processo == "") ? "" : processo);
                SqlParameter pPrazo = new SqlParameter("@PRAZO_ENTREGA", System.Data.SqlDbType.DateTime);
                if (prazo.ToString() == "01/01/0001 00:00:00")
                {
                    pPrazo.Value = DBNull.Value;
                }
                else
                {
                    pPrazo.Value = prazo;
                }
               
               
                SqlParameter pObservacao = new SqlParameter("@OBSERVACAO", (observacao == "") ? "" : observacao);

                try
                {
                    var linha = db.Database.SqlQuery<CompraFollowUp>("STO_U_FOLLOWUP @IDMOV, @NSEQITMMOV, @IDPRD, @STATUS_COMPRA, @PRAZO_ENTREGA, @OBSERVACAO, @HOUSE, @PROCESSO", pIdmov, pNseqitmov, pIdprd, pStatus, pPrazo, pObservacao, pHouse, pProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraFollowUp>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraFollowUp>();
                }
            }
        }
        public List<CompraFollowUp> Importar(CompraFollowUp compra)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    SqlParameter pProcesso = new SqlParameter("@PROCESSO", (compra.PROCESSO == "") ? "" : compra.PROCESSO);
                    SqlParameter pOrdem = new SqlParameter("@ORDEM", (compra.ORDEM == "") ? "" : compra.ORDEM);
                    SqlParameter pExportador = new SqlParameter("@EXPORTADOR", (compra.EXPORTADOR == "") ? "" : compra.EXPORTADOR);
                    SqlParameter pPartNumber = new SqlParameter("@PART_NUMBER", (compra.PART_NUMBER == "") ? "" : compra.PART_NUMBER);
                    SqlParameter pQuantidade = new SqlParameter("@QUANTIDADE", (compra.QUANTIDADE == 0) ? 0 : compra.QUANTIDADE);
                    //SqlParameter pPrazo = new SqlParameter("@PRAZO_ENTREGA", (prazo == null) ? DateTime.MinValue : prazo);
                    SqlParameter pNCM = new SqlParameter("@NCM", (compra.NCM == "") ? "" : compra.NCM);
                    //SqlParameter pDtLiberacao = new SqlParameter("@DT_LIBERACAO", (compra.STR_DT_LIBERACAO == "") ? "" : compra.STR_DT_LIBERACAO);
                    SqlParameter pNumFatura = new SqlParameter("@NUM_FATURA", (compra.NUM_FATURA == "") ? "" : compra.NUM_FATURA);

                    SqlParameter pDtLiberacao = new SqlParameter("@DT_LIBERACAO", System.Data.SqlDbType.DateTime);
                    if (compra.DT_LIBERACAO.ToString() == "01/01/0001 00:00:00" || compra.DT_LIBERACAO == null)
                    {
                        pDtLiberacao.Value = DBNull.Value;
                    }
                    else
                    {
                        pDtLiberacao.Value = compra.DT_LIBERACAO;
                    }

                    var linha = db.Database.SqlQuery<CompraFollowUp>("STO_U_FOLLOWUP_IMPORTACAO @PROCESSO, @ORDEM, @EXPORTADOR, @PART_NUMBER, @QUANTIDADE, @NCM, @DT_LIBERACAO, @NUM_FATURA", pProcesso, pOrdem, pExportador, pPartNumber, pQuantidade, pNCM, pDtLiberacao, pNumFatura).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraFollowUp>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraFollowUp>();
                }
            }
        }
        public List<CompraFollowUp> Item(int idmov = 0, int nseqitmov = 0, int idprd = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdmov = new SqlParameter("@IDMOV", (idmov == 0) ? 0 : idmov);
                SqlParameter pNseqitmov = new SqlParameter("@NSEQITMMOV", (nseqitmov == 0) ? 0 : nseqitmov);
                SqlParameter pIdprd = new SqlParameter("@IDPRD", (idprd == 0) ? 0 : idprd);


                try
                {
                    var linha = db.Database.SqlQuery<CompraFollowUp>("STO_S_FOLLOWUP_Item  @IDMOV, @NSEQITMMOV, @IDPRD", pIdmov, pNseqitmov, pIdprd).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<CompraFollowUp>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<CompraFollowUp>();
                }
            }
        }
    }
}
