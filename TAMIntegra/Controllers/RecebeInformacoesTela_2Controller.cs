using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAMINTEGRA.Controllers
{
    public class RecebeInformacoesTela_2Controller : Controller
    {
        // GET: RecebeInformacoesTela_2
        Business.RecebimentoBUS Bus = new Business.RecebimentoBUS();
        List<RecebimentoNota> ListNota = new List<RecebimentoNota>();
        List<RecebimentoNotaRM> ListNotaRM = new List<RecebimentoNotaRM>();
        List<RecebimentoNotaVinculada> ListNotaVinculada = new List<RecebimentoNotaVinculada>();
        List<NomeClienteNota> Lst = new List<NomeClienteNota>();
        RecebimentoViewModel ViewModel = new RecebimentoViewModel();
        List<Recebimento_AvalaraDetalheNota> ListNotaDetalhe = new List<Recebimento_AvalaraDetalheNota>();
        public ActionResult Index(string Descricaobusca = null)
        {
         
            ListNota = Bus.ObterInformacoesNota();
            ListNotaRM = Bus.ObterInformacoesNotaRM();
            ListNotaVinculada = Bus.ObterInformacoesNotaVinculada();
           

            ListNota = ListNota.Where(a => a.Situacao != "AN").ToList();

            if (Descricaobusca == null | Descricaobusca == "")
            {
                foreach (var item in ListNota)
                {
                    if (Lst.Count() == 0)
                        Lst.Add(new NomeClienteNota { NomeCliente = item.NOME, CGCCFO = item.CGCCFO, ID_AVALARA = item.ID_AVALARA });
                    else
                    {
                        if (Lst.Where(a => a.NomeCliente == item.NOME).ToList().Count == 0)
                        {
                            Lst.Add(new NomeClienteNota { NomeCliente = item.NOME, CGCCFO = item.CGCCFO, ID_AVALARA = item.ID_AVALARA });
                        }
                    }



                }

                Lst = Lst.Distinct().ToList();

                ViewModel.recebimentoNota = ListNota;
                ViewModel.NomeCliente = Lst;
                ViewModel.recebimentoNotaRM = ListNotaRM;
                ViewModel.ListNotaVinculada = ListNotaVinculada;
            }
            else
            {
                ListNota = ListNota.Where(a => a.NOME.ToUpper().Contains(Descricaobusca.ToUpper()) || a.CGCCFO.Contains(Descricaobusca.ToUpper()) || a.NF_NUMERO.Contains(Descricaobusca.ToUpper())).ToList();
                ViewModel.recebimentoNota = ListNota;
                ViewModel.recebimentoNotaRM = ListNotaRM;
                ViewModel.ListNotaVinculada = ListNotaVinculada;
            }
            


           

            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult BuscaNota(string Descricaobusca)
        {
            ListNota = Bus.ObterInformacoesNota();

            ListNota = ListNota.Where(a => a.CGCCFO == Descricaobusca).ToList();

            ViewModel.recebimentoNota = ListNota;

            return RedirectToAction("/index");
        }


        public ActionResult  DesvincularAnalise(int id, string NotaFiscal)
        {
            Bus.DesvincularDados(id, NotaFiscal);                        
            return RedirectToAction("Index", new { Descricaobusca = NotaFiscal });

        }

    }   

}