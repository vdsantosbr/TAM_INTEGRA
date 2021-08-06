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

namespace TAMINTEGRA.Controllers
{
    public class CavokController : Controller
    {
        private Cavok cavok = new Cavok();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private CavokBUS cavokBUS = new CavokBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        bool exporta_excel = false;


        [CustomAuthorize(Roles = "frmCavok")]
        public ActionResult Index(string modal = null, string modalInformativo = null, string situacao = null, string exibir = null, string strDataInicio = null, string strDataFim = null, int faturamento = 0, string numeroMov = null)
        {
            CarregarDados();

            List<Cavok> lstCavok = new List<Cavok>();
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

            cavok.strDataInicio = strDataInicio;
            cavok.strDataFim = strDataFim;

            if (exibir != null)
            {
                //lstCavok.Add(new Cavok
                //{
                //    Status = "IS",
                //    Id_Integracao = 1,
                //    Data = new DateTime(2019, 06, 12),
                //    Numero = "5577/2.1.51",
                //    Documento = "Att. Pista"
                //});
                //lstCavok.Add(new Cavok
                //{
                //    Status = "NI",
                //    Id_Integracao = 1,
                //    Data = new DateTime(2019, 06, 12),
                //    Numero = "5577/2.1.51",
                //    Documento = "Att. Pista"
                //});
                lstCavok = cavokBUS.Filtro(dataInicioDT, dataTerminoDT, faturamento, numeroMov, situacao);
            }

            //-------------------------------------------------//
           
            //-------------------------------------------------//

            var lstInforme = TempData["lstInforme"] as List<Cavok>;
            if(lstInforme != null)
            {
                lstCavok.Add(new Cavok
                {
                    Status = "IS",
                    Id_Integracao = 1,
                    Data = new DateTime(2019, 06, 12),
                    Numero = "5577/2.1.51",
                    Documento = "Att. Pista"
                });
                lstCavok.Add(new Cavok
                {
                    Status = "NI",
                    Id_Integracao = 1,
                    Data = new DateTime(2019, 06, 12),
                    Numero = "5577/2.1.51",
                    Documento = "Att. Pista"
                });


                cavok.lstInforme = lstInforme;
            }
            
            var lstReprocessamento = TempData["lstReprocessamento"] as List<Cavok>;
            if (lstReprocessamento != null)
            {
                cavok.lstReprocessamento = lstReprocessamento;
            }

            cavok.lstItens = lstCavok;
            Session["lstCavok"] = lstCavok;

            return View(cavok);
        }
        public ActionResult Exibir()
        {
            return null;
        }

     
        public JsonResult Informativo(int id_integracao = 0, int id_fatura = 0)
        {
            List<Cavok> lstInforme = new List<Cavok>();
            //lstInforme.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            //lstInforme = cavokBUS.Informe(id_integracao);

            //TempData["lstInforme"] = lstInforme.ToList();

            lstInforme = cavokBUS.Informe(id_integracao, id_fatura);
            string strData = "";
            foreach(var r in lstInforme)
            {
                strData = r.Data.ToShortDateString();
            }
            var resultado = (from info in lstInforme
                             select new
                             {
                                 Id_Integracao = info.Id_Integracao,
                                 Situacao = info.Situacao,
                                 Fatura = info.FATURA,
                                 Tipo = info.TIPO,
                                 HANDLING = info.HANDLING,
                                 FORMA_PAGAMENTO = info.FORMA_PAGAMENTO,
                                 MOEDA = info.MOEDA,
                                 VALOR = info.VALOR,
                                 COTACAO = info.COTACAO,
                                 VALOR_TOTAL = info.VALOR_TOTAL,
                                 VALOR_TOTAL_REAIS = info.VALOR_TOTAL_REAIS,
                                 Data = strData,
                                 Consideracoes = info.CONSIDERACOES,
                                 Cliente = info.CLIENTE,
                                 Numero = info.Numero,
                                 Base = info.BASE,
                                 Movimento = info.MOVIMENTO,
                                 ID = info.Id,
                                 Filial = info.FILIAL,
                                 Cor = info.COR
                             }).ToList();


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reprocessar(int id_integracao = 0, int id_fatura = 0)
        {
            CarregarDados();
            List<Cavok> lstReprocessamento = new List<Cavok>();
            string mensagem = "";
            //lstReprocessamento.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            ////lstInforme = cavokBUS.Informe(id_integracao);

            //TempData["lstReprocessamento"] = lstReprocessamento.ToList();


            //lstInforme.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            //lstInforme = cavokBUS.Informe(id_integracao);

            //TempData["lstInforme"] = lstInforme.ToList();

            lstReprocessamento = cavokBUS.Reprocessar(id_integracao, id_fatura, Convert.ToInt32(ViewBag.IdPessoa));
         
            if(lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Item = info.ITEM,
                                     Comentario = info.COMENTARIO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
          
        }
        public FileResult DownloadExcel()
        {
            string arquivo = "Cavok";
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // Start a new workbook
                    var wb = new XLWorkbook();

                    List<Cavok> lstCavok = new List<Cavok>();

                    lstCavok = (List<Cavok>)Session["lstCavok"];
                    IEnumerable<Cavok> result = lstCavok;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Data");
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Número");
                    dt.Columns.Add("Filial");
                    dt.Columns.Add("Base");
                    dt.Columns.Add("Movimento (RM)");
                    dt.Columns.Add("Valor");
                    dt.Columns.Add("Tipo");
                    dt.Columns.Add("Cliente");
                    dt.Columns.Add("Situação");

                    foreach (var cavok in result)
                    {
                        DataRow dr = dt.NewRow();

                        if (cavok.Data != null)
                        {
                            DateTime? data = cavok.Data;
                            string dataFormatada = data.Value.ToString("dd/MM/yyyy");
                            dr["Data"] = dataFormatada;
                        }
                        else
                        {
                            dr["Data"] = cavok.Data;
                        }

                        dr["ID"] = cavok.Id;
                        dr["Número"] = cavok.Numero;
                        dr["Filial"] = cavok.FILIAL;
                        dr["Base"] = cavok.BASE;
                        dr["Movimento (RM)"] = cavok.MOVIMENTO;
                        dr["Valor"] = cavok.VALOR;
                        dr["Tipo"] = cavok.TIPO;
                        dr["Cliente"] = cavok.CLIENTE;
                        dr["Situação"] = cavok.Situacao;

                        dt.Rows.Add(dr);
                    }



                    // Add a DataTable as a worksheet
                    wb.Worksheets.Add(dt, arquivo);

                    wb.SaveAs(stream, false);

                    // Return a byte array
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cavok" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                    //return File(@"C:\Users\itala.cordeiro\Downloads", "application /text", "teste" + ".xlsx");
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void CarregarDados()
        {
            exporta_excel = false;
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            exporta_excel = usuarioBUS.BuscaPorLogin(usr.Login).Exporta_Excel;
            ViewBag.ExportaExcel = exporta_excel;
            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");
            ViewBag.TipoFaturamento = new SelectList(tipoFatBUS.ListaTipoFaturamento(), "ID", "DESCRICAO");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
    }
}