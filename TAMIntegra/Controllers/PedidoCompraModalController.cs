using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using Business;

namespace TAMIntegra.Controllers
{
    public class PedidoCompraModalController : Controller
    {

        public Compra comprador = new Compra();
        public CompradorBUS compradorBUS = new CompradorBUS();
        // GET: PedidoCompraModal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FiltroPedido(string id, string dataEmissao)
        {
            int id_Perfil = Convert.ToInt32(Session["Id_Perfil"]);
            List<Compra> lstComprador = new List<Compra>();
            DateTime dataEmissaoDT = DateTime.Parse(dataEmissao);

            lstComprador = compradorBUS.PedidoCompraLista(dataEmissaoDT, dataEmissaoDT, null, null, null, id, id_Perfil, "frmComprasCessnaPedidoCompra", null,0, 0);

            IEnumerable<Compra> result = lstComprador;
            comprador.ListaComprador = result.ToList();

            //return View(comprador);
            return RedirectToAction("Index", "Compra", new { numeroMov = id, dataInicio = dataEmissao });
        }
    }
}