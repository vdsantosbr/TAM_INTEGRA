using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAMINTEGRA.Controllers
{
    public class RecebimentoAvalaraDetalheController : Controller
    {
        // GET: RecebimentoAvalaraDetalhe
        Business.RecebimentoBUS Bus = new Business.RecebimentoBUS();
        List<Recebimento_AvalaraDetalheNota> ListNotaDetalhe = new List<Recebimento_AvalaraDetalheNota>();
        RecebimentoViewModel DetalhePedidosItens = new RecebimentoViewModel();

        List<Recebimento_AvalaraDetalheNotaPedido> ListNotaDetalhePedido = new List<Recebimento_AvalaraDetalheNotaPedido>();
        List<Recebimento_AvalaraDetalheNotaPedidoItens> ListNotaDetalhePedidoItens = new List<Recebimento_AvalaraDetalheNotaPedidoItens>();

        public ActionResult Index(string numeronota, string CNPJ)
        {

            ListNotaDetalhe = Bus.ObterInformacoesNotaDetalhe(numeronota);
            ListNotaDetalhe = ListNotaDetalhe.Where(a => a.EMI_CNPJ == CNPJ).ToList();

            return View(ListNotaDetalhe);
        }

        public ActionResult DetalhePedido(string IdMov)
        {

            ListNotaDetalhePedido = Bus.ObterInformacoesNotaDetalhePedido(IdMov);
            ListNotaDetalhePedidoItens = Bus.ObterInformacoesNotaDetalheitensPedido(IdMov);

            DetalhePedidosItens.DetalhePedidos = ListNotaDetalhePedido;
            DetalhePedidosItens.DetalhePedidosItens = ListNotaDetalhePedidoItens;

            return View(DetalhePedidosItens);
        }
    }
}