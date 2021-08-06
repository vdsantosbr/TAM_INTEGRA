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
    public class EstoqueController : Controller
    {
        private Estoque estoque = new Estoque();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private EstoqueBUS estoqueBUS = new EstoqueBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private FinanceiroProcessoBUS financeiroProcessosBUS = new FinanceiroProcessoBUS();
        private CredorBUS credorBUS = new CredorBUS();

        [CustomAuthorize(Roles = "frmImportacaoNota")]
        public ActionResult Index(string exibir = null, string strDataInicio = null, string strDataFim = null, string declaracao = null, string processo = null, string nota = null, string situacao = null)
        {
            CarregarDados();

            List<Estoque> lstEstoque = new List<Estoque>();
            DateTime dataInicioDT = new DateTime();
            DateTime dataTerminoDT = new DateTime();

            if (strDataInicio == null || strDataInicio == "")
            {
                strDataInicio = DateTime.Today.ToString("dd/MM/yyyy");
                dataInicioDT = DateTime.Parse(strDataInicio);
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

            estoque.strDataInicio = strDataInicio;
            estoque.strDataFim = strDataFim;
            estoque.Situacao = situacao;

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
                lstEstoque = estoqueBUS.Filtro(dataInicioDT, dataTerminoDT, declaracao, processo, nota, situacao);
                if(lstEstoque != null)
                {
                    ViewBag.SituacaoFiltro = situacao;
                }
                else
                {
                    ViewBag.SituacaoFiltro = "";
                }




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
            if(lstEstoque != null)
            {
                var groups = lstEstoque
                .GroupBy(n => n.NF_COD_PROCESSO)
                .Select(n => new
                {
                Processo = n.Key,
                Total = n.Count()
                }
                )
                .OrderBy(n => n.Processo);
                foreach (var r in lstEstoque)
                {
                    foreach (var x in groups)
                    {
                        if (r.NF_COD_PROCESSO == x.Processo)
                        {
                            r.QtdLinhasProcesso = x.Total;
                        }
                    }
                }

            }

            var lstInforme = TempData["lstInforme"] as List<Estoque>;


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


            estoque.lstInforme = lstInforme;
            //}

            var lstReprocessamento = TempData["lstReprocessamento"] as List<Estoque>;
            if (lstReprocessamento != null)
            {
                estoque.lstReprocessamento = lstReprocessamento;
            }

            estoque.lstItens = lstEstoque;


            return View(estoque);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public JsonResult Informativo(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<Estoque> lstInforme = new List<Estoque>();

            lstInforme = estoqueBUS.Informe(id_integracao, sp_id, sp_id_despesa_processo);
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
            List<Estoque> lstReprocessamento = new List<Estoque>();

            lstReprocessamento = estoqueBUS.Reprocessar(id_integracao, id_brokersys, numnf_brokersys);

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
            List<Estoque> lstReprocessamento = new List<Estoque>();

            lstReprocessamento = estoqueBUS.Excluir(id_integracao, id_brokersys, numnf_brokersys);

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

        public JsonResult Emitir_NFE(string nf_cod_processo = "")
        {
            CarregarDados();
            List<Estoque> lst = new List<Estoque>();

            lst = estoqueBUS.Emitir_NFE(nf_cod_processo);

            if (lst != null)
            {
                var resultado = (from info in lst
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
        public JsonResult RecFisico(string nf_cod_processo = "", string tp_nf = "")
        {
            CarregarDados();
            List<Estoque> lst = new List<Estoque>();

            lst = estoqueBUS.RecFisico(nf_cod_processo, tp_nf);

            if (lst != null)
            {
                var resultado = (from info in lst
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
        public JsonResult Cancelar(string nf_cod_processo = "", string tp_nf = "")
        {
            CarregarDados();
            List<Estoque> lst = new List<Estoque>();

            lst = estoqueBUS.Cancelar(nf_cod_processo, tp_nf);

            if (lst != null)
            {
                var resultado = (from info in lst
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
            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "SITUACAODESC", "SITUACAODESC");
            ViewBag.Declaracao = new SelectList(financeiroProcessosBUS.Declaracao(), "NF_TIPO_DEC_SISCOMEX", "TIPO_DECLARACAO");
            //List<Credor> lstCredor = credorBUS.Credor();
            //if (lstCredor == null)
            //{
            //    lstCredor = new List<Credor>();
            //    Credor desp = new Credor(null, null);
            //    lstCredor.Add(desp);
            //    ViewBag.Credor = new SelectList(lstCredor, "SP_CODIGO_CREDOR_DESPESA", "CREDOR"); ;
            //}
            //else
            //{
            //    ViewBag.Credor = new SelectList(lstCredor, "SP_CODIGO_CREDOR_DESPESA", "CREDOR");
            //}
            //ViewBag.Credor = new SelectList(estoqueBUS.Credor(), "SP_CODIGO_CREDOR_DESPESA", "CREDOR");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            //Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
    }
}