using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;


namespace TAMINTEGRA.Controllers
{
    public class AtualiaAvalaraController : Controller
    {
        // GET: AtualiaAvalara

        string UsuarioAvalara = System.Configuration.ConfigurationSettings.AppSettings["UsuarioAvalara"].ToString();
        string SenhaAvalara = System.Configuration.ConfigurationSettings.AppSettings["SenhaAvalara"].ToString();
        List<AvalaraTotalNotas> totalnotas = new List<AvalaraTotalNotas>();
        WS_Avalara_Connect.ConsumoWSClient ws = new WS_Avalara_Connect.ConsumoWSClient();
        Business.RecebimentoBUS Bus = new Business.RecebimentoBUS();
        DateTime today = DateTime.Now;

        public ActionResult Index()
        {

            //var RetornaChavesNFePelaDataEntradaXml = ws.RetornaChavesNFePelaDataEntradaXml(UsuarioAvalara, SenhaAvalara, "", "01/04/2021", "", "", "");
            //RetornaChavesNFePelaDataEntradaXml = RetornaChavesNFePelaDataEntradaXml.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
            //RetornaChavesNFePelaDataEntradaXml rooT2 = JsonConvert.DeserializeObject<RetornaChavesNFePelaDataEntradaXml>(TAMIntegra.Helpers.Utils.ResultXML(RetornaChavesNFePelaDataEntradaXml));

            //var RetornaChavesNFePelaEmissao = ws.RetornaChavesNFePelaEmissao(UsuarioAvalara, SenhaAvalara, "", "01/04/2021", "", "", "");
            //RetornaChavesNFePelaEmissao = RetornaChavesNFePelaEmissao.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
            // Root_Ava_DownloadNFe rooT3 = JsonConvert.DeserializeObject<Root_Ava_DownloadNFe>(TAMIntegra.Helpers.Utils.ResultXML(RetornaChavesNFePelaEmissao));

            ////var RetornaChavesNFSeAprovadasPelaEmissao = ws.RetornaChavesNFSeAprovadasPelaEmissao(UsuarioAvalara, SenhaAvalara, "","01/04/2021","","","","");
            ////RetornaChavesNFSeAprovadasPelaEmissao = RetornaChavesNFSeAprovadasPelaEmissao.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
            ////Root_Ava_DownloadNFe rooT4 = JsonConvert.DeserializeObject<Root_Ava_DownloadNFe>(TAMIntegra.Helpers.Utils.ResultXML(RetornaChavesNFSeAprovadasPelaEmissao));


           

            //var DownloadNfse = ws.DownloadNfse(UsuarioAvalara, SenhaAvalara,"","01/04/2021","");
            //DownloadNfse = DownloadNfse.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
            //Root_Ava_DownloadNFe root1 = JsonConvert.DeserializeObject<Root_Ava_DownloadNFe>(TAMIntegra.Helpers.Utils.ResultXML(DownloadNFe));


            ////DownloadpdfNFe 
            ////var DownloadpdfNFe = ws.DownloadPdfNfe(UsuarioAvalara, SenhaAvalara, "35210511088644000108550010000249911051205326");
            ////DownloadNFe = DownloadNFe.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
            ////Root_Ava_DownloadNFe rootPDF = JsonConvert.DeserializeObject<Root_Ava_DownloadNFe>(TAMIntegra.Helpers.Utils.ResultXML(DownloadNFe));
            ////totalnotas.Add(new AvalaraTotalNotas { totalDOwnloadNfePDF = 150, totalDOwnloadNfe = 300 });

            totalnotas.Add(new AvalaraTotalNotas { totalDOwnloadNfePDF = 150, totalDOwnloadNfe = 300 });




            return View(totalnotas);
        }

        public ActionResult RetornaChavesNFePelaDataEntradaXml()
        {
           
            var RetornaChavesNFePelaDataEntradaXml = ws.RetornaChavesNFePelaDataEntradaXml(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-3).ToShortDateString(), "", "", "");            
            RetornaChavesNFePelaDataEntradaXml = RetornaChavesNFePelaDataEntradaXml.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
            Avalara_RetornaChavesNFePelaDataEntrada entradaxml = JsonConvert.DeserializeObject<Avalara_RetornaChavesNFePelaDataEntrada>(TAMIntegra.Helpers.Utils.ResultXML(RetornaChavesNFePelaDataEntradaXml));

            var cStat = entradaxml.retChavesNfe.cStat;
            var dhResp = entradaxml.retChavesNfe.dhResp;
            var Quantidade = entradaxml.retChavesNfe.Quantidade;
            var xMotivo = entradaxml.retChavesNfe.xMotivo;

            int ID = Bus.Insertnfepelaentrada(entradaxml);

            Session["totalentrada"] = ID;

            ///DownloadNFe();

            return View("Index");
        }

        public ActionResult DownloadNFe(string numeronfe)
        {
            List<RetornoNfe> Notaporentrada = new List<RetornoNfe>();

            Notaporentrada = Bus.IobterInfoEntrada();

            //foreach (var item in Notaporentrada)
            //{
                ////DownloadNFe 
               //var seentrada = ws.RetornaXMLNFesPelaEmissao(UsuarioAvalara, SenhaAvalara, "52045457000892", today.ToShortDateString(),"","","");
               //var seentradachaves = ws.RetornaChavesNFSePelaEntrada(UsuarioAvalara, SenhaAvalara, "52045457000892", today.ToShortDateString(), "", "", "","");


            var sesaida = ws.RetornaXMLNFSePelaEntrada(UsuarioAvalara, SenhaAvalara, "52045457000892", today.ToShortDateString(), "", "", "" );


            var DownloadNFe = ws.DownloadNFe(UsuarioAvalara, SenhaAvalara, numeronfe.ToString());
                DownloadNFe = DownloadNFe.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
                Root_Ava_DownloadNFe InstDownloadNFe = JsonConvert.DeserializeObject<Root_Ava_DownloadNFe>(TAMIntegra.Helpers.Utils.ResultXML(DownloadNFe));


                // validando List              

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(InstDownloadNFe.retDownloadNFe.retorno.Conteudo);
                XmlElement root = doc.DocumentElement;
                XmlNodeList elemList = root.GetElementsByTagName("prod");


                AvalaraInfoNotas AvalaraInfoNotas = new AvalaraInfoNotas();
                AvalaraInfoNotasLista AvalaraInfoNotasLista = new AvalaraInfoNotasLista();

                if (elemList.Count >= 2)
                {
                    AvalaraInfoNotasLista = JsonConvert.DeserializeObject<AvalaraInfoNotasLista>(TAMIntegra.Helpers.Utils.ResultXML(InstDownloadNFe.retDownloadNFe.retorno.Conteudo));
                    int ID = Bus.InsertDownloadNFe(InstDownloadNFe, numeronfe.ToString(), AvalaraInfoNotas, AvalaraInfoNotasLista, 2);
                }

                else
                {
                    AvalaraInfoNotas = JsonConvert.DeserializeObject<AvalaraInfoNotas>(TAMIntegra.Helpers.Utils.ResultXML(InstDownloadNFe.retDownloadNFe.retorno.Conteudo));
                    int ID = Bus.InsertDownloadNFe(InstDownloadNFe, numeronfe.ToString(), AvalaraInfoNotas, AvalaraInfoNotasLista, 1);
                }


            //}

           

            return View("Index");
            
        }

    }
}