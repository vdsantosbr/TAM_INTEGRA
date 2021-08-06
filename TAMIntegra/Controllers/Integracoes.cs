using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Entities;
using Business;

namespace TAMINTEGRA.Controllers
{
    public class IntegracoesController : Controller
    {
        private FinanceiroServicos financeiroServico  = new FinanceiroServicos();
        private ImportacaoDocumentoBUS importacaoDocumentoBUS = new ImportacaoDocumentoBUS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private FinanceiroServicosBUS financeiroServicosBUS = new FinanceiroServicosBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();

        [CustomAuthorize(Roles = "frmFinanceiroServico")]
        public ActionResult Index(string modal = null, string modalInformativo = null, string situacao = null, string exibir = null, string strDataInicio = null, string strDataFim = null, string codTMV = null, string numeroMov = null)
        {
            CarregarDados();

            List<FinanceiroServicos> lstItens = new List<FinanceiroServicos>();
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

            financeiroServico.strDataInicio = strDataInicio;
            financeiroServico.strDataFim = strDataFim;

            if (exibir != null)
            {
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
                //lstItens = financeiroServicosBUS.Filtro(dataInicioDT, dataTerminoDT, codTMV, numeroMov, situacao);
            }

            //-------------------------------------------------//

            //-------------------------------------------------//

            var lstInforme = TempData["lstInforme"] as List<FinanceiroServicos>;
            if (lstInforme != null)
            {
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


                financeiroServico.lstInforme = lstInforme;
            }

            var lstReprocessamento = TempData["lstReprocessamento"] as List<FinanceiroServicos>;
            if (lstReprocessamento != null)
            {
                financeiroServico.lstReprocessamento = lstReprocessamento;
            }

            financeiroServico.lstItens = lstItens;


            return View(financeiroServico);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public JsonResult Informativo(int id_integracao = 0, int id_fatura = 0)
        {
            List<Cavok> lstInforme = new List<Cavok>();
            string mensagem = "";
            //lstInforme.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            //lstInforme = cavokBUS.Informe(id_integracao);

            //TempData["lstInforme"] = lstInforme.ToList();

            //lstInforme = cavokBUS.Informe(id_integracao, id_fatura);
            string strData = "";
            var resultado = "";
            //foreach (var r in lstInforme)
            //{
            //    strData = r.Data.ToShortDateString();
            //}
            //var resultado = (from info in lstInforme
            //                 select new
            //                 {
            //                     Id_Integracao = info.Id_Integracao,
            //                     Situacao = info.Situacao,
            //                     Fatura = info.FATURA,
            //                     Tipo = info.TIPO,
            //                     HANDLING = info.HANDLING,
            //                     FORMA_PAGAMENTO = info.FORMA_PAGAMENTO,
            //                     MOEDA = info.MOEDA,
            //                     VALOR = info.VALOR,
            //                     COTACAO = info.COTACAO,
            //                     VALOR_TOTAL = info.VALOR_TOTAL,
            //                     VALOR_TOTAL_REAIS = info.VALOR_TOTAL_REAIS,
            //                     Data = strData,
            //                     Consideracoes = info.CONSIDERACOES,
            //                     Cliente = info.CLIENTE
            //                 }).ToList();


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

            ////lstReprocessamento = cavokBUS.Reprocessar(id_integracao, id_fatura, Convert.ToInt32(ViewBag.IdPessoa));

            if (lstReprocessamento != null)
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

        public JsonResult agendarIntegracao(string[] dados = null)
        {
            List<ImportacaoDocumento> lstItens = new List<ImportacaoDocumento>();
            int id_integracao_processo = 0;
            int idMov = 0;
            int idLan = 0;
          
            try
            {
                //if (Session["lstDocumento"] != null)
                //{
                //    lstItens = (List<ImportacaoDocumento>)Session["lstDocumento"];
                //}

                string linha = "";
                string[] linhaSplit = null;

                Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
                //Integracao integracao = integracaoBUS.IntegracaoDEBUS(0, Convert.ToInt32(idIntegracaoProcesso), "EDIPOP", "Depósito Especial", "AG", "", idPessoa);
                Integracao integracao3 = new Integracao();
                //Gerando id_integração


                
                integracao3.Id_integracao_Processo = 34;
                integracao3.Id_Pessoa = usr.Id_Pessoa;
                integracao3.Tipo = "LAYOUT_CAMBIOSYS";
                integracao3.Situacao = "AG";
                integracao3.Complemento = "IMPORTACAO";
                integracao3.Id_integracao = 0;
                integracao3.IdMov = idMov;
                integracaoBUS.Integracao(integracao3);


                for (var i = 0; i < dados.Length; i++)
                {
                    linha = dados[i];
                    linhaSplit = linha.Split(',');
                    idMov = Convert.ToInt32(linhaSplit[0]);
                    idLan = Convert.ToInt32(linhaSplit[1]);

                    integracaoBUS.IntegracaoParametroBUS(integracao3.Tipo, integracao3.Id_integracao, idMov, 0, 0, 0, "", "", 0, 0, "", idLan);
                }

               
       
             return Json(integracao3.Id_integracao, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //TempData["Mensagem"] = e.Message;
                //return new HttpStatusCodeResult(500, e.Message);
                List<string> resultado = new List<string>();

                resultado.Add("Erro");
                resultado.Add(e.Message);


                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }


        public void CarregarDados()
        {
            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");
            ViewBag.TipoFaturamento = new SelectList(tipoFatBUS.ListaTipoFaturamento(), "ID", "DESCRICAO");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
    }
}