using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Entities;
using Business;
using System.IO;
using ClosedXML.Excel;
using System.Data;
using System.Globalization;

namespace TAMINTEGRA.Controllers
{
    public class FinanceiroProFormaController : Controller
    {
        private FinanceiroServicos financeiroServico  = new FinanceiroServicos();
        private ImportacaoDocumentoBUS importacaoDocumentoBUS = new ImportacaoDocumentoBUS();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private FinanceiroServicosBUS financeiroServicosBUS = new FinanceiroServicosBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();
        bool exporta_excel = false;

        [CustomAuthorize(Roles = "frmFinanceiroProForma")]
        public ActionResult Index(string modal = null, string modalInformativo = null, string situacao = null, string exibir = null, string strDataInicio = null, string strDataFim = null, string codTMV = null, string numeroMov = null, string invoice = null,string documento = null, int id_integracao = 0)
        {
            CarregarDados();

            List<FinanceiroServicos> lstItens = new List<FinanceiroServicos>();
            DateTime dataInicioDT = new DateTime();
            DateTime dataTerminoDT = new DateTime();

            if (strDataInicio == null || strDataInicio == "")
            {
                //var dia = DateTime.Today.AddDays(-10);
                strDataInicio = DateTime.Today.ToString("dd/MM/yyyy");
               // dataInicioDT = dia;
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

            financeiroServico.strDataInicio = strDataInicio;
            financeiroServico.strDataFim = strDataFim;

            if (id_integracao > 0)
            {
                ViewBag.Id_Integracao_Agendamento = id_integracao;
                lstItens = financeiroServicosBUS.Filtro(dataInicioDT, dataTerminoDT, codTMV, numeroMov, invoice, documento, situacao, id_integracao);
                
                if (lstItens != null)
                {
                    Session["lstItensFin"] = lstItens;
                }
                else
                {
                    Session["lstItensFin"] = null;
                }
            }

            else if (exibir != null)
            {
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

                
               lstItens = financeiroServicosBUS.Filtro(dataInicioDT, dataTerminoDT, codTMV, numeroMov, invoice, documento, situacao, 0);
                
                //lstItens.Add(new FinanceiroServicos
                //{
                //    MOEDA = "E",
                //    Id_Integracao = 1,
                //    Data = new DateTime(2019, 06, 12),
                //    Documento = "Att. Pista",
                //    VALORORIGINAL = 100
                //});
                if (lstItens != null)
                {
                    Session["lstItensFin"] = lstItens;
                }
                else
                {
                    Session["lstItensFin"] = null;
                }
            }

            //-------------------------------------------------//

            //-------------------------------------------------//

            if(lstItens != null)
            {
                //foreach (var id in lstItens)
                //{
                //    if (id.Id_Integracao > 0)
                //    {
                //        ViewBag.Id_integracao = id.Id_Integracao;
                //    }
                //}

                ViewBag.Id_Integracao = Convert.ToInt32(lstItens.Count(x => x.Id_Integracao == 0));

            }



            var lstInforme = TempData["lstInforme"] as List<FinanceiroServicos>;
            if (lstInforme != null)
            {
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


                financeiroServico.lstInforme = lstInforme;
            }

            var lstReprocessamento = TempData["lstReprocessamento"] as List<FinanceiroServicos>;
            if (lstReprocessamento != null)
            {
                financeiroServico.lstReprocessamento = lstReprocessamento;
            }

            financeiroServico.lstItens = lstItens;


            return View(financeiroServico);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public JsonResult Informativo(int id_integracao = 0, int idMov = 0)
        {
            List<FinanceiroServicos> lstInforme = new List<FinanceiroServicos>();
            //lstInforme = (List<FinanceiroServicos>)Session["lstItensFin"];
            //lstInforme = lstInforme.Where(x => x.Documento == documento).ToList();
            //string mensagem = ""; 
            //lstInforme.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            lstInforme = financeiroServicosBUS.Informativo(id_integracao, idMov);

            //TempData["lstInforme"] = lstInforme.ToList();

            //lstInforme = cavokBUS.Informe(id_integracao, id_fatura);
            string emissao = "";
            string vencimento = "";
            string dt_enmissao_doc = "";
            string dt_transacao = "";
            string dt_contabil = "";

            try
            {
                foreach (var r in lstInforme)
                {
                    emissao = r.DATAEMISSAO == DateTime.MinValue ? "" : r.DATAEMISSAO.ToShortDateString();
                    vencimento = r.DATAVENCIMENTO == DateTime.MinValue ? "" : r.DATAVENCIMENTO.ToShortDateString();
                    if (r.DT_EMISSAO_DOC != null)
                    {
                        dt_enmissao_doc = r.DT_EMISSAO_DOC.Value.ToString("d");
                    }
                   if(r.DT_TRANSACAO != null)
                    {
                        dt_transacao = r.DT_TRANSACAO.Value.ToString("d");
                    }
                    if(r.DT_CONTABIL != null)
                    {
                        dt_contabil = r.DT_CONTABIL.Value.ToString("d");
                    }
                   
                }
            }
            catch(Exception e)
            {
                var erro = e.Message;
            }
            
            var resultado = (from info in lstInforme
                             select new
                             {
                                 Documento = info.Documento,
                                 Filial= info.FILIAL,
                                 Invoice = info.INVOICE,
                                 Titulo = info.TITULO,
                                 Emissao = emissao,
                                 Vencimento = vencimento,
                                 Valor = info.VALORORIGINAL,
                                 Situacao = info.Status,
                                 CC = info.CCUSTO,
                                 OS = info.ORDEM_SERVICO,
                                 Moeda = info.MOEDA,
                                 Historico = info.HISTORICOLONGO,
                                 Fornecedor = info.FORNECEDOR,
                                 Id_lancamento = info.ID_LANCAMENTO,
                                 Dt_emissao_doc = dt_enmissao_doc,
                                 Dt_transacao = dt_transacao,
                                 dt_contabil = dt_contabil,
                                 Taxa = info.TAXA_CONVERSAO,
                                 Valor_me = info.VALOR_ME_RM,
                                 Valor_mn = info.VALOR_MN_RM,
                                 Texto = info.TEXTO_COMPLEMENTAR,
                                 Cor = info.COR,
                                 Observacao = info.OBSERVACAO
                             }).ToList();


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reprocessar(int id_integracao = 0, int idMov = 0, int idLan = 0)
        {
            CarregarDados();
            int id_pessoa = ViewBag.IdPessoa;
            List<FinanceiroServicos> lstReprocessamento = new List<FinanceiroServicos>();
            try
            {
                lstReprocessamento = financeiroServicosBUS.Reprocessar(id_integracao, idMov, idLan, id_pessoa);
        
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Id_integracao = id_integracao,
                                     Comentario = info.Comentario,
                                     Erro = info.OBSERVACAO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Comentario = e.Message,
                                     Erro = "Erro"
                                     
                                 }).ToList();

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Excluir(int id_integracao = 0, int idMov = 0, int idLan = 0)
        {
            CarregarDados();
            int id_pessoa = ViewBag.IdPessoa;
            List<FinanceiroServicos> lstReprocessamento = new List<FinanceiroServicos>();
            try
            {
                lstReprocessamento = financeiroServicosBUS.Excluir(id_integracao, idMov, idLan, id_pessoa);

                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Id_integracao = id_integracao,
                                     Comentario = info.Comentario,
                                     Erro = info.OBSERVACAO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     Comentario = e.Message,
                                     Erro = "Erro"

                                 }).ToList();

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult agendarIntegracao(string[] dados = null)
        {
            List<ImportacaoDocumento> lstItens = new List<ImportacaoDocumento>();
            int id_integracao_processo = 0;
            int idMov = 0;
            int idLan = 0;
            string formulario = "";
            int id_forn = 0;

            try
            {
                //if (Session["lstDocumento"] != null)
                //{
                //    lstItens = (List<ImportacaoDocumento>)Session["lstDocumento"];
                //}

                List<UsuarioPerfilModulo> usuarioPerfil = usuarioBUS.BuscaPorIdPerfilFormulariosList(Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Formulario == "frmFinanceiroServico").ToList();
                foreach (var usrPerfil in usuarioPerfil)
                {
                    formulario = usrPerfil.Formulario;
                    id_integracao_processo = usrPerfil.Id_Integracao_Processo;
                    id_forn = usrPerfil.Id_Fornecedor;
                }

                string linha = "";
                string[] linhaSplit = null;

                Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
                //Integracao integracao = integracaoBUS.IntegracaoDEBUS(0, Convert.ToInt32(idIntegracaoProcesso), "EDIPOP", "Depósito Especial", "AG", "", idPessoa);
                Integracao integracao3 = new Integracao();
                //Gerando id_integração



                //integracao3.Id_integracao_Processo = financeiroServicosBUS.Integracao_Processo("frmFinanceiroServico");
                integracao3.Id_integracao_Processo = id_integracao_processo;
                integracao3.Id_Pessoa = usr.Id_Pessoa;
                integracao3.Tipo = "LAYOUT_CAMBIOSYS";
                integracao3.Situacao = "AG";
                integracao3.Complemento = "FINANCEIRO";
                integracao3.Id_integracao = 0;
                integracao3.IdMov = idMov;
                integracaoBUS.Integracao(integracao3);


                for (var i = 0; i < dados.Length; i++)
                {
                    linha = dados[i];
                    linhaSplit = linha.Split(',');
                    idMov = Convert.ToInt32(linhaSplit[0]);
                    idLan = Convert.ToInt32(linhaSplit[1]);

                    integracaoBUS.IntegracaoParametroBUS(integracao3.Tipo, integracao3.Id_integracao, idMov, 0, 0, 0, "", "", 0, 0, "", idLan);
                }



                return Json(integracao3.Id_integracao, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //TempData["Mensagem"] = e.Message;
                //return new HttpStatusCodeResult(500, e.Message);
                List<string> resultado = new List<string>();

                resultado.Add("Erro");
                resultado.Add(e.Message);


                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult DownloadExcel()
        {
            string arquivo = "FinanceiroServicos";
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // Start a new workbook
                    var wb = new XLWorkbook();

                    List<FinanceiroServicos> lstServico = new List<FinanceiroServicos>();

                    lstServico = (List<FinanceiroServicos>) Session["lstItensFin"];
                    IEnumerable<FinanceiroServicos> result = lstServico;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Documento");
                    dt.Columns.Add("Situação da Integração");
                    dt.Columns.Add("Invoice");
                    dt.Columns.Add("Vencimento");
                    dt.Columns.Add("Fornecedor");
                    dt.Columns.Add("Moeda");
                    dt.Columns.Add("Valor");
                    dt.Columns.Add("Centro de Custo");
                    dt.Columns.Add("Doc Origem");
                    dt.Columns.Add("Nome Fantasia");
                    dt.Columns.Add("Ordem Serviço");
                    dt.Columns.Add("Título");
                    dt.Columns.Add("Data Emissão");
                    dt.Columns.Add("Observação");
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Cod TMV");
                    dt.Columns.Add("Status Pedido RM");
                    dt.Columns.Add("Aprovação");
                    dt.Columns.Add("Status Lançamento");
                    dt.Columns.Add("Filial");
                    dt.Columns.Add("ID Mov");
                    dt.Columns.Add("ID Lan");

                    foreach (var servico in result)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Documento"] = servico.Documento;

                        if(servico.Id_Integracao > 0)
                        {
                            dr["Situação da Integração"] = servico.Situacao + " - " + servico.Id_Integracao;
                        }
                        else
                        {
                            dr["Situação da Integração"] = servico.Situacao;
                        }
                      
                        dr["Invoice"] = servico.INVOICE;

                        if (servico.DATAVENCIMENTO != null)
                        {
                            DateTime? vencTemp = servico.DATAVENCIMENTO;
                            string vencFormatada = vencTemp.Value.ToString("dd/MM/yyyy");
                            dr["Vencimento"] = vencFormatada;
                        }
                        else
                        {
                            dr["Vencimento"] = servico.DATAVENCIMENTO;
                        }

                        dr["Fornecedor"] = servico.FORNECEDOR;
                        dr["Moeda"] = servico.MOEDA;
                        dr["Valor"] = string.Format("{0:0,0.00}", servico.VALORORIGINAL);
                        dr["Centro de Custo"] = servico.CCUSTO;
                        dr["Doc Origem"] = servico.DOC_ORIGEM;
                        dr["Nome Fantasia"] = servico.NOMEFANTASIA;
                        dr["Ordem Serviço"] = servico.ORDEM_SERVICO;
                        dr["Título"] = servico.TITULO;

                        if (servico.DATAEMISSAO != null)
                        {
                            DateTime? emissaoTemp = servico.DATAEMISSAO;
                            string emissaoFormatada = emissaoTemp.Value.ToString("dd/MM/yyyy");
                            dr["Data Emissão"] = emissaoFormatada;
                        }
                        else
                        {
                            dr["Vencimento"] = servico.DATAEMISSAO;
                        }

                        dr["Observação"] = servico.OBSERVACAO;
                        dr["Status"] = servico.Status;
                        dr["Cod TMV"] = servico.CODTMV;
                        dr["Status Pedido RM"] = servico.STATUS_PEDIDO_RM;
                        dr["Aprovação"] = servico.APROVACAO;
                        dr["Status Lançamento"] = servico.STATUSLANCAMENTO;
                        dr["Filial"] = servico.FILIAL;
                        dr["ID Mov"] = servico.IDMOV;
                        dr["ID Lan"] = servico.IDLAN;

                        dt.Rows.Add(dr);
                    }



                    // Add a DataTable as a worksheet
                    wb.Worksheets.Add(dt, arquivo);

                    wb.SaveAs(stream, false);

                    // Return a byte array
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Financeiro_Serviços" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                    //return File(@"C:\Users\itala.cordeiro\Downloads", "application /text", "teste" + ".xlsx");
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void CarregarDados()
        {
            exporta_excel = false;
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            exporta_excel = usuarioBUS.BuscaPorLogin(usr.Login).Exporta_Excel;
            ViewBag.ExportaExcel = exporta_excel;
            ViewBag.Situacao = new SelectList(situacaoBUS.ListatBUS(), "CODSITUACAO", "SITUACAODESC");
            ViewBag.TipoFaturamento = new SelectList(financeiroServicosBUS.ListaTipoFaturamento(), "CODTMV", "CODTMV");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            ViewBag.IdPessoa = usr.Id_Pessoa;
        }
    }
}