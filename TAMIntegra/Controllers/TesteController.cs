using Business;
using Entities;
using QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;

namespace TAMIntegra.Controllers
{

    public class TesteController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Modal(string teste = null)
        {
            Home h = new Home();
            return PartialView();
        }

    }

}