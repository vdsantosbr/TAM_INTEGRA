using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAMINTEGRA.Controllers
{
    public class RecebimentoAvalaraParte_3Controller : Controller
    {
        // GET: RecebimentoAvalaraParte_3

        Business.RecebimentoBUS Bus = new Business.RecebimentoBUS();
       
        public ActionResult Index(Array IdMove, int IdAvalara, string pedidoremessa, Array IdCodtmv)
        {
            Session["IdAvalara"] = IdAvalara;
            Session["pedidoremessa"] = pedidoremessa;
            Session["IdMove"] = IdMove;
            Session["IdCodtmv"] = IdCodtmv;

            return View();
            //return RedirectToAction("../RecebimentoAvalaraPedidos/PedidoRecebido/?", new { id = IdAvalara });
        }


        public ActionResult SalvarAnalise(string cnpj )
        {
            Bus.salvarAnalise(cnpj, Convert.ToInt16(Session["tiponota"]));

            return RedirectToAction("/Recebimentoavalara/Index");
            
        }


    }
}
