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
    public class CompraController : Controller
    {
        private CompraBUS comBus = new CompraBUS();
        private FornecedorBUS fornecedorBUS = new FornecedorBUS();
        private CompradorBUS compradorBUS = new CompradorBUS();
        private TipoMovimentoBUS tipoMovimentoBUS = new TipoMovimentoBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        PerfilBUS perfilBUS = new PerfilBUS();
        List<Compra> lstComprador = new List<Compra>();

        private ImportacaoDocumentoBUS importacaoDocumentoBUS = new ImportacaoDocumentoBUS();
        Compra comprador = new Compra();
        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [CustomAuthorize(Roles = "frmComprasCessnaPedidoCompra")]
        public ActionResult Index(string strDataInicio, string strDataFim, string numeroMov = null, string[] numeroMovArr = null, string[] fornecedorNome = null, 
                                  string[] descricao = null,string situacao = null, string salvarPedidoCompra = null, int nroPedidoModal = 0, int idMov = 0,
                                  string[] tipoMovimento = null, string statusModal = null, string situacaoModal = null, string[] Descricao = null, string exibir = null, int id_integracao = 0)
           {
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);
            DateTime? dataTerminoDT = null;
            DateTime? dataInicioDT = null;
            Integracao integracaoProcesso = null;
            CarregaDados(0);


            string strFornecedor = "";
            if (fornecedorNome != null)
            {
                foreach (string idForn in fornecedorNome)
                {
                    if (strFornecedor.Trim().Length > 0) { strFornecedor += ","; }

                    if ((strFornecedor.Trim().Length + idForn.ToString().Length) < 4000)
                    {
                        strFornecedor += idForn.ToString();
                    }
                }
            }

            string strDescricao = "";
            if (Descricao != null)
            {
                foreach (string desc in Descricao)
                {
                    if (strDescricao.Trim().Length > 0) { strDescricao += ","; }

                    if ((strDescricao.Trim().Length + desc.ToString().Length) < 4000)
                    {
                        strDescricao += desc.ToString();
                    }
                }
            }

            string strTipoMov = "";
            if (tipoMovimento != null)
            {
                foreach (string tpMov in tipoMovimento)
                {
                    if (strTipoMov.Trim().Length > 0) { strTipoMov += ","; }

                    if ((strTipoMov.Trim().Length + tpMov.ToString().Length) < 4000)
                    {
                        strTipoMov += tpMov.ToString();
                    }
                }
            }


            if (strDataInicio == null)
            {
                //var dia = DateTime.Now.Day - 10;
                var dia = DateTime.Today.AddDays(-10);
                //strDataInicio = dia.ToString().PadLeft(2,'0') + DateTime.Today.ToString("/MM/yyyy");
                //dataInicioDT = DateTime.Parse(dia);
                strDataInicio = dia.ToString("dd/MM/yyyy");
                dataInicioDT = dia;
            }

            if (strDataFim == null)
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

            if(exibir != null || salvarPedidoCompra != null || id_integracao > 0 || idMov > 0)
            {
                if(id_integracao > 0 || idMov > 0)
                {
                    ViewBag.Id_Integracao_Agendamento = id_integracao;
                    ViewBag.IdMov_Desbloqueio = idMov;
                    lstComprador = compradorBUS.PedidoCompraLista(dataInicioDT, dataTerminoDT, strFornecedor, strDescricao, strTipoMov, numeroMov, id_Perfil, "frmComprasCessnaPedidoCompra", situacao, id_integracao, idMov);
                }
                else
                {
                    lstComprador = compradorBUS.PedidoCompraLista(dataInicioDT, dataTerminoDT, strFornecedor, strDescricao, strTipoMov, numeroMov, id_Perfil, "frmComprasCessnaPedidoCompra", situacao, 0, 0);
                }
               
            }
            

           //if (nroPedidoModal > 0 )
           // {
           //     //List<Comprador> idProcesso = compradorBUS.PedidoCompraLista(dataInicioDT, dataTerminoDT, strFornecedor, descricaoComprador, null, nroPedidoModal, id_Perfil, "frmComprasCessnaPedidoCompra", situacao);
           //     int idIntegracaoProcesso = ViewBag.IdIntegracaoProcesso;
           //     int idPessoa = ViewBag.IdPessoa;
           //     //Integracao integracao = integracaoBUS.IntegracaoDEBUS(0, Convert.ToInt32(idIntegracaoProcesso), "EDIPOP", "Depósito Especial", "AG", "", idPessoa);
           //     Integracao integracao = new Integracao();
           //     integracao.Id_integracao_Processo = Convert.ToInt32(idIntegracaoProcesso);
           //     integracao.Id_Pessoa = idPessoa;

           //     integracao.Tipo = "EDIPOP";
           //     integracao.Situacao = "AG";
           //     integracao.Complemento = "Depósito Especial";
           //     integracaoBUS.Integracao(integracao);

           //     integracaoBUS.IntegracaoParametroBUS(integracao.Id_integracao, idMov);
           // }

            foreach (var comp in lstComprador)
            {
                string date = comp.dataEmissao.ToString("dd/MM/yyyy");
                comp.dataEmissaoStr = date;
            }

            comprador.strDataInicio = strDataInicio;
            comprador.strDataFim = strDataFim;
            IEnumerable<Compra> result = lstComprador;
            comprador.ListaComprador = result.ToList();

            
            if(numeroMovArr != null)
            {
                var result2 = from Q in lstComprador
                              where numeroMovArr.Contains(Q.NUMEROMOV)
                              select Q;

                comprador.ListaComprador = result2.ToList();
            }
           
           
            return View(comprador);
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
            List<Entities.TipoMovimento> lstTipoMovimento = new List<TipoMovimento>();
            lstTipoMovimento = tipoMovimentoBUS.Lista("frmComprasCessnaPedidoCompra", 0);
            lstTipoMovimento = lstTipoMovimento.GroupBy(p => p.CODTMV)
                               .Select(g => g.First())
                               .ToList();

            ViewBag.TipoMovimento = new SelectList(lstTipoMovimento, "CODTMV", "Descricao");
            //if (id_integracao_processo > 0)
            //{
                ViewBag.Movimentos = new SelectList(lstTipoMovimento.GroupBy(p => p.CODTMV)
                                                                    .Select(g => g.First())
                                                                    .Where(x => x.CODTMV != null)
                                                                    .ToList(), "codtmv", "descricao");
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
            List<Perfil> lstPerfis = perfilBUS.Parametrizacao(usr.Id_Perfil).Where(x => x.Formulario == "frmComprasCessnaPedidoCompra").ToList();
            foreach(var r in lstPerfis)
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
    }
}
