using Business;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using System.Data;
using ClosedXML.Excel;
using ExcelDataReader;
using System.Globalization;

namespace TAMIntegra.Controllers
{
    public class CompraFollowUpController : Controller
    {
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        List<CompraFollowUp> lstDados = new List<CompraFollowUp>();
        private CompraFollowUpBUS compraBUS = new CompraFollowUpBUS();
        PerfilBUS perfilBUS = new PerfilBUS();
        bool exporta_excel = false;
        bool permite_edicao = false;

        private ImportacaoDocumentoBUS importacaoDocumentoBUS = new ImportacaoDocumentoBUS();
        CompraFollowUp comprador = new CompraFollowUp();

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [CustomAuthorize(Roles = "frmComprasFollowUP")]
        public ActionResult Index(string strDataInicio, string strDataFim, string dtIni2 = "", string dtFim = "", string pedido = "", string aplicacao = "", string pn = "", string invoice = "", string conhecimento = "", string exibir = "", string ckDatas = "", string status_compra = "", string status_pedido = "", string processo = "")
        {
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);
            DateTime? dataTerminoDT = null;
            DateTime? dataInicioDT = null;

            //if(dtIni2 != "")
            //{
            //    strDataInicio = dtIni2;
            //    strDataFim = dtFim;
            //}

            if (strDataInicio == null)
            {
                //var dia = DateTime.Now.Day - 10;
                strDataInicio = DateTime.Today.ToString("dd/MM/yyyy");
                dataInicioDT = DateTime.Parse(strDataInicio);
            }

            if (strDataFim == null)
            {
                strDataFim = DateTime.Today.ToString("dd/MM/yyyy");
                dataTerminoDT = DateTime.Parse(strDataFim);
            }


            if (!string.IsNullOrWhiteSpace(strDataInicio))
            {
                dataInicioDT = DateTime.Parse(strDataInicio);
            }

            if (!string.IsNullOrWhiteSpace(strDataFim))
            {
                dataTerminoDT = DateTime.Parse(strDataFim);
            }

            CarregaDados(0);

            if (exibir != null)
            {
                //if(id_integracao > 0 || idMov > 0)
                //{
                //    ViewBag.Id_Integracao_Agendamento = id_integracao;
                //    ViewBag.IdMov_Desbloqueio = idMov;
                //    lstComprador = compradorBUS.PedidoCompraLista(dataInicioDT, dataTerminoDT, strFornecedor, strDescricao, strTipoMov, numeroMov, id_Perfil, "frmComprasCessnaPedidoCompra", situacao, id_integracao, idMov);
                //}
                //else
                //{
                //    lstComprador = compradorBUS.PedidoCompraLista(dataInicioDT, dataTerminoDT, strFornecedor, strDescricao, strTipoMov, numeroMov, id_Perfil, "frmComprasCessnaPedidoCompra", situacao, 0, 0);
                //}
                //lstDados.Add(new CompraFollowUp
                //{
                //    Pedido = "173929",
                //    Item = 1,
                //    PN = "ST3U32ST4M",
                //    Aplicacao = "PR-ESP - SUPER KING AIR",
                //    Status = "Aberto",
                //    Entrega = new DateTime(2020, 01, 22)
                //});
                if(ckDatas != "on")
                {
                    lstDados = compraBUS.Filtro(null, null, pedido, aplicacao, pn, invoice, conhecimento, status_compra, status_pedido, processo);
                }
                else
                {
                    lstDados = compraBUS.Filtro(dataInicioDT, dataTerminoDT, pedido, aplicacao, pn, invoice, conhecimento, status_compra, status_pedido, processo);
                }
                
            }
            comprador.strDataInicio = strDataInicio;
            comprador.strDataFim = strDataFim;
            comprador.ckDatas = ckDatas;
            comprador.Pedido = pedido;

            IEnumerable<CompraFollowUp> result = lstDados;
            comprador.LstDados = result.ToList();

            return View(comprador);
        }

        private void CarregaDados(int id_integracao_processo = 0)
        {
            exporta_excel = false;
            permite_edicao = false;

            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            exporta_excel = usuarioBUS.BuscaPorLogin(usr.Login).Exporta_Excel;
            permite_edicao = usuarioBUS.BuscaPorLogin(usr.Login).Permite_Edicao; ;
            ViewBag.ExportaExcel = exporta_excel;
            ViewBag.PermiteEdicao = permite_edicao;
            //id_integracao_processo = 13;
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);

            List<SelectListItem> lstStatuscompra = new List<SelectListItem>();
            lstStatuscompra.Add(new SelectListItem() { Text = "AG. APROVAÇÃO", Value = "1" });
            lstStatuscompra.Add(new SelectListItem() { Text = "AG. EMBARQUE", Value = "2" });
            lstStatuscompra.Add(new SelectListItem() { Text = "AG. SAIR", Value = "3" });
            lstStatuscompra.Add(new SelectListItem() { Text = "BACKORDER", Value = "4" });
            lstStatuscompra.Add(new SelectListItem() { Text = "DG", Value = "5" });
            lstStatuscompra.Add(new SelectListItem() { Text = "EMBARCADO", Value = "6" });
            lstStatuscompra.Add(new SelectListItem() { Text = "REPARO EXTERNO", Value = "7" });
            //lstStatuscompra.Add(new SelectListItem() { Text = "VACKORDER", Value = "8" });
            ViewBag.StatusCompra = new SelectList(lstStatuscompra, "Text", "Text");

            List<SelectListItem> lstStatusPedido = new List<SelectListItem>();
            lstStatusPedido.Add(new SelectListItem() { Text = "ABERTO", Value = "1" });
            lstStatusPedido.Add(new SelectListItem() { Text = "RECEBIDO PARCIAL", Value = "2" });
            lstStatusPedido.Add(new SelectListItem() { Text = "RECEBIDO", Value = "3" });
            ViewBag.StatusPedido = new SelectList(lstStatusPedido, "Text", "Text");


            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");

            //Integracao integracao = integracaoBUS.BuscaIdIntegracaoProcessoBUS("frmComprasCessnaPedidoCompra");
            ViewBag.IdIntegracaoProcesso = id_integracao_processo;

            //string login = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().Replace("TAMMRL\\", "");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            ViewBag.IdPessoa = usr.Id_Pessoa;

            List<Perfil> lstPerfis = perfilBUS.Parametrizacao(usr.Id_Perfil).Where(x => x.Formulario == "frmComprasFollowUP").ToList();
            foreach (var r in lstPerfis)
            {
                ViewBag.PermitirConsultar = r.Permitir_Consultar;
                ViewBag.PermitirEditar = r.Permitir_Editar;
                ViewBag.PermitirExportar = r.Permitir_Exportar;
            }

        }
        public JsonResult Editar(int idMov = 0, int nseqitmov = 0, int idprd = 0)
        {
            CarregaDados();
            List<CompraFollowUp> lstInfo = new List<CompraFollowUp>();
            string mensagem = "";
            string dtPrazo = DateTime.Today.ToString("dd/MM/yyyy");
            DateTime dt_dtPrazo = DateTime.Parse(dtPrazo);

            lstInfo = compraBUS.Item(idMov, nseqitmov, idprd); ;
            string strData = "";

            comprador.LstDados = lstInfo;
            return Json(new { lstInfo, LstDados = lstInfo }, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult Salvar(int idMov = 0, int nseqitmmov = 0, int idprd = 0, string status = "", string prazo = null, string observacao = "", string house = "", string processo = "", List<String> dados = null)

        {
            CarregaDados();

            string linha = "";
            string[] linhaSplit = null;
            string idMovLst = "";
            string nseqitmmovLst = "";
            string idprdLst = "";


            DateTime prazodt = new DateTime();
            if (prazo != "")
            {
                prazodt = DateTime.Parse(prazo);
            } 

            if(status == "-- Selecione --")
            {
                status = "";
            }

            List<CompraFollowUp> lstInfo = new List<CompraFollowUp>();

            if (dados != null)
            {
                for (var i = 0; i <= dados.Count() - 1; i++)
                {
                    linha = dados[i];
                    linhaSplit = linha.Split(',');
                    idMovLst = linhaSplit[0];
                    nseqitmmovLst = linhaSplit[1];
                    idprdLst = linhaSplit[2];

                    compraBUS.Salvar(Convert.ToInt32(idMovLst), Convert.ToInt32(nseqitmmovLst), Convert.ToInt32(idprdLst), status, prazodt, observacao, house, processo);
                }
            }
            else
            {
                lstInfo = compraBUS.Salvar(idMov, nseqitmmov, idprd, status, prazodt, observacao, house, processo);
            }

           /// lstInfo = compraBUS.Salvar(idMov, nseqitmmov, idprd, status, prazodt, observacao, house, processo);

            string strData = "";

            return Json(lstInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Informativo(int idMov = 0, int nseqitmmov = 0, int idprd = 0)
        {
            CarregaDados();

            List<CompraFollowUp> lstInfo = new List<CompraFollowUp>();

            lstInfo = compraBUS.Informativo(idMov, nseqitmmov, idprd);

            return Json(lstInfo, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadExcel(string strDataInicio, string strDataFim, string pedido = "", string aplicacao = "", string pn = "", string invoice = "", string conhecimento = "", string status_compra = "", string status_pedido = "", string processo = "")
        {
            string arquivo = "Compras_FollowUp";
            DateTime? dataTerminoDT = null;
            DateTime? dataInicioDT = null;
            if (strDataInicio == null)
            {
                //var dia = DateTime.Now.Day - 10;
                strDataInicio = DateTime.Today.ToString("dd/MM/yyyy");
                dataInicioDT = DateTime.Parse(strDataInicio);
            }

            if (strDataFim == null)
            {
                strDataFim = DateTime.Today.ToString("dd/MM/yyyy");
                dataTerminoDT = DateTime.Parse(strDataFim);
            }


            if (!string.IsNullOrWhiteSpace(strDataInicio))
            {
                dataInicioDT = DateTime.Parse(strDataInicio);
            }

            if (!string.IsNullOrWhiteSpace(strDataFim))
            {
                dataTerminoDT = DateTime.Parse(strDataFim);
            }

            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // Start a new workbook
                    var wb = new XLWorkbook();

                    List<CompraFollowUp> lst = new List<CompraFollowUp>();
                    lst = compraBUS.Excel(dataInicioDT, dataTerminoDT, pedido, aplicacao, pn, invoice, conhecimento, status_compra, status_pedido, processo);

                    IEnumerable<CompraFollowUp> result = lst;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("PDC_NUMERO");
                    dt.Columns.Add("PDC_ITEM");
                    dt.Columns.Add("PDC_CODIGO");
                    dt.Columns.Add("DESCRICAO");
                    dt.Columns.Add("PDC_QUANTIDADE");
                    dt.Columns.Add("PDC_QTD_PENDENTE");
                    dt.Columns.Add("APLICACAO");
                    dt.Columns.Add("NEC_EMISSAO");
                    dt.Columns.Add("PDC_EMISSAO");
                    dt.Columns.Add("PDC_STATUS");
                    dt.Columns.Add("PROCESSO");
                    dt.Columns.Add("INVOICE");
                    dt.Columns.Add("HOUSE");
                    dt.Columns.Add("TIPO_IMPORTACAO");
                    dt.Columns.Add("STATUS_COMPRA");
                    dt.Columns.Add("PRAZO_ENTREGA");
                    dt.Columns.Add("NFE_EMISSAO");
                    dt.Columns.Add("NFE_NUMERO");
                    dt.Columns.Add("OBSERVACAO");
                    dt.Columns.Add("REQUISITANTE");
                    dt.Columns.Add("COMPRADOR");
                    dt.Columns.Add("FORNECEDOR");

                    foreach (var res in result)
                    {
                        DataRow dr = dt.NewRow();

                        if (res.NEC_EMISSAO != null)
                        {
                            DateTime? necEmissao = res.NEC_EMISSAO;
                            string necEmissaoFmt = necEmissao.Value.ToString("dd/MM/yyyy");
                            dr["NEC_EMISSAO"] = necEmissaoFmt;
                        }
                        else
                        {
                            dr["NEC_EMISSAO"] = res.NEC_EMISSAO;
                        }

                        if (res.PDC_EMISSAO != null)
                        {
                            DateTime? pdcEmissao = res.PDC_EMISSAO;
                            string pdcEmissaoFmt = pdcEmissao.Value.ToString("dd/MM/yyyy");
                            dr["PDC_EMISSAO"] = pdcEmissaoFmt;
                        }
                        else
                        {
                            dr["PDC_EMISSAO"] = res.PDC_EMISSAO;
                        }

                        dr["PDC_NUMERO"] = res.PDC_NUMERO;
                        dr["PDC_ITEM"] = res.PDC_ITEM;
                        dr["PDC_CODIGO"] = res.PDC_CODIGO;
                        dr["DESCRICAO"] = res.Descricao;
                        dr["PDC_QUANTIDADE"] = res.PDC_QUANTIDADE;
                        dr["PDC_QTD_PENDENTE"] = res.PDC_QTD_PENDENTE;
                        dr["APLICACAO"] = res.Aplicacao;
                        dr["PDC_STATUS"] = res.PDC_STATUS;
                        dr["PROCESSO"] = res.PROCESSO;
                        dr["INVOICE"] = res.INVOICE;
                        dr["HOUSE"] = res.HOUSE;
                        dr["TIPO_IMPORTACAO"] = res.TIPO_IMPORTACAO;
                        dr["STATUS_COMPRA"] = res.Status_Compra;
                        dr["PRAZO_ENTREGA"] = res.Prazo_Entrega;
                        dr["OBSERVACAO"] = res.OBSERVACAO;
                        dr["PDC_QTD_PENDENTE"] = res.PDC_QTD_PENDENTE;
                        dr["REQUISITANTE"] = res.REQUISITANTE;
                        dr["COMPRADOR"] = res.COMPRADOR;
                        dr["FORNECEDOR"] = res.FORNECEDOR;
                        string s = "";
                        if (res.NFE_EMISSAO != null)
                        {
                           s = res.NFE_EMISSAO.Value.ToString("dd/MM/yyyy");
                        }
                        dr["NFE_EMISSAO"] = s;
                        dr["NFE_NUMERO"] = res.NFE_NUMERO;

                        dt.Rows.Add(dr);
                    }



                    // Add a DataTable as a worksheet
                    wb.Worksheets.Add(dt, arquivo);

                    wb.SaveAs(stream, false);

                    // Return a byte array
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", arquivo + "_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                    //return File(@"C:\Users\itala.cordeiro\Downloads", "application /text", "teste" + ".xlsx");
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ActionResult Importar(HttpPostedFileBase upload, string strDataInicio, string strDataFim, string pedidoImp = "", string aplicacao = "", string pn = "", string invoice = "", string conhecimento = "", string exibir = "", string status_compra = "", string status_pedido = "", string processo = "")
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    CompraFollowUp compra;
                    Stream stream = upload.InputStream;
                    IExcelDataReader excelReader = null;

                    if (upload.FileName.ToUpper().EndsWith(".XLS"))
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.ToUpper().EndsWith(".XLSX"))
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Arquivo inválido!");
                        return View();
                    }

                    DataSet result = excelReader.AsDataSet();

                    if (result.HasErrors == false)
                    {
                        //rab = new Rab();
                        //rdBUS = new RabDetalheBUS();
                        //rab.IdUsuario = Convert.ToInt16(User.Identity.Name);
                        //rab.DataImportacao = DateTime.Now;
                        //rab.NomeArquivo = upload.FileName;
                        //int idRab = rabBUS.Insere(rab);

                        foreach (DataTable table in result.Tables)
                        {
                            for (int i = 1; i < table.Rows.Count; i++)
                            {
                              

                                compra = new CompraFollowUp();
                                string fmt_dt = "";

                                if (compra.DT_LIBERACAO != null)
                                {
                                    DateTime? dt = compra.DT_LIBERACAO;
                                    fmt_dt = dt.Value.ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    fmt_dt = "";
                                }
                                compra.STR_DT_LIBERACAO = fmt_dt;

                                compra.PROCESSO = table.Rows[i][0].ToString();
                                compra.ORDEM = table.Rows[i][1].ToString();
                                compra.EXPORTADOR = table.Rows[i][2].ToString();
                                compra.PART_NUMBER = table.Rows[i][3].ToString();
                                string quantidade = table.Rows[i][4].ToString();
                                compra.QUANTIDADE = Convert.ToDouble(quantidade);
                                compra.NCM = table.Rows[i][5].ToString();
                                compra.STR_DT_LIBERACAO = table.Rows[i][6].ToString();
                                compra.NUM_FATURA = table.Rows[i][7].ToString();
                                if(compra.STR_DT_LIBERACAO != null & compra.STR_DT_LIBERACAO != "")
                                {
                                    compra.DT_LIBERACAO = Convert.ToDateTime(compra.STR_DT_LIBERACAO);
                                }
                               
                                compraBUS.Importar(compra);
                            }
                        }
                        excelReader.Close();

                        //TempData["Mensagem"] = "Arquivo importado com sucesso!";
                        //TempData["FechaPopUp"] = 1;
                    }
                    else
                    {
                        TempData["Mensagem"] = "Erro ao ler conteúdo do arquivo";
                    }
                }
                else
                {
                    TempData["Mensagem"] = "Erro ao ler arquivo";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TempData["Mensagem"] = ex.Message;
                //return RedirectToAction("Index");
                return Json(new {erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
