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
    public class ImportacaoDocumentoController : Controller
    {
        private ImportacaoDocumentoBUS importacaoDocumentoBUS = new ImportacaoDocumentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private CompradorBUS compradorBUS = new CompradorBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [OutputCache(NoStore = true, Duration = 0)]
        [CustomAuthorize(Roles = "frmImportacaoDocumento")]
        public ActionResult Index(string fornecedorNome = null, string[] situacaoDoc = null, string strDataInicio = null, string strDataFim = null, string documento = null,
                                  string movimento = null, string fornecedorSelected = null, string documentoSelected = null, string numeroMovimento = null, int idMov = 0,
                                  string divShow = null, string divShow2 = null, string filtrar = null, int id_integracao = 0, int id_integracao2 = 0, int fornecedorAgendamento = 0, string situacao = null,
                                  string fornecedorHistorico = null, List<string> dados = null, string numeroMovArr = "")
       {
            DateTime dataTerminoDT = new DateTime();
            DateTime dataInicioDT = new DateTime();
            ImportacaoDocumento importacaoDoc = new ImportacaoDocumento();
            List<Compra> lstComprador = new List<Compra>();

            //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();
          

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

            CarregaDados(documento);

            string strSituacao = "";
            if (situacaoDoc != null)
            {
                foreach (string sit in situacaoDoc)
                {
                    if (strSituacao.Trim().Length > 0) { strSituacao += ","; }

                    if ((strSituacao.Trim().Length + sit.ToString().Length) < 4000)
                    {
                        strSituacao += sit.ToString();
                    }
                }
            }
           
            if (strDataInicio == null || strDataInicio == "")
            {
                //strDataInicio = "01/" + DateTime.Today.ToString("MM/yyyy");
                //dataInicioDT = DateTime.Parse(strDataInicio);
                var dia = DateTime.Today.AddDays(-10);
                //strDataInicio = dia.ToString().PadLeft(2,'0') + DateTime.Today.ToString("/MM/yyyy");
                //dataInicioDT = DateTime.Parse(dia);
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

            importacaoDoc.strDataInicio = strDataInicio;
            importacaoDoc.strDataFim = strDataFim;
            importacaoDoc.movimento = movimento;
            if(situacao != null)
            {
                ViewBag.Situacao = situacao;
            }

            //Gerando dados teste
            //importacaoDoc.lstDocumentos = new List<ImportacaoDocumento>();
            //importacaoDoc.lstDocumentos.Add(new ImportacaoDocumento()
            //{   Data = new DateTime(2018, 09, 01),
            //    Documento = "174509/1.1.32",
            //    Status_Pedido_RM = "Aberto",
            //    Valor = 15000,
            //    Aprovacao = "Aprovado",
            //    Integracao = "176/NãoIntegrado",
            //    Situacao = "NãoIntegrado",
            //    Id_Integracao = 0,
            //    Filial = "10",
            //    Descricao = documento,
            //    Fornecedor = fornecedorNome,
            //    Id_Documento = 123,
            //    Id_Fornecedor = 1
            //});

            //importacaoDoc.lstDocumentos.Add(new ImportacaoDocumento()
            //{
            //    Data = new DateTime(2018, 09, 01),
            //    Documento = "174508/1.1.32",
            //    Status_Pedido_RM = "Aberto",
            //    Valor = 15000,
            //    Aprovacao = "Aprovado",
            //    Integracao = "176/Integrado",
            //    Situacao = "Integrado",
            //    Id_Integracao = 0,
            //    Filial = "10",
            //    Descricao = "Pedido de compra em garantia",
            //    Fornecedor = fornecedorNome,
            //    Id_Documento = 123,
            //    Id_Fornecedor = 1
            //});
            //importacaoDoc.codtmv = "1.1.32";

            //Gerando dados teste

            List<ImportacaoDocumento> lstImportacaoDocumento = new List<ImportacaoDocumento>();

            importacaoDoc.lstItensPedido = importacaoDocumentoBUS.ItensPedido(idMov);
            if (id_integracao > 0 || (numeroMovArr != null & numeroMovArr != ""))
            {
                if (id_integracao > 0)
                {
                    ViewBag.Id_Integracao_Agendamento = id_integracao;
                    lstImportacaoDocumento = importacaoDocumentoBUS.Filtro(Convert.ToInt32(Session["Id_Perfil"]), dataInicioDT, dataTerminoDT, movimento, numeroMovimento, "AG", id_integracao);
                    if (lstImportacaoDocumento != null)
                    {
                        importacaoDoc.lstDocumentos = lstImportacaoDocumento;
                    }
                }
                if (numeroMovArr != null & numeroMovArr != "")
                {
                    string linha = "";
                    string[] linhaSplit = null;
                    string numeroMovStr = "";
                    List<ImportacaoDocumento> lstValidacao = new List<ImportacaoDocumento>();
                    List<ImportacaoDocumento> lstItensValidacao = new List<ImportacaoDocumento>();

                    linha = numeroMovArr;
                    linhaSplit = linha.Split(',');
                    string observacao = "";

                    for (var indice = 0; indice <= linhaSplit.Count() - 1; indice++)
                    {
                        if (linhaSplit[indice] != "")
                        {
                            string numeroMovOrigem = linhaSplit[indice];
                            int indiceStr = linhaSplit[indice].IndexOf('/');
                            int idIntegracao = 0;
                            if (indiceStr > -1)
                            {
                                linhaSplit[indice] = linhaSplit[indice].Substring(0, indiceStr);
                            }
                            else
                            {
                                numeroMovOrigem = numeroMovOrigem + "/" + movimento;
                            }

                            lstItensValidacao = importacaoDocumentoBUS.Filtro(Convert.ToInt32(Session["Id_Perfil"]), dataInicioDT, dataTerminoDT, movimento, linhaSplit[indice], strSituacao, 0).ToList();
                            var filtro = lstItensValidacao.Where(x => x.Documento == numeroMovOrigem).ToList();
                            //lstImportacaoDocumento.AddRange(lstItensValidacao);
                            foreach (var r in filtro)
                            {
                                idMov = r.idMov;
                                idIntegracao = r.Id_Integracao;
                            }
                            lstValidacao = importacaoDocumentoBUS.IntegracaoParametroValidar(idMov);
                            foreach (var r in lstValidacao)
                            {
                                observacao = r.Observacao;
                            }
                            foreach (var r in lstItensValidacao)
                            {
                                r.Observacao = observacao;
                            };
                            if (importacaoDoc.lstDocumentos == null)
                            {
                                importacaoDoc.lstDocumentos = new List<ImportacaoDocumento>();
                                importacaoDoc.lstDocumentos.AddRange(lstItensValidacao);
                            }
                            else
                            {
                                if (idIntegracao == 0)
                                {
                                    importacaoDoc.lstDocumentos.AddRange(lstItensValidacao);
                                }
                            }
                            importacaoDoc.lstDocumentos = importacaoDoc.lstDocumentos.Distinct().ToList();
                            importacaoDoc.CorLinha = "V";
                        }
                    }
                }
            }
            else if (documento != null & documento != "")
            {
                lstImportacaoDocumento = importacaoDocumentoBUS.Filtro(Convert.ToInt32(Session["Id_Perfil"]), dataInicioDT, dataTerminoDT, movimento, numeroMovimento, strSituacao, 0);
                importacaoDoc.lstDocumentos = lstImportacaoDocumento;
                if (lstImportacaoDocumento != null)
                {
                    foreach (var r in lstImportacaoDocumento)
                    {
                        if (r.Id_Integracao == 0)
                        {
                            ViewBag.Id_Integracao_Agendamento = r.Id_Integracao;
                        }
                    }

                    if (ViewBag.Id_Integracao_Agendamento != 0)
                    {
                        foreach (var r in lstImportacaoDocumento)
                        {
                            ViewBag.Id_Integracao_Agendamento = r.Id_Integracao;
                        }
                    }
                }
            }
             if(dados != null)
            {
                for(var x = 0; x <= dados.Count() - 1; x++)
                {
                    string linha = "";
                    string[] linhaSplit = null;
                    string numeroMovStr = "";
                    linha = dados[x];
                    int idMov2 = 0;
                    linhaSplit = linha.Split(',');
                    idMov2 = Convert.ToInt32(linhaSplit[10]);
                    numeroMovStr = linhaSplit[12];
                    dataInicioDT = Convert.ToDateTime(linhaSplit[2]);
                    int indiceStr = numeroMovStr.IndexOf('/');
                    string numeroMov = numeroMovStr.Substring(0, indiceStr);

                    List<ImportacaoDocumento> lstValidacao = new List<ImportacaoDocumento>();
                    List<ImportacaoDocumento> lstItensValidacao = new List<ImportacaoDocumento>();

                    lstValidacao = importacaoDocumentoBUS.IntegracaoParametroValidar(idMov2);
                    lstItensValidacao = importacaoDocumentoBUS.Filtro(Convert.ToInt32(Session["Id_Perfil"]), dataInicioDT, dataTerminoDT, movimento, numeroMov, strSituacao, 0).ToList();
                    lstImportacaoDocumento.AddRange(lstItensValidacao);
                    importacaoDoc.lstDocumentos = lstImportacaoDocumento;
                }
            }
           

            importacaoDoc.Documento = documento;
            importacaoDoc.movimento = movimento;
            //importacaoDoc.numeroMovimento = numeroMovimento;
            

            //ViewBag.Processos = new SelectList(importacaoDocumentoBUS.ListaProcesso(0, 0, null).Where(x => x.Id_Integracao_Processo == Convert.ToInt32(documento)), "id_integracao_processo", "nome");
            List<ImportacaoDocumento> lstProcessos = new List<ImportacaoDocumento>();
            //if((fornecedorNome != null & fornecedorNome != "") || fornecedorAgendamento > 0)
            //{
            //    if(fornecedorAgendamento > 0)
            //    {
            //        lstProcessos = importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", fornecedorAgendamento, Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Qualificacao == "Upload").Where(x => x.Situacao == "Ativo").ToList();
            //    }
            //    else
            //    {
            //        lstProcessos = importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", Convert.ToInt32(fornecedorNome), Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Qualificacao == "Upload").Where(x => x.Situacao == "Ativo").ToList();
            //    }
                
            //    ViewBag.Processos = new SelectList(lstProcessos, "id_integracao_processo", "nome");
               
            //    lstProcessos = lstProcessos.Where(x => x.Id_Integracao_Processo == Convert.ToInt32(documento)).ToList();
            //    ViewBag.PermiteEdicao = lstProcessos.Select(x => x.PERMITE_EDICAO).First();
            //}
            //else
            //{
            //    ViewBag.Processos = new SelectList(lstProcessos, "id_integracao_processo", "nome");
            //}
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
                importacaoDoc.lstItensDocumento = (List <ImportacaoDocumento> )Session["lstDocumento"];
            }

            if (Session["lstItensImpDoc"] != null)
            {
                importacaoDoc.lstItens = (List<ImportacaoDocumento>)Session["lstItensImpDoc"];
                ViewBag.Id_Integracao = Convert.ToInt32(importacaoDoc.lstItens.Count(x => x.Id_Integracao == 0));
            }
            else
            {
                Session["lstItensImpDoc"] = null;
            }
            //Integracao integracaoBuscaProcesso = integracaoBUS.BuscaIdIntegracaoProcessoBUS("frmImportacaoDocumento");
            //if (fornecedorNome != null & fornecedorNome != "")
            //{
            //    List<ImportacaoDocumento> integracaoProcesso = importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", Convert.ToInt32(fornecedorNome));
            //    integracaoProcesso = integracaoProcesso.Where(x => x.Nome.Equals(documentoSelected)).ToList();
            //    foreach(var r in integracaoProcesso)
            //    {
            //        importacaoDoc.PERMITE_EDICAO = r.PERMITE_EDICAO;
            //    }
               
            //}
            ViewBag.situacaoDoc = strSituacao;

            if(documentoSelected != null & documentoSelected != "")
            {
                Session["documento"] = documentoSelected;
            }

            if(id_integracao > 0)
            {
               Session["id_integracao"] = id_integracao;
                //TempData["Mensagem"] = id_integracao;
            }
           
            return View(importacaoDoc);

        }

        public ActionResult ddlTransType_Change(int idMov)
        {
            ImportacaoDocumento importacaoDoc = new ImportacaoDocumento();

            importacaoDoc.lstItensPedido = importacaoDocumentoBUS.ItensPedido(idMov);

            var entidades = (from forn in importacaoDoc.lstItensPedido
                             select new
                             {
                                 Numero = forn.NUMEROMOV,
                                 Chave = forn.Chave,
                                 Quantidade = forn.Quantidade,
                                 Produto = forn.NUMNOFABRIC,
                                 Serial = forn.SerialNumber
                             }).ToList();

            return Json(entidades, JsonRequestBehavior.AllowGet);



            //importacaoDoc.lstItensPedido = importacaoDocumentoBUS.ItensPedido(1669481);
            //return Json(new
            //{
            //    list = importacaoDoc.lstItensPedido
            //}, JsonRequestBehavior.AllowGet);

        }
        public ActionResult BuscaProcessos(int fornecedorNome)
        {
            if (fornecedorNome > 0)
            {
                List<ImportacaoDocumento> lstFornecedor = new List<ImportacaoDocumento>();

                //foreach (int id in fornecedorNome)
                //{
                //    //lstFornecedor.AddRange(importacaoDocumentoBUS.ListaProcesso(0, id, null).Where(x => x.Qualificacao == "Upload"));
                //    Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
                //    lstFornecedor.AddRange(importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", id, Convert.ToInt32(usr.Id_Pessoa)).Where(x => x.Qualificacao == "Upload").Where(x => x.Situacao == "Ativo"));
                //}
               if(fornecedorNome > 0)
                {
                    //lstFornecedor.AddRange(importacaoDocumentoBUS.ListaProcesso(0, id, null).Where(x => x.Qualificacao == "Upload"));
                    Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
                    lstFornecedor.AddRange(importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", fornecedorNome, Convert.ToInt32(usr.Id_Perfil)).Where(x => x.Qualificacao == "Upload" || x.Qualificacao == "U").Where(x => x.Situacao == "Ativo" || x.Situacao == "A"));
                }


                var fornecedor = (from forn in lstFornecedor
                                 select new
                                 {
                                     Text = forn.Nome,
                                     Value = forn.Id_Integracao_Processo
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
                List<ImportacaoDocumento> lstFornecedor = new List<ImportacaoDocumento>();

                //foreach (int id in fornecedorNome)
                //{
                //    //lstFornecedor.AddRange(importacaoDocumentoBUS.ListaProcesso(0, id, null).Where(x => x.Qualificacao == "Upload"));
             

                lstFornecedor = importacaoDocumentoBUS.ListaMovimento("frmImportacaoDocumento", documento);
                lstFornecedor = lstFornecedor.GroupBy(p => p.Descricao)
                .Select(g => g.First())
                .ToList();

                var fornecedor = (from forn in lstFornecedor
                                  select new
                                  {
                                      Text = forn.Descricao,
                                      Value = forn.codtmv
                                  }).ToList();

                return Json(fornecedor, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        private void CarregaDados(string documento = "")
        {
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);
            List<ImportacaoDocumento> lstProcessos = importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", 0, Convert.ToInt32(usr.Id_Perfil)).Where(x => x.Qualificacao == "Upload" || x.Qualificacao == "U").Where(x => x.Situacao == "Ativo" || x.Situacao == "A").ToList(); 

            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");
            ViewBag.Fornecedores = new SelectList(importacaoDocumentoBUS.ListaFornecedor(id_Perfil).Where(x=>x.Situacao == "Ativo"), "ID_INTEGRACAO_SERVIDOR", "Nome");
            ViewBag.Processos = new SelectList(lstProcessos, "id_integracao_processo", "nome");
            if(documento != null & documento != "")
            {
                lstProcessos = lstProcessos.Where(x => x.Id_Integracao_Processo == Convert.ToInt32(documento)).ToList();
                ViewBag.PermiteEdicao = lstProcessos.Select(x => x.PERMITE_EDICAO).First();
            }
            else
            {
                ViewBag.PermiteEdicao = lstProcessos.Select(x => x.PERMITE_EDICAO).First();
            }

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
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }

        public ActionResult DesbloquearPedido(int idIntegracao = 0, int idMov = 0)
        {
            //CarregaDados();
            try
            {
             string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            ViewBag.IdPessoa = usr.Id_Pessoa;

            integracaoBUS.DesbloquearPedidoBUS(idIntegracao, idMov, ViewBag.IdPessoa);

            TempData["Mensagem"] = "Pedido desbloqueado com sucesso.";
            return null;
                //integracaoBUS.IntegracaoParametroBUS(integracao.Id_integracao, idMov);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
                return new HttpStatusCodeResult(500, e.Message);
            }

        }


        public ActionResult abrirDocumento(int idMov = 0, int id_fornecedor = 0, string movimento = null, string mensagem = "", int id_integracao = 0)
        {
            int id_integracao_processo = 0;
            string permite_edicao = "";
            List<ImportacaoDocumento> lstItens = new List<ImportacaoDocumento>();

            if (id_fornecedor > 0)
            {
                Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
                List<ImportacaoDocumento> integracaoProcesso = importacaoDocumentoBUS.ListaProcesso("frmImportacaoDocumento", Convert.ToInt32(id_fornecedor), usr.Id_Perfil);
                integracaoProcesso = integracaoProcesso.Where(x => x.Nome.Equals(movimento)).ToList();
                foreach (var r in integracaoProcesso)
                {
                    id_integracao_processo = Convert.ToInt32(r.Id_Integracao_Processo);
                }

                List<Integracao> integracao = integracaoBUS.IntegracaoProcesso(id_integracao_processo);

                foreach (var r in integracao)
                {
                    permite_edicao = r.PERMITE_EDICAO;
                }
            }

            List<ImportacaoDocumento> lst = new List<ImportacaoDocumento>();

            Session["lstDocumento"] = importacaoDocumentoBUS.abrirDocumento(idMov);

            //if (permite_edicao.Equals("Não"))
            //{
            //    lstItens = new List<ImportacaoDocumento>();
            //    lstItens = importacaoDocumentoBUS.ItensPedido(idMov);
            //    lstItens.Select(c => { c.PERMITE_EDICAO = "Não"; return c; }).ToList();
            //    foreach(var r in lstItens)
            //    {
            //        r.Id_Integracao = id_integracao;
            //    }
            //    Session["lstItensImpDoc"] = lstItens;
            //}
            //else
            //{
                lstItens = new List<ImportacaoDocumento>();
                lstItens = importacaoDocumentoBUS.ItensPedido(idMov);
                lstItens.Select(c => { c.PERMITE_EDICAO = "Sim"; return c; }).ToList();
                foreach (var r in lstItens)
                {
                    r.Id_Integracao = id_integracao;
                }
                Session["lstItensImpDoc"] = lstItens;
            //}


            if(mensagem != "")
            {
                TempData["Mensagem"] = mensagem;
            }
           
            return RedirectToAction("Index", "ImportacaoDocumento", new { divShow = "#itensDocumento", id_integracao = id_integracao });
        }

        public JsonResult agendarIntegracao(int idMov2 = 0, List<string> campos = null, int id_fornecedor = 0, int id_integracao_processo = 0, int documento = 0, List<String> dados = null)
        {
            List<ImportacaoDocumento> lstItens = new List<ImportacaoDocumento>();
            
            string permite_edicao = "";
            string linha = "";
            string[] linhaSplit = null;
            string observacao = "";
            List<int> lstResultado = new List<int>();
            List<ImportacaoDocumento> integracaoValidar = new List<ImportacaoDocumento>();
            List<ImportacaoDocumento> lstIntegracaoValidar = new List<ImportacaoDocumento>();
            List<string> resultado = new List<string>();
            int iteracoes = 0;
            int idMov = 0;
            try
            {
                int tamanhoArray = 0;
                if (campos != null)
                {
                    tamanhoArray = campos.Count();
                }

                if (campos != null) {
                    resultado = campos;
                }
                else
                {
                    resultado = dados;
                }
                    List<Integracao> integracao2 = integracaoBUS.IntegracaoProcesso((id_integracao_processo));

                    foreach (var r in integracao2)
                    {
                        permite_edicao = r.PERMITE_EDICAO;
                    }

                if (Session["lstDocumento"] != null)
                {
                    lstItens = (List<ImportacaoDocumento>) Session["lstDocumento"];
                }

                if (campos != null)
                {
                    iteracoes = 1;
                }
                else {
                    iteracoes = resultado.Count();
                }

                Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
                Integracao integracao3 = new Integracao();
                //Gerando id_integração
                integracao3.Id_integracao_Processo = id_integracao_processo;
                integracao3.Id_Pessoa = usr.Id_Pessoa;
                integracao3.Tipo = "LAYOUT_IMPORTSYS";
                integracao3.Situacao = "AG";
                integracao3.Complemento = "IMPORTACAO";
                integracao3.Id_integracao = 0;
                integracao3.IdMov = idMov;
                integracaoBUS.Integracao(integracao3);

                for (var x = 0; x <= iteracoes - 1; x++)
                {
                    if(idMov2 == 0)
                    {
                        linha = resultado[x];
                        linhaSplit = linha.Split(',');
                        idMov = Convert.ToInt32(linhaSplit[11]);
                        integracao3.IdMov = idMov;
                    }
                    else
                    {
                        integracao3.IdMov = idMov2;
                        idMov = idMov2;
                    }

                    integracaoValidar = importacaoDocumentoBUS.IntegracaoParametroValidar(idMov);
                    foreach (var r in integracaoValidar)
                    {
                        observacao = r.Observacao;
                    }


                    if (observacao == "")
                    {
                        List<Integracao> integracao = integracaoBUS.IntegracaoProcesso(id_integracao_processo);

                        foreach (var r in integracao)
                        {
                            permite_edicao = r.PERMITE_EDICAO;
                        }


                        string IDCFO = "0";
                        string NSEQITMMOV = "0";
                        string IDPRD = "0";
                        string NF_ITE_NM_ADICAO = "0";
                        string NF_ITE_NM_ITEM_ADICAO = "0";
                        string serial = "";
                        string chave = "";

                        if (dados != null)
                        {

                            //for (var i = 0; i <= dados.Count() - 1; i++)
                            //{
                                linha = dados[x];
                                linhaSplit = linha.Split(',');
                                IDCFO = linhaSplit[x];
                                idMov = Convert.ToInt32(linhaSplit[11]);

                                integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, idMov, 0, Convert.ToInt32(NSEQITMMOV), Convert.ToInt32(IDPRD), serial, chave, Convert.ToInt32(NF_ITE_NM_ADICAO), Convert.ToInt32(NF_ITE_NM_ITEM_ADICAO));
                           // }
                        }
                        else
                        {
                            if (permite_edicao.Equals("Sim"))
                            {
                                //integracaoBUS.Integracao(integracao3);

                                foreach (var id in lstItens)
                                {
                                    for (var i = 0; i < campos.Count; i++)
                                    {
                                        {
                                            //integracaoBUS.Integracao(integracao3);

                                            linha = campos[i];
                                            linhaSplit = linha.Split(',');
                                            IDCFO = linhaSplit[0];
                                            NSEQITMMOV = linhaSplit[2];
                                            IDPRD = linhaSplit[0];
                                            NF_ITE_NM_ADICAO = linhaSplit[9];
                                            if (NF_ITE_NM_ADICAO == " " || NF_ITE_NM_ADICAO == "")
                                            {
                                                NF_ITE_NM_ADICAO = "0";
                                            }
                                            NF_ITE_NM_ITEM_ADICAO = linhaSplit[10];
                                            if (NF_ITE_NM_ITEM_ADICAO == " " || NF_ITE_NM_ITEM_ADICAO == "")
                                            {
                                                NF_ITE_NM_ITEM_ADICAO = "0";
                                            }
                                            serial = linhaSplit[5];
                                            if (serial == " " || serial == "")
                                            {
                                                serial = "";
                                            }
                                            chave = linhaSplit[6];
                                            if (chave == " " || chave == "")
                                            {
                                                chave = "";
                                            }
                                            integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, id.idMov, 0, Convert.ToInt32(NSEQITMMOV), Convert.ToInt32(IDPRD), serial, chave, Convert.ToInt32(NF_ITE_NM_ADICAO), Convert.ToInt32(NF_ITE_NM_ITEM_ADICAO));
                                        }
                                    }


                                }
                            }
                            else
                            {

                                integracaoBUS.Integracao(integracao3);
                                //integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, idMov, Convert.ToInt32(IDCFO), Convert.ToInt32(NSEQITMMOV), Convert.ToInt32(IDPRD), Convert.ToInt32(NF_ITE_NM_ADICAO), Convert.ToInt32(NF_ITE_NM_ITEM_ADICAO));
                                integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, idMov, 0, Convert.ToInt32(NSEQITMMOV), Convert.ToInt32(IDPRD), serial, chave, Convert.ToInt32(NF_ITE_NM_ADICAO), Convert.ToInt32(NF_ITE_NM_ITEM_ADICAO));
                            }
                        }

                        lstResultado.Add(integracao3.Id_integracao);
                        lstResultado.Add(id_fornecedor);

                        lstIntegracaoValidar.Add(new ImportacaoDocumento
                        {
                            Id_Integracao = integracao3.Id_integracao,
                            Id_Fornecedor = id_fornecedor
                        });


                        //return Json(lstResultado, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        string numeroMov = "";
                        if(dados != null)
                        {
                            linha = dados[x];
                            linhaSplit = linha.Split(',');
                            string numeroMovStr = linhaSplit[13];
                            int indiceStr = numeroMovStr.IndexOf('/');
                            numeroMov = numeroMovStr.Substring(0, indiceStr);
                        }
                        else
                        {
                            for (var i = 0; i < campos.Count; i++)
                            {
                                linha = campos[i];
                                linhaSplit = linha.Split(',');
                                numeroMov = linhaSplit[6];
                            }
                        }
                        

                                foreach (var r in integracaoValidar)
                        {
                            if(r.Observacao != null)
                            {
                                lstIntegracaoValidar.Add(new ImportacaoDocumento
                                {
                                    Id_Integracao = r.Id_Integracao,
                                    NUMEROMOV = numeroMov,
                                    Observacao = r.Observacao
                                });
                            }
                        }
                        //return Json(lstIntegracaoValidar, JsonRequestBehavior.AllowGet);
                    }

                }

                //List<ImportacaoDocumento> lstIntegracaoValidar = new List<ImportacaoDocumento>();
                //lstIntegracaoValidar = importacaoDocumentoBUS.IntegracaoParametroValidar(idMov);
                //foreach(var r in lstIntegracaoValidar)
                //{
                //    observacao = r.Observacao;
                //}


                //if(observacao == "")
                //{
                //    List<Integracao> integracao = integracaoBUS.IntegracaoProcesso(id_integracao_processo);

                //    foreach (var r in integracao)
                //    {
                //        permite_edicao = r.PERMITE_EDICAO;
                //    }

                //    Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
                //    Integracao integracao3 = new Integracao();
                //    //Gerando id_integração
                //    integracao3.Id_integracao_Processo = id_integracao_processo;
                //    integracao3.Id_Pessoa = usr.Id_Pessoa;
                //    integracao3.Tipo = "LAYOUT_IMPORTSYS";
                //    integracao3.Situacao = "AG";
                //    integracao3.Complemento = "IMPORTACAO";
                //    integracao3.Id_integracao = 0;
                //    integracao3.IdMov = idMov;

                //    string IDCFO = "0";
                //    string NSEQITMMOV = "0";
                //    string IDPRD = "0";
                //    string NF_ITE_NM_ADICAO = "0";
                //    string NF_ITE_NM_ITEM_ADICAO = "0";
                //    string serial = "";
                //    string chave = "";

                //    if (dados != null)
                //    {
                //        integracaoBUS.Integracao(integracao3);

                //        for (var i = 0; i <= dados.Count() - 1; i++)
                //        {
                //            linha = dados[i];
                //            linhaSplit = linha.Split(',');
                //            IDCFO = linhaSplit[9];
                //            idMov = Convert.ToInt32(linhaSplit[10]);

                //            integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, idMov, 0, Convert.ToInt32(NSEQITMMOV), Convert.ToInt32(IDPRD), serial, chave, Convert.ToInt32(NF_ITE_NM_ADICAO), Convert.ToInt32(NF_ITE_NM_ITEM_ADICAO));
                //        }
                //    }
                //    else
                //    {
                //        if (permite_edicao.Equals("Sim"))
                //        {
                //            integracaoBUS.Integracao(integracao3);

                //            foreach (var id in lstItens)
                //            {
                //                for (var i = 0; i < campos.Length; i++)
                //                {
                //                    {
                //                        //integracaoBUS.Integracao(integracao3);

                //                        linha = campos[i];
                //                        linhaSplit = linha.Split(',');
                //                        IDCFO = linhaSplit[0];
                //                        NSEQITMMOV = linhaSplit[2];
                //                        IDPRD = linhaSplit[0];
                //                        NF_ITE_NM_ADICAO = linhaSplit[9];
                //                        if (NF_ITE_NM_ADICAO == " " || NF_ITE_NM_ADICAO == "")
                //                        {
                //                            NF_ITE_NM_ADICAO = "0";
                //                        }
                //                        NF_ITE_NM_ITEM_ADICAO = linhaSplit[10];
                //                        if (NF_ITE_NM_ITEM_ADICAO == " " || NF_ITE_NM_ITEM_ADICAO == "")
                //                        {
                //                            NF_ITE_NM_ITEM_ADICAO = "0";
                //                        }
                //                        serial = linhaSplit[5];
                //                        if (serial == " " || serial == "")
                //                        {
                //                            serial = "";
                //                        }
                //                        chave = linhaSplit[6];
                //                        if (chave == " " || chave == "")
                //                        {
                //                            chave = "";
                //                        }
                //                        integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, id.idMov, 0, Convert.ToInt32(NSEQITMMOV), Convert.ToInt32(IDPRD), serial, chave, Convert.ToInt32(NF_ITE_NM_ADICAO), Convert.ToInt32(NF_ITE_NM_ITEM_ADICAO));
                //                    }
                //                }


                //            }
                //        }
                //        else
                //        {

                //            integracaoBUS.Integracao(integracao3);
                //            //integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, idMov, Convert.ToInt32(IDCFO), Convert.ToInt32(NSEQITMMOV), Convert.ToInt32(IDPRD), Convert.ToInt32(NF_ITE_NM_ADICAO), Convert.ToInt32(NF_ITE_NM_ITEM_ADICAO));
                //            integracaoBUS.IntegracaoParametroBUS("LAYOUT_IMPORTSYS", integracao3.Id_integracao, idMov, 0, Convert.ToInt32(NSEQITMMOV), Convert.ToInt32(IDPRD), serial, chave, Convert.ToInt32(NF_ITE_NM_ADICAO), Convert.ToInt32(NF_ITE_NM_ITEM_ADICAO));
                //        }
                //    }

                //    List<int> lstResultado = new List<int>();
                //    lstResultado.Add(integracao3.Id_integracao);
                //    lstResultado.Add(id_fornecedor);

                //    return Json(lstResultado, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(lstIntegracaoValidar, JsonRequestBehavior.AllowGet);
                //}
                return Json(lstIntegracaoValidar, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
                //return new HttpStatusCodeResult(500, e.Message);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Informativo(int idMov = 0)
        {
            List<ImportacaoDocumento> lstDocumento = new List<ImportacaoDocumento>();
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