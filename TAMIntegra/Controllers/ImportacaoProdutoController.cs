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

namespace TAMINTEGRA.Controllers
{
    public class ImportacaoProdutoController : Controller
    {
        ImportacaoProdutoBUS importacaoProdutoBUS = new ImportacaoProdutoBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private ImportacaoFornecedorBUS impFornecedorBUS = new ImportacaoFornecedorBUS();
        private ImportacaoDocumentoBUS importacaoDocumentoBUS = new ImportacaoDocumentoBUS();
        private TipoMovimentoBUS tipoMovimentoBUS = new TipoMovimentoBUS();

        [CustomAuthorize(Roles = "frmImportacaoProduto")]
        public ActionResult Index(string tipoMovimentoFiltro = null, string numeroMovimentoFiltro = null, string codigoFiltro = null, string nomeFantasiaFiltro = null,
            string checkNumeroPedido = null, string[] numNoFabric = null, string[] idMovArr = null, int id_fornecedorFilro = 0, int documento = 0,
            int fornecedorNome = 0, string movimento = null, int id_integracao = 0)
        {
            CarregaDados(fornecedorNome, documento);

            ImportacaoProduto importacaoProduto = new ImportacaoProduto();
            List<ImportacaoProduto> lstImportacaoProduto = new List<ImportacaoProduto>();

            //if (idMovArr != null)
            //{
            //    foreach(string id in idMovArr)
            //    {
            //        int idIntegracaoProcesso = ViewBag.IdIntegracaoProcesso;
            //        int idPessoa = ViewBag.IdPessoa;
            //        Integracao integracao = integracaoBUS.IntegracaoImportacaoProduto(0, Convert.ToInt32(idIntegracaoProcesso), null, null, null, null, idPessoa);
            //        integracaoBUS.IntegracaoParametroBUS(integracao.Id_integracao, Convert.ToInt32(id));
            //    }
            //}

            if((movimento != null & numeroMovimentoFiltro != null) || codigoFiltro != null)
            {
              lstImportacaoProduto = importacaoProdutoBUS.ListaProduto(id_integracao, movimento, numeroMovimentoFiltro, codigoFiltro);
            }
           
            importacaoProduto.lstProdutos = lstImportacaoProduto;


            List<UsuarioPerfilModulo> usuarioModulo = usuarioBUS.BuscaPorIdPerfilFormulariosList(Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Formulario == "frmImportacaoProduto").ToList();
            return View(importacaoProduto);
        }

        private void CarregaDados(int fornecedorFiltro = 0, int documento = 0)
        {
            List<ImportacaoDocumento> lstProcessos = new List<ImportacaoDocumento>();
            string formulario = "";
            int id_integracao_processo = 0;

            List<UsuarioPerfilModulo> usuarioPerfil = usuarioBUS.BuscaPorIdPerfilFormulariosList(Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Formulario == "frmImportacaoProduto").ToList();
            foreach (var usrPerfil in usuarioPerfil)
            {
                formulario = usrPerfil.Formulario;
                id_integracao_processo = usrPerfil.Id_Integracao_Processo;
            }

            Integracao integracao = new Integracao();
            integracao.Id_integracao_Processo = id_integracao_processo;
            if (integracao != null)
            {
                ViewBag.IdIntegracaoProcesso = integracao.Id_integracao_Processo;
            }
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;

            //ViewBag.Fornecedores = new SelectList(impFornecedorBUS.ListaFornecedor(usr.Id_Perfil).Where(x => x.Situacao == "Ativo"), "ID_INTEGRACAO_SERVIDOR", "Nome");

            //if (fornecedorFiltro > 0)
            //{
            //    ViewBag.Processos = new SelectList(importacaoDocumentoBUS.ListaProcesso(formulario, fornecedorFiltro, usr.Id_Pessoa).Where(x => x.Qualificacao == "Upload").Where(x => x.Situacao == "Ativo"), "id_integracao_processo", "nome");
            //}
            //else
            //{
            //    ViewBag.Processos = new SelectList(lstProcessos, "id_integracao_processo", "nome");
            //}

            //List<ImportacaoDocumento> lstMovimento = new List<ImportacaoDocumento>();
            //if (documento > 0)
            //{
            //    ViewBag.Movimentos = new SelectList(tipoMovimentoBUS.Lista(formulario, Convert.ToInt32(documento)), "codtmv", "descricao");
            //}
            //else
            //{
            //    ViewBag.Movimentos = new SelectList(lstMovimento, "codtmv", "descricao");
            //}
        }

        public ActionResult BuscaProcessos(int[] fornecedorNome)
        {
            if (fornecedorNome.Count() > 0)
            {
                List<Integracao> lstFornecedor = new List<Integracao>();

                foreach (int id in fornecedorNome)
                {
                    //lstFornecedor.AddRange(importacaoDocumentoBUS.ListaProcesso(0, id, null).Where(x => x.Qualificacao == "Upload"));
                    Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
                    lstFornecedor.AddRange(integracaoBUS.BuscaIdIntegracaoProcessoBUS("frmImportacaoProduto", id, Convert.ToInt32(usr.Id_Perfil)).Where(x => x.Qualificacao == "Upload").Where(x => x.Situacao == "Ativo").ToList());
                }

                var fornecedor = (from forn in lstFornecedor
                                  select new
                                  {
                                      Text = forn.Nome,
                                      Value = forn.Id_integracao_Processo
                                  }).ToList();

                return Json(fornecedor, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuscaTipoMovimento(int documento)
        {
            if (documento > 0)
            {
                List<TipoMovimento> lstMovimento = new List<TipoMovimento>();

                //foreach (int id in fornecedorNome)
                //{
                //    //lstFornecedor.AddRange(importacaoDocumentoBUS.ListaProcesso(0, id, null).Where(x => x.Qualificacao == "Upload"));


                lstMovimento = tipoMovimentoBUS.Lista("frmImportacaoProduto", documento);
                //}

                var movimento = (from forn in lstMovimento
                                  select new
                                  {
                                      Text = forn.DESCRICAO,
                                      Value = forn.CODTMV
                                  }).ToList();

                return Json(movimento, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuscaMovimento(int movimento = 0)
        {
            List<TipoMovimento> lstMovimento = new List<TipoMovimento>();

            var movimento2 = (from forn in lstMovimento
                                 select new
                                 {
                                     Text = "-- Selecione --",
                                     Value = 0
                                 }).ToList();

            return Json(movimento2, JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult DesbloquearPedido(int idIntegracao = 0, int idMov = 0)
        {
            IntegracaoBUS integracaoBUS = new IntegracaoBUS();
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);

            integracaoBUS.DesbloquearPedidoBUS(idIntegracao, idMov, id_Perfil);

            TempData["Mensagem"] = "Pedido desbloqueado com sucesso!";
            return null;
        }

        public JsonResult agendarIntegracao(string idPrd = null, int id_fornecedor = 0, string movimento = null)
        {
            List<ImportacaoDocumento> lstItens = new List<ImportacaoDocumento>();
            int id_integracao_processo = 0;
            string formulario = "";
            string permite_edicao = "";
            int id_forn = 0;

            try
            {
                List<UsuarioPerfilModulo> usuarioPerfil = usuarioBUS.BuscaPorIdPerfilFormulariosList(Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Formulario == "frmImportacaoProduto").ToList();
                foreach(var usrPerfil in usuarioPerfil)
                {
                    formulario = usrPerfil.Formulario;
                    id_integracao_processo = usrPerfil.Id_Integracao_Processo;
                    id_forn = usrPerfil.Id_Fornecedor;
                }

                List<ImportacaoDocumento> integracaoProcesso2 = importacaoDocumentoBUS.ListaProcesso(formulario, id_forn);
                //integracaoProcesso2 = integracaoProcesso2.Where(x => x.Id_Integracao_Processo.Equals(Convert.ToInt32(movimento))).ToList();
                //foreach (var r in integracaoProcesso2)
                //{
                //    id_integracao_processo = Convert.ToInt32(r.Id_Integracao_Processo);
                //}

                List<Integracao> integracao2 = integracaoBUS.IntegracaoProcesso(id_integracao_processo);

                foreach (var r in integracao2)
                {
                    permite_edicao = r.PERMITE_EDICAO;
                }

                if (Session["lstDocumento"] != null)
                {
                    lstItens = (List<ImportacaoDocumento>)Session["lstDocumento"];
                }

                //List<ImportacaoDocumento> integracaoProcesso = importacaoDocumentoBUS.ListaProcesso(formulario, id_forn);
                //integracaoProcesso = integracaoProcesso.Where(x => x.Nome.Equals(movimento)).ToList();
                //foreach (var r in integracaoProcesso)
                //{
                //    id_integracao_processo = Convert.ToInt32(r.Id_Integracao_Processo);
                //}


                List<string> lstProdutos = idPrd.Split(',').ToList();
                Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
                //Integracao integracao = integracaoBUS.IntegracaoDEBUS(0, Convert.ToInt32(idIntegracaoProcesso), "EDIPOP", "Depósito Especial", "AG", "", idPessoa);
                Integracao integracao3 = new Integracao();
                //Gerando id_integração
                integracao3.Id_integracao_Processo = id_integracao_processo;
                integracao3.Id_Pessoa = usr.Id_Pessoa;
                integracao3.Tipo = "PRODUTO";
                integracao3.Situacao = "AG";
                integracao3.Complemento = "IMPORTACAO";
                integracao3.Id_integracao = 0;

                integracaoBUS.Integracao(integracao3);
                foreach (var id in lstProdutos)
                {
                    //for (var i = 0; i < ; i++)
                    //{
                        {
                            integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, 0, 0, 0, Convert.ToInt32(id), "", "", 0, 0);
                        }
                    //}


                }



                //integracaoBUS.IntegracaoParametroBUS(integracao.Id_integracao, idMov);



                //TempData["Mensagem"] = "Integração agendada com sucesso.";
                //return RedirectToAction("Index", "StatementConciliacao", new { divShow = "#itensDocumento" });
                if (lstProdutos != null)
                {
                    var resultado = (from info in lstProdutos
                                     select new
                                     {
                                         //Item = info.ITEM,
                                         //Comentario = info.COMENTARIO
                                     }).ToList();
                    return Json(integracao3.Id_integracao, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Erro.";
                return null;
            }
        }
    }

   

}