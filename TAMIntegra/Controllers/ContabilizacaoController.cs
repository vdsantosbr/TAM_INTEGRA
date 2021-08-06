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
    public class ContabilizacaoController : Controller
    {
        private Contabilizacao cont = new Contabilizacao();
        private ContabilizacaoBUS contBUS = new ContabilizacaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        PerfilBUS perfilBUS = new PerfilBUS();

        [CustomAuthorize(Roles = "frmContabilizacaoVC")]
        public ActionResult Index(string exibir = "", int ano = 0, int mes = 0)
        {
            //ano = DateTime.Now.Year;
            int id_integracao = 0;
            int id_fechamento = 0;
            string situacao = "";
            string exibir_formatar = "";
            string exibir_integrar = "";
            string exibir_excluir = "";
            int anoFechamento = 0;
            int mesFechamento = 0;

            Usuario usr = usuarioBUS.BuscaPorLogin(Session["login"].ToString());
            List<Perfil> lstPerfis = perfilBUS.Parametrizacao(usr.Id_Perfil).Where(x => x.Formulario == "frmContabilizacaoVC").ToList();
            foreach (var r in lstPerfis)
            {
                ViewBag.PermitirConsultar = r.Permitir_Consultar;
                ViewBag.PermitirEditar = r.Permitir_Editar;
                ViewBag.PermitirExportar = r.Permitir_Exportar;
            }


            List<Contabilizacao> lstGRid = new List<Contabilizacao>();
            List<Contabilizacao> lstRegistrosContabilizacao = new List<Contabilizacao>();
            List<Contabilizacao> lstRegistrosInconsistentes = new List<Contabilizacao>();
            List<Contabilizacao> lstRegistrosErro = new List<Contabilizacao>();
            Contabilizacao anoAtual = new Contabilizacao();
            List<Contabilizacao> lstAno= new List<Contabilizacao>();
            List<Contabilizacao> lstMes = new List<Contabilizacao>();

            lstGRid = contBUS.Exibir(ano, mes);
            foreach (var r in lstGRid)
            {
                id_integracao = r.Id_Integracao;
                id_fechamento = r.Id_Fechamento;
                situacao = r.Situacao;
                exibir_excluir = r.EXIBIR_EXCLUIR_CONTABILIZACAO;
                exibir_formatar = r.EXIBIR_FORMATAR_CONTABILIZACAO;
                exibir_integrar = r.EXIBIR_INTEGRAR_CONTABILIZACAO;
                anoFechamento = r.Ano;
                mesFechamento = r.Mes;
            }
            lstRegistrosContabilizacao = contBUS.GridContabilizacaoFiltro(id_integracao, "", "", "", "", "", "", "", "");
            lstRegistrosInconsistentes = contBUS.GridRegistroInconsistente(id_integracao, id_fechamento);
            lstRegistrosErro = contBUS.GridRegistrosErro(id_integracao, id_fechamento);
            lstAno = contBUS.FechamentoAno();
            ViewBag.Ano = new SelectList(lstAno, "Ano", "Ano");
            foreach(var r in lstAno)
            {
                ano = r.Ano;
            }
            lstMes = contBUS.FechamentoMes(ano);
            ViewBag.Mes = new SelectList(lstMes, "Mes", "Descricao");

            cont.lstFechamentoAtual = lstGRid;
            cont.lstRegistrosContabilizacao = lstRegistrosContabilizacao;
            cont.lstRegistrosInconsistentes = lstRegistrosInconsistentes;
            cont.lstRegistrosErro = lstRegistrosErro;
            cont.Ano = anoFechamento;
            cont.Mes = mesFechamento;

            Session["lstRegistrosInconsistentes"] = lstRegistrosInconsistentes;
            Session["lstRegistrosErro"] = lstRegistrosErro;
            Session["Situação"] = situacao;
            Session["Exibir Integrar"] = exibir_integrar; ;
            Session["Exibir Excluir"] = exibir_excluir;
            Session["Exibir Formatar"] = exibir_formatar;

            return View(cont);
        }
        public ActionResult FormatarContabilizacao()
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            List<Contabilizacao> lstResultado = new List<Contabilizacao>();

            int id_fechamento = 0;

            lst = contBUS.Exibir();
            foreach (var r in lst)
            {
                id_fechamento = r.Id_Fechamento;
            }

            lstResultado = contBUS.FormatarContabilizacao(id_fechamento);

            if (lstResultado != null)
            {
                return Json(new { Resultado = lstResultado }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult IntegrarContabilizacao()
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            List<Contabilizacao> lstResultado = new List<Contabilizacao>();

            int id_fechamento = 0;

            lst = contBUS.Exibir();
            foreach (var r in lst)
            {
                id_fechamento = r.Id_Fechamento;
            }

            lstResultado = contBUS.IntegrarrContabilizacao(id_fechamento);

            if (lstResultado != null)
            {
                return Json(new { Resultado = lstResultado }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult ExcluirContabilizacao()
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            List<Contabilizacao> lstResultado = new List<Contabilizacao>();

            int id_fechamento = 0;

            lst = contBUS.Exibir();
            foreach (var r in lst)
            {
                id_fechamento = r.Id_Fechamento;
            }

            lstResultado = contBUS.ExcluirContabilizacao(id_fechamento);

            if (lstResultado != null)
            {
                return Json(new { Resultado = lstResultado }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public FileResult ExportarInconsistentes()
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    var wb = new ClosedXML.Excel.XLWorkbook();

                    IEnumerable<Contabilizacao> result = new List<Contabilizacao>();

                    result = (IEnumerable<Contabilizacao>)Session["lstRegistrosInconsistentes"];

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID LANÇAMENTO");
                    dt.Columns.Add("ID ENTIDADE");
                    dt.Columns.Add("REFERÊNCIA1");
                    dt.Columns.Add("FLEX5");
                    dt.Columns.Add("VALOR MN");
                    dt.Columns.Add("COMENTÁRIO");

                    foreach (var item in result)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID LANÇAMENTO"] = item.ID_LANCAMENTO;
                        dr["ID ENTIDADE"] = item.ID_ENTIDADE;
                        dr["REFERÊNCIA1"] = item.REFERENCIA1;                       
                        dr["FLEX5"] = item.FLEX5;
                        dr["VALOR MN"] = item.VALOR_MN;
                        dr["COMENTÁRIO"] = item.COMENTARIO;

                        dt.Rows.Add(dr);
                    }

                    var WsDados = wb.Worksheets.Add(dt, "Extração");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        WsDados.Cell(i + 2, 5).Value = dr["VALOR MN"].ToString().Replace(".", "").Replace(",", ".");
                        WsDados.Cell(i + 2, 5).Style.NumberFormat.Format = "#,##0.00";

                    }
                    WsDados.Columns().AdjustToContents();
                    wb.SaveAs(stream, false);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Extração_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public FileResult ExportarErros()
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    var wb = new ClosedXML.Excel.XLWorkbook();

                    IEnumerable<Contabilizacao> result = new List<Contabilizacao>();

                    result = (IEnumerable<Contabilizacao>)Session["lstRegistrosErro"];

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID LANÇAMENTO");
                    dt.Columns.Add("ID ENTIDADE");
                    dt.Columns.Add("REFERÊNCIA1");
                    dt.Columns.Add("FLEX5");
                    dt.Columns.Add("VALOR MN");
                    dt.Columns.Add("COMENTÁRIO");

                    foreach (var item in result)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID LANÇAMENTO"] = item.ID_LANCAMENTO;
                        dr["ID ENTIDADE"] = item.ID_ENTIDADE;
                        dr["REFERÊNCIA1"] = item.REFERENCIA1;
                        dr["FLEX5"] = item.FLEX5;
                        dr["VALOR MN"] = item.VALOR_MN;
                        dr["COMENTÁRIO"] = item.COMENTARIO;

                        dt.Rows.Add(dr);
                    }

                    var WsDados = wb.Worksheets.Add(dt, "Extração");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        WsDados.Cell(i + 2, 5).Value = dr["VALOR MN"].ToString().Replace(".", "").Replace(",", ".");
                        WsDados.Cell(i + 2, 5).Style.NumberFormat.Format = "#,##0.00";

                    }
                    WsDados.Columns().AdjustToContents();
                    wb.SaveAs(stream, false);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Extração_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public FileResult ExportarContabilizacao()
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    var wb = new ClosedXML.Excel.XLWorkbook();

                    IEnumerable<Contabilizacao> result = new List<Contabilizacao>();
                    List<Contabilizacao> lstFechamento = new List<Contabilizacao>();
                    int ano = 0;
                    int mes = 0;

                    lstFechamento = contBUS.Exibir();
                    foreach (var r in lstFechamento)
                    {
                        ano = r.Ano;
                        mes = r.Mes;
                    }

                    result = contBUS.ExportarContabilizacao(ano, mes);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("FECHAMENTO");
                    dt.Columns.Add("SITUAÇÃO");
                    dt.Columns.Add("VALIDAÇÃO");
                    dt.Columns.Add("VALIDAÇÃO COMPLEMENTO");
                    dt.Columns.Add("ANO");
                    dt.Columns.Add("MÊS");
                    dt.Columns.Add("GRUPO");
                    dt.Columns.Add("QUALIFICAÇÃO");
                    dt.Columns.Add("ID INTEGRAÇÃO");
                    dt.Columns.Add("ID LANÇAMENTO");
                    dt.Columns.Add("DT CONTÁBIL");
                    dt.Columns.Add("DT EMISSÃO DOC");
                    dt.Columns.Add("DT TRANSAÇÃO");
                    dt.Columns.Add("CHAVE LANÇAMENTO");
                    dt.Columns.Add("DT CONTABILIZAÇÃO");
                    dt.Columns.Add("CONTA CRÉDITO");
                    dt.Columns.Add("CONTA DÉBITO");
                    dt.Columns.Add("REFERÊNCIA1");
                    dt.Columns.Add("VALOR");
                    dt.Columns.Add("RECFIN COD FILIAL");
                    dt.Columns.Add("RECFIN COD CFO");
                    dt.Columns.Add("RECFIN CODTMV");
                    dt.Columns.Add("RECFIN NUMERO MOV");
                    dt.Columns.Add("COD PROCESSO");
                    dt.Columns.Add("HISTÓRICO");

                    foreach (var item in result)
                    {
                        DataRow dr = dt.NewRow();
                        dr["FECHAMENTO"] = item.Fechamento;
                        dr["SITUAÇÃO"] = item.Situacao;
                        dr["VALIDAÇÃO"] = item.VALIDACAO;
                        dr["VALIDAÇÃO COMPLEMENTO"] = item.VALIDACAO_COMPLEMENTO;
                        dr["ANO"] = item.Ano;
                        dr["MÊS"] = item.Mes;
                        dr["GRUPO"] = item.GRUPO;
                        dr["QUALIFICAÇÃO"] = item.Qualificacao;
                        dr["ID INTEGRAÇÃO"] = item.Id_Integracao;
                        dr["ID LANÇAMENTO"] = item.ID_LANCAMENTO;
                        if (item.DT_CONTABIL != null)
                        {
                            DateTime? str_DT_CONTABIL = item.DT_CONTABIL;
                            string fmt_DT_CONTABIL = str_DT_CONTABIL.Value.ToString("dd/MM/yyyy");
                            dr["DT CONTÁBIL"] = fmt_DT_CONTABIL;
                        }
                        else
                        {
                            dr["DT CONTÁBIL"] = item.DT_CONTABIL;
                        }
                        dr["DT EMISSÃO DOC"] = item.DT_EMISSAO_DOC;
                        if (item.DT_EMISSAO_DOC != null)
                        {
                            DateTime? str_DT_EMISSAO_DOC = item.DT_EMISSAO_DOC;
                            string fmt_DT_EMISSAO_DOC = str_DT_EMISSAO_DOC.Value.ToString("dd/MM/yyyy");
                            dr["DT EMISSÃO DOC"] = fmt_DT_EMISSAO_DOC;
                        }
                        else
                        {
                            dr["DT EMISSÃO DOC"] = item.DT_EMISSAO_DOC;
                        }
                        if (item.DT_TRANSACAO != null)
                        {
                            DateTime? str_DT_TRANSACAO = item.DT_TRANSACAO;
                            string fmt_DT_TRANSACAO = str_DT_TRANSACAO.Value.ToString("dd/MM/yyyy");
                            dr["DT TRANSAÇÃO"] = fmt_DT_TRANSACAO;
                        }
                        else
                        {
                            dr["DT TRANSAÇÃO"] = item.DT_TRANSACAO;
                        }
                        dr["CHAVE LANÇAMENTO"] = item.CHAVE_LANCAMENTO;
                        if (item.Data_contabilizacao != null)
                        {
                            DateTime? str_Data_contabilizacao = item.Data_contabilizacao;
                            string fmt_Data_contabilizacao = str_Data_contabilizacao.Value.ToString("dd/MM/yyyy");
                            dr["DT CONTABILIZAÇÃO"] = fmt_Data_contabilizacao;
                        }
                        else
                        {
                            dr["DT CONTABILIZAÇÃO"] = item.Data_contabilizacao;
                        }
                        dr["CONTA CRÉDITO"] = item.CONTA_CREDITO;
                        dr["CONTA DÉBITO"] = item.CONTA_DEBITO;
                        dr["REFERÊNCIA1"] = item.REFERENCIA1;
                        dr["VALOR"] = item.VALOR;
                        dr["RECFIN COD FILIAL"] = item.RECFIN_CODFILIAL;
                        dr["RECFIN COD CFO"] = item.RECFIN_CODCFO;
                        dr["RECFIN CODTMV"] = item.RECFIN_CODTMV;
                        dr["RECFIN NUMERO MOV"] = item.RECFIN_NUMEROMOV;
                        dr["COD PROCESSO"] = item.COD_PROCESSO;
                        dr["HISTÓRICO"] = item.HISTORICO;

                        dt.Rows.Add(dr);
                    }

                    var WsDados = wb.Worksheets.Add(dt, "Extração");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        WsDados.Cell(i + 2, 19).Value = dr["VALOR"].ToString().Replace(".", "").Replace(",", ".");
                        WsDados.Cell(i + 2, 19).Style.NumberFormat.Format = "#,##0.00";

                    }
                    WsDados.Columns().AdjustToContents();
                    wb.SaveAs(stream, false);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Extração_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public ActionResult PreencheMes(int ano)
        {
            if (ano > 0)
            {
                List<Contabilizacao> lst = new List<Contabilizacao>();
                lst.AddRange(contBUS.FechamentoMes(ano));

                var mes = (from r in lst
                                  select new
                                  {
                                      Text = r.Mes,
                                      Value = r.Mes
                                  }).ToList();

                return Json(mes, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}