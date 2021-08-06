using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Entities;
using Business;
using System.Globalization;

namespace TAMINTEGRA.Controllers
{
    public class ImportacaoFaturaController : Controller
    {
        private ImportacaoFatura importacaoFatura = new ImportacaoFatura();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private ImportacaoFaturaBUS importacaoFaturaBUS = new ImportacaoFaturaBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private FinanceiroProcessoBUS financeiroProcessosBUS = new FinanceiroProcessoBUS();

        [CustomAuthorize(Roles = "frmImportacaoNota")]
        public ActionResult Index(string exibir = null, string strDataInicio = null, string strDataFim = null, string tipoFatura = "", string processo = null, string numDI= null, string numInvoice = null, string situacao = null, int id_integracao = 0)
        {
            CarregarDados();

            List<ImportacaoFatura> lstImportacaoFatura = new List<ImportacaoFatura>();
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

            importacaoFatura.strDataInicio = strDataInicio;
            importacaoFatura.strDataFim = strDataFim;

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
                lstImportacaoFatura = importacaoFaturaBUS.Filtro(dataInicioDT, dataTerminoDT, tipoFatura, processo, numDI, numInvoice, situacao);

            //    var tipoMoedas = lstEstoque
            //    .GroupBy(w => w.MOEDA)
            //    .Select(g => new
            //    {
            //        Valor = g.Select(c => c.VALOR),
            //        TipoMoeda = g.Select(c => c.MOEDA)
            //    }).ToList();

            //    var r = from p in lstEstoque
            //            group p by p.MOEDA into g
            //            select new
            //            {
            //                TipoMoeda = (string)g.Select(x => x.MOEDA).First() + ": " + g.Sum(x => x.VALOR).ToString("C", CultureInfo.CurrentCulture).Replace("R$ ", ""),
            //                Valor = g.Select(x => x.VALOR)
            //            };

            //    var results = from line in lstEstoque
            //                  group line by line.MOEDA into g
            //                  select new
            //                  {
            //                      ProductName = g.First().MOEDA,
            //                      Price = g.Sum(x => x.VALOR).ToString("C", CultureInfo.CurrentCulture),
            //                  };

            //    List<string> lstTipoMoedas = new List<string>();
            //    foreach (var res in r)
            //    {
            //        lstTipoMoedas.Add(res.TipoMoeda);
            //    }

            //    estoque.lstMoedas = lstTipoMoedas;
            }

            //-------------------------------------------------//

            //-------------------------------------------------//

            var lstInforme = TempData["lstInforme"] as List<ImportacaoFatura>;
            //if (lstInforme != null)
            //{
            //lstCavok.Add(new FinanceiroServicos
            //{
            //    Status = "IS",
            //    Id_Integracao = 1,
            //    Data = new DateTime(2019, 06, 12),
            //    Numero = "5577/2.1.51",
            //    Documento = "Att. Pista"
            //});
            //lstCavok.Add(new FinanceiroServicos
            //{
            //    Status = "NI",
            //    Id_Integracao = 1,
            //    Data = new DateTime(2019, 06, 12),
            //    Numero = "5577/2.1.51",
            //    Documento = "Att. Pista"
            //});


            importacaoFatura.lstInforme = lstInforme;
            //}

            var lstReprocessamento = TempData["lstReprocessamento"] as List<ImportacaoFatura>;
            if (lstReprocessamento != null)
            {
                importacaoFatura.lstReprocessamento = lstReprocessamento;
            }

            importacaoFatura.lstItens = lstImportacaoFatura;


            return View(importacaoFatura);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public JsonResult Informativo(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<ImportacaoFatura> lstInforme = new List<ImportacaoFatura>();

            lstInforme = importacaoFaturaBUS.Informe(id_integracao, sp_id, sp_id_despesa_processo);
            string strData = "";
            DateTime date = DateTime.ParseExact("2017/07/31 08:08:24", "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            //foreach (var r in lstInforme)
            //{
            //    strData = r.Data.ToShortDateString();
            //    strLiberacao = DateTime.ParseExact(r.LIBERACAO, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToShortDateString();
            //    strVencimento = DateTime.ParseExact(r.VENCIMENTO, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToShortDateString();
            //    strCadastro = DateTime.ParseExact(r.CADASTRO, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToShortDateString();
            //}


            var resultado = (from info in lstInforme
                             select new
                             {
                                 //Cadastro = strCadastro,
                                 //Liberacao = strLiberacao,
                                 //Vencimento = strVencimento,
                                 //Documento = info.Documento,
                                 Id_Integracao = info.Id_Integracao,
                                 Id = info.Id,
                                 Data = strData,
                                 Situacao = info.Situacao,
                                 Cor = info.COR,
                                 Consideracoes = info.CONSIDERAÇÕES
                             }).ToList();


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reprocessar(int id_integracao = 0, string id_fatura = null)
        {
            CarregarDados();
            List<ImportacaoFatura> lstReprocessamento = new List<ImportacaoFatura>();

            lstReprocessamento = importacaoFaturaBUS.Reprocessar(id_integracao, id_fatura);

            if (lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Id_Integracao = info.Id_Integracao,
                                     Observacao = info.OBSERVACAO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Cancelar(int id_integracao = 0, string fat_fatura_id = null)
        {
            List<ImportacaoFatura> lstCancelar = new List<ImportacaoFatura>();

            //lstInforme = importacaoFaturaBUS.Cancelar(id_integracao, fat_fatura_id);
            string strData = "";
            DateTime date = DateTime.ParseExact("2017/07/31 08:08:24", "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);

            if(lstCancelar != null)
            {
                var resultado = (from info in lstCancelar
                                 select new
                                 {
                                     Id_Integracao = info.Id_Integracao,
                                     Observacao = info.OBSERVACAO
                                 }).ToList();

                return Json(lstCancelar, JsonRequestBehavior.AllowGet);
            }

             else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public void CarregarDados()
        {
            List<FinanceiroDespesas> lstTipoFatura = new List<FinanceiroDespesas>();
            List<ImportacaoFatura> lstCredor = new List<ImportacaoFatura>();
            //FinanceiroDespesas desp = new FinanceiroDespesas("Pró-Forma", 0, null, null, null, null);
            //lstTipoFatura.Add(desp);
            //desp = new FinanceiroDespesas("Comercial", 1, null, null, null, null);
            //lstTipoFatura.Add(desp);
            //desp = new FinanceiroDespesas("Cancelada", 2, null, null, null, null);
            //lstTipoFatura.Add(desp);
            //desp = new FinanceiroDespesas("Comercial X Pró-Forma", 3, null, null, null, null);
            //lstTipoFatura.Add(desp);
            lstTipoFatura.Add(new FinanceiroDespesas
            {
                NOME_DESPESA = "Pró-Forma",
                ID_DESPESA = 0
            });
            lstTipoFatura.Add(new FinanceiroDespesas
            {
                NOME_DESPESA = "Comercial",
                ID_DESPESA = 1
            });
            lstTipoFatura.Add(new FinanceiroDespesas
            {
                NOME_DESPESA = "Cancelada",
                ID_DESPESA = 2
            });
            lstTipoFatura.Add(new FinanceiroDespesas
            {
                NOME_DESPESA = "Comercial X Pró-Forma",
                ID_DESPESA = 3
            });
            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");
            ViewBag.TipoFatura = new SelectList(lstTipoFatura, "ID_DESPESA", "NOME_DESPESA");
            //lstCredor = importacaoFaturaBUS.Credor();
            //if(lstCredor == null)
            //{
            //    ViewBag.Credor = "";
            //}
            //else
            //{
            //    ViewBag.Credor = new SelectList(importacaoFaturaBUS.Credor(), "SP_CODIGO_CREDOR_DESPESA", "CREDOR");
            //}
           
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
    }
}