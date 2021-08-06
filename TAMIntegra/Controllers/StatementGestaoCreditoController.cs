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
    //[CustomAuthorize(Roles = "frmStatementCredito")]
    [CustomAuthorize(Roles = "frmStatement")]

    public class StatementGestaoCreditoController : Controller
    {
        private StatementContasBUS contasBUS = new StatementContasBUS();
        private StatementConciliacaoBUS concBUS = new StatementConciliacaoBUS();
        private StatementDepartamentoBUS depBUS = new StatementDepartamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private SatatementGestaoCreditoBUS gestaoBUS = new SatatementGestaoCreditoBUS();
        private SalesOrderAgingBUS soAgingBUS = new SalesOrderAgingBUS();
        private SalesOrderComposicaoBUS soComposicaoBUS = new SalesOrderComposicaoBUS();
        private SalesOrderQualificacaoBUS soQualificacaoBUS = new SalesOrderQualificacaoBUS();
        private SalesOrderSaldoBUS soSaldoBUS = new SalesOrderSaldoBUS();
        private SalesOrderSituacaoBUS soSituacaoBUS = new SalesOrderSituacaoBUS();

        public ActionResult Index(int[] contaFiltro = null, int[] qualificacaoFiltro = null, string[] tipoInvoiceFiltro = null, string divShow = null, string divShow2 = null,
             int[] saldoFiltro = null, string[] agingFiltro = null, int[] situacaoFiltro = null, string salesOrderFiltro = null, string invoiceFiltro = null, string menuAtivo = null,
              string pedidoCompraFiltro = null, string prefixoFiltro = null, string numMovimentoFiltro = null, string processoImportacaoFiltro = null, string numDIFiltro = null,
              int ordemServicoFiltro = 0, string preFaturamentoFiltro = null, string numMovtoFiltro = null, string numProcessoImportacaoFiltro = null, int id_so = 0, int numLoteFiltro = 0,
              string divShowPesquisa = null, int id_salesorder = 0, int pageNumber = 1, int pageNumberAnaliseItem = 1)
        {
            StatementGestaoCredito obj = new StatementGestaoCredito();
            List<SalesOrderQualificacao> lstQualificacao = new List<SalesOrderQualificacao>();
            List<SalesOrderAging> lstAging = new List<SalesOrderAging>();
            List<StatementGestaoCredito> lstSalesOrder = new List<StatementGestaoCredito>();
            List<StatementGestaoCredito> lstSalesOrderAnalise = new List<StatementGestaoCredito>();
            List<StatementGestaoCredito> lstInvoices = new List<StatementGestaoCredito>();
            List<SalesOrderComposicao> lstComposicao = new List<SalesOrderComposicao>();
            List<SalesOrderSaldo> lstSaldo = new List<SalesOrderSaldo>();
            List<SalesOrderSituacao> lstSituacao = new List<SalesOrderSituacao>();
            List<StatementGestaoCredito> lstComentarios = new List<StatementGestaoCredito>();


            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            ViewBag.ModalShow = 0;

            if (menuAtivo != null)
            {
                ViewBag.MenuAtivo = menuAtivo;
            }


            if (divShow != null)
            {
                ViewBag.DivShow = divShow;
            }
            else if (divShow2 != null)
            {
                ViewBag.DivShow = divShow2;
            }
            else if (divShowPesquisa != null)
            {
                ViewBag.DivShow = divShowPesquisa;
            }
            else
            {
                ViewBag.DivShow = "#divFiltro";
            }

            int pageSize = 5;
            //pageNumber = 1;
            ViewBag.QtdRegistros = 0;

            string situacao = null;

            string strConta = "";
            if (contaFiltro != null)
            {
                foreach (int conta in contaFiltro)
                {
                    if (strConta.Trim().Length > 0) { strConta += ","; }

                    if ((strConta.Trim().Length + conta.ToString().Length) < 4000)
                    {
                        strConta += conta.ToString();
                    }
                }
            }

            string strQualificacao = "";
            if (qualificacaoFiltro != null)
            {
                foreach (int qualificacao in qualificacaoFiltro)
                {
                    if (strQualificacao.Trim().Length > 0) { strQualificacao += ","; }

                    if ((strQualificacao.Trim().Length + qualificacao.ToString().Length) < 4000)
                    {
                        strQualificacao += qualificacao.ToString();
                    }
                }
            }

            string strTipoInvoice = "";
            if (tipoInvoiceFiltro != null)
            {
                foreach (string tipoInvoice in tipoInvoiceFiltro)
                {
                    if (strTipoInvoice.Trim().Length > 0) { strTipoInvoice += ","; }

                    if ((strTipoInvoice.Trim().Length + tipoInvoice.ToString().Length) < 4000)
                    {
                        strTipoInvoice += tipoInvoice.ToString();
                    }
                }
            }

            string strSaldo = "";
            if (saldoFiltro != null)
            {
                foreach (int saldo in saldoFiltro)
                {
                    if (strSaldo.Trim().Length > 0) { strSaldo += ","; }

                    if ((strSaldo.Trim().Length + saldo.ToString().Length) < 4000)
                    {
                        strSaldo += saldo.ToString();
                    }
                }
            }

            string strAging = "";
            if (agingFiltro != null)
            {
                foreach (string aging in agingFiltro)
                {
                    if (strAging.Trim().Length > 0) { strAging += ","; }

                    if ((strAging.Trim().Length + aging.ToString().Length) < 4000)
                    {
                        strAging += aging.ToString();
                    }
                }
            }

            string strSituacao = "";
            if (situacaoFiltro != null)
            {
                foreach (int sit in situacaoFiltro)
                {
                    if (strSituacao.Trim().Length > 0) { strSituacao += ","; }

                    if ((strSituacao.Trim().Length + sit.ToString().Length) < 4000)
                    {
                        strSituacao += sit.ToString();
                    }
                }
            }

            List<StatementContas> contas = contasBUS.Lista(null, null, null);
            ViewBag.Contas = new SelectList(contas.Where(x => x.Situacao == "Ativo"), "Id_Conta", "Conta");

            lstQualificacao = soQualificacaoBUS.ListaQualificacao();
            ViewBag.Qualificacao = new SelectList(lstQualificacao, "ID_QUALIFICACAO", "Descricao");
            ViewBag.QualificacaoAnalise = new SelectList(lstQualificacao.Where(x=>x.Visivel == "S"), "ID_QUALIFICACAO", "Descricao");

            lstComposicao = soComposicaoBUS.ListaComposicao();
            ViewBag.Composicao = new SelectList(lstComposicao, "Qualificacao_invoice", "Descricao");

            lstSaldo = soSaldoBUS.ListaComposicao();
            ViewBag.Saldo = new SelectList(lstSaldo, "Qualificacao_saldo", "Descricao");

            lstAging = soAgingBUS.ListaAging();
            ViewBag.Aging = new SelectList(lstAging, "Aging", "Descricao");

            lstSituacao = soSituacaoBUS.ListaSituacao();
            ViewBag.Situacao = new SelectList(lstSituacao, "Situacao", "Descricao");

            //lstSalesOrder.Add(new StatementGestaoCredito
            //{   SO_Ref = "77855423",
            //    Qualificacao = "Bloqueado",
            //    Tipo = "Crédito > Débito",
            //    Saldo = 100,
            //    Descricao = "Em curso",
            //    Debitos = 2200,
            //    Creditos = 2250,
            //    Warranty_Claim = 50,
            //    Qtd_Registros = 20,
            //    Conta = "77100",
            //    Diferenca = 50
            //});

            //lstSalesOrder.Add(new StatementGestaoCredito
            //{
            //    SO_Ref = "77855423",
            //    Qualificacao = "Bloqueado",
            //    Tipo = "Crédito > Débito",
            //    Saldo = 100,
            //    Descricao = "Em curso",
            //    Debitos = 1800,
            //    Creditos = 1950,
            //    Warranty_Claim = 70,
            //    Qtd_Registros = 10,
            //    Conta = "77100",
            //    Diferenca = 150
            //});

            //lstSalesOrder.Add(new StatementGestaoCredito
            //{
            //    SO_Ref = "77855444",
            //    Qualificacao = "Bloqueado",
            //    Tipo = "Crédito < Débito",
            //    Saldo = 100,
            //    Descricao = "Em curso",
            //    Debitos = 200,
            //    Creditos = 300,
            //    Warranty_Claim = 100,
            //    Qtd_Registros = 30,
            //    Conta = "77100",
            //    Diferenca = 100
            //});
            //--Convert.ToInt32(usr.Id_Pessoa)
            Session["lstSalesOrder"] = null;
            lstSalesOrder = gestaoBUS.ListaSalesOrder(strConta, strQualificacao, strTipoInvoice, strSaldo, strAging, strSituacao, salesOrderFiltro, invoiceFiltro,
                pedidoCompraFiltro, prefixoFiltro, numDIFiltro, 0, 0, preFaturamentoFiltro, numProcessoImportacaoFiltro, numLoteFiltro);
            if(lstSalesOrder != null)
            {
                Session["lstSalesOrder"] = lstSalesOrder;
            }
            if (Session["lstSalesOrder"] != null)
            {
                lstSalesOrder = (List<StatementGestaoCredito>)Session["lstSalesOrder"];
                obj.lstSalesOrder = lstSalesOrder;
            }

             if (Session["lstComentarios"] != null)
            {
                obj.LstComentarios = (List<StatementGestaoCredito>)Session["lstComentarios"];
            }

            obj.lstSalesOrder = lstSalesOrder;

            //lstSalesOrderAnalise = gestaoBUS.ListaSalesOrderAnalise(id_salesorder);
            //if (lstSalesOrderAnalise != null)
            //{
            //    Session["lstSalesOrderAnalise"] = lstSalesOrderAnalise;
            //}
            if (Session["lstSalesOrderAnalise"] != null)
            {
                lstSalesOrderAnalise = (List<StatementGestaoCredito>)Session["lstSalesOrderAnalise"];
                obj.lstSalesOrderAnalise = lstSalesOrderAnalise;
            }

            //lstInvoices = gestaoBUS.ListaSalesOrderInvoice(id_salesorder);
            //if (lstInvoices != null)
            //{
            //    Session["lstInvoices"] = lstInvoices;
            //}
            if (Session["lstInvoices"] != null)
            {
                lstInvoices = (List<StatementGestaoCredito>)Session["lstInvoices"];
                obj.lstInvoices = lstInvoices;
            }
            if (Session["lstComentarios"] != null)
            {
                lstComentarios = (List<StatementGestaoCredito>)Session["lstComentarios"];
                obj.LstComentarios = lstComentarios;
            }
            ////lstInvoices.Add(new StatementGestaoCredito
            //{
            //    Codigo = "CJ12564",
            //    Valor = 2600,
            //    Descricao = "Em aberto"
            //});

            //obj.lstInvoices = lstInvoices;

            pageSize = 10;
            int pageSizeAnaliseItem = 10;
            ViewBag.QtdRegistros = 0;
            double pageCount;

            if (lstSalesOrder != null)
            {
                pageCount = (double)((decimal)lstSalesOrder.Count() / Convert.ToDecimal(pageSize));
                obj.PageSize = (int)Math.Ceiling(pageCount);
                obj.PageNumber = pageNumber;
                IEnumerable<StatementGestaoCredito> lstAnaliseEnum = lstSalesOrder;
                lstAnaliseEnum = lstSalesOrder.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                lstSalesOrder = lstAnaliseEnum.ToList();
                obj.lstSalesOrder = lstSalesOrder;

                ViewBag.Pagina = pageNumber;
            }

            double pageCountLstAnaliseItem;
            if (lstSalesOrder != null && lstSalesOrderAnalise !=null)
            {
                //ViewBag.QtdRegistros = lstSalesOrder.Count();
                //ViewBag.Amount = Math.Round(lstSalesOrder.Sum(x => x.Amount), 2);
                //ViewBag.ValorRM = Math.Round(lstSalesOrder.Sum(x => x.RMValor), 2);
                //ViewBag.Diferenca = Math.Round(lstSalesOrder.Sum(x => x.Diferenca), 2);

                pageCountLstAnaliseItem = (double)((decimal)lstSalesOrder.Count() / Convert.ToDecimal(pageSizeAnaliseItem));
                obj.PageSizeAnaliseItem = (int)Math.Ceiling(pageCountLstAnaliseItem);
                obj.PageNumberanaliseItem = pageNumberAnaliseItem;
                IEnumerable<StatementGestaoCredito> lstAnaliseItemEnum = lstSalesOrderAnalise;
                lstAnaliseItemEnum = lstSalesOrderAnalise.Skip((pageNumberAnaliseItem - 1) * pageSizeAnaliseItem).Take(pageSizeAnaliseItem);
                lstSalesOrderAnalise = lstAnaliseItemEnum.ToList();
                obj.lstSalesOrderAnalise = lstSalesOrderAnalise;

                ViewBag.PaginaAnaliseItem = pageNumberAnaliseItem;

                if(salesOrderFiltro != null)
                {
                    obj.salesOrderFiltro = salesOrderFiltro;
                }
            }

            return View(obj);
        }

        public ActionResult Invoices(int id_salesorder = 0, string divShow = null)
        {
            List<StatementGestaoCredito> lstInvoices = new List<StatementGestaoCredito>();
            List<SalesOrderQualificacao> lstQualificacao = new List<SalesOrderQualificacao>();
            List<StatementGestaoCredito> lstSalesOrderAnalise = new List<StatementGestaoCredito>();

            lstInvoices = gestaoBUS.ListaSalesOrderInvoice(id_salesorder);
            if (lstInvoices != null)
            {
                Session["lstInvoices"] = lstInvoices;
            }
           
            lstSalesOrderAnalise = gestaoBUS.ListaSalesOrderAnalise(id_salesorder);
            if (lstSalesOrderAnalise != null)
            {
                Session["lstSalesOrderAnalise"] = lstSalesOrderAnalise;
            }
            lstQualificacao = soQualificacaoBUS.ListaQualificacao();
            int qualificacao = 0;
            foreach (var r in lstSalesOrderAnalise)
            {
                qualificacao = r.ID_QUALIFICACAO;
            }
            
            ViewBag.Qualificacao2 = new SelectList(lstQualificacao, "ID_QUALIFICACAO", "Descricao");

            List<StatementGestaoCredito> lstComentarios = new List<StatementGestaoCredito>();
            lstComentarios = gestaoBUS.ListaComentarios(id_salesorder);
            if (lstComentarios != null)
            {
                Session["lstComentarios"] = lstComentarios;
                //obj.LstComentarios = lstComentarios;
            }
            else
            {
                Session["lstComentarios"] = null;
            }
                return null;
        }
        public ActionResult SalvarComentario(int id_salesorder = 0, string divShow = null, string comentario = null, int id_qualificacao = 0)
        {
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
            StatementGestaoCredito obj = new StatementGestaoCredito();

            List<StatementGestaoCredito> lstComentarios = new List<StatementGestaoCredito>();
            gestaoBUS.SalvarComentario(id_salesorder, Convert.ToInt32(usr.Id_Pessoa), comentario, id_qualificacao);

            lstComentarios = gestaoBUS.ListaComentarios(id_salesorder);
            if (lstComentarios != null)
            {
                Session["lstComentarios"] = lstComentarios;
                obj.LstComentarios = lstComentarios;
            }

            obj.lstSalesOrderAnalise = gestaoBUS.ListaSalesOrderAnalise(id_salesorder);
            if (obj.lstSalesOrderAnalise != null)
            {
                Session["lstSalesOrderAnalise"] = obj.lstSalesOrderAnalise;
            }

            return null;
        }
        public ActionResult salvarAnaliseConcilicacao(string comentarios = null)
        {
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
            List<StatementDepartamento> lstDepartamentoFiltro = new List<StatementDepartamento>();
            List<StatementClassificacao> lstClassificacaoFiltro = new List<StatementClassificacao>();
            int id_departamento = 0;
            int id_classificacao = 0;

            
            return RedirectToAction("Index", "StatementConciliacao", new { divShow = "#divModal", divShow2 = "#divInvoices" });
        }
    }
}