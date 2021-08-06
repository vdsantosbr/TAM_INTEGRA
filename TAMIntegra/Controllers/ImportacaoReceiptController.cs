using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Entities;
using Business;
using System.Globalization;
using System.IO;
using ClosedXML.Excel;
using System.Data;

namespace TAMINTEGRA.Controllers
{
    public class ImportacaoReceiptController : Controller
    {
        private ImportacaoReceipt receipt = new ImportacaoReceipt();
        private ImportacaoReceiptBUS impBUS = new ImportacaoReceiptBUS();

        [CustomAuthorize(Roles = "frmImportacaoReceipt")]
        public ActionResult Index(string strDataInicio = "", string strDataFim = "", int status = 0, string transactionNumber = "", string bill = "", string partNumber = "", string exibir = "")
        {
            List<ImportacaoReceipt> lstReceipt = new List<ImportacaoReceipt>();
            DateTime dataInicioDT = new DateTime();
            DateTime dataTerminoDT = new DateTime();

            if (strDataInicio == null || strDataInicio == "")
            {
                var dia = DateTime.Today.AddDays(-31);
                strDataInicio = dia.ToString("dd/MM/yyyy");
                dataInicioDT = dia;
            }

            if (strDataFim == null || strDataFim == "")
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

            receipt.strDataInicio = strDataInicio;
            receipt.strDataFim = strDataFim;

            if(exibir != "")
            {
                lstReceipt = impBUS.Filtro(status, dataInicioDT, dataTerminoDT, "Receipt", transactionNumber, bill, partNumber);
                receipt.lstReceipts = lstReceipt;
                Session["lstDadosImpReceipt"] = lstReceipt;
            }

            return View(receipt);
        }
        public JsonResult ConfirmarConferencia(String[] dados = null, int id_integracao = 0, int transaction_number = 0, string bill = "")
        {
            List<ImportacaoReceipt> lstInforme = new List<ImportacaoReceipt>();
            string linha = "";
            string[] linhaSplit = null;
            //int id_integracao = 0;
            //int transaction_number = 0;
            //string bill = "";

            if(dados != null)
            {
                for (var i = 0; i <= dados.Count() - 1; i++)
                {
                    linha = dados[i];
                    linhaSplit = linha.Split(',');
                    id_integracao = Convert.ToInt32(linhaSplit[0]);
                    transaction_number = Convert.ToInt32(linhaSplit[1]);
                    bill = linhaSplit[2];

                    impBUS.ConfirmarConferencia(id_integracao, transaction_number, bill);
                }
            }
            else
            {
                impBUS.ConfirmarConferencia(id_integracao, transaction_number, bill);
            }

            return Json(lstInforme, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Informativo(int id_integracao = 0, int transaction_number = 0, string bill = "")
        {
            List<ImportReceiptInformativo> lst = new List<ImportReceiptInformativo>();
            lst = impBUS.Informativo(id_integracao, transaction_number, bill.Trim());
            if(lst == null)
            {
                lst = new List<ImportReceiptInformativo>();
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public FileResult DownloadExcel(string strDataInicio, string strDataFim, string pedido = "", string aplicacao = "", string pn = "", string invoice = "", string conhecimento = "", string status_compra = "", string status_pedido = "", string processo = "")
        {
            string arquivo = "Importação_Receipt";
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

                    IEnumerable<ImportacaoReceipt> result = (IEnumerable <ImportacaoReceipt>) Session["lstDadosImpReceipt"];

                    DataTable dt = new DataTable();
                    dt.Columns.Add("STATUS");
                    dt.Columns.Add("DATA");
                    dt.Columns.Add("TRANSACTION");
                    dt.Columns.Add("INVOICE");
                    dt.Columns.Add("INVOICE DATE");
                    dt.Columns.Add("CUSTOMER");
                    dt.Columns.Add("MEANS OF TRANSPORT");
                    dt.Columns.Add("COUNTRY OF SHIPPING");
                    dt.Columns.Add("WAREHOUSE");
                    dt.Columns.Add("BILL OF LADING");
                    dt.Columns.Add("TOTAL WEIGHT");
                    dt.Columns.Add("TOTAL VALUE");
                    dt.Columns.Add("MVTO");
                    dt.Columns.Add("NUM. MVTO");
                    dt.Columns.Add("EMISSÃO");
                    foreach (var res in result)
                    {
                        string status = "";
                        DataRow dr = dt.NewRow();

                        if(res.Status == 2)
                        {
                            status = "Confirmado";
                        }
                        else if (res.Status == 1)
                        {
                            status = "Não Confirmado";
                        }
                        dr["STATUS"] = status;

                        if (res.Data != null)
                        {
                            DateTime? data = res.Data;
                            string dataFmt = data.Value.ToString("dd/MM/yyyy");
                            dr["DATA"] = dataFmt;
                        }
                        else
                        {
                            dr["DATA"] = res.Data;
                        }
                        
                        dr["TRANSACTION"] = res.Transaction_Number;
                        dr["INVOICE"] = res.Invoice_Number;

                        if (res.Invoice_Date != null)
                        {
                            string invoiceDate = DateTime.ParseExact(res.Invoice_Date, "yyyy-MM-dd",  CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                            dr["INVOICE DATE"] = invoiceDate;
                        }
                        else
                        {
                            dr["INVOICE DATE"] = res.Invoice_Date;
                        }
                        dr["CUSTOMER"] = res.Customer;
                        dr["MEANS OF TRANSPORT"] = res.Means_of_Transport;
                        dr["COUNTRY OF SHIPPING"] = res.Country_of_Shipping;
                        dr["WAREHOUSE"] = res.Warehouse;
                        dr["BILL OF LADING"] = res.Bill_of_Lading_Number;
                        dr["TOTAL WEIGHT"] = res.Total_Weight.Value.ToString("0.00");
                        dr["TOTAL VALUE"] = res.Total_Value.Value.ToString("0.00");
                        dr["MVTO"] = res.CodTmv;
                        dr["NUM. MVTO"] = res.NumeroMov;

                        if (res.DataEmissao != null)
                        {
                            DateTime? dataEmissao = res.DataEmissao;
                            string dataEmissaoFmt = dataEmissao.Value.ToString("dd/MM/yyyy");
                            dr["EMISSÃO"] = dataEmissaoFmt;
                        }
                        else
                        {
                            dr["EMISSÃO"] = res.DataEmissao;
                        }
                        //if (res.PDC_EMISSAO != null)
                        //{
                        //    DateTime? pdcEmissao = res.PDC_EMISSAO;
                        //    string pdcEmissaoFmt = pdcEmissao.Value.ToString("dd/MM/yyyy");
                        //    dr["PDC_EMISSAO"] = pdcEmissaoFmt;
                        //}
                        //else
                        //{
                        //    dr["PDC_EMISSAO"] = res.PDC_EMISSAO;
                        //}

                        //dr["PDC_NUMERO"] = res.PDC_NUMERO;
                        //dr["PDC_ITEM"] = res.PDC_ITEM;
                        //dr["PDC_CODIGO"] = res.PDC_CODIGO;
                        //dr["DESCRICAO"] = res.Descricao;
                        //dr["PDC_QUANTIDADE"] = res.PDC_QUANTIDADE;
                        //dr["PDC_QTD_PENDENTE"] = res.PDC_QTD_PENDENTE;
                        //dr["APLICACAO"] = res.Aplicacao;
                        //dr["PDC_STATUS"] = res.PDC_STATUS;
                        //dr["PROCESSO"] = res.PROCESSO;
                        //dr["INVOICE"] = res.INVOICE;
                        //dr["HOUSE"] = res.HOUSE;
                        //dr["TIPO_IMPORTACAO"] = res.TIPO_IMPORTACAO;
                        //dr["STATUS_COMPRA"] = res.Status_Compra;
                        //dr["PRAZO_ENTREGA"] = res.Prazo_Entrega;
                        //dr["OBSERVACAO"] = res.OBSERVACAO;
                        //dr["PDC_QTD_PENDENTE"] = res.PDC_QTD_PENDENTE;
                        //dr["REQUISITANTE"] = res.REQUISITANTE;
                        //dr["COMPRADOR"] = res.COMPRADOR;
                        //dr["FORNECEDOR"] = res.FORNECEDOR;
                        //string s = "";
                        //if (res.NFE_EMISSAO != null)
                        //{
                        //    s = res.NFE_EMISSAO.Value.ToString("dd/MM/yyyy");
                        //}
                        //dr["NFE_EMISSAO"] = s;
                        //dr["NFE_NUMERO"] = res.NFE_NUMERO;

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
    }
}