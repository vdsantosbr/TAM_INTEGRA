using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TAMIntegra.Helpers;

namespace TAMIntegraAvalara.Console
{
    class Program
    {
        static string UsuarioAvalara = "tam.webservice";
        static string SenhaAvalara = "Avalara2021!";
        static List<AvalaraTotalNotas> totalnotas = new List<AvalaraTotalNotas>();
        
        static Business.RecebimentoBUS Bus = new Business.RecebimentoBUS();
        static DateTime today = DateTime.Now;
        static DateTime horainicio = new DateTime();



        static void Main(string[] args)
        {
            System.Console.BackgroundColor = ConsoleColor.White;
            System.Console.ForegroundColor = ConsoleColor.Red;

            //IMPORTANDO NOTAS NFSE
            RetornaChavesNfse();

            //IMPORTANDO NOTAS NFE
            RetornaChavesNFePelaDataEntradaXml();
            System.Console.ReadKey();
        }

        public static void RetornaChavesNfse()
        {
            var ws = new WS_Avalara_Connect.ConsumoWSClient();
            horainicio = DateTime.Now;
            System.Console.WriteLine("Data / Hora inicio" + DateTime.Now);
            System.Console.WriteLine("......>>>>");
            System.Console.WriteLine("......>>>>");
            System.Console.WriteLine("Buscando Chaves de Notas Fiscais [ NFSE ] ");
            Bus.GuardarInformacoesLog(horainicio, DateTime.Now, 0, 0);


            List<string> ListCNPJ = new List<string>();

            ListCNPJ.Add("52045457000116");
            ListCNPJ.Add("52045457000892");
            ListCNPJ.Add("52045457000973");
            ListCNPJ.Add("52045457001198");


            foreach (var item in ListCNPJ)
            {
                var nfepelaemissao = ws.RetornaChavesNFSePelaEmissao(UsuarioAvalara, SenhaAvalara, item, today.AddDays(-30).Day.ToString() + "/" + today.AddMonths(-1).Month.ToString() + "/" + today.Year.ToString(), "", "", "", "");
                var nfepelaentrada = ws.RetornaChavesNFSePelaEntrada(UsuarioAvalara, SenhaAvalara, item, today.AddDays(-30).Day.ToString() + "/" + today.AddMonths(-1).Month.ToString() + "/" + today.Year.ToString(), "", "", "", "");

               var RetornaJsonPelaentrada = JsonConvert.DeserializeObject<RecebimentoAvalaraNfse>(Utils.ResultXML(nfepelaentrada));

                if (RetornaJsonPelaentrada.retChavesNfse.retorno != null)
                {

                    System.Console.WriteLine("Chaves encontradas...");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("Total de Chaves: " + RetornaJsonPelaentrada.retChavesNfse.retorno.NFSe.Count());
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("Processando Notas Fiscais  [ NFSE EN ] ");
                    int cont = 0;

                    foreach (var itemchaveentrada in RetornaJsonPelaentrada.retChavesNfse.retorno.NFSe)
                    {
                        
                        //Guardando as Chaves..
                     
                        
                        //Guardando as notas
                        var nfsedownload = ws.DownloadNfse(UsuarioAvalara, SenhaAvalara, itemchaveentrada.NumeroNFe, itemchaveentrada.DataEmissaoNFe.Day.ToString() + "/" + itemchaveentrada.DataEmissaoNFe.Month.ToString() + "/" + itemchaveentrada.DataEmissaoNFe.Year.ToString()  , itemchaveentrada.CNPJ);

                        Bus.InsertChaveNFSEENTEMI(item, itemchaveentrada.CNPJ, itemchaveentrada.DataEmissaoNFe, itemchaveentrada.NumeroNFe, "EN", nfsedownload.ToString());

                        var RetornaNotaEMI = JsonConvert.DeserializeObject<RootRetDownloadNFSes>(Utils.ResultXML(nfsedownload));

                        cont++;

                        System.Console.WriteLine("Processando { " + cont + " }  de " + RetornaJsonPelaentrada.retChavesNfse.retorno.NFSe.Count() + " Notas Fiscais...");
                        System.Console.WriteLine("......>>>>");
                        System.Console.WriteLine("Filial de :  " + item.ToString());
                        System.Console.WriteLine("......>>>>");
                        System.Console.WriteLine("......>>>>");


                        if (RetornaNotaEMI.retDownloadNFSes.retorno.NFSe != null)
                            Bus.InserDownloadNFSEENTEMI(RetornaNotaEMI);



                    }
                }


                var RetornaJsonPelemissao = JsonConvert.DeserializeObject<RecebimentoAvalaraNfse>(Utils.ResultXML(nfepelaemissao));

                if (RetornaJsonPelemissao.retChavesNfse.retorno != null)
                {

                    System.Console.WriteLine("Chaves encontradas...");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("Total de Chaves: " + RetornaJsonPelaentrada.retChavesNfse.retorno.NFSe.Count());
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("Processando Notas Fiscais  [ NFSE EM ] ");
                    int cont = 0;


                    foreach (var itemchaveentrada in RetornaJsonPelemissao.retChavesNfse.retorno.NFSe)
                    {
                        

                        var nfsedownload = ws.DownloadNfse(UsuarioAvalara, SenhaAvalara, itemchaveentrada.NumeroNFe, itemchaveentrada.DataEmissaoNFe.Day.ToString() + "/" + itemchaveentrada.DataEmissaoNFe.Month.ToString() + "/" + itemchaveentrada.DataEmissaoNFe.Year.ToString(), itemchaveentrada.CNPJ);

                        Bus.InsertChaveNFSEENTEMI(item, itemchaveentrada.CNPJ, itemchaveentrada.DataEmissaoNFe, itemchaveentrada.NumeroNFe, "EM", nfsedownload);

                        var RetornaNotaEMI = JsonConvert.DeserializeObject<RootRetDownloadNFSes>(Utils.ResultXML(nfsedownload));

                        cont++;

                        System.Console.WriteLine("Processando { " + cont + " }  de " + RetornaJsonPelaentrada.retChavesNfse.retorno.NFSe.Count() + " Notas Fiscais...");
                        System.Console.WriteLine("......>>>>");
                        System.Console.WriteLine("Filial de :  " + item.ToString());
                        System.Console.WriteLine("......>>>>");
                        System.Console.WriteLine("......>>>>");


                        if (RetornaNotaEMI.retDownloadNFSes.retorno.NFSe != null)
                            Bus.InserDownloadNFSEENTEMI(RetornaNotaEMI);

                    }
                }
                

               


            }

       

            


        }



        public static void RetornaChavesNFePelaDataEntradaXml()
        {
            horainicio = DateTime.Now;
            var ws = new WS_Avalara_Connect.ConsumoWSClient();
            System.Console.WriteLine("Data / Hora inicio" + DateTime.Now);
            System.Console.WriteLine("......>>>>");
            System.Console.WriteLine("......>>>>");
            System.Console.WriteLine("Buscando Chaves de Notas Fiscais... [ NFE ]");
             Bus.GuardarInformacoesLog(horainicio, DateTime.Now, 0, 0);



            // var RetornaChavesNFePelaDataEntradaXml = ws.RetornaChavesNFePelaDataEntradaXml(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-30).ToShortDateString(), "", "", "");
            var RetornaChavesNFePelaDataEntradaXml = ws.RetornaChavesNFePelaDataEntradaXml(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-30).Day.ToString() + "/" + today.AddMonths(-1).Month.ToString() + "/" + today.Year.ToString(), "", "", "");
            
            RetornaChavesNFePelaDataEntradaXml = RetornaChavesNFePelaDataEntradaXml.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
            Avalara_RetornaChavesNFePelaDataEntrada entradaxml = JsonConvert.DeserializeObject<Avalara_RetornaChavesNFePelaDataEntrada>(Utils.ResultXML(RetornaChavesNFePelaDataEntradaXml));

            var cStat = entradaxml.retChavesNfe.cStat;
            var dhResp = entradaxml.retChavesNfe.dhResp;
            var Quantidade = entradaxml.retChavesNfe.Quantidade;
            var xMotivo = entradaxml.retChavesNfe.xMotivo;

            int ID = Bus.Insertnfepelaentrada(entradaxml);

            

            DownloadNFe(Quantidade,  entradaxml);

        }

        public static void DownloadNFe(int total, Avalara_RetornaChavesNFePelaDataEntrada entradaxml)
        {
            WS_Avalara_Connect.ConsumoWSClient ws = new WS_Avalara_Connect.ConsumoWSClient();

            List<RetornoNfe> Notaporentrada = new List<RetornoNfe>();

            Notaporentrada = Bus.IobterInfoEntrada();
            Bus.GuardarInformacoesLog(horainicio, DateTime.Now, total, 0);
            System.Console.WriteLine("Chaves encontradas...");
            System.Console.WriteLine("......>>>>");
            System.Console.WriteLine("......>>>>");
            System.Console.WriteLine("Total de Chaves: " + total);
            System.Console.WriteLine("......>>>>");
            System.Console.WriteLine("......>>>>");
            System.Console.WriteLine("Processando Notas Fiscais...");

            //var seentrada = ws.RetornaChavesNFSePelaEmissao(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-10).ToShortDateString(), "", "", "", "");
            //var sesaida = ws.RetornaChavesNFSePelaEntrada(UsuarioAvalara, SenhaAvalara, "", today.AddDays(-10).ToShortDateString(), "", "", "", "");


            int cont = 0;
            foreach (var item in entradaxml.retChavesNfe.retorno.ChaveNFe)
            {
                ////DownloadNFe 

                cont++;

                var DownloadNFe = ws.DownloadNFe(UsuarioAvalara, SenhaAvalara, item.ToString());
                DownloadNFe = DownloadNFe.ToString().Replace("<xml version=\"1.0\">", "").Replace("</xml>", "");
                Root_Ava_DownloadNFe InstDownloadNFe = JsonConvert.DeserializeObject<Root_Ava_DownloadNFe>(Utils.ResultXML(DownloadNFe));


                // validando List              

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(InstDownloadNFe.retDownloadNFe.retorno.Conteudo);
                XmlElement root = doc.DocumentElement;
                XmlNodeList elemList = root.GetElementsByTagName("prod");


                AvalaraInfoNotas AvalaraInfoNotas = new AvalaraInfoNotas();
                AvalaraInfoNotasLista AvalaraInfoNotasLista = new AvalaraInfoNotasLista();

                if (elemList.Count >= 2)
                {
                    AvalaraInfoNotasLista = JsonConvert.DeserializeObject<AvalaraInfoNotasLista>(Utils.ResultXML(InstDownloadNFe.retDownloadNFe.retorno.Conteudo));
                    int ID = Bus.InsertDownloadNFe(InstDownloadNFe, item.ToString(), AvalaraInfoNotas, AvalaraInfoNotasLista, 2);
                }

                else
                {
                    AvalaraInfoNotas = JsonConvert.DeserializeObject<AvalaraInfoNotas>(Utils.ResultXML(InstDownloadNFe.retDownloadNFe.retorno.Conteudo));
                    int ID = Bus.InsertDownloadNFe(InstDownloadNFe, item.ToString(), AvalaraInfoNotas, AvalaraInfoNotasLista, 1);
                }


               System.Console.WriteLine("Processando { " + cont + " }  de " + total + " Notas Fiscais...");
               System.Console.WriteLine("......>>>>");
               System.Console.WriteLine("Chave:  " + item.ToString());
               System.Console.WriteLine("......>>>>");
               System.Console.WriteLine("......>>>>");

                if(cont== total)
                {
                    
                    var texto = "Download de Notas finalizada com sucesso!";         
                    Bus.GuardarInformacoesLog(horainicio, DateTime.Now, total, 0);
                    System.Console.WriteLine("Download de Notas finalizada com sucesso!");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("Data / Hora  Inicio : " + horainicio);
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("......>>>>");
                    System.Console.WriteLine("Data / Hora Fim : " + DateTime.Now);
                }

            }

        }
    }
}
