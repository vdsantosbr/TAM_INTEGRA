using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAMIntegra.Controllers
{
    public class ErroController : BaseController
    {
        // GET: Erro
        public ActionResult Erro404()
        {
            return View();
        }

        public ActionResult AcessoNegado()
        {
            return View();
        }
    }
}