using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAMINTEGRA.Controllers
{
    public class RecebimentoAvalaraPedidosController : Controller
    {

        Business.RecebimentoBUS Bus = new Business.RecebimentoBUS();
        List<Recebimento_Tipo_Movimento_Avalara> Combo1 = new List<Recebimento_Tipo_Movimento_Avalara>();
        List<Recebimento_Tipo_Movimento_Remessa_Avalara> Combo1Remessa = new List<Recebimento_Tipo_Movimento_Remessa_Avalara>();
        List<Recebimento_Class_Fin_Avalara> Combo2 = new List<Recebimento_Class_Fin_Avalara>();
        List<Recebimento_Forma_Pagto_Avalara> Combo3 = new List<Recebimento_Forma_Pagto_Avalara>();
        List<RecebimentoNota> ListNota = new List<RecebimentoNota>();
        List<RecebimentoNotaRM> ListNotaRM = new List<RecebimentoNotaRM>();
        List<RecebimentoNotaVinculada> ListNotaVinculada = new List<RecebimentoNotaVinculada>();
        List<Recebimento_Compra_item_Avalara> Item_compra = new List<Recebimento_Compra_item_Avalara>();
        List<Recebimento_item_Avalara> item_Avalara = new List<Recebimento_item_Avalara>();
        List<Recebimento_CFOP_Avalara> cfop_Avalara = new List<Recebimento_CFOP_Avalara>();
        List<Recebimento_NCM_Avalara> ncm_Avalara = new List<Recebimento_NCM_Avalara>();
        List<Recebimento_AvalaraDetalheNota> ListNotaDetalhe = new List<Recebimento_AvalaraDetalheNota>();
        List<RecebimentoAvalaraDeParaNFSE> RecebimentoDEPARA = new List<RecebimentoAvalaraDeParaNFSE>();

        List<SelectIDMOV> SelectIDMOV = new List<SelectIDMOV>();

        

        RecebimentoCombosViewModel ViewModelCombos = new RecebimentoCombosViewModel();


        // GET: RecebimentoAvalaraPedidos
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            return View();
        }


        [HttpPost]
        public ActionResult ShowForm(FormCollection infoCollection)
        {
            RecebimentoAvalaraEnviaForm Enviaform = new RecebimentoAvalaraEnviaForm();

            var count = infoCollection.Count;

            if (count > 0)
            {


                //itens selecionados
                Enviaform.chekPedidos = infoCollection.Get(5);


                int cont = 0;

                if (infoCollection["TipoNota"] == "1")
                {



                    var teste = infoCollection["checkPedidos"];

                    foreach (var itempedidos in infoCollection["checkPedidos"].Split(','))
                    {
                        cont++;


                        Enviaform.NumpedidoRM = itempedidos;
                        Enviaform.tipomovimento = infoCollection["tipomovimento"];
                        Enviaform.Classefinanceira = infoCollection["classefinanceira"];
                        Enviaform.FormaPagamento = infoCollection["formapagamento"];
                        Enviaform.QuantidadeReceber = infoCollection["quantidadeReceber"].Split(',')[cont - 1];
                        Enviaform.Coligada = infoCollection["COLIGADA"].Split(',')[cont - 1];
                        Enviaform.Idmov = infoCollection["IDMOV"].Split(',')[cont - 1];
                        Enviaform.nItem = infoCollection.Get(3).Remove(1, infoCollection.Get(3).Length - 1);
                        Enviaform.NotaFiscal = infoCollection.Get(3).Substring(1, infoCollection.Get(3).Length - 1);
                        Enviaform.Idprd = infoCollection["IDPRD"].Split(',')[cont - 1];
                        Enviaform.Itemcfop = infoCollection["itemCFOP"].Split(',')[cont - 1];


                        using (DatabaseContext db = new DatabaseContext())
                        {
                            //Susing (DatabaseContext db = new DatabaseContext())
                            {
                                var linhas = " insert into Avalara_DownloadNFe_Vinculo (IdAvalara, nItem, NotaFiscal, QuantidadeReceber, TipoMovimento "
                                    + " , ClasseFinanceira, TipoPagamento, itemCFOP, NumpedidoRM, CodColigada, Idmov, Idprd)  values (" + Convert.ToInt32(Session["IdAvalara"]) +
                                    " , " + Enviaform.nItem +
                                    " , " + Enviaform.NotaFiscal +
                                    " , " + Enviaform.QuantidadeReceber +
                                    " , '" + Enviaform.tipomovimento +
                                    "' , '" + Enviaform.Classefinanceira +
                                    "' , '" + Enviaform.FormaPagamento +
                                    "' , '" + Enviaform.Itemcfop +
                                    "' , '" + Enviaform.NumpedidoRM +
                                    "' , '" + Enviaform.Coligada +
                                    "' , '" + Enviaform.Idmov +
                                    "' , " + Enviaform.Idprd +
                                    " ) ";

                                //refatorar...
                                db.Database.SqlQuery<RecebimentoNota>(linhas).ToList();
                                //atualiza tabela de EMI
                                db.Database.SqlQuery<RecebimentoNota>("UPDATE Avalara_DownloadNFe_Emit SET Situacao='AN'" +
                                    " , vinculoTipoMovimento='" + Enviaform.tipomovimento + "' , vinculoClassFin='" + Enviaform.Classefinanceira + "'" +
                                    "  , vinculoFormaPagto='" + Enviaform.FormaPagamento + "', TipoNFe= " + Session["tiponota"] + "  WHERE iD='" + Convert.ToInt32(Session["IdAvalara"]) + "'").ToList();

                            }

                        }

                    }
                }

                else
                {
                    foreach (var itempedidos in infoCollection["checkncm"].Split(','))
                    {
                        cont++;


                        Enviaform.NumpedidoRM = itempedidos;
                        Enviaform.tipomovimento = infoCollection["tipomovimento"];
                        Enviaform.Classefinanceira = infoCollection["classefinanceira"];
                        Enviaform.FormaPagamento = infoCollection["formapagamento"];
                        Enviaform.QuantidadeReceber = "0";
                        Enviaform.Coligada = "0";
                        Enviaform.Idmov = "0";
                        Enviaform.nItem = "0";
                        Enviaform.NotaFiscal = infoCollection["NotaFiscal"].Split(',')[cont - 1];
                        Enviaform.itemDEPARA = infoCollection["itemCorrespondente"].Split(',')[cont - 1];
                        Enviaform.Idprd = infoCollection["itemncm"].Split(',')[cont - 1];
                        Enviaform.Itemcfop = infoCollection["itemCFOP"].Split(',')[cont - 1];
                        


                        using (DatabaseContext db = new DatabaseContext())
                        {
                            //Susing (DatabaseContext db = new DatabaseContext())
                            {
                                var linhas = " insert into Avalara_DownloadNFe_Vinculo (IdAvalara, nItem, NotaFiscal, QuantidadeReceber, TipoMovimento "
                                    + " , ClasseFinanceira, TipoPagamento, itemCFOP, NumpedidoRM, CodColigada, Idmov, Idprd,ItemRM, itemDEPARA)  values (" + Convert.ToInt32(Session["IdAvalara"]) +
                                    " , " + Enviaform.nItem +
                                    " , " + Enviaform.NotaFiscal +
                                    " , " + Enviaform.QuantidadeReceber +
                                    " , '" + Enviaform.tipomovimento +
                                    "' , '" + Enviaform.Classefinanceira +
                                    "' , '" + Enviaform.FormaPagamento +
                                    "' , '" + Enviaform.Itemcfop +
                                    "' , '" + Enviaform.NumpedidoRM +
                                    "' , '" + Enviaform.Coligada +
                                    "' , '" + Enviaform.Idmov +
                                    "' , '" + Enviaform.Idprd +
                                    "' , '" + Enviaform.Idprd +
                                     "' , '" + Enviaform.itemDEPARA +
                                    "' ) ";

                                //refatorar...
                                db.Database.SqlQuery<RecebimentoNota>(linhas).ToList();
                                //atualiza tabela de EMI
                                db.Database.SqlQuery<RecebimentoNota>("UPDATE Avalara_DownloadNFe_Emit SET Situacao='AN'" +
                                    " , vinculoTipoMovimento='" + Enviaform.tipomovimento + "' , vinculoClassFin='" + Enviaform.Classefinanceira + "'" +
                                    "  , vinculoFormaPagto='" + Enviaform.FormaPagamento + "', TipoNFe= " + Session["tiponota"] + "  WHERE iD='" + Convert.ToInt32(Session["IdAvalara"]) + "'").ToList();

                            }

                        }

                    }
                }
            }


            //carregar informacoes atualizadas e mandar por viewModel.


            int idavalara = Convert.ToInt32(Session["IdAvalara"]);
            int pedidoremessa = Convert.ToInt32(Session["pedidoremessa"]);
            ViewModelCombos.remessa = false;
            Session["tiponota"] = 1;

            ListNota = Bus.ObterInformacoesNota();
            ListNotaRM = Bus.ObterInformacoesNotaRM();
            ListNotaVinculada = Bus.ObterInformacoesNotaVinculada();

            Array Arr = (Array)Session["IdMove"];
            string Listpedido = string.Empty;
            foreach (var item in Arr)
            {
                Listpedido += item + ",";                
            }

            ListNotaDetalhe = Bus.ObterInformacoesNotaDetalhe(pedidoremessa.ToString());
            Session["chavenfe"] = Bus.ObterInformacoesNotaDetalhe(pedidoremessa.ToString()).FirstOrDefault().CHAVENF;
            string Codtmv = ListNota.Where(a => a.ID_AVALARA == idavalara).FirstOrDefault().CFOP.ToString();

            if (ViewModelCombos.remessa)
                Combo1 = Bus.ObterInformacoesCombo1("");
            else
                Combo1 = Bus.ObterInformacoesCombo1(Codtmv);

            Combo2 = Bus.ObterInformacoesCombo2();
            Combo3 = Bus.ObterInformacoesCombo3();
            Item_compra = Bus.ObterInformacoes_CompraItemAvalara(Listpedido);
            item_Avalara = Bus.ObterInformacoes_item_Avalara(Convert.ToInt32(Session["IdAvalara"]), Listpedido);
            cfop_Avalara = Bus.ObterInformacoes_CFOP_Avalara(ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).FirstOrDefault().CFOP.ToString());
            ncm_Avalara = Bus.ObterInformacoes_NCM_Avalara(ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).FirstOrDefault().CFOP.ToString());


            ViewModelCombos.Combo1 = Combo1;
            ViewModelCombos.Combo2 = Combo2;
            ViewModelCombos.Combo3 = Combo3;
            ViewModelCombos.RecebimentoNota = ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).ToList();
            ViewModelCombos.RecebimentoNotaRM = ListNotaRM;
            ViewModelCombos.Compra_item_Avalara = Item_compra;
            ViewModelCombos.item_Avalara = item_Avalara;
            ViewModelCombos.cfop_Avalara = cfop_Avalara;
            ViewModelCombos.ListNotaDetalhe = ListNotaDetalhe;
            ViewModelCombos.NCM_Avalara = ncm_Avalara;
            ViewModelCombos.ListNotaVinculada = ListNotaVinculada;

            return RedirectToAction("PedidoRecebido", new { id = idavalara });
            //return RedirectToAction("PedidoRecebido", new { model = ViewModelCombos });

        }
        [HttpGet]
        public ActionResult PedidoRecebido(int id = 0)
        {
            //pegar itempedido            
            int idavalara = Convert.ToInt32(Session["IdAvalara"]);
            string pedidoremessa = Session["pedidoremessa"].ToString();
            ViewModelCombos.remessa = false;
            Session["tiponota"] = 1;

            ListNota = Bus.ObterInformacoesNota();
            RecebimentoDEPARA = Bus.ObterRecebimentoDEPARA();
            ListNotaRM = Bus.ObterInformacoesNotaRM();
            ListNotaVinculada = Bus.ObterInformacoesNotaVinculada();
            SelectIDMOV = Bus.SelectIDMOv(Convert.ToInt16(Session["IdAvalara"]), ListNota.Where(a => a.ID_AVALARA == Convert.ToInt16(Session["IdAvalara"])).FirstOrDefault().NF_NUMERO);

            Array Arr = (Array)Session["IdMove"];
            string Listpedido = string.Empty;

            foreach (var item in Arr)
            {
                Listpedido += item + ",";

                if (SelectIDMOV.Count==0)
                    Bus.InsertIDMOv(idavalara, Convert.ToInt32(item), pedidoremessa.ToString());

                if (Listpedido == "1,")
                {
                    ViewModelCombos.remessa = true;
                    Session["tiponota"] = 2;
                }
            }

            Array Arrcod = (Array)Session["IdCodtmv"];
            string ListpedidoCod = string.Empty;

            foreach (var item in Arrcod)
            {
                ListpedidoCod += item + ",";
               
            }



            ListNotaDetalhe = Bus.ObterInformacoesNotaDetalhe(pedidoremessa.ToString());

            Session["chavenfe"] = Bus.ObterInformacoesNotaDetalhe(pedidoremessa.ToString()).FirstOrDefault().CHAVENF;

            string Codtmv = ListNota.Where(a => a.ID_AVALARA == idavalara).FirstOrDefault().CFOP.ToString();

           

            Combo2 = Bus.ObterInformacoesCombo2();
            Combo3 = Bus.ObterInformacoesCombo3();
            Item_compra = Bus.ObterInformacoes_CompraItemAvalara(Listpedido);
            item_Avalara = Bus.ObterInformacoes_item_Avalara(Convert.ToInt32(Session["IdAvalara"]), Listpedido);
            cfop_Avalara = Bus.ObterInformacoes_CFOP_Avalara(ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).FirstOrDefault().CFOP.ToString());
            ncm_Avalara = Bus.ObterInformacoes_NCM_Avalara(ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).FirstOrDefault().CFOP.ToString());

           

            if (ViewModelCombos.remessa)
                Combo1 = Bus.ObterInformacoesCombo1("");
            else
                Combo1 = Bus.ObterInformacoesCombo1(ListpedidoCod);


            ViewModelCombos.Combo1 = Combo1;
            ViewModelCombos.Combo2 = Combo2;
            ViewModelCombos.Combo3 = Combo3;
            ViewModelCombos.RecebimentoNota = ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).ToList();
            ViewModelCombos.RecebimentoNotaRM = ListNotaRM;
            ViewModelCombos.Compra_item_Avalara = Item_compra;
            ViewModelCombos.item_Avalara = item_Avalara;
            ViewModelCombos.cfop_Avalara = cfop_Avalara;
            ViewModelCombos.ListNotaDetalhe = ListNotaDetalhe;
            ViewModelCombos.NCM_Avalara = ncm_Avalara;
            ViewModelCombos.RecebimentoDEPARA = RecebimentoDEPARA;
            ViewModelCombos.ListNotaVinculada = ListNotaVinculada;

            return View(ViewModelCombos);
            
        }

        public ActionResult PedidoAnalisado(int IdAvalara)
        {
            Session["IdAvalara"] = IdAvalara;
            return View();
        }

        public ActionResult PedidoAnalisadoSessao()
        {
            ListNota = Bus.ObterInformacoesNota();
           
            ListNotaRM = Bus.ObterInformacoesNotaRM();
            ListNotaVinculada = Bus.ObterInformacoesNotaVinculada();

            ListNotaDetalhe = Bus.ObterInformacoesNotaDetalhe(ListNota.Where(a => a.ID_AVALARA == Convert.ToInt16(Session["IdAvalara"])).FirstOrDefault().NF_NUMERO);

            Session["chavenfe"] = Bus.ObterInformacoesNotaDetalhe(ListNota.Where(a => a.ID_AVALARA == Convert.ToInt16(Session["IdAvalara"])).FirstOrDefault().NF_NUMERO).FirstOrDefault().CHAVENF;
            SelectIDMOV = Bus.SelectIDMOv(Convert.ToInt16(Session["IdAvalara"]), ListNota.Where(a => a.ID_AVALARA == Convert.ToInt16(Session["IdAvalara"])).FirstOrDefault().NF_NUMERO);

            int pedidoremessa = Convert.ToInt32(Session["pedidoremessa"]);
            ViewModelCombos.remessa = false;
            Session["tiponota"] = 1;

            string ListpedidoCod = string.Empty;
            ListpedidoCod = "99";

            string Listpedido = string.Empty;
            foreach (var item in SelectIDMOV )
            {
                Listpedido += item.IdMov + ",";

            }

            Combo2 = Bus.ObterInformacoesCombo2();
            Combo3 = Bus.ObterInformacoesCombo3();
            item_Avalara = Bus.ObterInformacoes_item_Avalara(Convert.ToInt32(Session["IdAvalara"]), Listpedido);
            cfop_Avalara = Bus.ObterInformacoes_CFOP_Avalara(ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).FirstOrDefault().CFOP.ToString());
            ncm_Avalara = Bus.ObterInformacoes_NCM_Avalara(ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).FirstOrDefault().CFOP.ToString());


            if (ListNota.Where(a=>a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).FirstOrDefault().TIPONFE == "2")
            {
                ViewModelCombos.remessa = true;
                Combo1 = Bus.ObterInformacoesCombo1("");
            }
                
            else
                Combo1 = Bus.ObterInformacoesCombo1(ListpedidoCod);


            ViewModelCombos.Combo1 = Combo1;
            ViewModelCombos.Combo2 = Combo2;
            ViewModelCombos.Combo3 = Combo3;
            ViewModelCombos.RecebimentoNota = ListNota.Where(a => a.ID_AVALARA == Convert.ToInt32(Session["IdAvalara"])).ToList();
            ViewModelCombos.RecebimentoNotaRM = ListNotaRM;
            ViewModelCombos.Compra_item_Avalara = Item_compra;
            ViewModelCombos.item_Avalara = item_Avalara;
            ViewModelCombos.cfop_Avalara = cfop_Avalara;
            ViewModelCombos.ListNotaDetalhe = ListNotaDetalhe;
            ViewModelCombos.NCM_Avalara = ncm_Avalara;
            ViewModelCombos.ListNotaVinculada= ListNotaVinculada;
            ViewModelCombos.SelectIDMOV = SelectIDMOV;

            return View(ViewModelCombos);
        }

    }
}