using Business;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TAMIntegra.App_Start;

namespace TAMIntegra.Controllers
{
    public class PedidoVendaController : Controller
    {
        private CompraBUS comBus = new CompraBUS();
        private FornecedorBUS fornecedorBUS = new FornecedorBUS();
        private PedidoVendaBUS pedidoVendaBUS = new PedidoVendaBUS();
        private TipoMovimentoBUS movimentoBUS = new TipoMovimentoBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        List<PedidoVenda> lstPedido = new List<PedidoVenda>();
        List<PedidoVenda> lstItem = new List<PedidoVenda>();
        List<PedidoVenda> lstAprovacao = new List<PedidoVenda>();
        List<PedidoVenda> lstJustificativa = new List<PedidoVenda>();
        PerfilBUS perfilBUS = new PerfilBUS();

        List<Motivo> lstMotivo = new List<Motivo>();

        private ImportacaoDocumentoBUS importacaoDocumentoBUS = new ImportacaoDocumentoBUS();
        PedidoVenda pdv = new PedidoVenda();

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [CustomAuthorize(Roles = "frmPedidoVenda")]
        public ActionResult Index(string fornecedorNome = "", string movimento = "", string numeroMov = "", string exibir = null, string exibir2 = null, string motivo = "", string exibirpdv = "")
        {
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);
            string exibir_editar = "";
            string exibir_liberar = "";
            List<PedidoVenda> pdvLst = new List<PedidoVenda>();
            IEnumerable<PedidoVenda> result = lstPedido;

            if (fornecedorNome != "")
            {
                CarregaDados(Convert.ToInt32(fornecedorNome));
            }
            else
            {
                CarregaDados(0);
            }


            if (exibir != null || exibir2 != null)
            {
                pdv.Pedido = numeroMov;
                pdv.tipoMovimento = movimento;
                 lstPedido = pedidoVendaBUS.Pedido(movimento, numeroMov);
                if (lstPedido.Count == 0)
                {
                    Session["MensagemPedido"] = "Pedido de compra não existe!";
                }
                else
                {
                    Session["MensagemPedido"] = "";
                }
                foreach(var r in lstPedido)
                {
                    ViewBag.IdMov = r.idMov;
                }
                if (ViewBag.IdMov != null)
                {
                    pdvLst = pedidoVendaBUS.PedidoVenda(Convert.ToInt32(ViewBag.Id_integracao_processo), Convert.ToInt32(ViewBag.Id_integracao_servidor),Convert.ToInt32(ViewBag.IdMov));
                }
                else
                {
                    pdvLst = null;
                }

                if (pdvLst != null)
                {
                    foreach (var r in pdvLst)
                    {
                        if (r.idMov == 0)
                        {
                            ViewBag.ExibirBotaoGerarVenda = "S";
                        }
                    }
                }
                else
                {
                    ViewBag.ExibirBotaoGerarVenda = "S";
                }


                if (exibirpdv == "")
                {
                    if (lstPedido != null)
                    {
                        pdv.LstPedidoVenda = lstPedido.ToList();
                        pdv.lstPedidoCompra = lstPedido.ToList();
                    }
                    lstItem = pedidoVendaBUS.Item(movimento, numeroMov);
                    lstAprovacao = pedidoVendaBUS.Aprovacao(movimento, numeroMov);
                    lstJustificativa = pedidoVendaBUS.Justificativa(movimento, numeroMov);
                    lstMotivo = pedidoVendaBUS.Motivo();
                    if (lstAprovacao.Count == 0)
                    {
                        //TempData["Mensagem"] = "Pedido de compra não encontrado!";
                    }
                    foreach (var j in lstJustificativa)
                    {
                        if (j.Responsavel == "" || j.Responsavel == null)
                        {
                            j.Responsavel = ViewBag.Nome;
                        }
                        pdv.Justificativa_Edicao = j.Justificativa;
                        if (j.IdLiberacaoPOMotivo > 0)
                        {
                            pdv.Motivo = j.IdLiberacaoPOMotivo;
                        }
                    }
                    lstPedido = null;
                    lstAprovacao = null;
                    lstItem = null;
                }
            }

            pdv.LstItem = lstItem;
            pdv.LstAprovacao = lstAprovacao;
            pdv.LstJustificativa = lstJustificativa;
            pdv.lstMotivo = lstMotivo;


            if (lstJustificativa != null)
            {
                foreach (var r in lstJustificativa)
                {
                    exibir_editar = r.Exibir_Editar;
                    exibir_liberar = r.Exibir_Liberar;
                }
                pdv.Exibir_Liberar = exibir_editar;
                pdv.Exibir_Liberar = exibir_liberar;
            }
            pdv.Movimento = movimento;
            pdv.pdvLst = pdvLst;
            return PartialView(pdv);
        }

        public JsonResult DesbloquearPedido(int idIntegracao = 0, int idMov = 0)
        {
            CarregaDados();

            try
            {
                string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
                string login = temx[1];
                Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
                ViewBag.IdPessoa = usr.Id_Pessoa;

                try
                {
                    integracaoBUS.DesbloquearPedidoBUS(idIntegracao, idMov, ViewBag.IdPessoa);
                    ViewBag.IdMov_Desbloqueio = idMov;
                }
                catch (Exception e)
                {
                    ViewBag.IdMov_Desbloqueio = null;
                }


                return Json(idMov, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
                //return new HttpStatusCodeResult(500, e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        private void CarregaDados(int id_integracao_processo = 0)
        {
            //id_integracao_processo = 13;
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);
            List<PedidoVenda> lstFornecedor = pedidoVendaBUS.Fornecedor();
            ViewBag.Fornecedores = new SelectList(lstFornecedor, "Id_Integracao_Processo", "Nome");
            lstFornecedor = lstFornecedor.Where(x => x.Id_Integracao_Processo == id_integracao_processo).ToList();
            foreach(var r in lstFornecedor)
            {
                id_integracao_processo = r.Id_Integracao_Processo;
                ViewBag.Id_integracao_processo = r.Id_Integracao_Processo;
                ViewBag.Id_integracao_servidor = r.Id_Integracao_Servidor;
            }
            List<Entities.PedidoVenda> lstmovimento = new List<PedidoVenda>();
            if(id_integracao_processo > 0){
                lstmovimento = pedidoVendaBUS.Movimento(id_integracao_processo);
                ViewBag.Movimento = new SelectList(lstmovimento, "CodTMV", "Descricao");
            }
            else
            {
                ViewBag.Movimento = new SelectList(lstmovimento, "CodTMV", "Descricao");
            }

            ViewBag.IdIntegracaoProcesso = id_integracao_processo;

            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;
            ViewBag.Nome = usr.Nome;
            ViewBag.Motivo = new SelectList(pedidoVendaBUS.Motivo(), "idLiberacaoPOMotivo", "DESCRICAO");

            List<Perfil> lstPerfis = perfilBUS.Parametrizacao(usr.Id_Perfil).Where(x => x.Formulario == "frmComprasAprovacao").ToList();
            foreach (var r in lstPerfis)
            {
                ViewBag.PermitirConsultar = r.Permitir_Consultar;
                ViewBag.PermitirEditar = r.Permitir_Editar;
                ViewBag.PermitirExportar = r.Permitir_Exportar;
            }

        }
        public JsonResult AgendarIntegracao(string origem = null, int idMov = 0, int id_integracao = 0)
        {
            try
            {
                int integracaoProcesso = 0;
                Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());

                List<Fornecedor> lstFornecedor = fornecedorBUS.ListaConta(usr.Id_Perfil, "frmComprasCessnaPedidoCompra");
                foreach (var f in lstFornecedor)
                {
                    integracaoProcesso = f.id_integracao_processo;
                }
                //Integracao integracaoProcesso = integracaoBUS.BuscaIdIntegracaoProcessoBUS("frmComprasCessnaPedidoCompra");



                //Integracao integracao = integracaoBUS.IntegracaoDEBUS(0, Convert.ToInt32(idIntegracaoProcesso), "EDIPOP", "Depósito Especial", "AG", "", idPessoa);
                Integracao integracao = new Integracao();
                integracao.Id_integracao_Processo = Convert.ToInt32(integracaoProcesso);
                integracao.Id_Pessoa = usr.Id_Pessoa;
                integracao.Tipo = "EDIPOP";
                integracao.Situacao = "AG";
                integracao.Complemento = "PedidoCompra";
                integracao.Id_integracao = id_integracao;
                integracaoBUS.Integracao(integracao);

                integracaoBUS.IntegracaoParametroBUS(origem, integracao.Id_integracao, idMov);
                return Json(integracao.Id_integracao, JsonRequestBehavior.AllowGet);

                //TempData["Mensagem"] = "Integração agendada com sucesso.";
                //return null;
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Erro.";
                return null;
            }



        }

        public JsonResult Informativo(int idMov = 0)
        {
            List<ImportacaoDocumento> lstDocumento = new List<ImportacaoDocumento>();
            string mensagem = "";
            //lstInforme.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            //lstInforme = cavokBUS.Informe(id_integracao);

            //TempData["lstInforme"] = lstInforme.ToList();

            lstDocumento = importacaoDocumentoBUS.abrirDocumento(idMov); ;
            string strData = "";
            //foreach (var r in lstDocumento)
            //{
            //    strData = r.Data.ToShortDateString();
            //}

            foreach (var r in lstDocumento)
            {
                strData = r.DataEmissao.ToShortDateString();
            }

            var resultado = (from info in lstDocumento
                             select new
                             {
                                 Documento = info.Documento,
                                 Filial = info.Filial,
                                 DataEmissao = strData,
                                 Status = info.Status,
                                 Moeda = info.Moeda,
                                 Valor = info.Valor,
                                 Condicao_Pagamento = info.Condicao_Pagamento,
                                 Fornecedor = info.Fornecedor,
                                 pdv = info.Comprador
                             }).ToList();


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CancelarPedido(int idMov = 0, string justificativa = "")
        {
            CarregaDados();

            try
            {
                string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
                string login = temx[1];
                Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
                ViewBag.IdPessoa = usr.Id_Pessoa;

                try
                {
                    pedidoVendaBUS.CancelarPedidoBUS(idMov, ViewBag.IdPessoa, justificativa);
                    //ViewBag.IdMov_Desbloqueio = idMov;
                }
                catch (Exception e)
                {
                    //ViewBag.IdMov_Desbloqueio = null;
                }

                return Json(idMov, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
                //return new HttpStatusCodeResult(500, e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EditarPedido(int idMov = 0, string justificativa = "", int motivo = 0)
        {
            CarregaDados();

            try
            {
                string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
                string login = temx[1];
                Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
                ViewBag.IdPessoa = usr.Id_Pessoa;

                try
                {
                    pedidoVendaBUS.EditarPedidoBUS(idMov, ViewBag.IdPessoa, motivo, justificativa);
                    //ViewBag.IdMov_Desbloqueio = idMov;
                }
                catch (Exception e)
                {
                    //ViewBag.IdMov_Desbloqueio = null;
                }

                return Json(idMov, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
                //return new HttpStatusCodeResult(500, e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult LiberarPedido(int idMov = 0, string justificativa = "", int motivo = 0, int idLiberacaoPO = 0)
        {
            CarregaDados();

            try
            {
                string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
                string login = temx[1];
                Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
                ViewBag.IdPessoa = usr.Id_Pessoa;
                try
                {
                    pedidoVendaBUS.LiberarPedidoBUS(idLiberacaoPO, motivo, idMov, ViewBag.IdPessoa, justificativa);
                    //ViewBag.IdMov_Desbloqueio = idMov;
                }
                catch (Exception e)
                {
                    //ViewBag.IdMov_Desbloqueio = null;
                }

                return Json(idMov, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
                //return new HttpStatusCodeResult(500, e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult VerificarMotivo(string justificativa = "", int motivo = 0)
        {
            string retorno = "";
            List<Motivo> lstMotivo = new List<Motivo>();
            lstMotivo = pedidoVendaBUS.Motivo();
            lstMotivo = lstMotivo.Where(x => x.idLiberacaoPOMotivo == motivo).ToList();
            foreach (var r in lstMotivo)
            {
                if (r.OBRIGATORIO_JUSTIFICATIVA == "S" & justificativa == "")
                {
                    retorno = "erro";
                }
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuscaMovimentos(int id_integracao_processo = 0)
        {
            if (id_integracao_processo > 0)
            {
                List<PedidoVenda> lstMovimento = new List<PedidoVenda>();
                lstMovimento.AddRange(pedidoVendaBUS.Movimento(id_integracao_processo));
               
                var lst = (from mov in lstMovimento
                                  select new
                                  {
                                      Text = mov.Nome,
                                      Value = mov.CodTMV
                                  }).ToList();

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult gerarPedidoVenda(int id_integracao_servidor = 0, int id_integracao_processo = 0, int id_mov = 0)
        {
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            List<PedidoVenda> lst = new List<PedidoVenda>();
            lst = pedidoVendaBUS.GerarPedidoVenda(id_integracao_processo, id_integracao_servidor, id_mov, usr.Id_Pessoa);

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}
