using Business;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TAMIntegra.App_Start;

namespace TAMINTEGRA.Controllers
{   
    public class FinanceiroFaturasController : Controller
    {
        private FinanceiroFaturas faturas = new FinanceiroFaturas();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private FinanceiroFaturasBUS financeiroBUS = new FinanceiroFaturasBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();

        [CustomAuthorize(Roles = "frmFinanceiroFatura")]
        // GET: FinanceiroFaturas
        public ActionResult Index(string modal = null, string modalInformativo = null, string situacao = null, string exibir = null, string strDataInicio = null, string strDataFim = null, int faturamento = 0, string numDI = null, string numProcesso = null, string numInvoice = null)
        {
            CarregarDados();

            List<FinanceiroFaturas> lstFaturas = new List<FinanceiroFaturas>();
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

            faturas.strDataInicio = strDataInicio;
            faturas.strDataFim = strDataFim;

            if (exibir != null)
            {
                //lstCavok.Add(new FinanceiroFaturas
                //{
                //    Status = "IS",
                //    Id_Integracao = 1,
                //    Data = new DateTime(2019, 06, 12),
                //    Numero = "5577/2.1.51",
                //    Documento = "Att. Pista"
                //});
                //lstCavok.Add(new FinanceiroFaturas
                //{
                //    Status = "NI",
                //    Id_Integracao = 1,
                //    Data = new DateTime(2019, 06, 12),
                //    Numero = "5577/2.1.51",
                //    Documento = "Att. Pista"
                //});
                lstFaturas = financeiroBUS.Filtro(dataInicioDT, dataTerminoDT, faturamento, numDI, numInvoice, numProcesso, situacao);
                if(lstFaturas != null)
                {
                    var tipoMoedas = lstFaturas
                    .GroupBy(w => w.FAT_COD_MOEDA)
                    .Select(g => new
                    {
                        Valor = g.Select(c => c.FAT_VMCV_TOTAL),
                        TipoMoeda = g.Select(c => c.FAT_COD_MOEDA)
                    }).ToList();

                    var r = from p in lstFaturas
                            group p by p.FAT_COD_MOEDA into g
                            select new
                            {
                                TipoMoeda = (string)g.Select(x => x.FAT_COD_MOEDA).First() + ": " + g.Sum(x => x.FAT_VMCV_TOTAL).ToString("C", CultureInfo.CurrentCulture).Replace("R$ ", ""),
                                Valor = g.Select(x => x.FAT_VMCV_TOTAL)
                            };

                    var results = from line in lstFaturas
                                  group line by line.FAT_COD_MOEDA into g
                                  select new
                                  {
                                      ProductName = g.First().FAT_COD_MOEDA,
                                      Price = g.Sum(x => x.FAT_VMCV_TOTAL).ToString("C", CultureInfo.CurrentCulture),
                                  };

                    List<string> lstTipoMoedas = new List<string>();
                    foreach (var res in r)
                    {
                        lstTipoMoedas.Add(res.TipoMoeda);
                    }

                    faturas.lstMoedas = lstTipoMoedas;

                }


            }
            //-------------------------------------------------//

            //-------------------------------------------------//

            //var lstInforme = TempData["lstInforme"] as List<FinanceiroFaturas>;
            //if (lstInforme != null)
            //{
            //    lstFaturas.Add(new FinanceiroFaturas
            //    {
            //        Status = "IS",
            //        Id_Integracao = 1,
            //        Data = new DateTime(2019, 06, 12),
            //        Numero = "5577/2.1.51",
            //        Documento = "Att. Pista"
            //    });
            //    lstFaturas.Add(new FinanceiroFaturas
            //    {
            //        Status = "NI",
            //        Id_Integracao = 1,
            //        Data = new DateTime(2019, 06, 12),
            //        Numero = "5577/2.1.51",
            //        Documento = "Att. Pista"
            //    });


            //    faturas.lstInforme = lstInforme;
            //}

            var lstReprocessamento = TempData["lstReprocessamento"] as List<FinanceiroFaturas>;
            if (lstReprocessamento != null)
            {
                faturas.lstReprocessamento = lstReprocessamento;
            }

            faturas.lstItens = lstFaturas;


            return View(faturas);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public JsonResult Informativo(int id_integracao = 0, string id_fatura = null)
        {
            List<FinanceiroFaturas> lstInforme = new List<FinanceiroFaturas>();
            string mensagem = "";
            //lstInforme.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            //lstInforme = cavokBUS.Informe(id_integracao);

            //TempData["lstInforme"] = lstInforme.ToList();

            lstInforme = financeiroBUS.Informe(id_integracao, id_fatura);
            string[] strData = null;
            string ano = "";
            string mes = "";
            string dia = "";
            string data = "";
            string strVencimento = "";
            string strDataFatura = "";
            foreach (var r in lstInforme)
            {
                //strData = r.FAT_DATA_FATURA.Replace(" 00:00:00", "").Split('-');
                strVencimento = r.DATA_VENCIMENTO.ToShortDateString();
                //strDataFatura = DateTime.ParseExact(r.FAT_DATA_FATURA, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToShortDateString();
                strDataFatura = r.FAT_DATA_FATURA.ToShortDateString();
            }

            //dia = strData[2];
            //mes = strData[1];
            //ano = strData[0];
            //data = dia + "/" + mes + "/" + ano;
            if (lstInforme != null)
            {
                var resultado = (from info in lstInforme
                                 select new
                                 {
                                     Exportador = info.EXPORTADOR,
                                     NumFatura = info.FAT_NUM_FATURA,
                                     TipoFatura = info.FAT_TIPO_FATURA,
                                     DataFatura = strDataFatura,
                                     CodProcesso = info.FAT_COD_PROCESSO,
                                     DI = info.FAT_NUM_DI,
                                     CondPgto = info.FAT_COND_PAGTO,
                                     Moeda = info.FAT_COD_MOEDA,
                                     Embarque = info.FAT_EMBARQUE_NUM,
                                     Valor = info.FAT_VMCV_TOTAL,
                                     NumConhecimento = info.FAT_NUMERO_CONHECIMENTO,
                                     UserName = info.FAT_USERNAME,
                                     DataVenc = strVencimento,
                                     TipoProcesso = info.FAT_TIPO_CONHEC_PROCESSO,
                                     Processo = info.PROCESSO,
                                     Id_integracao = info.Id_Integracao,
                                     Id = info.Id,
                                     Data = data,
                                     Situacao = info.Situacao,
                                     Tipo = info.TIPO,
                                     Complemento = info.COMPLEMENTO,
                                     IdInt = info.ID_INT,
                                     CodTMV = info.CODTMV_INT,
                                     ChaveOrigem = info.CHAVEORIGEM_INT,
                                     Identificador = info.IDENTIFICADOR,
                                     Movimento = info.MOVIMENTO,
                                     Documento = info.Documento,
                                     Cor = info.COR,
                                     Consideracoes = info.CONSIDERAÇÕES
                                 }).ToList();


                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Reprocessar(int id_integracao = 0, string id_fatura = null)
        {
            CarregarDados();
            List<FinanceiroFaturas> lstReprocessamento = new List<FinanceiroFaturas>();
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

            lstReprocessamento = financeiroBUS.Reprocessar(id_integracao, id_fatura);
            //string strData = "";

            if (lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Item = info.Item,
                                     Comentario = info.OBSERVACAO,
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Excluir(int id_integracao = 0, string id_fatura = null)
        {
            CarregarDados();
            List<FinanceiroFaturas> lstReprocessamento = new List<FinanceiroFaturas>();
            string mensagem = "";

            lstReprocessamento = financeiroBUS.Excluir(id_integracao, id_fatura);
            //string strData = "";

            if (lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Item = info.Item,
                                     Comentario = info.OBSERVACAO,
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
            ViewBag.TipoFaturamento = new SelectList(tipoFatBUS.ListaTipoFaturamento(), "ID", "DESCRICAO");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
    }
}