using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Entities;
using Business;
using System.IO;
using ClosedXML.Excel;
using System.Data;

namespace TAMINTEGRA.Controllers
{
    public class FinanceiroCambioSYSController : Controller
    {
        private FinanceiroCambioSYS cambio = new FinanceiroCambioSYS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private FinanceiroCambioSYSBUS cambioBUS = new FinanceiroCambioSYSBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        PerfilBUS perfilBUS = new PerfilBUS();

        [CustomAuthorize(Roles = "frmFinanceiroImportacaoBaixa")]
        public ActionResult Index(string modal = null, string modalInformativo = null, string situacao = null, string exibir = null, string strDataInicio = null, string strDataFim = null, int tipo_arquivo = 0, string guia = null)
        {
            CarregarDados();

            List<FinanceiroCambioSYS> lstCambio = new List<FinanceiroCambioSYS>();
            DateTime dataInicioDT = new DateTime();
            DateTime dataTerminoDT = new DateTime();

            if (strDataInicio == null || strDataInicio == "")
            {
                var dia = DateTime.Today.AddDays(-10);
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

            cambio.strDataInicio = strDataInicio;
            cambio.strDataFim = strDataFim;

            if (guia == "#divHistorico")
            {
                //lstCambio.Add(new FinanceiroCambioSYS
                //{
                //    Data = new DateTime(2019, 06, 12),
                //    Arquivo = "Teste.txt",
                //    Tipo_Arquivo = "Baixa"

                //});
                //lstCambio.Add(new FinanceiroCambioSYS
                //{
                //    Data = new DateTime(2019, 08, 09),
                //    Arquivo = "Teste2.txt",
                //    Tipo_Arquivo = "Variação Cambial"
                //});

                lstCambio = cambioBUS.Filtro(dataInicioDT, dataTerminoDT);
                
                if(lstCambio != null)
                {
                    foreach (var r in lstCambio)
                    {
                        if (r.Mensagem != null & r.Mensagem != "")
                        {
                            TempData["Mensagem"] = r.Mensagem;
                        }
                        else
                        {
                            TempData["Mensagem"] = null;
                        }
                    }
                }
            }

            cambio.lstItens = lstCambio;

            if(guia == null)
            {
                ViewBag.Guia = "#divArquivoBaixa";
                ViewBag.HideDiv = "#divHistorico";
            }
            else
            {
                if(guia == "#divArquivoBaixa")
                {
                    ViewBag.Guia = guia;
                    ViewBag.HideDiv = "#divHistorico";
                }
                else
                {
                    ViewBag.Guia = guia;
                    ViewBag.HideDiv = "#divArquivoBaixa";
                }
            }

            return View(cambio);
        }

        public void CarregarDados()
        {
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;
            List<Perfil> lstPerfis = perfilBUS.Parametrizacao(usr.Id_Perfil).Where(x => x.Formulario == "frmFinanceiroImportacaoBaixa").ToList();
            foreach (var r in lstPerfis)
            {
                ViewBag.PermitirConsultar = r.Permitir_Consultar;
                ViewBag.PermitirEditar = r.Permitir_Editar;
                ViewBag.PermitirExportar = r.Permitir_Exportar;
            }

        }
        public FileResult DownloadExcel(int id_integracao, string arquivo = null)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // Start a new workbook
                    var wb = new XLWorkbook();

                    List<FinanceiroCambioSYS> lstCambio = new List<FinanceiroCambioSYS>();
                    //lstCambio.Add(new FinanceiroCambioSYS
                    //{
                    //    Data = new DateTime(2019, 06, 12),
                    //    Arquivo = "Teste.txt",
                    //    Tipo_Arquivo = "Baixa"

                    //});
                    lstCambio = cambioBUS.DownloadExcel(id_integracao);
                    IEnumerable<FinanceiroCambioSYS> result = lstCambio;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Texto");

                    foreach (var cambio in result)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Texto"] = cambio.Texto;
                        //if (cambio.Data != null)
                        //{
                        //    DateTime? docDateTemp = cambio.Data;
                        //    string DocDateFormatada = docDateTemp.Value.ToString("dd/MM/yyyy");
                        //    dr["Data"] = DocDateFormatada;
                        //}
                        //else
                        //{
                        //    dr["Data"] = cambio.Data;
                        //}


                     
                        dt.Rows.Add(dr);
                    }



                    // Add a DataTable as a worksheet
                    wb.Worksheets.Add(dt, arquivo);

                    wb.SaveAs(stream, false);

                    // Return a byte array
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", arquivo + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                    //return File(@"C:\Users\itala.cordeiro\Downloads", "application /text", "teste" + ".xlsx");
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public FileResult DownloadTXT(int id_integracao = 0, string arquivo = null)
        {
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);

            List<FinanceiroCambioSYS> lstArquivo = new List<FinanceiroCambioSYS>();
            lstArquivo = cambioBUS.DownloadTxt(id_integracao);
 
            if(lstArquivo != null)
            {
                if(!(arquivo.ToLower().Contains("txt")))
                {
                    arquivo = arquivo + ".txt";
                }

                foreach (var res in lstArquivo)
                {
                    tw.WriteLine(res.Texto);
                }
                tw.Flush();

                var length = memoryStream.Length;
                tw.Close();
                var toWrite = new byte[length];
                Array.Copy(memoryStream.GetBuffer(), 0, toWrite, 0, length);

                return File(toWrite, "application/octet-stream", arquivo);
            }
            else
            {
                return null;
            }
            

        }
        public JsonResult guiaBaixa()
        {
            List<FinanceiroCambioSYS> lstCambioInsert = new List<FinanceiroCambioSYS>();
            List<FinanceiroCambioSYS> lstCambioExcelIS = new List<FinanceiroCambioSYS>();

            try
            {
                Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
                var id_integracao = 0;
                lstCambioInsert = cambioBUS.GuiaBaixaInsert(usr.Id_Pessoa);
                foreach(var r in lstCambioInsert)
                {
                    id_integracao = r.Id_Integracao;
                }

                //Gerar aquivo de inconsistencia
                //lstCambioExcelIS = cambioBUS.ExcelIS(Convert.ToInt32(id_integracao));
            }
            catch(Exception e)
            {
                var erro = e.Message;
            }
            


            return Json(new { ListaCambioInsert = lstCambioInsert, Arquivo_inconsistencia = lstCambioExcelIS }, JsonRequestBehavior.AllowGet);
        }
    }
}