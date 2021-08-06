using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Entities;
using Business;
using System.Globalization;
using System.IO;
using ClosedXML.Excel;
using System.Data;

namespace TAMINTEGRA.Controllers
{
    public class ContabilizacaoVariacaoCambialController : Controller
    {
        private ContabilizacaoVariacaoCambial variacaoCambial = new ContabilizacaoVariacaoCambial();
        private ContabilizacaoVariacaoCambialBUS vcBUS = new ContabilizacaoVariacaoCambialBUS();
        private IntegracaoBUS integracaoBUS = new IntegracaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();


        [CustomAuthorize(Roles = "frmContabilizacaoVC")]
        public ActionResult Index(int ano = 0, int mes = 0, string codProcesso = "", string invoice = "", string tipo = "", string filtrar = "", string classificacao = "")
        {
            //CarregaDados(fornecedorNome, documento);
            List<ContabilizacaoVariacaoCambial> lstVariacaoCambial = new List<ContabilizacaoVariacaoCambial>();
            List<ContabilizacaoVariacaoCambial> lstTipo = new List<ContabilizacaoVariacaoCambial>();



            string mesCorrente = "";
            string anoCorrente = "";

            mesCorrente = DateTime.Today.ToString("MM");
            anoCorrente = DateTime.Today.ToString("yyyy");

            //if ((movimento != null & numeroMovimentoFiltro != null) || codigoFiltro != null)
            //{
            //    lstImportacaoProduto = importacaoProdutoBUS.ListaProduto(id_integracao, movimento, numeroMovimentoFiltro, codigoFiltro);
            //}

            if(filtrar != "")
            {
                //lstVariacaoCambial.Add(new ContabilizacaoVariacaoCambial
                //{
                //    Titulo = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
                //});
                //lstVariacaoCambial.Add(new ContabilizacaoVariacaoCambial
                //{
                //    Titulo = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
                //});
                //lstVariacaoCambial.Add(new ContabilizacaoVariacaoCambial
                //{
                //    Titulo = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
                //});
                //lstVariacaoCambial.Add(new ContabilizacaoVariacaoCambial
                //{
                //    Titulo = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
                //});
                //lstVariacaoCambial.Add(new ContabilizacaoVariacaoCambial
                //{
                //    Titulo = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
                //});
                //lstVariacaoCambial.Add(new ContabilizacaoVariacaoCambial
                //{
                //    Titulo = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
                //});

                lstVariacaoCambial = vcBUS.Filtro(ano, mes,codProcesso, invoice, tipo, classificacao);
                if(lstVariacaoCambial != null)
                {
                    Session["lstItens"] = lstVariacaoCambial;
                    var valor = lstVariacaoCambial.Sum(t => t.Valor_MN_RM ?? 0);
                    ViewBag.Valor = valor;
                    Session["Valor"] = valor;
                }
                
            }




            variacaoCambial.Mes = mes == 0 ? Convert.ToInt32(mesCorrente) : mes;
            variacaoCambial.Ano = ano == 0 ? Convert.ToInt32(anoCorrente) : ano;
            variacaoCambial.Tipo = tipo;
            variacaoCambial.Classificacao = classificacao;

            variacaoCambial.lstVariacaoCambial = lstVariacaoCambial;


            List<UsuarioPerfilModulo> usuarioModulo = usuarioBUS.BuscaPorIdPerfilFormulariosList(Convert.ToInt32(Session["Id_Perfil"])).Where(x => x.Formulario == "frmImportacaoProduto").ToList();
           
 return View(variacaoCambial);
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
        }

        public JsonResult Reprocessar(string[] dados = null)
        {
            List<ContabilizacaoVariacaoCambial> lstVC = new List<ContabilizacaoVariacaoCambial>();
            string linha = "";
            string[] linhaSplit = null;
            int id_integracao = 0;
            string id_lancamento = "";

            for (int i =0; i<= dados.Length; i++)
            {
                linha = dados[i];
                linhaSplit = linha.Split(',');
                id_integracao = Convert.ToInt32(linhaSplit[0]);
                id_lancamento = linhaSplit[1];
                lstVC = vcBUS.Reprocessar(id_integracao, id_lancamento);
            }

            if (lstVC != null)
            {
                var resultado = (from info in lstVC
                                 select new
                                 {
                                     //Id_Integracao = info.Id_Integracao,
                                     //Observacao = info.OBSERVACAO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Excluir(string[] dados = null)
        {
            List<ContabilizacaoVariacaoCambial> lstVC = new List<ContabilizacaoVariacaoCambial>();
            string linha = "";
            string[] linhaSplit = null;
            int id_integracao = 0;
            string id_lancamento = "";

            for (int i = 0; i <= dados.Length; i++)
            {
                linha = dados[i];
                linhaSplit = linha.Split(',');
                id_integracao = Convert.ToInt32(linhaSplit[0]);
                id_lancamento = linhaSplit[1];
                lstVC = vcBUS.Excluir(id_integracao, id_lancamento);
            }

            if (lstVC != null)
            {
                var resultado = (from info in lstVC
                                 select new
                                 {
                                     //Id_Integracao = info.Id_Integracao,
                                     //Observacao = info.OBSERVACAO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }


        public FileResult DownloadExcel()
        {
            List<FinanceiroGuiaDeclaracao> lstInformeDeclaracao = new List<FinanceiroGuiaDeclaracao>();

            try
            {
                List<ContabilizacaoVariacaoCambial> lstVC = (List<ContabilizacaoVariacaoCambial>)Session["lstItens"];

                using (MemoryStream stream = new MemoryStream())
                {
                    var wb = new XLWorkbook();
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Cód. Processo");
                    dt.Columns.Add("Invoice");
                    dt.Columns.Add("Moeda");
                    dt.Columns.Add("Valor MN RM");
                    dt.Columns.Add("Sinal");
                    dt.Columns.Add("Conta Contábil 1");
                    dt.Columns.Add("Conta Contrapartida");
                    dt.Columns.Add("Dt. Contábil");
                    dt.Columns.Add("Dt. Emissão Doc");
                    dt.Columns.Add("Dt. Transação");
                    dt.Columns.Add("Tipo");
                    dt.Columns.Add("Classificação");
                    dt.Columns.Add("Cód. Parceiro");
                    dt.Columns.Add("Mvto RM");
                    dt.Columns.Add("Histórico");

                    foreach (var vc in lstVC)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Cód. Processo"] = vc.Cod_Processo;
                        dr["Invoice"] = vc.Invoice;
                        dr["Moeda"] = vc.Moeda;
                        dr["Valor MN RM"] = string.Format("{0:0,0.000}", vc.Valor_MN_RM); ;
                        dr["Sinal"] = vc.Sinal;
                        dr["Conta Contábil 1"] = vc.Conta_Contabil1;
                        dr["Conta Contrapartida"] = vc.Conta_Contrapartida;

                        if (vc.Dt_Contabil != null)
                        {
                            DateTime? str_Dt_Contabil = vc.Dt_Contabil;
                            string fmt_str_Dt_Contabil = str_Dt_Contabil.Value.ToString("dd/MM/yyyy");
                            dr["Dt. Contábil"] = fmt_str_Dt_Contabil;
                        }
                        else
                        {
                            dr["Dt. Contábil"] = vc.Dt_Contabil;
                        }

                        if (vc.Dt_Emissao_Doc != null)
                        {
                            DateTime? str_Dt_Emissao_Doc = vc.Dt_Emissao_Doc;
                            string fmt_str_Dt_Emissao_Doc = str_Dt_Emissao_Doc.Value.ToString("dd/MM/yyyy");
                            dr["Dt. Emissão Doc"] = fmt_str_Dt_Emissao_Doc;
                        }
                        else
                        {
                            dr["Dt. Emissão Doc"] = vc.Dt_Emissao_Doc;
                        }

                        if (vc.Dt_Transacao != null)
                        {
                            DateTime? str_Dt_Transacao = vc.Dt_Transacao;
                            string fmt_str_Dt_Transacao = str_Dt_Transacao.Value.ToString("dd/MM/yyyy");
                            dr["Dt. Transação"] = fmt_str_Dt_Transacao;
                        }
                        else
                        {
                            dr["Dt. Transação"] = vc.Dt_Transacao;
                        }

                        dr["Tipo"] = vc.Tipo;
                        dr["Classificação"] = vc.Classificacao;
                        dr["Cód. Parceiro"] = vc.Cod_Parceiro;
                        dr["Mvto RM"] = vc.Mvto_RM;
                        dr["Histórico"] = vc.Historico;

                        dt.Rows.Add(dr);

                    }

                    // Add a DataTable as a worksheet

                    var WsVC = wb.Worksheets.Add(dt, "Variação Cambial");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr2 = dt.Rows[i];
                        if (dr2["Valor MN RM"] != null)
                        {

                            WsVC.Cell(i + 2, 4).Value = dr2["Valor MN RM"].ToString().Replace(".", "").Replace(",", ".");
                            WsVC.Cell(i + 2, 4).Style.NumberFormat.Format = "#,##0.00";
                        }
                    }

                     WsVC.Cell(dt.Rows.Count + 2, 4).Value = Session["Valor"].ToString().Replace(".", "").Replace(",", ".");
                     WsVC.Cell(dt.Rows.Count + 2, 4).Style.NumberFormat.Format = "#,##0.00";
                   
                  

                    wb.SaveAs(stream, false);

                    // Return a byte array
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TAMIntegra_Processo_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                    //return File(@"C:\Users\itala.cordeiro\Downloads", "application /text", "teste" + ".xlsx");


                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }



}