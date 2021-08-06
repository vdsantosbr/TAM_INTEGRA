using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public  class RecebimentoBUS
    {
        RecebimentoDAL dalRecebimento = new RecebimentoDAL();
        public List<RecebimentoNota> ObterInformacoesNota()
        {
            return dalRecebimento.ObterInformacoesNota();
        }

        public List<RecebimentoAvalaraDeParaNFSE> ObterRecebimentoDEPARA()
        {
            return dalRecebimento.ObterRecebimentoDEPARA();
        }
        public List<RecebimentoContadores> ObterContadores()
        {
            return dalRecebimento.ObterContadores();
        }

        public void InsertIDMOv(int idavalara, int idmov, string NotaFiscal)
        {
            dalRecebimento.InsertIDMOv(idavalara, idmov, NotaFiscal);
        }

        public void DesvincularDados(int idavalara, string NotaFiscal)
        {
            dalRecebimento.DesvincularDados(idavalara, NotaFiscal);
        }

        public List<Recebimento_AvalaraDetalheNotaPedidoItens> ObterInformacoesNotaDetalheitensPedido(string idMov)
        {
            return dalRecebimento.ObterInformacoesNotaDetalheitensPedido(idMov);
        }

        public List<Recebimento_AvalaraDetalheNotaPedido> ObterInformacoesNotaDetalhePedido(string idMov)
        {
            return dalRecebimento.ObterInformacoesNotaDetalhePedido(idMov);
        }

        public List<SelectIDMOV> SelectIDMOv(int idavalara, string NotaFiscal)
        {
           return dalRecebimento.SelectIDMOv(idavalara, NotaFiscal);
        }

        



        public void salvarAnalise(string nfe, int tiponova)
        {
            dalRecebimento.salvarAnalise(nfe, tiponova);
        }


        public List<RecebimentoNotaRM> ObterInformacoesNotaRM()
        {
            return dalRecebimento.ObterInformacoesNotaRM();
        }

              public List<RecebimentoNotaVinculada> ObterInformacoesNotaVinculada()
        {
            return dalRecebimento.ObterInformacoesNotaVinculada();
        }

        public List<Recebimento_AvalaraDetalheNota> ObterInformacoesNotaDetalhe(string numeronota)
        {
            return dalRecebimento.ObterInformacoesNotaDetalhe(numeronota);
        }

        public void InsertChaveNFSEENTEMI(string CNPJorigem, string cNPJ, DateTime dataEmissaoNFe, string numeroNFe, string v, string ValorXML)
        {
             dalRecebimento.InsertChaveNFSEENTEMI(CNPJorigem, cNPJ, dataEmissaoNFe, numeroNFe, v, ValorXML);
        }

        public List<Recebimento_Tipo_Movimento_Avalara> ObterInformacoesCombo1(string fonte)
        {
            return dalRecebimento.ObterInformacoesCombo1(fonte);
        }


        public List<Recebimento_Class_Fin_Avalara> ObterInformacoesCombo2()
        {
            return dalRecebimento.ObterInformacoesCombo2();
        }

        public List<Recebimento_Forma_Pagto_Avalara> ObterInformacoesCombo3()
        {
            return dalRecebimento.ObterInformacoesCombo3();
        }

        public List<Recebimento_Compra_item_Avalara> ObterInformacoes_CompraItemAvalara(string IDMOV)
        {
            return dalRecebimento.ObterInformacoes_CompraItemAvalara(IDMOV);
        }

        public void InserDownloadNFSEENTEMI(RootRetDownloadNFSes retornaNotaEMI)
        {
            dalRecebimento.InserDownloadNFSEENTEMI(retornaNotaEMI);
        }

        public List<Recebimento_item_Avalara> ObterInformacoes_item_Avalara(int IdAvalara, string IDMOV)
        {
            return dalRecebimento.ObterInformacoes_item_Avalara(IdAvalara, IDMOV);
        }

        public List<Recebimento_CFOP_Avalara> ObterInformacoes_CFOP_Avalara(string cfop)
        {
            return dalRecebimento.ObterInformacoes_CFOP_Avalara(cfop);
        }

        public List<Recebimento_NCM_Avalara> ObterInformacoes_NCM_Avalara(string cfop)
        {
            return dalRecebimento.ObterInformacoes_NCM_Avalara(cfop);
        }


        //insercao de dados avalara
        public int Insertnfepelaentrada(Avalara_RetornaChavesNFePelaDataEntrada Entrada)
        {
            return dalRecebimento.Insertnfepelaentrada(Entrada);
        }
        

        public List<RetornoNfe> IobterInfoEntrada()
        {
            return dalRecebimento.IobterInfoEntrada();
        }

        public int InsertDownloadNFe(Root_Ava_DownloadNFe Entrada, string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas, AvalaraInfoNotasLista AvalaraInfoNotasLista, int cont)
        {
            return dalRecebimento.InsertDownloadNFe(Entrada, ChaveNFE, AvalaraInfoNotas, AvalaraInfoNotasLista, cont);
        }

        public void GuardarInformacoesLog(DateTime horainicio, DateTime now, int vnfe, int vsfev)
        {
             dalRecebimento.GuardarInformacoesLog(horainicio, now, vnfe, vsfev);
        }
    }
}
