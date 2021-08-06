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
    public class FinanceiroDespesasController : Controller
    {
        //private FinanceiroDespesas despesas = new FinanceiroDespesas(null, 0, null, null, null, null);
        private FinanceiroDespesas despesas = new FinanceiroDespesas();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private FinanceiroDespesasBUS financeiroDespesasBUS = new FinanceiroDespesasBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();

        [CustomAuthorize(Roles = "frmFinanceiroDespesas")]
        public ActionResult Index(string situacao = null, string exibir = null, string strDataInicio = null, string strDataFim = null, int faturamento = 0, string codCredorDespesa = null, string codigoCredorDespesa = null, string processo = null)
        {
            CarregarDados();

            List<FinanceiroDespesas> lstDespesas = new List<FinanceiroDespesas>();
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

            despesas.strDataInicio = strDataInicio;
            despesas.strDataFim = strDataFim;

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
                lstDespesas = financeiroDespesasBUS.Filtro(dataInicioDT, dataTerminoDT, faturamento, codCredorDespesa, codigoCredorDespesa, processo, situacao);
                if(lstDespesas != null)
                {
                    var tipoMoedas = lstDespesas
                .GroupBy(w => w.SP_COD_MOEDA)
                .Select(g => new
                {
                    Valor = g.Select(c => c.SP_VR_A_PAGAR_DESPESA),
                    TipoMoeda = g.Select(c => c.SP_COD_MOEDA)
                }).ToList();

                    var r = from p in lstDespesas
                            group p by p.SP_COD_MOEDA into g
                            select new
                            {
                                TipoMoeda = (string)g.Select(x => x.SP_COD_MOEDA).First() + ": " + g.Sum(x => x.SP_VR_A_PAGAR_DESPESA).ToString("C", CultureInfo.CurrentCulture).Replace("R$ ", ""),
                                Valor = g.Select(x => x.SP_VR_A_PAGAR_DESPESA)
                            };

                    var results = from line in lstDespesas
                                  group line by line.SP_COD_MOEDA into g
                                  select new
                                  {
                                      ProductName = g.First().SP_COD_MOEDA,
                                      Price = g.Sum(x => x.SP_VR_A_PAGAR_DESPESA).ToString("C", CultureInfo.CurrentCulture),
                                  };

                    List<string> lstTipoMoedas = new List<string>();
                    foreach (var res in r)
                    {
                        lstTipoMoedas.Add(res.TipoMoeda);
                    }

                    despesas.lstMoedas = lstTipoMoedas;
                }
            }
                

            //-------------------------------------------------//

            //-------------------------------------------------//

            var lstInforme = TempData["lstInforme"] as List<FinanceiroDespesas>;
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


            despesas.lstInforme = lstInforme;
            //}

            var lstReprocessamento = TempData["lstReprocessamento"] as List<FinanceiroDespesas>;
            if (lstReprocessamento != null)
            {
                despesas.lstReprocessamento = lstReprocessamento;
            }

            despesas.lstItens = lstDespesas;


            return View(despesas);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public JsonResult Informativo(int id_integracao = 0,string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<FinanceiroDespesas> lstInforme = new List<FinanceiroDespesas>();

            lstInforme = financeiroDespesasBUS.Informe(id_integracao, sp_id, sp_id_despesa_processo);
            string strData = "";
            string strLiberacao = "";
            string strCadastro = "";
            string strVencimento = "";
            DateTime date = DateTime.ParseExact("2017/07/31 08:08:24", "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            foreach (var r in lstInforme)
            {
                strData = r.Data.ToShortDateString();
                strLiberacao = r.SP_DATA_LIBERACAO.ToShortDateString();
                strVencimento = r.SP_DATA_VENCIMENTO.ToShortDateString();
                strCadastro = DateTime.ParseExact(r.CADASTRO, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToShortDateString();
            }

            
            var resultado = (from info in lstInforme
                             select new
                             {
                                 Credor = info.CREDOR,
                                 Processo = info.SP_COD_PROCESSO,
                                 Cadastro = strCadastro,
                                 Liberacao = strLiberacao,
                                 Vencimento = strVencimento,
                                 Despesa = info.DESPESA,
                                 Documento = info.Documento,
                                 Serie = info.SERIE,
                                 Moeda = info.SP_COD_MOEDA,
                                 CodCredorDespesa = info.SP_COD_CREDOR_DESPESA,
                                 VrPrevistoDespesa = info.VR_PREVISTO_DESPESA,
                                 VrAdiantadoDespesa = info.VR_ADIANTADO_DESPESA,
                                 VrRealDespesa = info.VR_REAL_DESPESA,
                                 VrPagarDespesa = info.SP_VR_A_PAGAR_DESPESA,
                                 ProcessoIntegracao = info.PROCESSO_INTEGRACAO,
                                 Id_Integracao = info.Id_Integracao,
                                 Id = info.Id,
                                 Data = strData,
                                 Situacao = info.Situacao,
                                 Tipo = info.TIPO,
                                 Complemento = info.COMPLEMENTO,
                                 IdInt = info.ID_INT,
                                 CodTMVInt = info.CODTMV_INT,
                                 ChaveOrigemInt = info.CHAVEORIGEM_INT,
                                 Identificador = info.IDENTIFICADOR,
                                 Movimento = info.MOVIMENTO,
                                 NumMovimento = info.NUM_MOVIMENTO,
                                 Cor = info.COR,
                                 Consideracoes = info.CONSIDERAÇÕES
                             }).ToList();


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reprocessar(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            CarregarDados();
            List<FinanceiroDespesas> lstReprocessamento = new List<FinanceiroDespesas>();
            string mensagem = "";

            lstReprocessamento = financeiroDespesasBUS.Reprocessar(id_integracao, sp_id, sp_id_despesa_processo);

            if (lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
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

        public JsonResult Excluir(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            CarregarDados();
            List<FinanceiroDespesas> lstReprocessamento = new List<FinanceiroDespesas>();
            string mensagem = "";

            lstReprocessamento = financeiroDespesasBUS.Excluir(id_integracao, sp_id, sp_id_despesa_processo);

            if (lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
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
            //List<FinanceiroDespesas> lstTiposDespesas = new List<FinanceiroDespesas>();
            //lstTiposDespesas = financeiroDespesasBUS.TiposDespesas();
            List<FinanceiroDespesas> lstCredor = financeiroDespesasBUS.Credor();
            //if (lstTiposDespesas == null)
            //{
            //    lstTiposDespesas = new List<FinanceiroDespesas>();
            //    FinanceiroDespesas desp = new FinanceiroDespesas("", 0, null, null, null, null);
            //    lstTiposDespesas.Add(desp);
            //    ViewBag.TipoFaturamento = new SelectList(lstTiposDespesas, "SP_COD_CREDOR_DESPESA", "SP_COD_DESPESA"); ;
            //}
            //else
            //{
                //ViewBag.TipoFaturamento = new SelectList(lstTiposDespesas, "SP_COD_CREDOR_DESPESA", "SP_COD_DESPESA");
            //}
            //if(lstCredor == null)
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
         
            
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
    }
}