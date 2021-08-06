using Business;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using System.IO;
using System.Data;
using System.Globalization;
using ClosedXML.Excel;

namespace TAMINTEGRA.Controllers
{
    //[CustomAuthorize(Roles = "frmStatementRelatorio")]
    [CustomAuthorize(Roles = "frmStatement")]

    public class StatementRelatoriosController : Controller
    {
        StatementConciliacaoBUS concBUS = new StatementConciliacaoBUS();
        StatementRelatoriosExportacaoDadosBUS relBUS = new StatementRelatoriosExportacaoDadosBUS();
        private StatementContasBUS contasBUS = new StatementContasBUS();
        StatementRelatorioInfoComplementaresBUS relInfoComplBUS = new StatementRelatorioInfoComplementaresBUS();
        StatementRelatorioReceberPagarBUS relReceberPagarBUS = new StatementRelatorioReceberPagarBUS();
        StatementRelatorioComentariosBUS relComentariosBUS = new StatementRelatorioComentariosBUS();
        StatementRelatorioResumoStatementBUS relResumoBUS = new StatementRelatorioResumoStatementBUS();
        // GET: Relatorios
        public ActionResult Index(int ano = 0, int mes = 0, string conta = null,int relatorio = 0, string Id_Conciliacao = null)
        {
            if(Id_Conciliacao != null & Id_Conciliacao != "")
            {
                return RedirectToAction("Download", new { Id_Conciliacao = Id_Conciliacao, id_relatorio = relatorio });
            }

            StatementRelatorios obj = new StatementRelatorios();

            if (ano == 0)
            {
                ano = DateTime.Now.Year;
            }

            if (mes == 0)
            {
                mes = DateTime.Now.Month;
            }

            List<StatementConciliacao> lstAnalise = new List<StatementConciliacao>();
            
            lstAnalise = concBUS.Analise(Convert.ToInt32(ano), Convert.ToInt32(mes), null, null, null, null);
            TempData["lstanalise"] = lstAnalise;
            

            if (lstAnalise == null & TempData["lstanalise"] != null)
            {
                lstAnalise = (List<StatementConciliacao>)TempData["lstanalise"];
            }

            obj.Ano = ano;
            obj.Mes = mes;
            obj.lstAnalise = lstAnalise;

            return View(obj);
        }
        
        public ActionResult gerarRelatorios(string Id_Conciliacao = null, int radio = 0)
        {
            return RedirectToAction("Download", new { Id_Conciliacao = Id_Conciliacao });
        }

        public FileResult Download(string id_Conciliacao, int id_relatorio)
        {

            try
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    // Start a new workbook
                    var wb = new XLWorkbook();

                    if (id_relatorio == 1)
                    {
                        IEnumerable<StatementRelatorioExportacaoDados> result = relBUS.RelatorioExportacaoDados(id_Conciliacao);

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Id");
                        dt.Columns.Add("Conta");
                        dt.Columns.Add("Data Conciliação");
                        dt.Columns.Add("Situação");
                        dt.Columns.Add("Classificação da análise");
                        dt.Columns.Add("Documento No");
                        dt.Columns.Add("SO Ref");
                        dt.Columns.Add("Invoice");
                        dt.Columns.Add("Doc Date");
                        dt.Columns.Add("Net Due Date");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("RM Valor");
                        dt.Columns.Add("Diferença");
                        dt.Columns.Add("Arrear");
                        dt.Columns.Add("Dias em aberto");
                        dt.Columns.Add("Text");
                        dt.Columns.Add("Status");
                        dt.Columns.Add("Departamento");
                        dt.Columns.Add("Situação análise");
                        dt.Columns.Add("Data atualização");


                        foreach (var ent in result)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Id"] = ent.Id;
                            dr["Conta"] = ent.Conta;
                            if (ent.Data_Conciliacao != null)
                            {
                                DateTime? dataConc = ent.Data_Conciliacao;
                                string dataConcFormatada = dataConc.Value.ToString("dd/MM/yyyy");
                                dr["Data Conciliação"] = dataConcFormatada;
                            }
                            else
                            {
                                dr["Data Conciliação"] = ent.Data_Conciliacao;
                            }
                            dr["Situação"] = ent.Situacao;
                            dr["Classificação da análise"] = ent.Classificacao_da_Analise;
                            dr["Documento No"] = ent.Documento_No;
                            dr["SO Ref"] = ent.SO_Ref;
                            dr["Invoice"] = ent.Invoice;

                            if (ent.Doc_Date != null)
                            {
                                DateTime? Doc_Date = ent.Doc_Date;
                                string Doc_DateFormatada = Doc_Date.Value.ToString("dd/MM/yyyy");
                                dr["Doc Date"] = Doc_DateFormatada;
                            }
                            else
                            {
                                dr["Doc Date"] = ent.Doc_Date;
                            }

                            if (ent.Net_Due_Date != null)
                            {
                                DateTime? Net_Due_Date = ent.Net_Due_Date;
                                string Net_Due_DateFormatada = Net_Due_Date.Value.ToString("dd/MM/yyyy");
                                dr["Net Due Date"] = "";
                            }
                            else
                            {
                                dr["Net Due Date"] = ent.Net_Due_Date;
                            }

                            dr["Amount"] = string.Format("{0:#,##0.00#}", ent.Amount);
                            dr["RM Valor"] = string.Format("{0:#,##0.00#}", ent.RMValor);
                            dr["Diferença"] = string.Format("{0:#,##0.00#}", ent.Diferenca);
                            dr["Arrear"] = string.Format("{0:#,##}", ent.Arrear);
                            dr["Dias em aberto"] = ent.Dias_em_Aberto;
                            dr["Text"] = ent.Text;
                            dr["Status"] = ent.Status;
                            dr["Departamento"] = ent.Departamento;
                            dr["Situação análise"] = ent.Situacao_Analise;

                            if (ent.Data_Atualizacao != null)
                            {
                                DateTime? Data_Atualizacao = ent.Data_Atualizacao;
                                string Data_AtualizacaoFormatada = Data_Atualizacao.Value.ToString("dd/MM/yyyy");
                                dr["Data Atualização"] = Data_AtualizacaoFormatada;
                            }
                            else
                            {
                                dr["Data Atualização"] = "";
                            }

                            dt.Rows.Add(dr);
                        }

                   
                    // Add a DataTable as a worksheet
                    wb.Worksheets.Add(dt, "Extração");

                    wb.SaveAs(stream, false);
                  }//fim relatorio 1
                    if (id_relatorio == 2)
                    {
                        IEnumerable<StatementRelatorioInfoComplementares> result = relInfoComplBUS.RelatorioExportacaoDados(id_Conciliacao);

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Id Conciliação");
                        dt.Columns.Add("Id Conciliação Item");
                        dt.Columns.Add("Conta");
                        dt.Columns.Add("Invoice");
                        dt.Columns.Add("Situação");
                        dt.Columns.Add("Classificação");
                        dt.Columns.Add("Documento No");
                        dt.Columns.Add("SO Ref");
                        dt.Columns.Add("Doc Date");
                        dt.Columns.Add("Net Due Date");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("RM Valor");
                        dt.Columns.Add("Diferença");
                        dt.Columns.Add("Arrear");
                        dt.Columns.Add("Status");
                        dt.Columns.Add("Num DI");
                        dt.Columns.Add("Num Processo");
                        dt.Columns.Add("Num House");
                        dt.Columns.Add("Tipo Processo");
                        dt.Columns.Add("Depósito Especial");
                        dt.Columns.Add("Canal");
                        dt.Columns.Add("Pedido Compra");
                        dt.Columns.Add("Aprovação");
                        dt.Columns.Add("Título Financeiro");
                        dt.Columns.Add("CTT Câmbio");
                        dt.Columns.Add("Data Baixa");
                        dt.Columns.Add("Status Título");
                        dt.Columns.Add("Text");
                        dt.Columns.Add("Historico");
                        dt.Columns.Add("Departamento");
                        dt.Columns.Add("Situação análise");
                        dt.Columns.Add("Data atualização");
                        dt.Columns.Add("Data statement");
                        dt.Columns.Add("Data conciliação");
                        dt.Columns.Add("NF Numero NF");
                        dt.Columns.Add("NF Numeo DI");
                        dt.Columns.Add("NF Cod Processo");
                        dt.Columns.Add("NF Num Conhecimento");
                        dt.Columns.Add("Observação");

                        if (result != null)
                        {
                            foreach (var ent in result)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Id Conciliação"] = ent.Id_Conciliacao;
                                dr["Id Conciliação Item"] = ent.Id_Conciliacao_Item;
                                dr["Conta"] = ent.Conta;
                                dr["Invoice"] = ent.Invoice;
                                dr["Situação"] = ent.Situacao;
                                dr["Classificação"] = ent.Classificacao;
                                dr["Documento No"] = ent.Documento_No;
                                dr["SO Ref"] = ent.SO_Ref;

                                if (ent.Doc_Date != null)
                                {
                                    DateTime? Doc_Date = ent.Doc_Date;
                                    string Doc_DateFormatada = Doc_Date.Value.ToString("dd/MM/yyyy");
                                    dr["Doc Date"] = Doc_Date;
                                }
                                else
                                {
                                    dr["Doc Date"] = ent.Doc_Date;
                                }

                                if (ent.Net_Due_Date != null)
                                {
                                    DateTime? Net_Due_Date = ent.Net_Due_Date;
                                    string Net_Due_DateFormatada = Net_Due_Date.Value.ToString("dd/MM/yyyy");
                                    dr["Net Due Date"] = Net_Due_Date;
                                }
                                else
                                {
                                    dr["Net Due Date"] = ent.Doc_Date;
                                }

                                dr["Amount"] = string.Format("{0:#,##0.00#}", ent.Amount);
                                dr["RM Valor"] = string.Format("{0:#,##0.00#}", ent.RMValor);
                                dr["Diferença"] = string.Format("{0:#,##0.00#}", ent.Diferenca);
                                dr["Arrear"] = string.Format("{0:#,##}", ent.Arrear);
                                dr["Status"] = ent.Status;
                                dr["Num DI"] = ent.Num_DI;
                                dr["Num Processo"] = ent.Num_Processo;
                                dr["Num House"] = ent.Num_House;
                                dr["Tipo Processo"] = ent.Tipo_Processo;
                                dr["Depósito especial"] = ent.Deposito_Especial;
                                dr["Canal"] = ent.Canal;
                                dr["Pedido Compra"] = ent.Pedido_Compra;
                                dr["Aprovação"] = ent.Aprovacao;
                                dr["Título Financeiro"] = ent.Titulo_Financeiro;
                                if (ent.Data_Baixa != null)
                                {
                                    DateTime? Data_Baixa = ent.Data_Baixa;
                                    string Data_BaixaFormatada = Data_Baixa.Value.ToString("dd/MM/yyyy");
                                    dr["Data Baixa"] = Data_Baixa;
                                }
                                else
                                {
                                    dr["Data Baixa"] = "";
                                }
                                dr["Status Título"] = ent.Status_Titulo;
                                dr["Text"] = ent.Text;
                                dr["Historico"] = ent.Historico;
                                dr["Departamento"] = ent.Departamento;
                                dr["Situação análise"] = ent.Situacao_Analise;
                                if (ent.Data_Atualizacao != null)
                                {
                                    DateTime? Data_Atualizacao = ent.Data_Atualizacao;
                                    string Data_AtualizacaoFormatada = Data_Atualizacao.Value.ToString("dd/MM/yyyy");
                                    dr["Data atualização"] = Data_Atualizacao;
                                }
                                else
                                {
                                    dr["Data atualização"] = "";
                                }
                                if (ent.Data_Statement != null)
                                {
                                    DateTime? Data_Statement = ent.Data_Statement;
                                    string Data_StatementFormatada = Data_Statement.Value.ToString("dd/MM/yyyy");
                                    dr["Data statement"] = Data_Statement;
                                }
                                else
                                {
                                    dr["Data statement"] = "";
                                }
                                if (ent.Data_Conciliacao != null)
                                {
                                    DateTime? Data_Conciliacao = ent.Data_Conciliacao;
                                    string Data_ConciliacaoFormatada = Data_Conciliacao.Value.ToString("dd/MM/yyyy");
                                    dr["Data conciliação"] = Data_Conciliacao;
                                }
                                else
                                {
                                    dr["Data conciliação"] = "";
                                }
                                dr["NF Numero NF"] = ent.NFNUMNF;
                                dr["NF Numero DI"] = ent.NF_NUMERO_DI;
                                dr["NF Cod Processo"] = ent.Nf_Cod_Processo;
                                dr["NF Num Conhecimento"] = ent.NF_Num_conhecimeno;
                                dr["Observação"] = ent.Obsercacao;

                                dt.Rows.Add(dr);
                            }
                        }


                        // Add a DataTable as a worksheet
                        wb.Worksheets.Add(dt, "Extração");

                        wb.SaveAs(stream, false);
                    }
                    if(id_relatorio == 3)
                    {
                        IEnumerable<StatementRelatorioReceberPagar> result = relReceberPagarBUS.RelatorioExportacaoDados(id_Conciliacao);

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Id");
                        dt.Columns.Add("Conta");
                        dt.Columns.Add("Conciliação");
                        dt.Columns.Add("Situação");
                        dt.Columns.Add("Classificação");
                        dt.Columns.Add("SO Ref");
                        dt.Columns.Add("Invoice");
                        dt.Columns.Add("Doc Date");
                        dt.Columns.Add("Net Due Date");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("RM Valor");
                        dt.Columns.Add("Diferença");
                        dt.Columns.Add("Tipo processo");
                        dt.Columns.Add("Depósito especial");
                        dt.Columns.Add("Canal");
                        dt.Columns.Add("Arrear");
                        dt.Columns.Add("Documento No");
                        dt.Columns.Add("Text");
                        dt.Columns.Add("Departamento");
                        dt.Columns.Add("Situação análise");
                        dt.Columns.Add("Status");

                        if (result != null)
                        {
                            foreach (var ent in result)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Id"] = ent.Id;
                                dr["Conta"] = ent.Conta;
                                if (ent.Conciliacao != null)
                                {
                                    DateTime? dataConc = ent.Conciliacao;
                                    string dataConcFormatada = dataConc.Value.ToString("dd/MM/yyyy");
                                    dr["Conciliação"] = dataConcFormatada;
                                }
                                else
                                {
                                    dr["Conciliação"] = ent.Conciliacao;
                                }
                                dr["Situação"] = ent.Situacao;
                                dr["Classificação"] = ent.Classificacao;
                                dr["SO Ref"] = ent.SO_Ref;
                                dr["Invoice"] = ent.Invoice;

                                if (ent.Doc_Date != null)
                                {
                                    DateTime? Doc_Date = ent.Doc_Date;
                                    string Doc_DateFormatada = Doc_Date.Value.ToString("dd/MM/yyyy");
                                    dr["Doc Date"] = Doc_DateFormatada;
                                }
                                else
                                {
                                    dr["Doc Date"] = ent.Doc_Date;
                                }

                                if (ent.Net_Due_Dt != null)
                                {
                                    DateTime? Net_Due_Date = ent.Net_Due_Dt;
                                    string Net_Due_DateFormatada = Net_Due_Date.Value.ToString("dd/MM/yyyy");
                                    dr["Net Due Date"] = "";
                                }
                                else
                                {
                                    dr["Net Due Date"] = ent.Net_Due_Dt;
                                }

                                dr["Amount"] = string.Format("{0:#,##0.00#}", ent.Amount);
                                dr["RM Valor"] = string.Format("{0:#,##0.00#}", ent.RMValor);
                                dr["Diferença"] = string.Format("{0:#,##0.00#}", ent.Diferenca);
                                dr["Tipo processo"] = ent.Tipo_Processo;
                                dr["Depósito especial"] = ent.Deposito_Especial;
                                dr["Canal"] = ent.Canal;
                                dr["Arrear"] = string.Format("{0:#,##}", ent.Arrear);
                                dr["Documento No"] = ent.Documento_No;
                                dr["Text"] = ent.Text;
                                dr["Departamento"] = ent.Departamento;
                                dr["Situação análise"] = ent.Situacao_Analise;
                                dr["Status"] = ent.Status;

                                dt.Rows.Add(dr);
                            }
                        }
                        wb.Worksheets.Add(dt, "Extração");

                        wb.SaveAs(stream, false);
                    }//fim relatorio 3
                    if (id_relatorio == 4)
                    {
                        IEnumerable<StatementRelatorioComentarios> result = relComentariosBUS.RelatorioExportacaoDados(id_Conciliacao);

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Id");
                        dt.Columns.Add("Data Conciliação");
                        dt.Columns.Add("Conta");
                        dt.Columns.Add("Situação da análise");
                        dt.Columns.Add("Situação da invoice");
                        dt.Columns.Add("Classificação da invoice");
                        dt.Columns.Add("Departamento");
                        dt.Columns.Add("Documento No");
                        dt.Columns.Add("SO Ref");
                        dt.Columns.Add("Invoice");
                        dt.Columns.Add("Doc Date");
                        dt.Columns.Add("Net Due Date");
                        dt.Columns.Add("Arrear");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("RM Valor");
                        dt.Columns.Add("Diferença");
                        dt.Columns.Add("Status");
                        dt.Columns.Add("Text");
                        dt.Columns.Add("Comentários");

                        foreach (var ent in result)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Id"] = ent.Id;
                            if (ent.Data_Conciliacao != null)
                            {
                                DateTime? dataConc = ent.Data_Conciliacao;
                                string dataConcFormatada = dataConc.Value.ToString("dd/MM/yyyy");
                                dr["Data Conciliação"] = dataConcFormatada;
                            }
                            else
                            {
                                dr["Data Conciliação"] = ent.Data_Conciliacao;
                            }
                            dr["Conta"] = ent.Conta;
                            
                            dr["Situação da análise"] = ent.Situacao_Analise;
                            dr["Situação da invoice"] = ent.Situacao_Invoice;
                            dr["Classificação da invoice"] = ent.Classificacao_Invoice;
                            dr["Departamento"] = ent.Departamento;
                            dr["Documento No"] = ent.Documento_No;
                            dr["SO Ref"] = ent.SO_Ref;
                            dr["Invoice"] = ent.Invoice;

                            if (ent.Doc_Date != null)
                            {
                                DateTime? Doc_Date = ent.Doc_Date;
                                string Doc_DateFormatada = Doc_Date.Value.ToString("dd/MM/yyyy");
                                dr["Doc Date"] = Doc_DateFormatada;
                            }
                            else
                            {
                                dr["Doc Date"] = ent.Doc_Date;
                            }

                            if (ent.Net_Due_Dt != null)
                            {
                                DateTime? Net_Due_Date = ent.Net_Due_Dt;
                                string Net_Due_DateFormatada = Net_Due_Date.Value.ToString("dd/MM/yyyy");
                                dr["Net Due Date"] = "";
                            }
                            else
                            {
                                dr["Net Due Date"] = ent.Net_Due_Dt;
                            }

                            dr["Amount"] = string.Format("{0:#,##0.00#}", ent.Amount);
                            dr["RM Valor"] = string.Format("{0:#,##0.00#}", ent.RMValor);
                            dr["Diferença"] = string.Format("{0:#,##0.00#}", ent.Diferenca);
                            dr["Arrear"] = string.Format("{0:#,##}", ent.Arrear);
                            dr["Text"] = ent.Text;
                            dr["Status"] = ent.Status;
                            dr["Comentários"] = ent.Comentarios;

                            dt.Rows.Add(dr);
                        }


                        // Add a DataTable as a worksheet
                        wb.Worksheets.Add(dt, "Extração");

                        wb.SaveAs(stream, false);
                    }//fim relatorio 4
                    if (id_relatorio == 7)
                    {
                        IEnumerable<StatementRelatorioResumoStatement> result = relResumoBUS.RelatorioExportacaoDados(id_Conciliacao);

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Grupo");
                        dt.Columns.Add("Descrição");
                        dt.Columns.Add("Quantidade invoice");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("Saldo");


                        foreach (var ent in result)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Grupo"] = ent.Grupo;
                            dr["Descrição"] = ent.Descricao;
                            dr["Quantidade invoice"] = ent.Qtd_invoice;
                            dr["Amount"] = string.Format("{0:#,##0.00#}", ent.Amount);
                            dr["Saldo"] = string.Format("{0:#,##0.00#}", ent.Saldo);

                            dt.Rows.Add(dr);
                        }


                        // Add a DataTable as a worksheet
                        wb.Worksheets.Add(dt, "Extração");

                        wb.SaveAs(stream, false);
                    }//fim relatorio 7
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Extração_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                }
            }
         
            catch (Exception e)
            {
                var erro = e.Message;
                return null;
            }
        }
    }
}