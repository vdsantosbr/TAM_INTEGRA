using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace TAMINTEGRA.Controllers
{
    public class RecebimentoAvalaraController : Controller
    {
        // GET: RecebimentoAvalara
        Business.RecebimentoBUS Bus = new Business.RecebimentoBUS();        
        List<RecebimentoNota> ListNota = new List<RecebimentoNota>();
        RecebimentoViewModel ViewModel = new RecebimentoViewModel();
        WS_Avalara_Connect.ConsumoWSClient ws = new WS_Avalara_Connect.ConsumoWSClient();
        string UsuarioAvalara = System.Configuration.ConfigurationSettings.AppSettings["UsuarioAvalara"].ToString();
        string SenhaAvalara = System.Configuration.ConfigurationSettings.AppSettings["SenhaAvalara"].ToString();
        DateTime today = DateTime.Now;

        public ActionResult Index()
        {
            List<RecebimentoContadores> contadores = new List<RecebimentoContadores>();
            ListNota = Bus.ObterInformacoesNota();
            ListNota = ListNota.Where(a => a.Situacao == "AN").ToList();

            Session["ListNota"] = ListNota;

            contadores = Bus.ObterContadores();

            ViewModel.Contadores = contadores;
            ViewModel.recebimentoNota = ListNota.Where(a=>a.Situacao!=null || a.Situacao!="NULL").ToList();

            return View(ViewModel);
        }

        public ActionResult RetornaContadores()
        {
            //List<RecebimentoContadores> contadores = new List<RecebimentoContadores>();

            //contadores = Bus.ObterContadores();            

            var RetornaChavesNFePelaDataEntradaXml = ws.RetornaChavesNFePelaDataEntradaXml(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-3).ToShortDateString(), "", "", "");

            List<RetornoNfe> Notaporentrada = new List<RetornoNfe>();

            Notaporentrada = Bus.IobterInfoEntrada();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(RetornaChavesNFePelaDataEntradaXml);
            string json = JsonConvert.SerializeXmlNode(doc);

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}