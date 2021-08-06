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
    public class ImportacaoSolPgtoController : Controller
    {
        private ImportacaoSolPgto importacaoSolPgto = new ImportacaoSolPgto();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private ImportacaoSolPgtoBUS importacaoSolPgtoBUS = new ImportacaoSolPgtoBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private FinanceiroProcessoBUS financeiroProcessosBUS = new FinanceiroProcessoBUS();
        private FinanceiroDespesasBUS financeiroDespesasBUS = new FinanceiroDespesasBUS();

        [CustomAuthorize(Roles = "frmImportacaoNota")]
        public ActionResult Index(string situacao = null, string exibir = null, string strDataInicio = null, string strDataFim = null, string tipoDespesa = null, string codCredorDespesa = null, string codigoCredorDespesa = null, string processo = null)
        {
            CarregarDados();

            List<ImportacaoSolPgto> lstImportacaoSolPgto = new List<ImportacaoSolPgto>();
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

            importacaoSolPgto.strDataInicio = strDataInicio;
            importacaoSolPgto.strDataFim = strDataFim;

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
                lstImportacaoSolPgto = importacaoSolPgtoBUS.Filtro(dataInicioDT, dataTerminoDT, tipoDespesa, codigoCredorDespesa, processo, situacao);
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

            var lstInforme = TempData["lstInforme"] as List<ImportacaoSolPgto>;
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


            importacaoSolPgto.lstInforme = lstInforme;
            //}

            var lstReprocessamento = TempData["lstReprocessamento"] as List<ImportacaoSolPgto>;
            if (lstReprocessamento != null)
            {
                importacaoSolPgto.lstReprocessamento = lstReprocessamento;
            }

            importacaoSolPgto.lstItens = lstImportacaoSolPgto;


            return View(importacaoSolPgto);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public JsonResult Informativo(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<ImportacaoSolPgto> lstInforme = new List<ImportacaoSolPgto>();

            lstInforme = importacaoSolPgtoBUS.Informe(id_integracao, sp_id, sp_id_despesa_processo);
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
        public JsonResult Reprocessar(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            CarregarDados();
            List<ImportacaoSolPgto> lstReprocessamento = new List<ImportacaoSolPgto>();

            lstReprocessamento = importacaoSolPgtoBUS.Reprocessar(id_integracao, id_brokersys, numnf_brokersys);

            if (lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Observacao = info.OBSERVACAO
                                     //Item = info.ITEM,
                                     //Comentario = info.COMENTARIO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Excluir(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            CarregarDados();
            List<ImportacaoSolPgto> lstReprocessamento = new List<ImportacaoSolPgto>();

            lstReprocessamento = importacaoSolPgtoBUS.Reprocessar(id_integracao, id_brokersys, numnf_brokersys);

            if (lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Observacao = info.OBSERVACAO
                                     //Item = info.ITEM,
                                     //Comentario = info.COMENTARIO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public void CarregarDados()
        {
            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");

            List<FinanceiroDespesas> lstTiposDespesas = new List<FinanceiroDespesas>();
            lstTiposDespesas = financeiroDespesasBUS.TiposDespesas();
            List<FinanceiroDespesas> lstCredor = financeiroDespesasBUS.Credor();
            //if (lstTiposDespesas == null || lstTiposDespesas.Count == 0)
            //{
            //    lstTiposDespesas = new List<FinanceiroDespesas>();
            //    FinanceiroDespesas desp = new FinanceiroDespesas("", 0, null, null, null, null);
            //    lstTiposDespesas.Add(desp);
            //    ViewBag.TipoFaturamento = new SelectList(lstTiposDespesas, "SP_COD_CREDOR_DESPESA", "SP_COD_DESPESA"); ;
            //}
            //else
            //{
                ViewBag.TipoFaturamento = new SelectList(lstTiposDespesas, "SP_COD_CREDOR_DESPESA", "SP_COD_DESPESA");
            //}

            //if (lstCredor == null)
            //{
            //    lstCredor = new List<FinanceiroDespesas>();
            //    FinanceiroDespesas desp = new FinanceiroDespesas("", 0, null, null, null, null);
            //    lstCredor.Add(desp);
            //    ViewBag.Credor = new SelectList(lstCredor, "SP_CODIGO_CREDOR_DESPESA", "CREDOR"); ;
            //}
            //else
            //{
                ViewBag.Credor = new SelectList(lstCredor, "SP_CODIGO_CREDOR_DESPESA", "CREDOR");
            //}

            //ViewBag.TipoFaturamento = new SelectList(financeiroDespesasBUS.TiposDespesas(), "SP_COD_CREDOR_DESPESA", "SP_COD_DESPESA");
            //ViewBag.Credor = new SelectList(financeiroDespesasBUS.Credor(), "SP_CODIGO_CREDOR_DESPESA", "CREDOR");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
    }
}