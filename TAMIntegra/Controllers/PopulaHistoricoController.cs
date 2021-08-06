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

namespace TAMINTEGRA.Controllers
{
    public class PopulaHistoricoController : Controller
    {
        // GET: PopulaHistorico
        private StatementConciliacaoBUS concBUS = new StatementConciliacaoBUS();
        public ActionResult Index()
        {
            PopulaHistorico obj = new PopulaHistorico();
            string invoice = "CJ05154546";
            obj.lstPopulaHistorico = concBUS.PopulaHistorico(invoice).ToList();
            return View(obj);
        }
    }
}