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
    public class FinanceiroProcessoController : Controller
    {
        private FinanceiroProcesso processos = new FinanceiroProcesso();
        private SituacaoBUS situacaoBUS = new SituacaoBUS();
        private FinanceiroProcessoBUS financeiroProcessosBUS = new FinanceiroProcessoBUS();
        private TipoFaturamentoBUS tipoFatBUS = new TipoFaturamentoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        PerfilBUS perfilBUS = new PerfilBUS();

        bool exporta_excel = false;

        [CustomAuthorize(Roles = "frmProcesso")]
        public ActionResult Index(string situacao = null, string exibir = null, string strDataInicio = null, string strDataFim = null, int declaracao = 0, string numProcesso = null, string numDI = null)
        {
            CarregarDados();

            List<FinanceiroProcesso> lstProcessos = new List<FinanceiroProcesso>();
            DateTime dataInicioDT = new DateTime();
            DateTime dataTerminoDT = new DateTime();

            if (strDataInicio == null || strDataInicio == "")
            {
                var dia = DateTime.Today.AddDays(-31);
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

            processos.strDataInicio = strDataInicio;
            processos.strDataFim = strDataFim;

            if (exibir != null)
            {
                //lstProcessos.Add(new FinanceiroProcesso
                //{
                //    Situacao = "IS",
                //    Id_Integracao = 1,
                //    Data = new DateTime(2019, 06, 12),
                //    PROCESSO = "M19/5545"
                //});
                //lstProcessos.Add(new FinanceiroProcesso
                //{
                //    Situacao = "NI",
                //    Id_Integracao = 1,
                //    Data = new DateTime(2019, 06, 12),
                //    PROCESSO = "M19/5545"
                //});
                lstProcessos = financeiroProcessosBUS.Filtro(dataInicioDT, dataTerminoDT, numProcesso, numDI);

                //var tipoMoedas = lstProcessos
                //.GroupBy(w => w.MOEDA)
                //.Select(g => new
                //{
                //    Valor = g.Select(c => c.VALOR),
                //    TipoMoeda = g.Select(c => c.MOEDA)
                //}).ToList();

                //var r = from p in lstProcessos
                //        group p by p.MOEDA into g
                //        select new
                //        {
                //            TipoMoeda = (string)g.Select(x => x.MOEDA).First() + ": " + g.Sum(x => x.VALOR).ToString("C", CultureInfo.CurrentCulture).Replace("R$ ", ""),
                //            Valor = g.Select(x => x.VALOR)
                //        };

                //var results = from line in lstProcessos
                //              group line by line.MOEDA into g
                //              select new
                //              {
                //                  ProductName = g.First().MOEDA,
                //                  Price = g.Sum(x => x.VALOR).ToString("C", CultureInfo.CurrentCulture),
                //              };

                //List<string> lstTipoMoedas = new List<string>();
                //foreach (var res in r)
                //{
                //    lstTipoMoedas.Add(res.TipoMoeda);
                //}

                //processos.lstMoedas = lstTipoMoedas;
            }

            //-------------------------------------------------//

            //-------------------------------------------------//

            var lstInforme = TempData["lstInforme"] as List<FinanceiroProcesso>;
            //if (lstInforme != null)
            //{
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


            processos.lstInforme = lstProcessos;
            //}

            var lstReprocessamento = TempData["lstReprocessamento"] as List<FinanceiroProcesso>;
            if (lstReprocessamento != null)
            {
                processos.lstReprocessamento = lstReprocessamento;
            }

            processos.lstItens = lstProcessos;
            Session["lstItens"] = lstProcessos;


            return View(processos);
        }
        public ActionResult Exibir()
        {
            return null;
        }


        public JsonResult Informativo(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null, string codProcesso = null)
        {
            List<FinanceiroProcesso> lstInforme = new List<FinanceiroProcesso>();
          

            lstInforme = financeiroProcessosBUS.Informe(id_integracao, sp_id, sp_id_despesa_processo);
            string strData = "";
            string strLiberacao = "";
            string strCadastro = "";
            string strVencimento = "";
            DateTime date = DateTime.ParseExact("2017/07/31 08:08:24", "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            foreach (var r in lstInforme)
            {
                strData = r.Data.ToShortDateString();
                strLiberacao = DateTime.ParseExact(r.LIBERACAO, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToShortDateString();
                strVencimento = DateTime.ParseExact(r.VENCIMENTO, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToShortDateString();
                strCadastro = DateTime.ParseExact(r.CADASTRO, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToShortDateString();
            }

     

            var resultado = (from info in lstInforme
                             select new
                             {
                             })
                             .ToList();

           
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reprocessar(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            CarregarDados();
            List<FinanceiroProcesso> lstReprocessamento = new List<FinanceiroProcesso>();
            //lstReprocessamento.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            ////lstInforme = cavokBUS.Informe(id_integracao);

            //TempData["lstReprocessamento"] = lstReprocessamento.ToList();


            //lstInforme.Add(new Cavok
            //{
            //    DescricaoInforme = "teste"
            //});
            //lstInforme = cavokBUS.Informe(id_integracao);

            //TempData["lstInforme"] = lstInforme.ToList();

            lstReprocessamento = financeiroProcessosBUS.Reprocessar(id_integracao, sp_id, sp_id_despesa_processo);

            if (lstReprocessamento != null)
            {
                var resultado = (from info in lstReprocessamento
                                 select new
                                 {
                                     //Item = info.ITEM,
                                     //Comentario = info.COMENTARIO
                                 }).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult abrirProcesso(string codProcesso = null)
        {
            List<FinanceiroProcesso> lstProcesso = new List<FinanceiroProcesso>();
            List<FinanceiroGuiaFatura> lstInformeFatura = new List<FinanceiroGuiaFatura>();
            List<FinanceiroGuiaImpostos> lstInformeImpostos = new List<FinanceiroGuiaImpostos>();
            List<FinanceiroGuiaDespesas> lstInformeDespesas = new List<FinanceiroGuiaDespesas>();
            List<FinanceiroGuiaEstoque> lstInformeEstoque = new List<FinanceiroGuiaEstoque>();
            List<FinanceiroGuiaEstoque> lstInformeEstoqueLACCTB = new List<FinanceiroGuiaEstoque>();
            List<FinanceiroGuiaHistorico> lstInformeHistorico = new List<FinanceiroGuiaHistorico>();
            List<FinanceiroGuiaDeclaracao> lstInformeDeclaracao = new List<FinanceiroGuiaDeclaracao>();
            List<FinanceiroGuiaFinanceiro> lstInformeFinanceiro = new List<FinanceiroGuiaFinanceiro>();
            List<FinanceiroGuiaNotaCompl> lstInformeNotaCompl= new List<FinanceiroGuiaNotaCompl>();
            List<FinanceiroGuiaFaturaPDC> lstInformeFaturaPDC = new List<FinanceiroGuiaFaturaPDC>();

            lstProcesso = financeiroProcessosBUS.Guia(codProcesso);
            lstInformeFatura = financeiroProcessosBUS.InformeFatura(codProcesso);
            lstInformeImpostos = financeiroProcessosBUS.InformeImpostos(codProcesso);
            lstInformeDespesas = financeiroProcessosBUS.InformeDespesas(codProcesso);
            lstInformeEstoque = financeiroProcessosBUS.InformeEstoque(codProcesso);
            lstInformeEstoqueLACCTB = financeiroProcessosBUS.InformeEstoqueLACCTB(codProcesso);
            lstInformeHistorico = financeiroProcessosBUS.InformeHistorico(codProcesso);
            lstInformeDeclaracao = financeiroProcessosBUS.InformeDeclaracao(codProcesso);
            lstInformeFinanceiro = financeiroProcessosBUS.InformeFinanceiro(codProcesso);
            lstInformeNotaCompl = financeiroProcessosBUS.InformeNotacompl(codProcesso);
            lstInformeFaturaPDC = financeiroProcessosBUS.InformeFaturaPDC(codProcesso);


            if (lstProcesso != null || lstInformeFatura != null || lstInformeImpostos != null || lstInformeDespesas != null || lstInformeEstoque != null)
            {
                if (lstProcesso != null)
                {
                    var resultado = (from info in lstProcesso
                                     select new
                                     {
                                         PROCESSO = codProcesso
                                     })
                                     .ToList();
                  
                }

                if(lstInformeFatura != null)
                {
                    string strDataFatura = "";
                    string strData = "";

                    foreach (var r in lstInformeFatura)
                    {
                        strData = r.DATA.ToShortDateString();
                        r.STR_DATA = strData;
                        strDataFatura = r.DATA_FATURA.ToShortDateString();
                        r.STR_DATA_FATURA = strDataFatura;

                    }

                    var resFatura = (from info in lstInformeFatura
                                     select new
                                     {
                                         FAT_EXPORTADOR = info.EXPORTADOR,
                                         FAT_NUM_FATURA = info.FAT_NUM_FATURA,
                                         FAT_COD_MOEDA = info.FAT_COD_MOEDA,
                                         FAT_COND_PAGTO  = info.FAT_COND_PAGTO,
                                         FAT_DATA_VENCIMENTO  = info.FAT_DATA_VENCIMENTO,
                                         FAT_TAXA_DI  = info.FAT_TAXA_DI,
                                         FAT_VMCV_TOTAL = info.FAT_VMCV_TOTAL,
                                         NUCLEUS_MVTO = info.NUCLEUS_MVTO,
                                         NUCLEUS_NUM  = info.NUCLEUS_NUM,
                                         FLUXUS_DOC  = info.FLUXUS_DOC,
                                         STR_DATA_FATURA  = info.STR_DATA_FATURA,
                                         CONHECIMENTO  = info.CONHECIMENTO,
                                         TIPO_CONHECIMENTO  = info.TIPO_CONHECIMENTO,
                                         FAT_INCOTERM  = info.FAT_INCOTERM,
                                         ID_INTEGRACAO  = info.ID_INTEGRACAO,
                                         SITUACAO = info.SITUACAO,
                                         STR_DATA = info.STR_DATA,
                                         ID_INT= info.ID_INT,
                                         CHAVEORIGEM_INT  = info.CHAVEORIGEM_INT,
                                         NUCLEUS_ID  = info.NUCLEUS_ID,
                                         FLUXUS_ID  = info.FLUXUS_ID,
                                         ImportSys  = info.ImportSys,
                                         REC_NUCLEOUS  = info.REC_NUCLEUS,
                                         doc_fluxus  = info.doc_fluxus
                                     })
                                     .ToList();
                }

                if(lstInformeImpostos != null)
                {
                    var strVencimento = "";
                    var strDataCadastro = "";
                    var strDataLiberacao = "";
                        
                    foreach (var r in lstInformeImpostos)
                    {
                        strVencimento = r.VENCIMENTO.ToShortDateString();
                        r.strVENCIMENTO = strVencimento;
                        strDataCadastro = r.DATA_CADASTRO.ToShortDateString();
                        r.strDataCadastro = strDataCadastro;
                        strDataLiberacao = r.DATA_LIBERACAO.ToShortDateString();
                        r.strDataLiberacao = strDataLiberacao;
                    }

                    var resImpostos = (from info in lstInformeImpostos
                                     select new
                                     {
                                         FAT_EXPORTADOR = info.EXPORTADOR,
                                         NUCLEUS_ID = info.NUCLEUS_ID,
                                         FLUXUS_ID = info.FLUXUS_ID,
                                         ImportSys = info.ImportSys,
                                         REC_NUCLEOUS = info.REC_NUCLEUS,
                                         doc_fluxus = info.doc_fluxus
                                     })
                                     .ToList();
                }


                return Json(new { ListFatura = lstInformeFatura, ListImpostos = lstInformeImpostos, ListDespesas = lstInformeDespesas, ListEstoque = lstInformeEstoque, ListHistorico = lstInformeHistorico, ListDeclaracao = lstInformeDeclaracao, ListInformeLACCTB = lstInformeEstoqueLACCTB, ListInformeFinanceiro = lstInformeFinanceiro, ListInformeNotaCompl = lstInformeNotaCompl, ListInformeFaturaPDC = lstInformeFaturaPDC }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        public FileResult DownloadExcel()
        {
            List<FinanceiroGuiaDeclaracao> lstInformeDeclaracao = new List<FinanceiroGuiaDeclaracao>();
            List<FinanceiroGuiaFatura> lstInformeFatura = new List<FinanceiroGuiaFatura>();
            List<FinanceiroGuiaFaturaPDC> lstInformeFaturaPDC = new List<FinanceiroGuiaFaturaPDC>();
            List<FinanceiroGuiaImpostos> lstInformeImpostos = new List<FinanceiroGuiaImpostos>();
            List<FinanceiroGuiaDespesas> lstInformeDespesas = new List<FinanceiroGuiaDespesas>();
            List<FinanceiroGuiaEstoque> lstInformeEstoque = new List<FinanceiroGuiaEstoque>();
            List<FinanceiroGuiaNotaCompl> lstInformeNotaCompl = new List<FinanceiroGuiaNotaCompl>();
            List<FinanceiroGuiaEstoque> lstInformeEstoqueLACCTB = new List<FinanceiroGuiaEstoque>();
            List<FinanceiroGuiaFinanceiro> lstInformeFinanceiro = new List<FinanceiroGuiaFinanceiro>();
            List<FinanceiroGuiaHistorico> lstInformeHistorico = new List<FinanceiroGuiaHistorico>();

            try
            {
                List<FinanceiroProcesso> lstCodProcessos = (List<FinanceiroProcesso>)Session["lstItens"];

                using (MemoryStream stream = new MemoryStream())
                {
                    var wb = new XLWorkbook();
                    DataTable dt = new DataTable();
                    DataTable dtDeclaracao = new DataTable();
                    DataTable dtFaturaPDC = new DataTable();
                    DataTable dtFinanceiro = new DataTable();
                    DataTable dtImposto = new DataTable();
                    DataTable dtDespesa = new DataTable();
                    DataTable dtEstoque = new DataTable();
                    DataTable dtNotaCompl = new DataTable();
                    DataTable dtEstoqueLACCTB = new DataTable();
                    DataTable dtHistorico = new DataTable();

                    //Declaração
                    dtDeclaracao.Columns.Add("Processo");
                    dtDeclaracao.Columns.Add("Tipo de Declaração");
                    dtDeclaracao.Columns.Add("DI");
                    dtDeclaracao.Columns.Add("Data DI");
                    dtDeclaracao.Columns.Add("Conhecimento");
                    dtDeclaracao.Columns.Add("Data Conhecimento");
                    dtDeclaracao.Columns.Add("Taxa DI");

                    //Fatura
                    dt.Columns.Add("Processo");
                    dt.Columns.Add("ImportSys");
                    dt.Columns.Add("Movimento");
                    dt.Columns.Add("Documento");
                    dt.Columns.Add("Exportador");
                    dt.Columns.Add("Fatura");
                    dt.Columns.Add("Tipo Fatura");
                    dt.Columns.Add("Moeda");
                    dt.Columns.Add("Cond. Pgto");
                    dt.Columns.Add("Dt. Fatura");
                    dt.Columns.Add("Vencimento");
                    dt.Columns.Add("Taxa DI");
                    dt.Columns.Add("Valor");
                    dt.Columns.Add("Conhecimento");
                    dt.Columns.Add("Incoterm");

                    //FaturaPDC
                    dtFaturaPDC.Columns.Add("Processo");
                    dtFaturaPDC.Columns.Add("ImportSys");
                    dtFaturaPDC.Columns.Add("Filial");
                    dtFaturaPDC.Columns.Add("Mov. Pedido");
                    dtFaturaPDC.Columns.Add("Importação");
                    dtFaturaPDC.Columns.Add("Condição Pgto");
                    dtFaturaPDC.Columns.Add("Fatura");
                    dtFaturaPDC.Columns.Add("Mov. Recebimento");

                    //Impostos
                    dtImposto.Columns.Add("Processo");
                    dtImposto.Columns.Add("ImportSys");
                    dtImposto.Columns.Add("Mov. Recebimento");
                    dtImposto.Columns.Add("Documento");
                    dtImposto.Columns.Add("Exportador");
                    dtImposto.Columns.Add("Referência");
                    dtImposto.Columns.Add("Descrição");
                    dtImposto.Columns.Add("Dt. Vencimento");
                    dtImposto.Columns.Add("Moeda");
                    dtImposto.Columns.Add("Vl. Previsto");
                    dtImposto.Columns.Add("Vl. Adiantado");
                    dtImposto.Columns.Add("Vl. Real");
                    dtImposto.Columns.Add("Vl. Pagar");
                    dtImposto.Columns.Add("Dt. Cadastro");
                    dtImposto.Columns.Add("Dt. Liberação");

                    //Despesas
                    dtDespesa.Columns.Add("Processo");
                    dtDespesa.Columns.Add("ImportSys");
                    dtDespesa.Columns.Add("Mov. Emissão");
                    dtDespesa.Columns.Add("Mov. Recebimento");
                    dtDespesa.Columns.Add("Documento");
                    dtDespesa.Columns.Add("Exportador");
                    dtDespesa.Columns.Add("Referência");
                    dtDespesa.Columns.Add("Descrição");
                    dtDespesa.Columns.Add("Dt. Vencimento");
                    dtDespesa.Columns.Add("Moeda");
                    dtDespesa.Columns.Add("Vl. Previsto");
                    dtDespesa.Columns.Add("Vl. Adiantado");
                    dtDespesa.Columns.Add("Vl. Real");
                    dtDespesa.Columns.Add("Vl. Pagar");
                    dtDespesa.Columns.Add("Dt. Cadastro");
                    dtDespesa.Columns.Add("Dt. Liberação");

                    //Estoque
                    dtEstoque.Columns.Add("Processo");
                    dtEstoque.Columns.Add("BrokerSys");
                    dtEstoque.Columns.Add("Mov. Emissão");
                    dtEstoque.Columns.Add("Rec. Físico");
                    dtEstoque.Columns.Add("Fatura");
                    dtEstoque.Columns.Add("Taxa DI");
                    dtEstoque.Columns.Add("II");
                    dtEstoque.Columns.Add("COFINS");
                    dtEstoque.Columns.Add("IPI");
                    dtEstoque.Columns.Add("PIS");
                    dtEstoque.Columns.Add("Frete");
                    dtEstoque.Columns.Add("Seguro");
                    dtEstoque.Columns.Add("Vl. Produto");
                    dtEstoque.Columns.Add("ICMS");
                    dtEstoque.Columns.Add("Vl. Nota");
                    dtEstoque.Columns.Add("Despesa Complementar");
                    dtEstoque.Columns.Add("Vl. Estoque");

                    //EstoqueLACCTB
                    dtNotaCompl.Columns.Add("Processo");
                    dtNotaCompl.Columns.Add("BrokerSys");
                    dtNotaCompl.Columns.Add("Fatura");
                    dtNotaCompl.Columns.Add("DI");
                    dtNotaCompl.Columns.Add("II");
                    dtNotaCompl.Columns.Add("COFINS");
                    dtNotaCompl.Columns.Add("IPI");
                    dtNotaCompl.Columns.Add("PIS");
                    dtNotaCompl.Columns.Add("Frete");
                    dtNotaCompl.Columns.Add("Seguro");
                    dtNotaCompl.Columns.Add("Vl. Produto");
                    dtNotaCompl.Columns.Add("ICMS");
                    dtNotaCompl.Columns.Add("Vl. Nota");
                    dtNotaCompl.Columns.Add("Vl. Estoque");

                    //Financeiro
                    dtFinanceiro.Columns.Add("Processo");
                    dtFinanceiro.Columns.Add("Operação");
                    dtFinanceiro.Columns.Add("Mov. Recebimento");
                    dtFinanceiro.Columns.Add("Dt. Emissão");
                    dtFinanceiro.Columns.Add("Dt. Documento");
                    dtFinanceiro.Columns.Add("Fatura");
                    dtFinanceiro.Columns.Add("Documento");
                    dtFinanceiro.Columns.Add("Tipo");
                    dtFinanceiro.Columns.Add("Valor (ME)");
                    dtFinanceiro.Columns.Add("Valor (MN)");
                    dtFinanceiro.Columns.Add("Situação");
                    dtFinanceiro.Columns.Add("Dt. Vencimento");
                    dtFinanceiro.Columns.Add("Ctt. Câmbio");
                    dtFinanceiro.Columns.Add("Dt. Baixa");

                    //Historico
                    dtHistorico.Columns.Add("Processo");
                    dtHistorico.Columns.Add("ImportSys");
                    dtHistorico.Columns.Add("Registro");
                    dtHistorico.Columns.Add("Mov. Emissão");
                    dtHistorico.Columns.Add("Mov. Recebimento");
                    dtHistorico.Columns.Add("Documento");
                    dtHistorico.Columns.Add("Valor");
                    dtHistorico.Columns.Add("Dt. Integração");
                    dtHistorico.Columns.Add("ID Integração");
                    dtHistorico.Columns.Add("Chave Integração");

                   

                    foreach (var cod in lstCodProcessos)
                    {
                        lstInformeFatura = financeiroProcessosBUS.InformeFatura(cod.COD_PROCESSO);
                        IEnumerable<FinanceiroGuiaFatura> resultFatura = lstInformeFatura;

                        lstInformeFaturaPDC = financeiroProcessosBUS.InformeFaturaPDC(cod.COD_PROCESSO);

                        lstInformeImpostos = financeiroProcessosBUS.InformeImpostos(cod.COD_PROCESSO);
                        IEnumerable<FinanceiroGuiaImpostos> resultImpostos = lstInformeImpostos;

                        lstInformeDespesas = financeiroProcessosBUS.InformeDespesas(cod.COD_PROCESSO);
                        IEnumerable<FinanceiroGuiaImpostos> resultDespesas = lstInformeImpostos;

                        lstInformeEstoque = financeiroProcessosBUS.InformeEstoque(cod.COD_PROCESSO);
                        IEnumerable<FinanceiroGuiaEstoque> resultEstoque = lstInformeEstoque;

                        lstInformeNotaCompl = financeiroProcessosBUS.InformeNotacompl(cod.COD_PROCESSO);
                        IEnumerable<FinanceiroGuiaNotaCompl> resultNotaCompl = lstInformeNotaCompl;

                        lstInformeEstoqueLACCTB = financeiroProcessosBUS.InformeEstoqueLACCTB(cod.COD_PROCESSO);
                        IEnumerable<FinanceiroGuiaEstoque> resultEstoqueLACCTB = lstInformeEstoqueLACCTB;

                        lstInformeFinanceiro = financeiroProcessosBUS.InformeFinanceiro(cod.COD_PROCESSO);

                        lstInformeHistorico = financeiroProcessosBUS.InformeHistorico(cod.COD_PROCESSO);

                        lstInformeDeclaracao = financeiroProcessosBUS.InformeDeclaracao(cod.COD_PROCESSO);


                        //lstCambio.Add(new FinanceiroCambioSYS
                        //{
                        //    Data = new DateTime(2019, 06, 12),
                        //    Arquivo = "Teste.txt",
                        //    Tipo_Arquivo = "Baixa"

                        //});

                        foreach (var declaracao in lstInformeDeclaracao)
                        {
                            DataRow dr = dtDeclaracao.NewRow();

                            if (declaracao.NF_DATA_REGISTRO_DI != null)
                            {
                                DateTime? str_NF_DATA_REGISTRO_DI = declaracao.NF_DATA_REGISTRO_DI;
                                string fmt_str_NF_DATA_REGISTRO_DI = str_NF_DATA_REGISTRO_DI.Value.ToString("dd/MM/yyyy");
                                dr["Data DI"] = fmt_str_NF_DATA_REGISTRO_DI;
                            }
                            else
                            {
                                dr["Data DI"] = declaracao.NF_DATA_REGISTRO_DI;
                            }

                            if (declaracao.NF_DATA_CONHECIMENTO != null)
                            {
                                DateTime? str_NF_DATA_CONHECIMENTO = declaracao.NF_DATA_CONHECIMENTO;
                                string fmt_NF_DATA_CONHECIMENTO = str_NF_DATA_CONHECIMENTO.Value.ToString("dd/MM/yyyy");
                                dr["Data Conhecimento"] = fmt_NF_DATA_CONHECIMENTO;
                            }
                            else
                            {
                                dr["Data Conhecimento"] = declaracao.NF_DATA_CONHECIMENTO;
                            }


                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["Tipo de Declaração"] = cod.TIPO_DECLARACAO;
                            dr["DI"] = declaracao.NF_NUMERO_DI;
                            dr["Conhecimento"] = declaracao.NF_NUM_CONHECIMENTO;
                            dr["Taxa DI"] = string.Format("{0:0,0.000}", declaracao.NF_TAXA_DOLAR_REGISTRO_DI);  ;

                            dtDeclaracao.Rows.Add(dr);

                        }

                        foreach (var fatura in resultFatura)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["ImportSys"] = fatura.ImportSys;
                            dr["Movimento"] = fatura.REC_NUCLEUS;
                            dr["Documento"] = fatura.doc_fluxus;
                            dr["Exportador"] = fatura.EXPORTADOR;
                            dr["Fatura"] = fatura.FAT_NUM_FATURA;
                            dr["Tipo Fatura"] = fatura.TIPO_FATURA;
                            dr["Moeda"] = fatura.FAT_COD_MOEDA;
                            dr["Cond. Pgto"] = fatura.FAT_COND_PAGTO;

                            if (fatura.DATA_FATURA != null)
                            {
                                DateTime? str_DATA_FATURA = fatura.DATA_FATURA;
                                string fmt_DATA_FATURA = str_DATA_FATURA.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Fatura"] = fmt_DATA_FATURA;
                            }
                            else
                            {
                                dr["Dt. Fatura"] = fatura.DATA_FATURA;
                            }

                            if (fatura.FAT_DATA_VENCIMENTO != null)
                            {
                                DateTime? str_FAT_DATA_VENCIMENTO = fatura.FAT_DATA_VENCIMENTO;
                                string fmt_FAT_DATA_VENCIMENTO = str_FAT_DATA_VENCIMENTO.Value.ToString("dd/MM/yyyy");
                                dr["Vencimento"] = fmt_FAT_DATA_VENCIMENTO;
                            }
                            else
                            {
                                dr["Vencimento"] = fatura.FAT_DATA_VENCIMENTO;
                            }

                            dr["Taxa DI"] = string.Format("{0:0,0.000}", fatura.FAT_TAXA_DI);  ;
                            dr["Valor"] = string.Format("{0:0,0.00}", fatura.FAT_VMCV_TOTAL);
                          

                            dr["Conhecimento"] = fatura.CONHECIMENTO;
                            dr["Incoterm"] = fatura.FAT_INCOTERM;
                         
                            dt.Rows.Add(dr);
                            
                        }

                        foreach (var faturaPDC in lstInformeFaturaPDC)
                        {
                            DataRow dr = dtFaturaPDC.NewRow();
                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["ImportSys"] = faturaPDC.IMPORTSYS;
                            dr["Filial"] = faturaPDC.FILIAL;
                            dr["Mov. Pedido"] = faturaPDC.PEDIDO_COMPRA;
                            dr["Importação"] = faturaPDC.TIPO_IMPORTACAO;
                            dr["Condição Pgto"] = faturaPDC.COND_PGTO;
                            dr["Fatura"] = faturaPDC.FAT_NUM_FATURA;
                            dr["Mov. Recebimento"] = faturaPDC.REC_NUCLEUS;

                            dtFaturaPDC.Rows.Add(dr);

                        }

                        foreach (var imposto in lstInformeImpostos)
                        {
                            DataRow dr = dtImposto.NewRow();
                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["ImportSys"] = imposto.ImportSys;
                            dr["Mov. Recebimento"] = imposto.REC_NUCLEUS;
                            dr["Documento"] = imposto.doc_fluxus;
                            dr["Exportador"] = imposto.EXPORTADOR;
                            dr["Referência"] = imposto.SP_NUM_DOCUMENTO;
                            dr["Descrição"] = imposto.DESCRICAO;

                            if (imposto.VENCIMENTO != null)
                            {
                                DateTime? str_VENCIMENTO = imposto.VENCIMENTO;
                                string fmt_VENCIMENTO = str_VENCIMENTO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Vencimento"] = fmt_VENCIMENTO;
                            }
                            else
                            {
                                dr["Dt. Vencimento"] = imposto.VENCIMENTO;
                            }

                            dr["Moeda"] = imposto.MOEDA;
                            dr["Vl. Previsto"] = string.Format("{0:0,0.00}", imposto.VR_PREVISTO);
                            dr["Vl. Adiantado"] = string.Format("{0:0,0.00}", imposto.VR_ADIANTADO);
                            dr["Vl. Real"] = string.Format("{0:0,0.00}", imposto.VR_REAL);
                            dr["Vl. Pagar"] = string.Format("{0:0,0.00}", imposto.VR_A_PAGAR);

                            if (imposto.DATA_CADASTRO != null)
                            {
                                DateTime? str_DATA_CADASTRO = imposto.DATA_CADASTRO;
                                string fmt_DATA_CADASTRO = str_DATA_CADASTRO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Cadastro"] = fmt_DATA_CADASTRO;
                            }
                            else
                            {
                                dr["Dt. Cadastro"] = imposto.DATA_CADASTRO;
                            }

                            if (imposto.DATA_LIBERACAO != null)
                            {
                                DateTime? str_DATA_LIBERACAO = imposto.DATA_LIBERACAO;
                                string fmt_DATA_LIBERACAO = str_DATA_LIBERACAO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Liberação"] = fmt_DATA_LIBERACAO;
                            }
                            else
                            {
                                dr["Dt. Liberação"] = imposto.DATA_LIBERACAO;
                            }


                            dtImposto.Rows.Add(dr);
                        }

                        foreach(var despesa in lstInformeDespesas)
                        {
                            DataRow dr = dtDespesa.NewRow();
                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["ImportSys"] = despesa.ImportSys;
                            dr["Mov. Recebimento"] = despesa.EMISSAO;
                            dr["Documento"] = despesa.doc_fluxus;
                            dr["Exportador"] = despesa.EXPORTADOR;
                            dr["Referência"] = despesa.SP_NUM_DOCUMENTO;
                            dr["Descrição"] = despesa.DESCRICAO;

                            if (despesa.VENCIMENTO != null)
                            {
                                DateTime? str_VENCIMENTO = despesa.VENCIMENTO;
                                string fmt_VENCIMENTO = str_VENCIMENTO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Vencimento"] = fmt_VENCIMENTO;
                            }
                            else
                            {
                                dr["Dt. Vencimento"] = despesa.VENCIMENTO;
                            }

                            dr["Moeda"] = despesa.MOEDA;
                            dr["Vl. Previsto"] = string.Format("{0:0,0.00}", despesa.VR_PREVISTO);
                            dr["Vl. Adiantado"] = string.Format("{0:0,0.00}", despesa.VR_ADIANTADO);
                            dr["Vl. Real"] = string.Format("{0:0,0.00}", despesa.VR_REAL);
                            dr["Vl. Pagar"] = string.Format("{0:0,0.00}", despesa.VR_A_PAGAR);

                            if (despesa.DATA_CADASTRO != null)
                            {
                                DateTime? str_DATA_CADASTRO = despesa.DATA_CADASTRO;
                                string fmt_DATA_CADASTRO = str_DATA_CADASTRO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Cadastro"] = fmt_DATA_CADASTRO;
                            }
                            else
                            {
                                dr["Dt. Cadastro"] = despesa.DATA_CADASTRO;
                            }

                            if (despesa.DATA_LIBERACAO != null)
                            {
                                DateTime? str_DATA_LIBERACAO = despesa.DATA_LIBERACAO;
                                string fmt_DATA_LIBERACAO = str_DATA_LIBERACAO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Liberação"] = fmt_DATA_LIBERACAO;
                            }
                            else
                            {
                                dr["Dt. Liberação"] = despesa.DATA_LIBERACAO;
                            }


                            dtDespesa.Rows.Add(dr);
                        }

                        foreach (var estoque in lstInformeEstoque)
                        {
                            DataRow dr = dtEstoque.NewRow();
                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["BrokerSys"] = estoque.BROKERSYS;
                            dr["Mov. Emissão"] = estoque.EMISSAO; 
                            dr["Rec. Físico"] = estoque.RECFISICO;
                            dr["Fatura"] = string.Format("{0:0,0.00}", estoque.VR_FATURA);
                            dr["Taxa DI"] = estoque.TAXA_DI;
                            dr["II"] = string.Format("{0:0,0.00}", estoque.II);
                            dr["COFINS"] = string.Format("{0:0,0.00}", estoque.COFINS);
                            dr["IPI"] = string.Format("{0:0,0.00}", estoque.IPI);
                            dr["PIS"] = string.Format("{0:0,0.00}", estoque.PIS);
                            dr["Frete"] = string.Format("{0:0,0.00}", estoque.FRETEINTER);
                            dr["Seguro"] = string.Format("{0:0,0.00}", estoque.SEGURO);
                            dr["Vl. Produto"] = string.Format("{0:0,0.00}", estoque.VR_PRODUTO);
                            dr["ICMS"] = string.Format("{0:0,0.00}", estoque.ICMS);
                            dr["Vl. Nota"] = string.Format("{0:0,0.00}", estoque.VR_NOTA);
                            dr["Despesa Complementar"] = string.Format("{0:0,0.00}", estoque.DESPESA_COMPL);
                            dr["Vl. Estoque"] = string.Format("{0:0,0.00}", estoque.VR_REC_FISICO);

                            dtEstoque.Rows.Add(dr);
                        }

                        foreach (var notaCompl in lstInformeNotaCompl)
                        {
                            DataRow dr = dtNotaCompl.NewRow();
                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["BrokerSys"] = notaCompl.BROKERSYS;
                            dr["Fatura"] = string.Format("{0:0,0.00}", notaCompl.VR_FATURA);
                            dr["DI"] = string.Format("{0:0,0.00}", notaCompl.TAXA_DI);
                            dr["II"] = string.Format("{0:0,0.00}", notaCompl.II);
                            dr["COFINS"] = string.Format("{0:0,0.00}", notaCompl.COFINS);
                            dr["IPI"] = string.Format("{0:0,0.00}", notaCompl.IPI);
                            dr["PIS"] = string.Format("{0:0,0.00}", notaCompl.PIS);
                            dr["Frete"] = string.Format("{0:0,0.00}", notaCompl.FRETEINTER);
                            dr["Seguro"] = string.Format("{0:0,0.00}", notaCompl.SEGURO);
                            dr["Vl. Produto"] = string.Format("{0:0,0.00}", notaCompl.VR_PRODUTO);
                            dr["ICMS"] = string.Format("{0:0,0.00}", notaCompl.ICMS);
                            dr["Vl. Nota"] = string.Format("{0:0,0.00}", notaCompl.VR_NOTA);
                            dr["Vl. Estoque"] = string.Format("{0:0,0.00}", notaCompl.VR_REC_FISICO);

                            dtNotaCompl.Rows.Add(dr);
                        }

                        foreach (var financeiro in lstInformeFinanceiro)
                        {
                            DataRow dr = dtFinanceiro.NewRow();
                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["Operação"] = financeiro.OPERACAO;
                            dr["Mov. Recebimento"] = financeiro.MOVIMENTO;

                            if (financeiro.DATAEMISSAO != null)
                            {
                                DateTime? str_DATAEMISSAO = financeiro.DATAEMISSAO;
                                string fmt_DATAEMISSAO = str_DATAEMISSAO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Emissão"] = fmt_DATAEMISSAO;
                            }
                            else
                            {
                                dr["Dt. Emissão"] = financeiro.DATAEMISSAO;
                            }

                            if (financeiro.DATA_DOCUMENTO != null)
                            {
                                DateTime? str_DATA_DOCUMENTO = financeiro.DATA_DOCUMENTO;
                                string fmt_DATA_DOCUMENTO = str_DATA_DOCUMENTO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Documento"] = fmt_DATA_DOCUMENTO;
                            }
                            else
                            {
                                dr["Dt. Documento"] = financeiro.DATA_DOCUMENTO;
                            }

                            dr["Fatura"] = financeiro.INVOICE;
                            dr["Documento"] = financeiro.NUMERODOCUMENTO;
                            dr["Tipo"] = financeiro.TIPO;
                            dr["Valor (ME)"] = string.Format("{0:0,0.00}", financeiro.VALOR_ME);
                            dr["Valor (MN)"] = string.Format("{0:0,0.00}", financeiro.VALOR_MN);
                            dr["Situação"] = financeiro.SITUACAO;

                            if (financeiro.DATAVENCIMENTO != null)
                            {
                                DateTime? str_DATAVENCIMENTO = financeiro.DATAVENCIMENTO;
                                string fmt_DATAVENCIMENTO = str_DATAVENCIMENTO.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Vencimento"] = fmt_DATAVENCIMENTO;
                            }
                            else
                            {
                                dr["Dt. Vencimento"] = financeiro.DATAVENCIMENTO;
                            }

                            dr["Ctt. Câmbio"] = financeiro.CTT_CAMBIO;

                            if (financeiro.DATABAIXA != null)
                            {
                                DateTime? str_DATABAIXA = financeiro.DATABAIXA;
                                string fmt_DATABAIXA = str_DATABAIXA.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Baixa"] = fmt_DATABAIXA;
                            }
                            else
                            {
                                dr["Dt. Baixa"] = financeiro.DATABAIXA;
                            }


                            dtFinanceiro.Rows.Add(dr);
                        }

                        foreach (var historico in lstInformeHistorico)
                        {
                            DataRow dr = dtHistorico.NewRow();
                            dr["Processo"] = cod.COD_PROCESSO;
                            dr["ImportSys"] = historico.IMPORTSYS;
                            dr["Registro"] = historico.REGISTRO;
                            dr["Mov. Emissão"] = historico.EMISSAO;
                            dr["Mov. Recebimento"] = historico.RECEBIMENTO;
                            dr["Documento"] = historico.DOCUMENTO;
                            dr["Valor"] = string.Format("{0:0,0.00}", historico.VALOR);

                            if (historico.DATA != null)
                            {
                                DateTime? str_DATA = historico.DATA;
                                string fmt_DATA = str_DATA.Value.ToString("dd/MM/yyyy");
                                dr["Dt. Integração"] = fmt_DATA;
                            }
                            else
                            {
                                dr["Dt. Integração"] = historico.DATA;
                            }

                            dr["ID Integração"] = historico.ID_INTEGRACAO;
                            dr["Chave Integração"] = historico.CHAVEORIGEM_INT;

                            dtHistorico.Rows.Add(dr);
                        }
                    }


                    // Add a DataTable as a worksheet
                    
                    var WsDeclaracao = wb.Worksheets.Add(dtDeclaracao, "Declaração de Importação");
                    for (int i = 0; i < dtDeclaracao.Rows.Count; i++)
                    {
                        DataRow dr = dtDeclaracao.Rows[i];

                        WsDeclaracao.Cell(i + 2, 7).Value = dr["Taxa DI"].ToString().Replace(".", "").Replace(",", ".");
                        WsDeclaracao.Cell(i + 2, 7).Style.NumberFormat.Format = "#,##0.000";

                    }

                    var WsFatura = wb.Worksheets.Add(dt, "Faturas");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        WsFatura.Cell(i + 2, 13).Value = dr["Valor"].ToString().Replace(".", "").Replace(",", ".");
                        WsFatura.Cell(i + 2, 13).Style.NumberFormat.Format = "#,##0.00";

                        WsFatura.Cell(i + 2, 12).Value = dr["Taxa DI"].ToString().Replace(".", "").Replace(",", ".");
                        WsFatura.Cell(i + 2, 12).Style.NumberFormat.Format = "#,##0.000";
                    }
                    WsFatura.Column(12).AdjustToContents();
                    WsFatura.Column(13).AdjustToContents();

                    wb.Worksheets.Add(dtFaturaPDC, "Pedido de Compra");

                    var WsImpostos = wb.Worksheets.Add(dtImposto, "Impostos");
                    for (int i = 0; i < dtImposto.Rows.Count; i++)
                    {
                        DataRow dr = dtImposto.Rows[i];

                        WsImpostos.Cell(i + 2, 10).Value = dr["Vl. Previsto"].ToString().Replace(".", "").Replace(",", ".");
                        WsImpostos.Cell(i + 2, 10).Style.NumberFormat.Format = "#,##0.00";

                        WsImpostos.Cell(i + 2, 11).Value = dr["Vl. Adiantado"].ToString().Replace(".", "").Replace(",", ".");
                        WsImpostos.Cell(i + 2, 11).Style.NumberFormat.Format = "#,##0.00";

                        WsImpostos.Cell(i + 2, 12).Value = dr["Vl. Real"].ToString().Replace(".", "").Replace(",", ".");
                        WsImpostos.Cell(i + 2, 12).Style.NumberFormat.Format = "#,##0.00";

                        WsImpostos.Cell(i + 2, 13).Value = dr["Vl. Pagar"].ToString().Replace(".", "").Replace(",", ".");
                        WsImpostos.Cell(i + 2, 13).Style.NumberFormat.Format = "#,##0.00";
                    }

                    

                    var WsDespesas = wb.Worksheets.Add(dtDespesa, "Despesas");
                    for (int i = 0; i < dtDespesa.Rows.Count; i++)
                    {
                        DataRow dr = dtDespesa.Rows[i];

                        WsDespesas.Cell(i + 2, 11).Value = dr["Vl. Previsto"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesas.Cell(i + 2, 11).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesas.Cell(i + 2, 12).Value = dr["Vl. Adiantado"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesas.Cell(i + 2, 12).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesas.Cell(i + 2, 13).Value = dr["Vl. Real"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesas.Cell(i + 2, 13).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesas.Cell(i + 2, 14).Value = dr["Vl. Pagar"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesas.Cell(i + 2, 14).Style.NumberFormat.Format = "#,##0.00";
                    }

                    var WsNotas = wb.Worksheets.Add(dtEstoque, "Notas");
                    for (int i = 0; i < dtEstoque.Rows.Count; i++)
                    {
                        DataRow dr = dtEstoque.Rows[i];

                        WsNotas.Cell(i + 2, 5).Value = dr["Fatura"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 5).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 6).Value = dr["Taxa DI"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 6).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 7).Value = dr["II"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 7).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 8).Value = dr["COFINS"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 8).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 9).Value = dr["IPI"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 9).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 10).Value = dr["PIS"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 10).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 11).Value = dr["Frete"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 11).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 12).Value = dr["Seguro"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 12).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 13).Value = dr["Vl. Produto"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 13).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 14).Value = dr["ICMS"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 14).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 15).Value = dr["Vl. Nota"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 15).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 16).Value = dr["Despesa Complementar"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 16).Style.NumberFormat.Format = "#,##0.00";

                        WsNotas.Cell(i + 2, 17).Value = dr["Vl. Estoque"].ToString().Replace(".", "").Replace(",", ".");
                        WsNotas.Cell(i + 2, 17).Style.NumberFormat.Format = "#,##0.00";
                    }

                    var WsDespesasCompl = wb.Worksheets.Add(dtNotaCompl, "Despesas Complementares");
                    for (int i = 0; i < dtNotaCompl.Rows.Count; i++)
                    {
                        DataRow dr = dtNotaCompl.Rows[i];

                        WsDespesasCompl.Cell(i + 2, 3).Value = dr["Fatura"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 3).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 4).Value = dr["DI"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 4).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 5).Value = dr["II"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 5).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 6).Value = dr["COFINS"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 6).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 7).Value = dr["IPI"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 7).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 8).Value = dr["PIS"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 8).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 9).Value = dr["Frete"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 9).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 10).Value = dr["Seguro"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 10).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 11).Value = dr["Vl. Produto"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 11).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 12).Value = dr["ICMS"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 12).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 13).Value = dr["Vl. Nota"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 13).Style.NumberFormat.Format = "#,##0.00";

                        WsDespesasCompl.Cell(i + 2, 14).Value = dr["Vl. Estoque"].ToString().Replace(".", "").Replace(",", ".");
                        WsDespesasCompl.Cell(i + 2, 14).Style.NumberFormat.Format = "#,##0.00";
                    }

                    var WsFinanceiro = wb.Worksheets.Add(dtFinanceiro, "Financeiro");
                    for (int i = 0; i < dtFinanceiro.Rows.Count; i++)
                    {
                        DataRow dr = dtFinanceiro.Rows[i];

                        WsFinanceiro.Cell(i + 2, 9).Value = dr["Valor (ME)"].ToString().Replace(".", "").Replace(",", ".");
                        WsFinanceiro.Cell(i + 2, 9).Style.NumberFormat.Format = "#,##0.00";

                        WsFinanceiro.Cell(i + 2, 10).Value = dr["Valor (MN)"].ToString().Replace(".", "").Replace(",", ".");
                        WsFinanceiro.Cell(i + 2, 10).Style.NumberFormat.Format = "#,##0.00";

                    }

                    var WsHistorico = wb.Worksheets.Add(dtHistorico, "Histórico");
                    for (int i = 0; i < dtHistorico.Rows.Count; i++)
                    {
                        DataRow dr = dtHistorico.Rows[i];

                        WsHistorico.Cell(i + 2, 7).Value = dr["Valor"].ToString().Replace(".", "").Replace(",", ".");
                        WsHistorico.Cell(i + 2, 7).Style.NumberFormat.Format = "#,##0.00";

                    }

                    



                    wb.SaveAs(stream, false);

                    // Return a byte array
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TAMIntegra_Processo_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                    //return File(@"C:\Users\itala.cordeiro\Downloads", "application /text", "teste" + ".xlsx");


                }
            }
            catch (Exception)
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
            ViewBag.Credor = new SelectList(financeiroProcessosBUS.Declaracao(), "NF_TIPO_DEC_SISCOMEX", "TIPO_DECLARACAO");
            string[] temx = Convert.ToString(HttpContext.Request.LogonUserIdentity.Name).Split('\\');
            string login = temx[1];
            ViewBag.IdPessoa = usr.Id_Pessoa;
            List<Perfil> lstPerfis = perfilBUS.Parametrizacao(usr.Id_Perfil).Where(x => x.Formulario == "frmProcesso").ToList();
            foreach (var r in lstPerfis)
            {
                ViewBag.PermitirConsultar = r.Permitir_Consultar;
                ViewBag.PermitirEditar = r.Permitir_Editar;
                ViewBag.PermitirExportar = r.Permitir_Exportar;
            }
        }
    }
}