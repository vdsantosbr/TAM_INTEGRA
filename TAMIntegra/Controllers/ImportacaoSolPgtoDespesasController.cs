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
    public class ImportacaoSolPgtoDespesasController : Controller
    {
        private ImportacaoSolPgtoDespesas impSolPgtoDesp = new ImportacaoSolPgtoDespesas();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private ImportacaoSolPgtoDespesasBUS impBUS = new ImportacaoSolPgtoDespesasBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private FinanceiroProcessoBUS financeiroProcessosBUS = new FinanceiroProcessoBUS();
        private CredorBUS credorBUS = new CredorBUS();

        [CustomAuthorize(Roles = "frmImportacaoNota")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(int id_spdesp = 0, string exibir = null, string strDataInicio = null, string strDataFim = null, string declaracao = null, string processo = null, string nota = null, 
            string situacao = "", string divExibir = "", string modalItem = "", int tpDocumento = 0, string numDoc = "", int id_integracao = 0, int sp_id = 0, int spd_id_despesa_processo = 0, ImportacaoSolPgtoDespesas despesas = null, string salvar = "")
        { 
            CarregarDados();

            List<ImportacaoSolPgtoDespesas> lstDados = new List<ImportacaoSolPgtoDespesas>();
            DateTime dataInicioDT = new DateTime();
            DateTime dataTerminoDT = new DateTime();
            List<ImportacaoSolPgtoDespesas> lstDespesas = new List<ImportacaoSolPgtoDespesas>();
            List<ImportacaoSolPgtoDespesas> lstDivDespesas = new List<ImportacaoSolPgtoDespesas>();

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

            impSolPgtoDesp.strDataInicio = strDataInicio;
            impSolPgtoDesp.strDataFim = strDataFim;

            if(salvar != "")
            {
                id_spdesp = impBUS.Salvar(despesas);
                exibir = "#divDesp";
            }

            if (exibir == "#divDesp")
            {
                impSolPgtoDesp.Div = "S";
                lstDivDespesas = impBUS.Edicao(id_spdesp, id_integracao, sp_id, spd_id_despesa_processo);
                if(lstDivDespesas != null)
                {
                    impSolPgtoDesp.lstDivDespesas = lstDivDespesas;
                    foreach (var r in lstDivDespesas)
                    {
                        impSolPgtoDesp.strDataInicio = strDataInicio;
                        impSolPgtoDesp.MoedaDiv = r.Sp_Cod_Moeda;
                        impSolPgtoDesp.NumDocDiv = r.Sp_Num_Documento;
                        impSolPgtoDesp.Id_Tipo_Documento = r.Id_Tipo_Documento;
                        impSolPgtoDesp.id_tp_documento_div = r.Id_Tipo_Documento;
                        impSolPgtoDesp.ProcessoDiv = r.Sp_Cod_Processo;
                        impSolPgtoDesp.InvoiceDiv = r.Fat_Num_Fatura;
                        impSolPgtoDesp.DtEmissaoDiv = r.Sp_Data_Cadastro.ToString();
                        string data = data = r.Sp_Data_Cadastro.ToString().Substring(0, 10);
                        DateTime result = new DateTime();
                        try
                        {
                            result = DateTime.ParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }catch(Exception e)
                        {                            
                            result = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        impSolPgtoDesp.DtEmissaoDivFmt = result.ToString("dd/MM/yyyy");
                        data = r.Sp_Data_Vencimento.ToString().Substring(0, 10);
                        try
                        {
                            result = DateTime.ParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        catch(Exception e)
                        {
                            result = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        impSolPgtoDesp.VencimentoDivFmt = result.ToString("dd/MM/yyyy");
                        impSolPgtoDesp.VencimentoDiv = r.Sp_Data_Vencimento.ToString();
                        impSolPgtoDesp.Sp_ID = r.Sp_ID;
                        impSolPgtoDesp.Id_SPDesp = r.Id_SPDesp;
                        impSolPgtoDesp.FornecedorDiv = r.Sp_Codigo_Credor_Despesa;
                        impSolPgtoDesp.Id_Integracao = r.Id_Integracao;
                        impSolPgtoDesp.DtLiberacao = r.Sp_Data_Liberacao.ToString();
                        impSolPgtoDesp.Id_SPDesp = r.Id_SPDesp;
                        impSolPgtoDesp.ValorDiv = 0;
                    };
                    impSolPgtoDesp.lstDivDespesas = lstDivDespesas;
                }
            }

            else
            {
                //lstDados.Add(new ImportacaoSolPgtoDespesas
                //{
                //    Dt_Emissao = new DateTime(2020, 01, 30),
                //    Fornecedor = "Textron",
                //    Dt_Vencimento = new DateTime(2020, 01, 30),
                //    NumDocumento = "1234",
                //    Processo = "M19/0759"
                //});
                lstDados = impBUS.Filtro(dataInicioDT, dataTerminoDT, tpDocumento, numDoc, processo, nota, situacao);
                impSolPgtoDesp.lstDespesas = lstDespesas;
            }
               
            impSolPgtoDesp.lstItens = lstDados;

            if(divExibir == "")
            {
                divExibir = "#divFiltros";
            }
            impSolPgtoDesp.DivExibir = divExibir;


            if(modalItem != "")
            {
                lstDespesas.Add(new ImportacaoSolPgtoDespesas
                {
                    DescItem = "Fedex",
                });
            }
            

            
            
            return View(impSolPgtoDesp);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public ActionResult Edicao(int id_spdesp = 0, int id_integracao = 0, int sp_id = 0, int spd_id_despesa_processo = 0)
        {
            List<ImportacaoSolPgtoDespesas> lstInforme = new List<ImportacaoSolPgtoDespesas>();

            lstInforme = impBUS.Edicao(id_spdesp, id_integracao, sp_id, spd_id_despesa_processo);

            return Json(lstInforme, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", new { exibir = "#divDesp", id_spdesp = id_spdesp, id_integracao = id_integracao, sp_id = sp_id, spd_id_despesa_processo  = spd_id_despesa_processo });
        }

        public JsonResult BuscaFornecedor(string valor = "")
        {
            List<ImportacaoSolPgtoDespesas> lst = new List<ImportacaoSolPgtoDespesas>();
            lst = impBUS.Fornecedor();
            var names = from forn in lst
                        where forn.Nome.ToLower().Contains(valor.ToLower())
                        select forn;
            lst = names.ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DescricaoItem()
        {
            List<ImportacaoSolPgtoDespesas> lstDescItem = new List<ImportacaoSolPgtoDespesas>();

            lstDescItem.Add(new ImportacaoSolPgtoDespesas
            {
                DescItem = "Fedex"
            });
            lstDescItem.Add(new ImportacaoSolPgtoDespesas
            {
                DescItem = "Custos não programados na importação"
            });

            List<ImportacaoSolPgtoDespesas> lstCC = new List<ImportacaoSolPgtoDespesas>();

            lstCC.Add(new ImportacaoSolPgtoDespesas
            {
                CC = "Jundiaí"
            });
            lstCC.Add(new ImportacaoSolPgtoDespesas
            {
                CC = "Aracati"
            });

            return Json(new { lstDescItem = lstDescItem, lstCC = lstCC }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SalvarSolicitacao(string descItem = "", string desc = "", string cc = "", double preco = 0, int qtd = 0, double valor = 0, string data = "")
        {
            DateTime dtData = new DateTime();
            if (data == null || data == "")
            {
                data = DateTime.Today.ToString("dd/MM/yyyy");
                dtData = DateTime.Parse(data);
            }

            if (!string.IsNullOrWhiteSpace(data))
            {
                dtData = DateTime.Parse(data);
            }

            List<ImportacaoSolPgtoDespesas> lst = new List<ImportacaoSolPgtoDespesas>();

            lst.Add(new ImportacaoSolPgtoDespesas
            {
                DescItem = "Fedex",
                Desc = "Teste1"
            });
            lst.Add(new ImportacaoSolPgtoDespesas
            {
                DescItem = "Custos não programados na importação",
                Desc = "Teste2"
            });

            return Json(lst, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", new { divExibir = "exibir", modalItem = "ok" });
        }

        public void CarregarDados()
        {
            ViewBag.Situacao = new SelectList(impBUS.Situacao(), "CODSITUACAO", "SITUACAODESC");
            ViewBag.TipoDocumento = new SelectList(impBUS.TipoDocumento(), "ID_TIPO_DOCUMENTO", "DESCRICAO");
            ViewBag.Filial = new SelectList(impBUS.Filial(), "CODFILIAL", "NOMEFANTASIA", "Jundiaí");
            ViewBag.Fornecedor = new SelectList(impBUS.Fornecedor(), "CODCFO", "NOME");
            ViewBag.Moeda = new SelectList(impBUS.Moeda(), "SIMBOLO", "SIMBOLO_DESCRICAO", "R$ - Real");


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
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;

            List<ImportacaoSolPgtoDespesas> lstDescItem = new List<ImportacaoSolPgtoDespesas>();

            lstDescItem.Add(new ImportacaoSolPgtoDespesas
            {
                DescItem = "Fedex"
            });
            lstDescItem.Add(new ImportacaoSolPgtoDespesas
            {
                DescItem = "Custos não programados na importação"
            });
            ViewBag.DescItem = new SelectList(lstDescItem, "DescItem", "DescItem");

            List<ImportacaoSolPgtoDespesas> lstCC = new List<ImportacaoSolPgtoDespesas>();

            lstCC.Add(new ImportacaoSolPgtoDespesas
            {
                CC = "Jundiaí"
            });
            lstCC.Add(new ImportacaoSolPgtoDespesas
            {
                CC = "Aracati"
            });
            ViewBag.Filial = new SelectList(lstCC, "CC", "CC");
        }
        [HttpPost]
        public ActionResult Salvar(ImportacaoSolPgtoDespesas despesas)
        {
            int id_spdesp = 0;
            id_spdesp = impBUS.Salvar(despesas);
            List<ImportacaoSolPgtoDespesas> lst = new List<ImportacaoSolPgtoDespesas>();
            lst = impBUS.Edicao(id_spdesp, 0, 0, 0);

            //return Json(lst, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", new { exibir = "#divDesp", id_spdesp = id_spdesp });
            return Index(id_spdesp, "#divDesp");

            //return RedirectToAction("Index", new { exibir = "#divDesp", id_spdesp = id_spdesp, id_integracao = id_integracao, sp_id = sp_id, spd_id_despesa_processo  = spd_id_despesa_processo });
        }

    }
}