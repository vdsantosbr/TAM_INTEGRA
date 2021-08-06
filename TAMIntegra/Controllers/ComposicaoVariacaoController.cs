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
    public class ComposicaoVariacaoController : Controller
    {
        private ComposicaoVariacao compVar = new ComposicaoVariacao();
        private ComposicaoVariacaoBUS compBUS = new ComposicaoVariacaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();

        [CustomAuthorize(Roles = "frmContabilizacaoVCEditar")]
        public ActionResult Index(string exibir = "", int id_integracao = 0, string situacao = "", string qualificacao = "", string tipo_vc = "", string tipo_fatura = "",
            string conta_debito = "", string conta_credito = "", string cod_processo = "", string referencia = "")
        {
            List<ComposicaoVariacao> lstGRid = new List<ComposicaoVariacao>();
            List<ComposicaoVariacao> lstRegistrosContabilizacao = new List<ComposicaoVariacao>();
            List<ComposicaoVariacao> lstRegistros = new List<ComposicaoVariacao>();
            int id_integracaoConta = 0;

            lstGRid = compBUS.Fechamento_Novo();
            foreach(var r in lstGRid)
            {
                id_integracaoConta = r.Id_Integracao;
            }

            CarregarDados(id_integracaoConta);
            compVar.lstGrid = lstGRid;

            if (exibir != "")
            {
                lstRegistrosContabilizacao = compBUS.GridContabilizacao(id_integracaoConta, situacao, qualificacao, tipo_vc, tipo_fatura, conta_debito, conta_credito, cod_processo, referencia);
                lstRegistros = compBUS.GridContabilizacaoFiltro(id_integracaoConta, situacao, qualificacao, tipo_vc, tipo_fatura, conta_debito, conta_credito, cod_processo, referencia);

                compVar.lstRegistros = lstRegistros;
                compVar.lstRegistrosContabilizacao = lstRegistrosContabilizacao;
                Session["lstDados"] = lstRegistros;
            }

            return View(compVar);
        }
        public ActionResult PopupReferencia(int id_integracao = 0, int id_lancamento = 0, string chave_lancamento = "", int id_entidade = 0,
            string referencia = "")
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();

            lst = compBUS.PopupReferencia(id_integracao, id_lancamento, chave_lancamento, id_entidade, referencia);

            if (lst != null)
            {
                return Json(new { Resultado = lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AddExcecao(int id_excecao = 0, string referencia = "", string observacao = "")
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            Usuario usuario = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            lst = compBUS.AddExcecao(id_excecao, Convert.ToInt32(usuario.Id_Pessoa), referencia, observacao);

            if (lst != null)
            {
                return Json(new { Resultado = lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Salvar(int id_integracao = 0, int id_entidade = 0, int id_lancamento = 0, string referencia = "", int tp_fatura = 0, string chave = "")
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            Usuario usuario = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            lst = compBUS.Salvar(id_integracao, id_lancamento, chave, id_entidade, referencia, tp_fatura);

            if (lst != null)
            {
                return Json(new { Resultado = lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public void CarregarDados(int id_integracao = 0)
        {
            ViewBag.Situacao = new SelectList(compBUS.Situacao(), "Cod_vc_composicao_situacao", "Des_vc_composicao_situacao");
            ViewBag.Qualificacao = new SelectList(compBUS.Qualificacao(), "Des_vc_composicao_qualificacao", "Des_vc_composicao_qualificacao");
            ViewBag.Tipo_vc = new SelectList(compBUS.Tipo_vc(), "Cod_vc_composicao_tipo_vc", "Des_vc_composicao_tipo_vc");
            ViewBag.Tipo_fatura = new SelectList(compBUS.Tipo_fatura(), "Cod_vc_composicao_tipo_fatura", "Des_vc_composicao_tipo_fatura");
            ViewBag.Conta_debito = new SelectList(compBUS.Conta_debito(id_integracao), "Conta_debito", "Descricao");
            ViewBag.Conta_credito = new SelectList(compBUS.Conta_credito(id_integracao), "Conta_credito", "Descricao");
        }
        public FileResult Exportar()
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    var wb = new ClosedXML.Excel.XLWorkbook();

                    IEnumerable<ComposicaoVariacao> result = new List<ComposicaoVariacao>();

                    result = (IEnumerable<ComposicaoVariacao>)Session["lstDados"];

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SITUAÇÃO");
                    dt.Columns.Add("QUALIFICAÇÃO");
                    dt.Columns.Add("TIPO DT. CONTABILIZAÇÃO");
                    dt.Columns.Add("REFERÊNCIA1");
                    dt.Columns.Add("CONTA DÉBITO");
                    dt.Columns.Add("CONTA CRÉDITO");
                    dt.Columns.Add("VALOR");
                    dt.Columns.Add("CÓD. PROCESSO");
                    dt.Columns.Add("FILIAL");
                    dt.Columns.Add("MVTO");
                    dt.Columns.Add("NÚMERO");
                    dt.Columns.Add("HISTÓRICO");

                    foreach (var item in result)
                    {
                        DataRow dr = dt.NewRow();
                        dr["SITUAÇÃO"] = item.Situacao;
                        dr["QUALIFICAÇÃO"] = item.Qualificacao;
                        dr["TIPO DT. CONTABILIZAÇÃO"] = item.Data_contabilizacao;
                        if (item.Data_contabilizacao != null)
                        {
                            DateTime? str_Data_contabilizacao = item.Data_contabilizacao;
                            string fmt_Data_contabilizacao = str_Data_contabilizacao.Value.ToString("dd/MM/yyyy");
                            dr["TIPO DT. CONTABILIZAÇÃO"] = fmt_Data_contabilizacao;
                        }
                        else
                        {
                            dr["TIPO DT. CONTABILIZAÇÃO"] = item.Data_contabilizacao;
                        }

                        dr["REFERÊNCIA1"] = item.Referencia1;
                        dr["CONTA DÉBITO"] = item.Conta_debito;
                        dr["CONTA CRÉDITO"] = item.Conta_credito;
                        dr["VALOR"] = item.Valor;
                        dr["CÓD. PROCESSO"] = item.Cod_processo;
                        if(item.CodFilial == 0)
                        {
                            dr["FILIAL"] = "";
                        }
                        else
                        {
                            dr["FILIAL"] = item.CodFilial;
                        }
                        dr["MVTO"] = item.Mvto;
                        dr["NÚMERO"] = item.Numero;
                        dr["HISTÓRICO"] = item.Historico;

                        dt.Rows.Add(dr);
                    }
                
                    var WsDados = wb.Worksheets.Add(dt, "Extração");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        WsDados.Cell(i + 2, 7).Value = dr["VALOR"].ToString().Replace(".", "").Replace(",", ".");
                        WsDados.Cell(i + 2, 7).Style.NumberFormat.Format = "#,##0.00";

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
   }
}