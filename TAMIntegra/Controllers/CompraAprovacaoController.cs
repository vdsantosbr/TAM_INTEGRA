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
    public class CompraAprovacaoController : Controller
    {
        private CompraBUS comBus = new CompraBUS();
        private FornecedorBUS fornecedorBUS = new FornecedorBUS();
        private CompraAprovacaoBUS compradorBUS = new CompraAprovacaoBUS();
        private TipoMovimentoBUS tipoMovimentoBUS = new TipoMovimentoBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        List<CompraAprovacao> lstPedido = new List<CompraAprovacao>();
        List<CompraAprovacao> lstItem = new List<CompraAprovacao>();
        List<CompraAprovacao> lstAprovacao = new List<CompraAprovacao>();
        List<CompraAprovacao> lstJustificativa = new List<CompraAprovacao>();
        PerfilBUS perfilBUS = new PerfilBUS();

        List<Motivo> lstMotivo = new List<Motivo>();

        private ImportacaoDocumentoBUS importacaoDocumentoBUS = new ImportacaoDocumentoBUS();
        CompraAprovacao comprador = new CompraAprovacao();
        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [CustomAuthorize(Roles = "frmComprasAprovacao")]
        public ActionResult Index(string tipoMovimento = "", string numeroMov = "", string exibir = null, string exibir2 = null, string motivo = "")
           {
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);
            string exibir_editar = "";
            string exibir_liberar = "";
            CarregaDados(0);

            if(tipoMovimento == "")
            {
                Session["MensagemPedido"] = "";
            }

            if(exibir != null || exibir2 != null)
            {
                comprador.Pedido = numeroMov;
                comprador.tipoMovimento = tipoMovimento;
                lstPedido = compradorBUS.Pedido(tipoMovimento, numeroMov);
                if(lstPedido.Count == 0)
                {
                    Session["MensagemPedido"] = "Pedido de compra não existe!";
                }
                else
                {
                    Session["MensagemPedido"] = "";
                }
                lstItem = compradorBUS.Item(tipoMovimento, numeroMov);
                lstAprovacao = compradorBUS.Aprovacao(tipoMovimento, numeroMov);
                lstJustificativa = compradorBUS.Justificativa(tipoMovimento, numeroMov);
                lstMotivo = compradorBUS.Motivo();
                //lstJustificativa.Add(new CompraAprovacao
                //{
                //    Observacao = "Pedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico canceladoPedico cancelado"
                //});
                if (lstAprovacao.Count == 0)
                {
                    //TempData["Mensagem"] = "Pedido de compra não encontrado!";
                }
                foreach(var j in lstJustificativa)
                {
                    if(j.Responsavel == "" || j.Responsavel == null)
                    {
                        j.Responsavel = ViewBag.Nome;
                    }
                    comprador.Justificativa_Edicao = j.Justificativa;
                    if(j.IdLiberacaoPOMotivo > 0)
                    {
                        comprador.Motivo = j.IdLiberacaoPOMotivo;
                    }
                }

            }
            
            IEnumerable<CompraAprovacao> result = lstPedido;
            comprador.LstCompraAprovacao = result.ToList();
            comprador.LstItem = lstItem;
            comprador.LstAprovacao = lstAprovacao;
            comprador.LstJustificativa = lstJustificativa;
            comprador.lstMotivo = lstMotivo;

            if(lstJustificativa != null)
            {
               foreach(var r in lstJustificativa)
                {
                    exibir_editar = r.Exibir_Editar;
                    exibir_liberar = r.Exibir_Liberar;
                }
                comprador.Exibir_Liberar = exibir_editar;
                comprador.Exibir_Liberar = exibir_liberar;
            }

            
            return PartialView(comprador);
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
                catch(Exception e)
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
            List<Fornecedor> lstFornecedor = fornecedorBUS.ListaConta(id_Perfil, "frmComprasCessnaPedidoCompra");
            ViewBag.Fornecedores = new SelectList(lstFornecedor, "codcfo", "Nome");

            ViewBag.Comprador = new SelectList(compradorBUS.Lista(), "codigo", "descricao");
            List<Entities.CompraAprovacao> lstTipoMovimento = new List<CompraAprovacao>();
            //lstTipoMovimento = tipoMovimentoBUS.Lista("frmComprasCessnaPedidoCompra", 0);
            //lstTipoMovimento = lstTipoMovimento.GroupBy(p => p.CODTMV)
            //                   .Select(g => g.First())
            //                   .ToList();

            lstTipoMovimento = compradorBUS.TipoMovimento();
            ViewBag.TipoMovimento = new SelectList(lstTipoMovimento, "CODTMV", "Descricao");
            //if (id_integracao_processo > 0)
            //{
                //ViewBag.Movimentos = new SelectList(lstTipoMovimento.GroupBy(p => p.CODTMV)
                //                                                    .Select(g => g.First())
                //                                                    .Where(x => x.CODTMV != null)
                //                                                    .ToList(), "codtmv", "descricao");
        //    }
        //    else
        //    {
        //        ViewBag.Movimentos = new SelectList(lstTipoMovimento, "codtmv", "descricao");
        //}

        ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");

            //Integracao integracao = integracaoBUS.BuscaIdIntegracaoProcessoBUS("frmComprasCessnaPedidoCompra");
            ViewBag.IdIntegracaoProcesso = id_integracao_processo;

            //string login = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().Replace("TAMMRL\\", "");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;
            ViewBag.Nome = usr.Nome;
            ViewBag.Motivo = new SelectList(compradorBUS.Motivo(), "idLiberacaoPOMotivo", "DESCRICAO");

            List<Perfil> lstPerfis = perfilBUS.Parametrizacao(usr.Id_Perfil).Where(x => x.Formulario == "frmComprasAprovacao").ToList();
            foreach (var r in lstPerfis)
            {
                ViewBag.PermitirConsultar = r.Permitir_Consultar;
                ViewBag.PermitirEditar = r.Permitir_Editar;
                ViewBag.PermitirExportar = r.Permitir_Exportar;
            }

        }
        public JsonResult  AgendarIntegracao(string origem = null, int idMov = 0, int id_integracao = 0)
        {
            try
            {
                int integracaoProcesso = 0;
                Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());

                List<Fornecedor> lstFornecedor = fornecedorBUS.ListaConta(usr.Id_Perfil, "frmComprasCessnaPedidoCompra");
                foreach(var f in lstFornecedor)
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
           catch(Exception e)
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
                                 Comprador = info.Comprador
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
                    compradorBUS.CancelarPedidoBUS(idMov, ViewBag.IdPessoa, justificativa);
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
                    compradorBUS.EditarPedidoBUS(idMov, ViewBag.IdPessoa, motivo, justificativa);
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
                    compradorBUS.LiberarPedidoBUS(idLiberacaoPO, motivo, idMov, ViewBag.IdPessoa, justificativa);
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
            lstMotivo = compradorBUS.Motivo();
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


    }
}
