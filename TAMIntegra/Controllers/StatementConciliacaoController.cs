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

namespace TAMINTEGRA.Controllers
{
    //[CustomAuthorize(Roles = "frmStatementConciliacao")]
    [CustomAuthorize(Roles = "frmStatement")]

    public class StatementConciliacaoController : Controller
    {
        private StatementContasBUS contasBUS = new StatementContasBUS();
        private StatementConciliacaoBUS concBUS = new StatementConciliacaoBUS();
        private StatementDepartamentoBUS depBUS = new StatementDepartamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        public ActionResult Index(int ano = 0, int mes = 0, string[] contaFiltro = null, string classificacao = null, string departamento = null, 
            string analise = null, string documento = null, string pedidoCompraFiltro = null, string invoiceFiltro = null, string salesOrderFiltro = null,
            string[] id_conciliacao = null, int[] id_situacao_Filtro = null, int[] id_classificacao2 = null, int[] id_departamento2 = null, string[] id_situacao_analise2 = null, 
            List<string> btnFiltroAnalise = null, string idLstConciliacao = null, int id_conciliacao_item = 0, List<StatementConciliacao> lstConciliacaoItem2 = null, string divShow = null,
            string divShow2 = null, string comentarios = null, string menuAtivo = null, int id_conciliacao_analise = 0, int id_conciliacao_item_analise = 0, string invoice_analise = null, int pageNumber = 1, int pageNumberAnaliseItem = 1, int voltarDiv = 0,
            string poAnalise = null, string invoiceAnalise = null, string so_refAnalise = null)
        {
            StatementConciliacao obj = new StatementConciliacao();
            StatementClassificacaoBUS classificacaoBUS = new StatementClassificacaoBUS();
            List<StatementConciliacao> lstPopulaHistorico = TempData["result"] as List<StatementConciliacao>;
            List<StatementConciliacao> lstConciliacaoItem = new List<StatementConciliacao>();
            List<StatementConciliacao> lstObservacao = new List<StatementConciliacao>();
            List<StatementConciliacao> lstPedidocompra = new List<StatementConciliacao>();
            List<StatementConciliacao> lstFinanceiro = new List<StatementConciliacao>();
            List<StatementConciliacao> lstInvoice = new List<StatementConciliacao>();
            List<StatementConciliacao> lstHistorico = new List<StatementConciliacao>();


            ViewBag.ModalShow = 0;

            if(menuAtivo != null)
            {
                ViewBag.MenuAtivo = menuAtivo;
            }

            
            if(divShow != null)
            {
                ViewBag.DivShow = divShow;
            }
            else if (divShow2 != null)
            {
                ViewBag.DivShow = divShow2;
            }
            else
            {
                ViewBag.DivShow = "#divFiltro";
            }

            int pageSize = 5;
            int pageSizeAnaliseItem = 15;
            ViewBag.QtdRegistros = 0;

            string situacao = null;

            

            string strIdsituacao = "";
            if (id_situacao_Filtro != null)
            {
                foreach (int sit in id_situacao_Filtro)
                {
                    if (strIdsituacao.Trim().Length > 0) { strIdsituacao += ","; }

                    if ((strIdsituacao.Trim().Length + sit.ToString().Length) < 4000)
                    {
                        strIdsituacao += sit.ToString();
                    }
                }
            }
            string strIdClassificacao = "";
            if (id_classificacao2 != null)
            {
                foreach (int classif in id_classificacao2)
                {
                    if (strIdClassificacao.Trim().Length > 0) { strIdClassificacao += ","; }

                    if ((strIdClassificacao.Trim().Length + classif.ToString().Length) < 4000)
                    {
                        strIdClassificacao += classif.ToString();
                    }
                }
            }

            string strIdDepartamento = "";
            if (id_departamento2 != null){
                var distinctDept = (from w in id_departamento2
                                    select w).Distinct().ToList();

                
                if (distinctDept != null)
                {
                    foreach (int dep in distinctDept)
                    {
                        if (strIdDepartamento.Trim().Length > 0) { strIdDepartamento += ","; }

                        if ((strIdDepartamento.Trim().Length + dep.ToString().Length) < 4000)
                        {
                            strIdDepartamento += dep.ToString();
                        }
                    }
                }
            }

            string strIdSituacaoAnalise = "";
            if (id_situacao_analise2 != null)
            {
                var distinctSituacaoAnalise = (from w in id_situacao_analise2
                                    select w).Distinct().ToList();


                if (distinctSituacaoAnalise != null)
                {
                    foreach (string dep in distinctSituacaoAnalise)
                    {
                        if (strIdSituacaoAnalise.Trim().Length > 0) { strIdSituacaoAnalise += ","; }

                        if ((strIdSituacaoAnalise.Trim().Length + dep.ToString().Length) < 4000)
                        {
                            strIdSituacaoAnalise += dep.ToString();
                        }
                    }
                }
            }

            string strConta = "";
            if (contaFiltro != null)
            {
               foreach (string conta in contaFiltro)
               {
                   if (strConta.Trim().Length > 0) { strConta += ","; }

                   if ((strConta.Trim().Length + conta.ToString().Length) < 4000)
                   {
                        strConta += conta.ToString();
                   }
               }
            }


            if (ano == 0)
            {
                ano = DateTime.Now.Year;
            }

            if (mes == 0)
            {
                mes = DateTime.Now.Month;
            }

            List<StatementContas> contas = contasBUS.Lista(null, null, null);
            ViewBag.Contas = new SelectList(contas.Where(x => x.Situacao == "Ativo"), "Id_Conta", "Conta");

            List<StatementClassificacao> lstClassificacao = classificacaoBUS.Lista(null, null, null);
            ViewBag.Classificacao = new SelectList(lstClassificacao.Where(x=>x.Visivel.Equals("S")).Where(x=>x.Situacao.Equals("Ativo")), "id_classificacao", "descricao");
            Session["lstClassificacao"] = lstClassificacao;

            List<StatementConciliacao> lstSituacao = concBUS.Situacao(null, null);
            ViewBag.Situacao = new SelectList(lstSituacao, "id_situacao", "descricao");

            List<StatementDepartamento> lstDepartamento = depBUS.Lista(null);
            ViewBag.Departamentos = new SelectList(lstDepartamento, "id_departamento", "nome");
            Session["lstDeps"] = lstDepartamento;


            //List<StatementConciliacao> lstSituacao =
            List<StatementConciliacao> lstAnalise = new List<StatementConciliacao>();
            if (voltarDiv == 0)
            {
                lstAnalise = concBUS.Analise(ano, mes, strConta, pedidoCompraFiltro, invoiceFiltro, salesOrderFiltro);
                TempData["lstanalise"] = lstAnalise;
            }

            if(lstAnalise == null & TempData["lstanalise"] != null)
            {
                lstAnalise = (List<StatementConciliacao>) TempData["lstanalise"];
            }


            List<string> registrosChecked = new List<string>();
            List<string> lstIdconciliacao = new List<string>();
            if (btnFiltroAnalise != null)
            {
                foreach (var x in btnFiltroAnalise)
                {
                    registrosChecked.Add(x.Split(',').Last());
                    lstIdconciliacao.Add(x.Split(',').First());
                }
                ViewBag.RegistrosChecked = registrosChecked;
            }
          

            IEnumerable result = null;
           if(lstAnalise != null)
            {
                result = lstAnalise.Select(s => s.Id_Conciliacao).Distinct();
                
            }

            List<StatementConciliacao> lstAnaliseItem = new List<StatementConciliacao>();
            if(lstAnalise != null & voltarDiv == 0)
            {
                if (lstIdconciliacao.Count > 0 || id_situacao_Filtro != null || id_classificacao2 != null || id_situacao_analise2 != null || id_departamento2 != null)
                {
                    string strIdConciliacao = "";


                    if (lstIdconciliacao != null)
                    {
                        TempData["btnFiltroAnalise"] = lstIdconciliacao;
                        ViewBag.FiltroAnalise = lstIdconciliacao;


                        foreach (string conc in lstIdconciliacao)
                        {
                            if (strIdConciliacao.Trim().Length > 0) { strIdConciliacao += ","; }

                            if ((strIdConciliacao.Trim().Length + conc.ToString().Length) < 4000)
                            {
                                strIdConciliacao += conc.ToString();
                            }
                        }
                    }
                    else
                    {
                        foreach (int conc in result)
                        {
                            if (strIdConciliacao.Trim().Length > 0) { strIdConciliacao += ","; }

                            if ((strIdConciliacao.Trim().Length + conc.ToString().Length) < 4000)
                            {
                                strIdConciliacao += conc.ToString();
                            }
                        }
                    }
                    ViewBag.FiltroAnalise = strIdConciliacao;
                    if (strIdConciliacao == null || strIdConciliacao == "")
                    {
                        strIdConciliacao = idLstConciliacao;
                    }
                    lstAnaliseItem = concBUS.AnaliseItem(strIdConciliacao, strIdsituacao, strIdClassificacao, strIdDepartamento, strIdSituacaoAnalise, pedidoCompraFiltro, invoiceFiltro, salesOrderFiltro);
                    
                }
               
            }

            if(lstAnaliseItem.Count() > 0)
            {
                Session["lstAnaliseItem"] = lstAnaliseItem;
            }

           if(lstAnaliseItem.Count() == 0 & Session["lstAnaliseItem"] != null)
            {
                lstAnaliseItem = (List<StatementConciliacao>) Session["lstAnaliseItem"];
                obj.lstAnaliseItem = lstAnaliseItem;
            }

            
if (id_conciliacao_item_analise > 0)
            {
                lstConciliacaoItem = concBUS.ConciliacaoItem(id_conciliacao_item_analise);
                Session["lstConciliacaoItem"] = lstConciliacaoItem;
                if(invoice_analise != null)
                {
                    lstObservacao = concBUS.selectObservacao(invoice_analise);
                    Session["lstObservacao"] = lstObservacao;
                    lstPedidocompra = concBUS.PedidoCompra(invoice_analise);
                    Session["lstPedidocompra"] = lstPedidocompra;
                    lstFinanceiro = concBUS.Financeiro(invoice_analise);
                    Session["lstFinanceiro"] = lstFinanceiro;
                    lstInvoice = concBUS.Invoice(invoice_analise);
                    Session["lstInvoice"] = lstInvoice;
                    lstHistorico = concBUS.Historico(invoice_analise);
                    Session["lstHistorico"] = lstHistorico;
                }
               
            }
            obj.lstConciliacaoItem = lstConciliacaoItem;
            obj.lstObservacao = lstObservacao;
            obj.lstHistorico = lstHistorico;

            //lstSituacaoAnalise.Add(new StatementConciliacao
            //{
            //    SituacaoAnalise = "Analisado",
            //    Situacao = "Divergente",

            //});

            var situacaoAnalise = new List<SelectListItem>();
            situacaoAnalise.Add(new SelectListItem
            { Text = "Pendente", Value = "P" });
            situacaoAnalise.Add(new SelectListItem
            { Text = "Analisado", Value = "A" });
            ViewBag.SituacaoAnalise = new SelectList(situacaoAnalise, "Value", "Text");



            obj.lstAnalise = lstAnalise;
            obj.lstAnaliseItem = lstAnaliseItem;
            obj.Ano = ano;
            obj.Mes = mes;

            double pageCount;
            if (lstAnalise != null)
            {
                pageCount = (double)((decimal)lstAnalise.Count() / Convert.ToDecimal(pageSize));
                obj.PageSize = (int)Math.Ceiling(pageCount);
                obj.PageNumber = pageNumber;
                IEnumerable<StatementConciliacao> lstAnaliseEnum = lstAnalise;
                lstAnaliseEnum = lstAnalise.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                lstAnalise = lstAnaliseEnum.ToList();
                obj.lstAnalise = lstAnalise;

                ViewBag.Pagina = pageNumber;
            }

            double pageCountLstAnaliseItem;
            if(lstAnaliseItem != null)
            {
                ViewBag.QtdRegistros = lstAnaliseItem.Count();
                ViewBag.Amount = Math.Round(lstAnaliseItem.Sum(x=>x.Amount), 2);
                ViewBag.ValorRM = Math.Round(lstAnaliseItem.Sum(x => x.RMValor), 2);
                ViewBag.Diferenca = Math.Round(lstAnaliseItem.Sum(x => x.Diferenca), 2);

                pageCountLstAnaliseItem = (double)((decimal)lstAnaliseItem.Count() / Convert.ToDecimal(pageSizeAnaliseItem));
                obj.PageSizeAnaliseItem = (int)Math.Ceiling(pageCountLstAnaliseItem);
                obj.PageNumberanaliseItem = pageNumberAnaliseItem;
                IEnumerable<StatementConciliacao> lstAnaliseItemEnum = lstAnaliseItem;
                lstAnaliseItemEnum = lstAnaliseItem.Skip((pageNumberAnaliseItem - 1) * pageSizeAnaliseItem).Take(pageSizeAnaliseItem);
                lstAnaliseItem = lstAnaliseItemEnum.ToList();
                obj.lstAnaliseItem = lstAnaliseItem;

                ViewBag.PaginaAnaliseItem = pageNumberAnaliseItem;
            }




            obj.lstPopulaHistorico = lstPopulaHistorico;
            if(lstPopulaHistorico != null)
            {
                TempData["result2"] = lstPopulaHistorico;
            }

            if(lstPopulaHistorico != null)
            {
                Session["lstPopulaHistorico"] = lstPopulaHistorico;
            }

            if(Session["lstPopulaHistorico"] != null)
            {
                lstPopulaHistorico = (List<StatementConciliacao>) Session["lstPopulaHistorico"];
                obj.lstPopulaHistorico = lstPopulaHistorico;
            }

            if (Session["lstConciliacaoItem"] != null)
            {
                lstConciliacaoItem = (List<StatementConciliacao>)Session["lstConciliacaoItem"];
                obj.lstConciliacaoItem = lstConciliacaoItem;
            }

            if (Session["lstObservacao"] != null)
            {
                lstObservacao = (List<StatementConciliacao>)Session["lstObservacao"];
                obj.lstObservacao = lstObservacao;
            }
            if (Session["lstPedidocompra"] != null)
            {
                lstPedidocompra = (List<StatementConciliacao>)Session["lstPedidocompra"];
                obj.lstPedidoCompra = lstPedidocompra;
            }
            if (Session["lstFinanceiro"] != null)
            {
                lstFinanceiro = (List<StatementConciliacao>)Session["lstFinanceiro"];
                obj.lstFinanceiro = lstFinanceiro;
            }
            if (Session["lstInvoice"] != null)
            {
                lstInvoice = (List<StatementConciliacao>)Session["lstInvoice"];
                obj.lstInvoice = lstInvoice;
            }
            if (Session["lstHistorico"] != null)
            {
                lstHistorico = (List<StatementConciliacao>)Session["lstHistorico"];
                obj.lstHistorico = lstHistorico;
            }
            

            return View(obj);
        }
       
        public ActionResult PopulaHistorico(string invoice)
        {
            invoice = "CJ05154546";
            TempData["result"] = concBUS.PopulaHistorico(invoice).ToList();
            return RedirectToAction("Index", "StatementConciliacao");
        }

        public ActionResult ConciliacaoItem(string divShow = null, string invoice_analise = null, int id_conciliacao_analise = 0, int id_conciliacao_item_analise = 0, int ano = 0,
            int mes = 0, string idLstConciliacao = null, List<string> btnFiltroAnalise = null, int[] id_departamento = null, int[] id_situacao_filtro = null, int[] id_classificacao = null,
            int[] id_situacao_analise = null)
        {
            StatementConciliacao obj = new StatementConciliacao();
            List<StatementConciliacao> lstConciliacaoItem = new List<StatementConciliacao>();
            List<StatementConciliacao> lstPopulaHistorico = TempData["result"] as List<StatementConciliacao>;
            //List<StatementConciliacao> lstConciliacaoItem = new List<StatementConciliacao>();
            List<StatementConciliacao> lstObservacao = new List<StatementConciliacao>();
            List<StatementConciliacao> lstPedidocompra = new List<StatementConciliacao>();
            List<StatementConciliacao> lstFinanceiro = new List<StatementConciliacao>();
            List<StatementConciliacao> lstInvoice = new List<StatementConciliacao>();
            List<StatementConciliacao> lstHistorico = new List<StatementConciliacao>();

            if (id_conciliacao_item_analise > 0)
            {
                lstConciliacaoItem = concBUS.ConciliacaoItem(id_conciliacao_item_analise);
                Session["lstConciliacaoItem"] = lstConciliacaoItem;
                lstObservacao = concBUS.selectObservacao(invoice_analise);
                Session["lstObservacao"] = lstObservacao;
                lstPedidocompra = concBUS.PedidoCompra(invoice_analise);
                Session["lstPedidocompra"] = lstPedidocompra;
                lstFinanceiro = concBUS.Financeiro(invoice_analise);
                Session["lstFinanceiro"] = lstFinanceiro;
                lstInvoice = concBUS.Invoice(invoice_analise);
                Session["lstInvoice"] = lstInvoice;
                lstHistorico = concBUS.Historico(invoice_analise);
                Session["lstHistorico"] = lstHistorico;
            }
            obj.lstConciliacaoItem = lstConciliacaoItem;
            obj.lstObservacao = lstObservacao;
            obj.lstHistorico = lstHistorico;

            return RedirectToAction("Index", "StatementConciliacao", new { idLstConciliacao = id_conciliacao_analise, id_conciliacao_item_analise = id_conciliacao_item_analise, ano = ano, mes= mes});
        }
        public ActionResult salvarAnaliseConcilicacao(int id_conciliacao= 0, int id_conciliacao_item = 0, string classificacao = null, string departamento = null,
            string invoice = null, string situacao_analise = null, string divShow2 = null, string comentarios = null)
        {
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            List<StatementDepartamento> lstDepartamentoFiltro = new List<StatementDepartamento>();
            List<StatementClassificacao> lstClassificacaoFiltro = new List<StatementClassificacao>();
            List<StatementConciliacao> lstObservacao = new List<StatementConciliacao>();
            int id_departamento = 0;
            int id_classificacao = 0;

            int intValue;
            bool caracter = int.TryParse(Request.QueryString[id_departamento], out intValue);
            if (caracter == false)
            {
                List<StatementDepartamento> lstDepartamento = (List<StatementDepartamento>)Session["lstDeps"];
                lstDepartamentoFiltro = lstDepartamento.Where(x => x.Nome == departamento).ToList();
                id_departamento = lstDepartamentoFiltro.Select(x => x.Id_Departamento).First();
            }
            else
            {
                id_departamento = Convert.ToInt32(departamento);
            }

            bool caracter2 = int.TryParse(Request.QueryString[classificacao], out intValue);
            if (caracter2 == false)
            {
                List<StatementClassificacao> lstClassificacao = (List<StatementClassificacao>)Session["lstClassificacao"];
                lstClassificacaoFiltro = lstClassificacao.Where(x => x.Descricao == classificacao).ToList();
                id_classificacao = lstClassificacaoFiltro.Select(x => x.Id_Classificacao).First();
            }
            else
            {
                id_classificacao = Convert.ToInt32(classificacao);
            }


            concBUS.updateConciliacaoItem(id_conciliacao, id_conciliacao_item, Convert.ToInt32(id_classificacao), Convert.ToInt32(id_departamento),
            usr.Id_Pessoa, invoice, situacao_analise);
            lstObservacao = concBUS.selectObservacao(invoice);
            Session["lstObservacao"] = lstObservacao;
            //concBUS.insertObservacao(id_conciliacao, id_conciliacao_item, invoice, comentarios, usr.Id_Pessoa, 0);
            return RedirectToAction("Index", "StatementConciliacao", new { menuAtivo = "Statement", divShow = "#divModal", divShow2 = divShow2, invoice_analise = invoice , id_conciliacao_item_analise = id_conciliacao_item}); 
        }
    }
}