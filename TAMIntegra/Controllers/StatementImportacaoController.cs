using Business;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using System.IO;
using System.Data;
using System.Globalization;
using ClosedXML.Excel;

namespace TAMINTEGRA.Controllers
{
    //[CustomAuthorize(Roles = "frmStatementImportacao")]
    [CustomAuthorize(Roles = "frmStatement")]

    public class StatementImportacaoController : Controller
    {
        // GET: StatementImportacao
        private StatementImportacaoBUS statementBUS = new StatementImportacaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private StatementContasBUS contasBUS = new StatementContasBUS();
        private ImportacaoConciliacaoBUS impConcBUS = new ImportacaoConciliacaoBUS();
        private ImportacaoRMFluxusBUS impRMFluxus = new ImportacaoRMFluxusBUS();
        private StatementConciliacaoBUS statementConcBUS = new StatementConciliacaoBUS();

        public ActionResult Index(int ano = 0, int mes = 0, string statementFiltro = null, string rmFluxusFiltro = null, string contaFiltro = null,
            string[] dataBaseFiltro = null, string uploadInput = null, string filtro = "")
        {
            //ModelState.Clear();

            if (ano == 0)
            {
                ano = DateTime.Now.Year;
            }

            if (mes == 0)
            {
                mes = DateTime.Now.Month;
            }

            StatementImportacao imp = new StatementImportacao();
            List<StatementContas> contas = contasBUS.Lista(null, null, null);
            ViewBag.Contas = new SelectList(contas.Where(x => x.Situacao == "Ativo"), "Id_Conta", "Conta");

            ViewData["Importacao_Concilicacao"] = impConcBUS.Lista(ano, mes);
            ViewData["Importacao_RMFluxus"] = impRMFluxus.Lista(ano, mes);

            List<StatementImportacao> lst = new List<StatementImportacao>();
            //lst.Add(new StatementImportacao
            //{
            //    Origem = "Conciliaçao",
            //    Importacao = new DateTime(2019, 02, 01),
            //    Conciliacao = new DateTime(2019, 02, 01),
            //    SituacaoConciliacao = "Ativo",
            //    Conta = "Cessna 77105 - Compras",
            //    DataImportacao = new DateTime(2019, 02, 01),
            //    DataBase = new DateTime(2019, 02, 01),
            //    QtdeRegistros = 2,
            //    SituacaoStatement = "Ativo",
            //    DocumentoNo = "100422",
            //    SORef = "355891",
            //    Invoice = "IJ48988300",
            //    Id_Conta = 1
            //});

            List<StatementImportacao> lstDropDown = statementBUS.Lista(0, 0);
            if (lstDropDown != null)
            {
                ViewBag.StatementFiltro = new SelectList(lstDropDown, "Id_Statement", "Conta");
                ViewBag.Data_Base = new SelectList(
                      lstDropDown
                      .GroupBy(p => p.Data_Base)
                      .Select(g => g.First())
                      .Where(x => x.Data_Base != null)
                      .ToList()
                    , "Data_Base", "Data_Base");
            }
            else
            {
                ViewBag.StatementFiltro = null;
                ViewBag.Data_Base = null;
            }

            try
            {
                ViewBag.RMFluxusFiltro = new SelectList(impRMFluxus.Lista(0, 0), "Id_RM_Fluxus", "IMAGEM_RM");
            }
            catch (Exception e)
            {
                ViewBag.RMFluxusFiltro = null;
            }

            lst = statementBUS.Lista(ano, mes);
            imp.lstConciliacao = lst;

            if(filtro == "")
            {
                if (uploadInput == null || uploadInput == "")
                {
                    ViewBag.Divshow = "conciliacao";
                }
                else
                {
                    ViewBag.Divshow = uploadInput;
                }
            }
            else
            {
                ViewBag.Divshow = filtro;
            }


            imp.Ano = ano;
            imp.Mes = mes;

            return View(imp);
        }

        public ActionResult salvarStatement(string conta, string dataBase)
        {
            CarregarDados();

            DateTime dataBasedt = DateTime.Parse(dataBase);

            statementBUS.SalvarStatement(conta, dataBasedt, Convert.ToInt32(ViewBag.Id_pessoa));

            return null;
        }

        public string excluirStatement(int id_statement = 0)
        {
            CarregarDados();

            try
            {
                statementBUS.ExcluirStatement(id_statement, Convert.ToInt32(ViewBag.Id_pessoa));
                return "Statement excluído com sucesso!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string excluirConciliacao(int id_conciliacao = 0)
        {
            CarregarDados();

            try
            {
                statementBUS.ExcluirConciliacao(id_conciliacao, Convert.ToInt32(ViewBag.Id_pessoa));
                return "Conciliação excluída com sucesso!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string excluirRMFluxus(int id_RM_Fluxus = 0)
        {
            CarregarDados();

            try
            {
                statementBUS.ExcluirRMFluxus(id_RM_Fluxus, Convert.ToInt32(ViewBag.Id_pessoa));
                return "Imagem excluída com sucesso!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public ActionResult GerarImagem()
        {
            try
            {
                impRMFluxus.GerarImagem();
                TempData["Mensagem"] = "O procedimento de geração de imagem RM Fluxus foi iniciado. Aguarde alguns minutos para conferência.";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
                return RedirectToAction("Index");
            }

        }

        public string AgendarConciliacao(int idStatement, int idRMFluxus)
        {
            CarregarDados();

            return impConcBUS.AgendarConciliacao(idStatement, idRMFluxus, Convert.ToInt32(ViewBag.Id_pessoa)); ;
        }

        public ActionResult ExecutarConciliacao()
        {
            try
            {
                impConcBUS.ExecutarConciliacao();
                //TempData["Mensagem"] = "O procedimento de execução da conciliação foi iniciado. Aguarde alguns minutos para conferência.";
                TempData["Mensagem"] = "O processamento para as conciliações agendadas está em atividade!";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Admin(HttpPostedFileBase upload, FormCollection form = null)
        {
            StatementImportacao imp;
            string conta = form["id_conta"];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            if (upload != null && upload.ContentLength > 0)
            {
                StreamReader stream = new StreamReader(upload.InputStream);

                int indexMes;
                string mes;
                int indexDia;
                string dia;
                string fimData;
                string ano;
                var line = stream.ReadLine();

                while (!stream.EndOfStream)
                {
                    try
                    {
                        line = stream.ReadLine();
                        var values = line.Split(',');
                        imp = new StatementImportacao();

                        //string data = "02/19/2019 12:00:00 AM";
                        //int indexMes = data.IndexOf("/", 1);
                        //string mes = data.Substring(0, indexMes);
                        //int indexDia = data.IndexOf('/', data.IndexOf('/') + 1);
                        //string ano = data.Substring(indexMes + 1, indexDia - (indexMes + 1));

                        //DateTime dt = DateTime.ParseExact("02/19/2019 12:00:00 AM", "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);


                        string dt_doc_str = values[1];
                        string dt_due_str = values[2];
                        string dt_paid_date = values[3];

                        var formats = new string[]
                        {
                           "MM/dd/yyyy hh:mm:ss tt",
                           "MM/d/yyyy hh:mm:ss tt",
                           "M/dd/yyyy hh:mm:ss tt",
                           "M/d/yyyy hh:mm:ss tt"
                        };

                        DateTime data_doc;
                        DateTime due_date;
                        DateTime paid_date_date;

                        if (DateTime.TryParseExact(dt_doc_str, formats, CultureInfo.InvariantCulture,
                                                      DateTimeStyles.None, out data_doc))
                        {
                            imp.Doc_Date = data_doc;
                        }

                        if (DateTime.TryParseExact(dt_due_str, formats, CultureInfo.InvariantCulture,
                                                     DateTimeStyles.None, out due_date))
                        {
                            imp.NET_DUE_DT = due_date;
                        }



                        imp.Invoice = values[0];
                        imp.Id_Statement = Convert.ToInt32(form["idStatementModal"]);
                        imp.Doc_Type = values[4];
                        imp.Amount = decimal.Parse(values[6], CultureInfo.InvariantCulture);
                        imp.Po_Number = values[8];
                        imp.So_Ref = values[9].Trim();

                        if (dt_paid_date == "")
                        {
                            imp.Paid_Date = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                        }
                        else
                        {
                            if (DateTime.TryParseExact(dt_paid_date, formats, CultureInfo.InvariantCulture,
                                                   DateTimeStyles.None, out paid_date_date))
                            {
                                imp.Paid_Date = paid_date_date;
                            }
                        }


                        statementBUS.Insere(imp, Convert.ToInt32(usr.Id_Pessoa));
                    }
                    catch (Exception e)
                    {
                        return View();
                    }
                }
            }

            return null;
            //        try
            //        {
            //            if (upload != null && upload.ContentLength > 0)
            //            {
            //                StatementImportacao imp;

            //                Stream stream = upload.InputStream;
            //                IExcelDataReader excelReader = null;

            //                if (upload.FileName.EndsWith(".xls"))
            //                {
            //                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            //                }
            //                else if (upload.FileName.EndsWith(".xlsx"))
            //                {
            //                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            //                }
            //                else
            //                {
            //                    ModelState.AddModelError("File", "Arquivo inválido!");
            //                    return View();
            //                }

            //                DataSet result = excelReader.AsDataSet();

            //                if (result.HasErrors == false)
            //                {
            //                    //imp = new StatementImportacao();

            //                    foreach (DataTable table in result.Tables)
            //                    {
            //                        for (int i = 0; i < table.Rows.Count; i++)
            //                        {
            //                            imp = new StatementImportacao();
            //                            imp.Conta = "77701";

            //                            statementBUS.Insere(imp, Convert.ToInt32(User.Identity.Name));

            //                        }
            //                    }
            //                    excelReader.Close();

            //                    TempData["Mensagem"] = "Rab importado com sucesso!";
            //                    TempData["FechaPopUp"] = 1;

            //                }
            //                else
            //                {
            //                    TempData["Mensagem"] = "Erro ao ler arquivo";
            //                }
            //            }

            //            ViewBag.Divshow = "statement";
            //            return null;
            //            }
            //        catch (Exception ex)
            //        {
            //            TempData["Mensagem"] = ex.Message;
            //            return RedirectToAction("Admin");
            //        }
            //    }
        }

        public void CarregarDados()
        {
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.Id_Pessoa = usr.Id_Pessoa;
        }

        public FileResult Download2(int id_Statement)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // Start a new workbook
                    var wb = new XLWorkbook();

                    IEnumerable<StatementImportacao> result = statementBUS.Download(id_Statement); //aeronaveRabBUS.Lista();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Conta");
                    dt.Columns.Add("PO Number");
                    dt.Columns.Add("SO Ref");
                    dt.Columns.Add("Invoice");
                    dt.Columns.Add("Doc Date");
                    dt.Columns.Add("Net Due Date");
                    dt.Columns.Add("Amount");
                    dt.Columns.Add("Amount Reveived");
                    dt.Columns.Add("Balance");
                    dt.Columns.Add("Arrear");
                    dt.Columns.Add("Document No");
                    dt.Columns.Add("Paid Date");
                    dt.Columns.Add("Doc Type");
                    dt.Columns.Add("Currency");
                    dt.Columns.Add("Aircraft");
                    dt.Columns.Add("Suffix");
                    dt.Columns.Add("Check");
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Text");

                    foreach (var ent in result)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Conta"] = ent.Conta;
                        dr["PO Number"] = ent.Po_Number;
                        dr["SO Ref"] = ent.So_Ref;
                        dr["Invoice"] = ent.Invoice;

                        if (ent.Doc_Date != null)
                        {
                            DateTime? docDateTemp = ent.Doc_Date;
                            string DocDateFormatada = docDateTemp.Value.ToString("dd/MM/yyyy");
                            dr["Doc Date"] = DocDateFormatada;
                        }
                        else
                        {
                            dr["Doc Date"] = ent.Doc_Date;
                        }

                        //dr["Doc Date"] = ent.Doc_Date;

                        if(ent.NET_DUE_DT != null)
                        {
                            DateTime? NetDueDateTemp = ent.NET_DUE_DT;
                            string NetdueDateFormatada = NetDueDateTemp.Value.ToString("dd/MM/yyyy");
                            dr["Net Due Date"] = NetdueDateFormatada;
                        }
                        else
                        {
                            dr["Net Due Date"] = ent.NET_DUE_DT;
                        }

                        //dr["Net Due Date"] = ent.NET_DUE_DT;
                        dr["Amount"] = ent.Amount;
                        dr["Amount Reveived"] = ent.AMOUNT_REVEIVED;
                        dr["Balance"] = ent.Balance;
                        dr["Arrear"] = ent.Arrear;
                        dr["Document No"] = ent.Documento_No;

                        if (ent.Paid_Date != null)
                        {
                            DateTime? PaidDateTemp = ent.Paid_Date;
                            string PaidDateFormatada = PaidDateTemp.Value.ToString("dd/MM/yyyy");
                            dr["Paid Date"] = PaidDateFormatada;
                        }
                        else
                        {
                            dr["Paid Date"] = ent.Paid_Date;
                        }

                        //dr["Paid Date"] = ent.Paid_Date;
                        dr["Doc Type"] = ent.Doc_Type;
                        dr["Currency"] = ent.Currency;
                        dr["Aircraft"] = ent.Aircraft;
                        dr["Suffix"] = ent.Suffix;
                        dr["Check"] = ent.Check;
                        dr["Status"] = ent.Status;
                        dr["Text"] = ent.Text;

                        dt.Rows.Add(dr);
                    }



                    // Add a DataTable as a worksheet
                    wb.Worksheets.Add(dt, "Extração");

                    wb.SaveAs(stream, false);

                    // Return a byte array
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Extração_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                    //return File(@"C:\Users\itala.cordeiro\Downloads", "application /text", "teste" + ".xlsx");
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public FileResult Download(string id_statement)
        {
            string nomeArquivo = "tste.pdf";

            string contentType = "application/pdf";
            return File(nomeArquivo, contentType, "Report.pdf");
        }

    }
  }

