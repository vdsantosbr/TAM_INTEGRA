using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TAMIntegra.Helpers;

namespace TAMIntegraConsole.NOTAS
{
    class Program
    {
        static string UsuarioAvalara = System.Configuration.ConfigurationSettings.AppSettings["UsuarioAvalara"].ToString();
        static string SenhaAvalara = System.Configuration.ConfigurationSettings.AppSettings["SenhaAvalara"].ToString();
        List<AvalaraTotalNotas> totalnotas = new List<AvalaraTotalNotas>();
        static WS_Avalara_Connect.ConsumoWSClient ws = new WS_Avalara_Connect.ConsumoWSClient();
        static Business.RecebimentoBUS  Bus = new Business.RecebimentoBUS();
        static DateTime today = DateTime.Now;
        

        static void Main(string[] args)
        {
           // RetornaChavesNFePelaDataEntradaXml();
            Console.ReadKey();
        }

        public static void RetornaChavesNFePelaDataEntradaXml()
        {
            Console.WriteLine("Data / Hora inicio"  + DateTime.Now );
            Console.WriteLine("Buscando Chaves de Notas...");

            var RetornaChavesNFePelaDataEntradaXml = ws.RetornaChavesNFePelaDataEntradaXml(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-30).ToShortDateString(), "", "", "");
            RetornaChavesNFePelaDataEntradaXml = RetornaChavesNFePelaDataEntradaXml.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
            Avalara_RetornaChavesNFePelaDataEntrada entradaxml = JsonConvert.DeserializeObject<Avalara_RetornaChavesNFePelaDataEntrada>(Utils.ResultXML(RetornaChavesNFePelaDataEntradaXml));

            var cStat = entradaxml.retChavesNfe.cStat;
            var dhResp = entradaxml.retChavesNfe.dhResp;
            var Quantidade = entradaxml.retChavesNfe.Quantidade;
            var xMotivo = entradaxml.retChavesNfe.xMotivo;

            int ID = Bus.Insertnfepelaentrada(entradaxml);


            

            DownloadNFe();

        }

        public static  void DownloadNFe()
        {
            List<RetornoNfe> Notaporentrada = new List<RetornoNfe>();

            Notaporentrada = Bus.IobterInfoEntrada();

            Console.WriteLine("Chaves Baixadas...");
            Console.WriteLine("Total de Chaves ..." + Notaporentrada.Count() );

            Console.WriteLine("Processando Notas Fiscais...");

            //var seentrada = ws.RetornaChavesNFSePelaEmissao(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-10).ToShortDateString(), "", "", "", "");
            //var sesaida = ws.RetornaChavesNFSePelaEntrada(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-10).ToShortDateString(), "", "", "", "");


            int cont = 0;
            foreach (var item in Notaporentrada)
            {
                ////DownloadNFe 

                cont++;

                var DownloadNFe = ws.DownloadNFe(UsuarioAvalara, SenhaAvalara, Notaporentrada.ToString());
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
                    int ID = Bus.InsertDownloadNFe(InstDownloadNFe, Notaporentrada.ToString(), AvalaraInfoNotas, AvalaraInfoNotasLista, 2);
                }

                else
                {
                    AvalaraInfoNotas = JsonConvert.DeserializeObject<AvalaraInfoNotas>(TAMIntegra.Helpers.Utils.ResultXML(InstDownloadNFe.retDownloadNFe.retorno.Conteudo));
                    int ID = Bus.InsertDownloadNFe(InstDownloadNFe, Notaporentrada.ToString(), AvalaraInfoNotas, AvalaraInfoNotasLista, 1);
                }


                Console.WriteLine("Processando " + cont + "  de " + Notaporentrada.Count());


            } 

        }
    }
}
