using Business;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TAMIntegra.App_Start;

namespace TAMIntegra.Controllers
{
    [CustomAuthorize]
    public class HomeController : BaseController
    {
        private UsuarioBUS uBUS = new UsuarioBUS();
        private HomeBUS homeBUS = new HomeBUS();

        public ActionResult Index(string strDataInicio, string strDataFim, string numeroPO = null, string numProcesso = null)
        {
            DateTime dataTerminoDT = new DateTime();
            DateTime dataInicioDT = new DateTime();
            Home home = new Home();
            List<Home> lstHome = new List<Home>();

            //if (strDataInicio == null)
            //{
            //    strDataInicio = "01/" + DateTime.Today.ToString("MM/yyyy");
            //    dataInicioDT = DateTime.Parse(strDataInicio);
            //}

            //if (strDataFim == null)
            //{
            //    strDataFim = DateTime.Today.ToString("dd/MM/yyyy");
            //    dataTerminoDT = DateTime.Parse(strDataFim);
            //}


            //if (!string.IsNullOrWhiteSpace(strDataInicio))
            //{
            //    dataInicioDT = DateTime.Parse(strDataInicio);
            //    home.strDataInicio = strDataInicio;

            //}

            //if (!string.IsNullOrWhiteSpace(strDataFim))
            //{
            //    dataTerminoDT = DateTime.Parse(strDataFim);
            //    home.strDataFim = strDataFim;
            //}

            //lstHome = homeBUS.Lista(dataInicioDT, dataTerminoDT, numeroPO, numProcesso);
            //home.lstWorkflow = lstHome;

            //if(lstHome != null)
            //{ 
            //List<Home> distinctPO = lstHome
            //  .GroupBy(p => p.PO)
            //  .Select(g => g.First())
            //  .Where(x=>x.PO != null)
            //  .ToList();

            //home.POQtd = distinctPO.Count();

            //List<Home> distinctEDI = lstHome
            //  .GroupBy(p => p.EDI)
            //  .Select(g => g.First())
            //  .Where(x => x.EDI != null)
            //  .ToList();
            //home.EDIQtd = distinctEDI.Count();


            //List<Home> distinctInvoice = lstHome
            //             .GroupBy(p => p.Invoice)
            //             .Select(g => g.First())
            //             .Where(x => x.Invoice != null)
            //             .ToList();
            //home.InvoiceQtd = distinctInvoice.Count();


            //List<Home> distinctNFEstoque = lstHome
            //  .GroupBy(p => p.NFEstoque)
            //  .Select(g => g.First())
            //  .Where(x => x.NFEstoque != null)
            //  .ToList();
            //home.NFEstoqueQtd = distinctNFEstoque.Count();

            //List<Home> distinctProcesso = lstHome
            //  .GroupBy(p => p.Processo)
            //  .Select(g => g.First())
            //  .Where(x => x.Processo != null)
            //  .ToList();
            //home.ProcessoQtd = distinctProcesso.Count();

            //List<Home> distinctInconsistencia = lstHome
            //  .GroupBy(p => p.Inconsistencia)
            //  .Select(g => g.First())
            //  .Where(x => x.Inconsistencia != null)
            //  .ToList();
            //home.InconsistenciaQtd = distinctInconsistencia.Count();
            //}

            //return View(home);
            return RedirectToAction("../RecebimentoAvalara");
        }
        public ActionResult Index_old()
        {
            ViewBag.Role = "";
            return View();
        }


        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult GraficoGerencial(string tipo)
        //{
        //    List<GraficoDetalhe> graph = new List<GraficoDetalhe>();
        //    IEnumerable<Usuario> lstU;
        //    DateTime inicioSemana = DateTime.Now;
        //    DateTime terminoSemana = DateTime.Now;
        //    int delta = 0;
        //    int totalMeta = 0;
        //    int totalRealizado = 0;
        //    double totalGeral = 0;

        //    //================

       
        //    ViewData["totalGeral"] = 100;
        //    ViewData["totalMeta"] = 50;

          

        //    //================

        //    return Json(graph);
        //}

       
    }
}