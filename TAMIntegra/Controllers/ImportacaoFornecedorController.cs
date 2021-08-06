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
    [CustomAuthorize(Roles = "frmImportacaoFornecedor")]
    public class ImportacaoFornecedorController : Controller
    {
        private ImportacaoFornecedorBUS importacaoDocumentoBUS = new ImportacaoFornecedorBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private CompradorBUS compradorBUS = new CompradorBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();
        public ActionResult Index(string fornecedorNome = null, string[] situacao = null, string strDataInicio = null, string strDataFim = null, string documento = null,
                                  string movimento = null, string fornecedorSelected = null, string documentoSelected = null, string numeroMovimento = null, int idMov = 0,
                                  string divShow = null, string divShow2 = null, string nomeFantasia = null, string codigo = null, int id_integracao = 0)
        {

            DateTime dataTerminoDT = new DateTime();
            DateTime dataInicioDT = new DateTime();
            ImportacaoFornecedor importacaoDoc = new ImportacaoFornecedor();
            List<Compra> lstComprador = new List<Compra>();

            if (divShow != null)
            {
                ViewBag.DivShow = divShow;
                ViewBag.DivHide = "#itensDocumento";
            }
            else if (divShow2 != null)
            {
                ViewBag.DivShow = divShow2;
                ViewBag.DivHide = "#registros";
            }
            else
            {
                ViewBag.DivShow = "#registros";
                ViewBag.DivHide = "#itensDocumento";
            }

            CarregaDados();

            string strSituacao = "";
            if (situacao != null)
            {
                foreach (string sit in situacao)
                {
                    if (strSituacao.Trim().Length > 0) { strSituacao += ","; }

                    if ((strSituacao.Trim().Length + sit.ToString().Length) < 4000)
                    {
                        strSituacao += sit.ToString();
                    }
                }
            }

            if (strDataInicio == null)
            {
                var dia = DateTime.Today.AddDays(-10);
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

            importacaoDoc.strDataInicio = strDataInicio;
            importacaoDoc.strDataFim = strDataFim;


            List<ImportacaoFornecedor> lstImportacaoDocumento = new List<ImportacaoFornecedor>();
            //if(movimento != null)
            //{
            //    lstImportacaoDocumento = importacaoDocumentoBUS.Filtro(movimento, numeroMovimento);
            //}

            if (codigo != null || nomeFantasia != null || id_integracao > 0)
            {
                lstImportacaoDocumento = importacaoDocumentoBUS.Filtro(id_integracao, codigo, nomeFantasia);
            }



            importacaoDoc.lstItensPedido = importacaoDocumentoBUS.ItensPedido(idMov);
            importacaoDoc.lstDocumentos = lstImportacaoDocumento;

            importacaoDoc.Documento = documento;
            importacaoDoc.movimento = movimento;
            //importacaoDoc.numeroMovimento = numeroMovimento;


            //ViewBag.Processos = new SelectList(importacaoDocumentoBUS.ListaProcesso(0, 0, null).Where(x => x.Id_Integracao_Processo == Convert.ToInt32(documento)), "id_integracao_processo", "nome");
            List<ImportacaoDocumento> lstProcessos = new List<ImportacaoDocumento>();
            if (fornecedorNome != null)
            {
                ViewBag.Processos = new SelectList(importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", Convert.ToInt32(fornecedorNome), Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Qualificacao == "Upload").Where(x => x.Situacao == "Ativo"), "id_integracao_processo", "nome");
            }
            else
            {
                ViewBag.Processos = new SelectList(lstProcessos, "id_integracao_processo", "nome");
            }
            List<ImportacaoDocumento> lstMovimento = new List<ImportacaoDocumento>();
            if (documento != null)
            {
                ViewBag.Movimentos = new SelectList(importacaoDocumentoBUS.ListaMovimento("frmImportacaoDocumento", Convert.ToInt32(documento)), "codtmv", "descricao");
            }
            else
            {
                ViewBag.Movimentos = new SelectList(lstMovimento, "codtmv", "descricao");
            }


            if (Session["lstDocumento"] != null)
            {
                try
                {
                    importacaoDoc.lstItensDocumento = (List<ImportacaoFornecedor>)Session["lstDocumento"];
                }
                catch(Exception e)
                {
                    importacaoDoc.lstItensDocumento = new List<ImportacaoFornecedor>();
                }
                   
            }

            if (Session["lstItens"] != null)
            {
                try
                {
                    importacaoDoc.lstItens = (List<ImportacaoFornecedor>)Session["lstItens"];
                }
                catch (Exception e)
                {
                    importacaoDoc.lstItens = new List<ImportacaoFornecedor>();
                }
            }
            //Integracao integracaoBuscaProcesso = integracaoBUS.BuscaIdIntegracaoProcessoBUS("frmImportacaoDocumento");
            //if (fornecedorNome != null)
            //{
            //    List<ImportacaoFornecedor> integracaoProcesso = importacaoDocumentoBUS.ListaProcesso("frmImportacaoFornecedor", Convert.ToInt32(fornecedorNome));
            //    integracaoProcesso = integracaoProcesso.Where(x => x.Nome.Equals(documentoSelected)).ToList();
            //    foreach (var r in integracaoProcesso)
            //    {
            //        importacaoDoc.PERMITE_EDICAO = r.PERMITE_EDICAO;
            //    }

            //}


            return View(importacaoDoc);
        }

        private void CarregaDados()
        {
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);

            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");
            ViewBag.Fornecedores = new SelectList(importacaoDocumentoBUS.ListaFornecedor(id_Perfil).Where(x => x.Situacao == "Ativo"), "ID_INTEGRACAO_SERVIDOR", "Nome");

            //List<ImportacaoDocumento> lstMov = importacaoDocumentoBUS.ListaMovimento()
            //.GroupBy(p => p.codtmv)
            //.Select(g => g.First())
            //.Where(x => x.codtmv != null)
            //.ToList();

            List<ImportacaoDocumento> lstMov = new List<ImportacaoDocumento>();
            //ViewBag.Movimentos = new SelectList(lstMov, "codtmv", "descricao");

            //string login = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().Replace("TAMMRL\\", "");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
        public JsonResult agendarIntegracao(string idMov = null, string idCFO = null, int id_fornecedor = 0, string movimento = null, string[] dados = null)
        {
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            List<ImportacaoFornecedor> lstItens = new List<ImportacaoFornecedor>();
            int id_integracao_processo = 0;
            string permite_edicao = "";
            string formulario = "";
            int id_forn = 0;

            try
            {
                List<UsuarioPerfilModulo> usuarioPerfil = usuarioBUS.BuscaPorIdPerfilFormulariosList(Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Formulario == "frmImportacaoFornecedor").ToList();
                foreach (var usrPerfil in usuarioPerfil)
                {
                    formulario = usrPerfil.Formulario;
                    id_integracao_processo = usrPerfil.Id_Integracao_Processo;
                    id_forn = usrPerfil.Id_Fornecedor;
                }

                //List<ImportacaoFornecedor> integracaoProcesso2 = importacaoDocumentoBUS.ListaProcesso(formulario, 0, usr.IdPerfil);
                ////integracaoProcesso2 = integracaoProcesso2.Where(x => x.Id_Integracao_Processo.Equals(Convert.ToInt32(movimento))).ToList();
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
                    lstItens = (List<ImportacaoFornecedor>)Session["lstDocumento"];
                }

                //List<ImportacaoFornecedor> integracaoProcesso = importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", Convert.ToInt32(id_fornecedor));
                //integracaoProcesso = integracaoProcesso.Where(x => x.Nome.Equals(movimento)).ToList();
                //foreach (var r in integracaoProcesso)
                //{
                //    id_integracao_processo = Convert.ToInt32(r.Id_Integracao_Processo);
                //}


                //List<string> lstProdutos = idMov.Split(',').ToList();
                //int pIdcfo = Convert.ToInt32(lstProdutos[1]);
                //int pIdMov = Convert.ToInt32(lstProdutos[0]);
                string strIdCFO = "";
                string strIdMov = "";

                string linha = "";
                string[] linhaSplit = null;


                //Integracao integracao = integracaoBUS.IntegracaoDEBUS(0, Convert.ToInt32(idIntegracaoProcesso), "EDIPOP", "Depósito Especial", "AG", "", idPessoa);
                Integracao integracao3 = new Integracao();
                //Gerando id_integração
                integracao3.Id_integracao_Processo = id_integracao_processo;
                integracao3.Id_Pessoa = usr.Id_Pessoa;
                integracao3.Tipo = "FORNECEDOR";
                integracao3.Situacao = "AG";
                integracao3.Complemento = "IMPORTACAO";
                integracao3.Id_integracao = 0;

                integracaoBUS.Integracao(integracao3);

                //foreach (var id in lstProdutos)
                //{
                for (var i = 0; i < dados.Length; i++)
                    {
                        {
                            linha = dados[i];
                            linhaSplit = linha.Replace("id+","").Split(',');
                            strIdMov = linhaSplit[0];
                            strIdCFO = linhaSplit[1];

                            integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, 0, Convert.ToInt32(strIdCFO), 0, 0, "", "");
                        }
                    }
                //}



                //integracaoBUS.IntegracaoParametroBUS(integracao.Id_integracao, idMov);



                //TempData["Mensagem"] = "Integração agendada com sucesso.";
                return Json(integracao3.Id_integracao, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Erro.";
                return null;
            }
        }
    }
}