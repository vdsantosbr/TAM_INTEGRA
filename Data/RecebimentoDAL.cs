using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RecebimentoDAL
    {
        public List<RecebimentoNota> ObterInformacoesNota()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {
                    var linhas = db.Database.SqlQuery<RecebimentoNota>("EXEC STO_S_Avalara_DownloadNFe_Emit").ToList();
                    return linhas;
                }
            }

        }

        public List<RecebimentoAvalaraDeParaNFSE> ObterRecebimentoDEPARA()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {
                    var linhas = db.Database.SqlQuery<RecebimentoAvalaraDeParaNFSE>("EXEC STO_S_RM_PROD_AVALARA_RM").ToList();
                    return linhas;
                }
            }

        }

        public List<RecebimentoContadores> ObterContadores()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {
                    var linhas = db.Database.SqlQuery<RecebimentoContadores>("EXEC STO_S_Avalara_CONTADORES").ToList();
                    return linhas;
                }
            }

        }

        public List<Recebimento_AvalaraDetalheNotaPedidoItens> ObterInformacoesNotaDetalheitensPedido(string idMov)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {

                    SqlParameter SidMov = new SqlParameter("@IDMOV", idMov);
                    var linhas = db.Database.SqlQuery<Recebimento_AvalaraDetalheNotaPedidoItens>("STO_S_RM_PEDIDO_COMPRA_ITEM_IDMOv @IDMOV", SidMov).ToList();
                    return linhas;
                }
            }
        }

        public List<Recebimento_AvalaraDetalheNotaPedido> ObterInformacoesNotaDetalhePedido(string idMov)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {
                    SqlParameter SidMov = new SqlParameter("@IDMOV", idMov);
                    var linhas = db.Database.SqlQuery<Recebimento_AvalaraDetalheNotaPedido>("EXEC STO_S_RM_PEDIDO_COMPRA_IDMOV " + Convert.ToInt32(idMov)).ToList();
                    return linhas;
                }
            }
        }

        public void InsertChaveNFSEENTEMI(string CNPJorigem, string cNPJ, DateTime dataEmissaoNFe, string numeroNFe, string v, string ValorXML)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                {

                    db.Database.SqlQuery<RecebimentoNota>("insert into Avalara_ChavesNFSE_Entrada_Emissao (CNPJOrigem, CNPJ , DataEmissaoNFSE, NumeroNFSE,  TipoNota, ValorXML) values ('" + CNPJorigem + "' , '" + cNPJ + "' , '" + dataEmissaoNFe + "','" + numeroNFe + "' ,'" + v + "', '" + ValorXML.Replace("'"," ") + "' )").ToList();
                }
            }
        }

        public void InsertIDMOv(int idavalara, int idmov, string NotaFiscal)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                {
                    // SqlParameter Scnpj = new SqlParameter("@CNPJtesteE", nfe);
                    db.Database.SqlQuery<RecebimentoNota>("insert into Avalara_RM_IdMov (IdAvalara, IdMov, NotaFiscal) values (" + idavalara + " , " + idmov + "," + NotaFiscal + " )").ToList();
                    db.Database.SqlQuery<RecebimentoNota>("EXEC STO_U_Avalara_DownloadNFe_Emit_Validar").ToList();

                }
            }

        }

        public void InserDownloadNFSEENTEMI(RootRetDownloadNFSes retornaNotaEMI)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                {

                    var sqlverifica = "select * from Avalara_DownloadChavesNFSE_Entrada_Emissao where NumeroNFe=  '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ChaveNFe.NumeroNFe + "'";
                   var verificado =  db.Database.SqlQuery<RetDownloadNFSes>(sqlverifica).ToList();

                    if (verificado.Count() == 0)

                    {
                        var sqlinser = "insert into Avalara_DownloadChavesNFSE_Entrada_Emissao " +
                            " (NumeroDocumento, InscricaoPrestador, NumeroNFe, CodigoVerificacao, DataEmissaoNFe, NumeroLote, SerieRPS, NumeroRPS, TipoRPS, DataEmissaoRPS, CPFCNPJPrestador, " +
                            " RazaoSocialPrestador, RazaoSocialTomador,  TipoLogradouro, Logradouro , NumeroEndereco, Bairro, Cidade, UF, CEP, EmailPrestador, StatusNFe, TributacaoNFe, OpcaoSimples, ValorServicos,  " +
                            " ValorPIS, ValorCOFINS, ValorINSS, ValorIR, ValorCSLL, CodigoServico, ValorISS, ValorCredito, ISSRetido, CPFCNPJTomador, Discriminacao) values ('" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.NumeroDocumento
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ChaveNFe.InscricaoPrestador
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ChaveNFe.NumeroNFe
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ChaveNFe.CodigoVerificacao
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.DataEmissaoNFe
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.NumeroLote
                            + "' ,' 0 '"
                            + " , ' 0  '"
                            + " , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.TipoRPS
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.DataEmissaoRPS
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.CPFCNPJPrestador.CNPJ
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.RazaoSocialPrestador.Replace("'", "")
                             + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.RazaoSocialTomador.Replace("'", "")
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.TipoLogradouro
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.Logradouro
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.NumeroEndereco
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.Bairro
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.Cidade
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.UF
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.CEP
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EmailPrestador
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.StatusNFe
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.TributacaoNFe
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.OpcaoSimples
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorServicos
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorPIS
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorCOFINS
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorINSS
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorIR
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorCSLL
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.CodigoServico
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorISS
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorCredito
                            + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ISSRetido
                            + "' , '" + (retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.CPFCNPJTomador == null ? "0" : retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.CPFCNPJTomador.CNPJ)
                             + "' , '" + (retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.Discriminacao == null ? "0" : retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.Discriminacao)
                            + "' )";

                        db.Database.SqlQuery<RetDownloadNFSes>(sqlinser).ToList();

                        //inserir na tabela EMIT
                        var insertemit = "insert into Avalara_DownloadNFe_Emit (SiglaNota, ChaveNfe, CNPJ, xNome, xLgr, nro, xBairro,  UF, CEP) values ('NFse', '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.NumeroDocumento
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.CPFCNPJPrestador.CNPJ
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.RazaoSocialPrestador.Replace("'", "")
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.Logradouro
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.NumeroEndereco
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.Bairro
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.UF
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoPrestador.CEP
                          + "')";

                        db.Database.SqlQuery<RetDownloadNFSes>(insertemit).ToList();

                        //inserir na tabela Dest

                        var insertDest = "insert into  Avalara_DownloadNFe_Dest (ChaveNfe, CNPJ, xNome, xLgr, nro, xBairro, UF, CEP) values ( '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.NumeroDocumento
                           + "' , '" + (retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.CPFCNPJTomador == null ? "0" : retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.CPFCNPJTomador.CNPJ)
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.RazaoSocialTomador.Replace("'", "")
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoTomador.Logradouro
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoTomador.NumeroEndereco
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoTomador.Bairro
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoTomador.UF
                          + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.EnderecoTomador.CEP
                          + "')";

                        db.Database.SqlQuery<RetDownloadNFSes>(insertDest).ToList();


                        //inserir na tabela IDe

                        var insertIde = "insert into  Avalara_DownloadNFe_Ide (ChaveNfe, cNF, nNF ) values ('" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.NumeroDocumento
                         + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ChaveNFe.NumeroNFe
                        + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.NumeroLote
                         + "')";

                        db.Database.SqlQuery<RetDownloadNFSes>(insertIde).ToList();



                        // Inserir na tabela TOTAL

                        var inserttotal = "insert into  Avalara_DownloadNFe_Total (ChaveNfe, vNF) values ( '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.NumeroDocumento
                         + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorServicos

                         + "')";

                        db.Database.SqlQuery<RetDownloadNFSes>(inserttotal).ToList();


                        // Inserir na tabela TOTAL
                        int val = 1;
                        string Ucon = "UN";
                        var insertintenstotal = "insert into  Avalara_DownloadNFe_ItensProd (ChaveNfe, cProd, nItemPed, qCom , xProd, vProd,uCom) values ( '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.NumeroDocumento
                         + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.CodigoServico
                         + "' , '" + val
                         + "' , '" + val                         
                         + "' , '" + (retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.Discriminacao == null ? "0" : retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.Discriminacao)
                         + "' , '" + retornaNotaEMI.retDownloadNFSes.retorno.NFSe.Conteudo.NFe.ValorServicos
                          + "' , '" + Ucon

                         + "')";

                        db.Database.SqlQuery<RetDownloadNFSes>(insertintenstotal).ToList();

                        //g.cProd 'CDPRODUTO'
                        //, g.nItemPed 'NUMEROITEMPEDIDO'
                        //, g.qCom 'QCOM'
                        //, g.xProd 'NOMEPRODUTO'
                        //, g.vProd 'VALORPRODUTO'
                        //, g.uCom 'UCOM'




                        db.Database.SqlQuery<RecebimentoNota>("EXEC STO_U_Avalara_DownloadNFSE_Emit_Validar").ToList();
                    }

                }
            }
        }

        public void DesvincularDados(int idavalara, string NotaFiscal)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                {
                    // SqlParameter Scnpj = new SqlParameter("@CNPJtesteE", nfe);
                    db.Database.SqlQuery<RecebimentoNota>("Delete from Avalara_DownloadNFe_Vinculo where idavalara =  " + idavalara + "").ToList();
                    db.Database.SqlQuery<RecebimentoNota>("UPDATE Avalara_DownloadNFe_Emit SET Situacao = null where id = " + idavalara + "").ToList();
                    db.Database.SqlQuery<RecebimentoNota>("Delete from Avalara_RM_IdMov where idavalara =  " + idavalara + "").ToList();
                }
            }

        }




        public List<SelectIDMOV> SelectIDMOv(int idavalara, string NotaFiscal)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                {
                    // SqlParameter Scnpj = new SqlParameter("@CNPJtesteE", nfe);
                    var linhas = db.Database.SqlQuery<SelectIDMOV>("Select * from Avalara_RM_IdMov where IdAvalara=" + idavalara + " and NotaFiscal = " + NotaFiscal + " ").ToList();
                    return linhas;
                }
            }

        }

        public void salvarAnalise(string nfe, int tiponova)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {
                    // SqlParameter Scnpj = new SqlParameter("@CNPJtesteE", nfe);

                    db.Database.SqlQuery<RecebimentoNota>("UPDATE Avalara_DownloadNFe_Emit SET TipoNFe=" + tiponova + ", Situacao='AN'  WHERE ChaveNfe='" + nfe + "'").ToList();
                }
            }

        }

        public void GuardarInformacoesLog(DateTime horainicio, DateTime now, int vnfe, int vsfe)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                {

                    db.Database.SqlQuery<RecebimentoNota>("INSERT INTO AvalaraSchedule (HoraInicio, HoraFim, QtdeNFE, QtdeNFSE) values ('" + horainicio + "', '" + now + "', " + vnfe + ", " + vsfe + " )").ToList();
                }
            }
        }

        public List<RecebimentoNotaVinculada> ObterInformacoesNotaVinculada()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {
                    var linhas = db.Database.SqlQuery<RecebimentoNotaVinculada>("EXEC STO_S_RM_PEDIDO_COMPRA_AVALARA_VINCULADA").ToList();
                    return linhas;
                }
            }

        }


        public List<RecebimentoNotaRM> ObterInformacoesNotaRM()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {
                    var linhas = db.Database.SqlQuery<RecebimentoNotaRM>("EXEC STO_S_RM_PEDIDO_COMPRA_AVALARA").ToList();
                    return linhas;
                }
            }

        }

        public List<Recebimento_AvalaraDetalheNota> ObterInformacoesNotaDetalhe(string numeronota)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //Susing (DatabaseContext db = new DatabaseContext())
                {
                    if (numeronota == null)
                        numeronota = "0";


                    SqlParameter Snumeronota = new SqlParameter("@NF", numeronota);

                    var linhas = db.Database.SqlQuery<Recebimento_AvalaraDetalheNota>("STO_S_AVALARA_DETALHE_NOTA @NF", Snumeronota).ToList();
                    return linhas;
                }
            }
        }

        public List<Recebimento_Tipo_Movimento_Avalara> ObterInformacoesCombo1(string fonte)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter CODTMV = new SqlParameter("@CODTMV", fonte);

                var linhas = db.Database.SqlQuery<Recebimento_Tipo_Movimento_Avalara>("EXEC STO_S_RM_TIPO_MOVIMENTO_ENTRADA_AVALARA @CODTMV", CODTMV).ToList();
                return linhas;

            }

        }

        public List<Recebimento_Class_Fin_Avalara> ObterInformacoesCombo2()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linhas = db.Database.SqlQuery<Recebimento_Class_Fin_Avalara>("STO_S_RM_CLASS_FIN_AVALARA").ToList();
                return linhas;

            }

        }


        public List<Recebimento_Forma_Pagto_Avalara> ObterInformacoesCombo3()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linhas = db.Database.SqlQuery<Recebimento_Forma_Pagto_Avalara>("STO_S_RM_FORMA_PAGTO_AVALARA").ToList();
                return linhas;

            }

        }


        public List<Recebimento_Compra_item_Avalara> ObterInformacoes_CompraItemAvalara(string IDMOV)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter IdMov = new SqlParameter("@IDMOV", IDMOV);

                var linhas = db.Database.SqlQuery<Recebimento_Compra_item_Avalara>("STO_S_RM_PEDIDO_COMPRA_ITEM_AVALARA @IDMOV", IdMov).ToList();
                return linhas;

            }

        }



        public List<Recebimento_item_Avalara> ObterInformacoes_item_Avalara(int ID_AVALARA, string IDMOV)
        {
            using (DatabaseContext db = new DatabaseContext())
            {


                SqlParameter IdMov = new SqlParameter("@IDMOV", IDMOV);

                var linhas = db.Database.SqlQuery<Recebimento_item_Avalara>("STO_S_RM_PEDIDO_COMPRA_ITEM_AVALARA  @IDMOV", IdMov).ToList();
                return linhas;

            }

        }

        public List<Recebimento_CFOP_Avalara> ObterInformacoes_CFOP_Avalara(string cfop)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter Idcfop = new SqlParameter("@CFOP", cfop);


                var linhas = db.Database.SqlQuery<Recebimento_CFOP_Avalara>("STO_S_RM_CFOP_AVALARA @CFOP", Idcfop).ToList();
                return linhas;

            }

        }

        public List<Recebimento_NCM_Avalara> ObterInformacoes_NCM_Avalara(string cfop)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter Idcfop = new SqlParameter("@CFOP", cfop);


                var linhas = db.Database.SqlQuery<Recebimento_NCM_Avalara>("STO_S_RM_PROD_NCM_AVALARA @CFOP", Idcfop).ToList();
                return linhas;

            }

        }

        public int Insertnfepelaentrada(Avalara_RetornaChavesNFePelaDataEntrada Entrada)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter cStat = new SqlParameter("@cStat", Entrada.retChavesNfe.cStat);
                SqlParameter dhResp = new SqlParameter("@dhResp", Entrada.retChavesNfe.dhResp);
                SqlParameter xMotivo = new SqlParameter("@xMotivo", Entrada.retChavesNfe.xMotivo);
                SqlParameter Quantidade = new SqlParameter("@Quantidade", Entrada.retChavesNfe.Quantidade);

                var linha = db.Database.SqlQuery<RetChavesNfe>("STO_I_AVALARA_ChavesNFePelaDataEntrada @cStat, @dhResp, @xMotivo, @Quantidade", cStat, dhResp, xMotivo, Quantidade).ToList();


                var id = db.Database.SqlQuery<RetChavesNfe>("STO_I_AVALARA_RetornaChavesNFePelaDataEntrada_recuperaI").ToList();

                if (id.Count() > 0)
                {

                    foreach (var item in Entrada.retChavesNfe.retorno.ChaveNFe)
                    {
                        var existe = db.Database.SqlQuery<Avalara_RetornaChavesNFePelaDataEntrada>("select * from Avalara_RetornaChavesNFePelaDataEntrada where ChaveNfe =  '" + item + "'").ToList();
                        if (existe.Count() == 0)
                        {
                            SqlParameter idavalara = new SqlParameter("@Id_Avalara", id.FirstOrDefault().Id);
                            SqlParameter itemnfe = new SqlParameter("@ChaveNfe", item);
                            var insert = db.Database.SqlQuery<Avalara_RetornaChavesNFePelaDataEntrada>("STO_I_AVALARA_RetornaChavesNFePelaDataEntrada @Id_Avalara, @ChaveNfe", idavalara, itemnfe).ToList();
                        }

                    }

                    return id.FirstOrDefault().Id;
                }



                return 0;

            }

        }


        public int InsertDownloadNFeNotaXMLIde(string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas, AvalaraInfoNotasLista AvalaraInfoNotasLista, int cont)
        {
            using (DatabaseContext dbide = new DatabaseContext())
            {
                // insert conteudo xml IDE

                if (cont == 2)
                {
                    SqlParameter ChaveIDE = new SqlParameter("@ChaveNFE", ChaveNFE);
                    SqlParameter cUF = new SqlParameter("@cUF", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.cUF);
                    SqlParameter cNF = new SqlParameter("@cNF", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.cNF);
                    SqlParameter natOp = new SqlParameter("@natOpl", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.natOp);
                    SqlParameter IDEmod = new SqlParameter("@mod", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.mod);
                    SqlParameter serie = new SqlParameter("@serie", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.serie);
                    SqlParameter nNF = new SqlParameter("@nNF", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.nNF == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.nNF);
                    SqlParameter dhEmi = new SqlParameter("@dhEmi", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.dhEmi == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.dhEmi);
                    SqlParameter tpNF = new SqlParameter("@tpNF", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.tpNF);
                    SqlParameter idDest = new SqlParameter("@idDest", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.idDest == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.idDest);
                    SqlParameter cMunFG = new SqlParameter("@cMunFG", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.cMunFG);
                    SqlParameter tpImp = new SqlParameter("@tpImp", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.tpImp);
                    SqlParameter tpEmis = new SqlParameter("@tpEmis", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.tpEmis);
                    SqlParameter cDV = new SqlParameter("@cDV", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.cDV);
                    SqlParameter tpAmb = new SqlParameter("@tpAmb", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.tpAmb);
                    SqlParameter finNFe = new SqlParameter("@finNFe", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.finNFe);
                    SqlParameter indFinal = new SqlParameter("@indFinal", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.indFinal == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.indFinal);
                    SqlParameter indPres = new SqlParameter("@indPres", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.indPres == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.indPres);
                    SqlParameter procEmi = new SqlParameter("@procEmi", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.procEmi == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.procEmi);
                    SqlParameter verProc = new SqlParameter("@verProc", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.verProc == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.ide.verProc);



                    var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_IDE @ChaveNFE, @cUF, @cNF, @natOpl, @mod, @serie, @nNF, @dhEmi, @tpNF, @idDest, @cMunFG, @tpImp,@tpEmis, @cDV, @tpAmb, @finNFe, @indFinal, @indPres, @procEmi, @verProc ",
                        ChaveIDE, cUF, cNF, natOp, IDEmod, serie, nNF, dhEmi, tpNF, idDest, cMunFG, tpImp, tpEmis, cDV, tpAmb, finNFe, indFinal, indPres, procEmi, verProc).ToList();


                }
                else
                {
                    SqlParameter ChaveIDE = new SqlParameter("@ChaveNFE", ChaveNFE);
                    SqlParameter cUF = new SqlParameter("@cUF", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.cUF);
                    SqlParameter cNF = new SqlParameter("@cNF", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.cNF);
                    SqlParameter natOp = new SqlParameter("@natOpl", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.natOp);
                    SqlParameter IDEmod = new SqlParameter("@mod", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.mod);
                    SqlParameter serie = new SqlParameter("@serie", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.serie);
                    SqlParameter nNF = new SqlParameter("@nNF", (AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.nNF == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.nNF);
                    SqlParameter dhEmi = new SqlParameter("@dhEmi", (AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.dhEmi == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.dhEmi);
                    SqlParameter tpNF = new SqlParameter("@tpNF", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.tpNF);
                    SqlParameter idDest = new SqlParameter("@idDest", (AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.idDest == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.idDest);
                    SqlParameter cMunFG = new SqlParameter("@cMunFG", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.cMunFG);
                    SqlParameter tpImp = new SqlParameter("@tpImp", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.tpImp);
                    SqlParameter tpEmis = new SqlParameter("@tpEmis", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.tpEmis);
                    SqlParameter cDV = new SqlParameter("@cDV", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.cDV);
                    SqlParameter tpAmb = new SqlParameter("@tpAmb", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.tpAmb);
                    SqlParameter finNFe = new SqlParameter("@finNFe", AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.finNFe);
                    SqlParameter indFinal = new SqlParameter("@indFinal", (AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.indFinal == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.indFinal);
                    SqlParameter indPres = new SqlParameter("@indPres", (AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.indPres == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.indPres);
                    SqlParameter procEmi = new SqlParameter("@procEmi", (AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.procEmi == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.procEmi);
                    SqlParameter verProc = new SqlParameter("@verProc", (AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.verProc == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.ide.verProc);


                    var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_IDE @ChaveNFE, @cUF, @cNF, @natOpl, @mod, @serie, @nNF, @dhEmi, @tpNF, @idDest, @cMunFG, @tpImp,@tpEmis, @cDV, @tpAmb, @finNFe, @indFinal, @indPres, @procEmi, @verProc ",
                        ChaveIDE, cUF, cNF, natOp, IDEmod, serie, nNF, dhEmi, tpNF, idDest, cMunFG, tpImp, tpEmis, cDV, tpAmb, finNFe, indFinal, indPres, procEmi, verProc).ToList();

                }



            }
            return 0;
        }

        public int InsertDownloadNFeNotaXMLEmit(string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas, AvalaraInfoNotasLista AvalaraInfoNotasLista, int cont)
        {
            using (DatabaseContext dbide = new DatabaseContext())
            {


                if (cont == 2)
                {

                    // insert conteudo xml emit
                    SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                    SqlParameter CNPJ = new SqlParameter("@CNPJ", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.CNPJ);
                    SqlParameter xNome = new SqlParameter("@xNome", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.xNome);
                    SqlParameter xLgr = new SqlParameter("@xLgr", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.xLgr);
                    SqlParameter nro = new SqlParameter("@NRO", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.nro);
                    SqlParameter xBairro = new SqlParameter("@xBairro", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.xBairro);
                    SqlParameter cMun = new SqlParameter("@cMun", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.cMun);
                    SqlParameter xMun = new SqlParameter("@xMun", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.xMun);
                    SqlParameter UF = new SqlParameter("@UF", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.UF);
                    SqlParameter CEP = new SqlParameter("@CEP", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.CEP == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.CEP);
                    SqlParameter cPais = new SqlParameter("@cPais", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.cPais == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.cPais);
                    SqlParameter xPais = new SqlParameter("@xPais", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.xPais == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.xPais);
                    SqlParameter fone = new SqlParameter("@fone", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.fone == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.enderEmit.fone);
                    SqlParameter IE = new SqlParameter("@ie", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.IE == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.IE);
                    SqlParameter CRT = new SqlParameter("@CRT", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.emit.CRT);
                    SqlParameter cStat = new SqlParameter("@cStat", (AvalaraInfoNotasLista.nfeProc.protNFe.infProt.cStat.ToString() == null) ? "0" : AvalaraInfoNotasLista.nfeProc.protNFe.infProt.cStat);
                    SqlParameter xMotivo = new SqlParameter("@xMotivo", (AvalaraInfoNotasLista.nfeProc == null) ? "0" : AvalaraInfoNotasLista.nfeProc.protNFe.infProt.xMotivo.ToString());



                    var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Emit @ChaveNFE, @CNPJ, @xNome, @xLgr, @NRO, @xBairro, @cMun, @UF, @CEP," +
                       "@cPais, @xPais,@fone, @ie, @CRT",
                       Chave, CNPJ, xNome, xLgr, nro, xBairro, cMun, xMun, UF, CEP, cPais, xPais, fone, IE, CRT).ToList();

                }
                else
                {

                    SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                    SqlParameter CNPJ = new SqlParameter("@CNPJ", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.CNPJ);
                    SqlParameter xNome = new SqlParameter("@xNome", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.xNome);
                    SqlParameter xLgr = new SqlParameter("@xLgr", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.xLgr);
                    SqlParameter nro = new SqlParameter("@NRO", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.nro);
                    SqlParameter xBairro = new SqlParameter("@xBairro", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.xBairro);
                    SqlParameter cMun = new SqlParameter("@cMun", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.cMun);
                    SqlParameter xMun = new SqlParameter("@xMun", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.xMun);
                    SqlParameter UF = new SqlParameter("@UF", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.UF);
                    SqlParameter CEP = new SqlParameter("@CEP", (AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.CEP == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.CEP);
                    SqlParameter cPais = new SqlParameter("@cPais", (AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.cPais == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.cPais);
                    SqlParameter xPais = new SqlParameter("@xPais", (AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.xPais == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.xPais);
                    SqlParameter fone = new SqlParameter("@fone", (AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.fone == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.enderEmit.fone);
                    SqlParameter IE = new SqlParameter("@ie", (AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.IE == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.IE);
                    SqlParameter CRT = new SqlParameter("@CRT", AvalaraInfoNotas.nfeProc.NFe.infNFe.emit.CRT);


                    var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Emit @ChaveNFE, @CNPJ, @xNome, @xLgr, @NRO, @xBairro, @cMun, @UF, @CEP," +
                       "@cPais, @xPais,@fone, @ie, @CRT",
                       Chave, CNPJ, xNome, xLgr, nro, xBairro, cMun, xMun, UF, CEP, cPais, xPais, fone, IE, CRT).ToList();

                }





            }

            return 0;
        }

        public int InsertDownloadNFeNotaXMLDest(string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas, AvalaraInfoNotasLista AvalaraInfoNotasLista, int cont)
        {
            using (DatabaseContext dbide = new DatabaseContext())
            {
                // insert conteudo xml DEST

                if (cont == 2)
                {
                    SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                    SqlParameter CNPJ = new SqlParameter("@CNPJ", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.CNPJ == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.CNPJ);
                    SqlParameter xNome = new SqlParameter("@xNome", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.xNome == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.xNome);
                    SqlParameter xLgr = new SqlParameter("@xLgr", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.xLgr);
                    SqlParameter nro = new SqlParameter("@NRO", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.nro);
                    SqlParameter xBairro = new SqlParameter("@xBairro", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.xBairro);
                    SqlParameter cMun = new SqlParameter("@cMun", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.cMun);
                    SqlParameter xMun = new SqlParameter("@xMun", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.xMun);
                    SqlParameter UF = new SqlParameter("@UF", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.UF);
                    SqlParameter CEP = new SqlParameter("@CEP", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.CEP == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.CEP);
                    SqlParameter cPais = new SqlParameter("@cPais", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.cPais == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.cPais);
                    SqlParameter xPais = new SqlParameter("@xPais", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.xPais == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.xPais);
                    SqlParameter fone = new SqlParameter("@fone", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.fone == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.enderDest.fone);
                    SqlParameter IE = new SqlParameter("@ie", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.IE == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.dest.IE);

                    var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_dest @ChaveNFE, @CNPJ, @xNome, @xLgr, @NRO, @xBairro, @cMun, @xMun, @UF," +
                        "@cPais, @xPais,@fone, @ie",
                        Chave, CNPJ, xNome, xLgr, nro, xBairro, cMun, xMun, UF, CEP, cPais, xPais, fone, IE).ToList();
                }
                else
                {
                    SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                    SqlParameter CNPJ = new SqlParameter("@CNPJ", (AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.CNPJ == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.CNPJ);
                    SqlParameter xNome = new SqlParameter("@xNome", AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.xNome);
                    SqlParameter xLgr = new SqlParameter("@xLgr", AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.xLgr);
                    SqlParameter nro = new SqlParameter("@NRO", AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.nro);
                    SqlParameter xBairro = new SqlParameter("@xBairro", AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.xBairro);
                    SqlParameter cMun = new SqlParameter("@cMun", AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.cMun);
                    SqlParameter xMun = new SqlParameter("@xMun", AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.xMun);
                    SqlParameter UF = new SqlParameter("@UF", (AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.UF == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.UF);
                    SqlParameter CEP = new SqlParameter("@CEP", (AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.CEP == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.CEP);
                    SqlParameter cPais = new SqlParameter("@cPais", (AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.cPais == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.cPais);
                    SqlParameter xPais = new SqlParameter("@xPais", (AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.xPais == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.xPais);
                    SqlParameter fone = new SqlParameter("@fone", (AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.fone == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.enderDest.fone);
                    SqlParameter IE = new SqlParameter("@ie", (AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.IE == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.dest.IE);

                    var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_dest @ChaveNFE, @CNPJ, @xNome, @xLgr, @NRO, @xBairro, @cMun, @xMun, @UF," +
                        "@cPais, @xPais,@fone, @ie",
                        Chave, CNPJ, xNome, xLgr, nro, xBairro, cMun, xMun, UF, CEP, cPais, xPais, fone, IE).ToList();
                }



            }
            return 0;
        }

        public int InsertDownloadNFeNotaXMLEntrega(string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas, AvalaraInfoNotasLista AvalaraInfoNotasLista, int cont)
        {
            using (DatabaseContext dbide = new DatabaseContext())
            {
                // insert conteudo xml entrega

                if (cont == 2)
                {
                    if (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega != null)
                    {


                        SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                        SqlParameter CNPJ = new SqlParameter("@CNPJ", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.CEP == null) ? "-" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.CEP);
                        SqlParameter xNome = new SqlParameter("@xNome", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.xNome == null) ? "-" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.xNome);
                        SqlParameter xLgr = new SqlParameter("@xLgr", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.xLgr);
                        SqlParameter nro = new SqlParameter("@NRO", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.nro);
                        SqlParameter xBairro = new SqlParameter("@xBairro", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.xBairro);
                        SqlParameter cMun = new SqlParameter("@cMun", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.cMun);
                        SqlParameter xMun = new SqlParameter("@xMun", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.xMun);
                        SqlParameter UF = new SqlParameter("@UF", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.UF);
                        SqlParameter CEP = new SqlParameter("@CEP", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.CEP == null) ? "-" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.CEP);
                        SqlParameter cPais = new SqlParameter("@cPais", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.cPais == null) ? "-" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.cPais);
                        SqlParameter xPais = new SqlParameter("@xPais", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.xPais == null) ? "-" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.xPais);
                        SqlParameter fone = new SqlParameter("@fone", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.fone == null) ? "-" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.fone);
                        SqlParameter IE = new SqlParameter("@ie", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.IE == null ? "-" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.entrega.IE);

                        var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Entrega @ChaveNFE, @CNPJ, @xNome, @xLgr, @NRO, @xBairro, @cMun, @xMun, @UF," +
                            "@cPais, @xPais,@fone, @ie",
                            Chave, CNPJ, xNome, xLgr, nro, xBairro, cMun, xMun, UF, CEP, cPais, xPais, fone, IE).ToList();
                    }
                }
                else
                {
                    if (AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega != null)
                    {



                        SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                        SqlParameter CNPJ = new SqlParameter("@CNPJ", (AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.CNPJ == null) ? "-" : AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.CNPJ);
                        SqlParameter xNome = new SqlParameter("@xNome", (AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.xNome == null) ? "-" : AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.xNome);
                        SqlParameter xLgr = new SqlParameter("@xLgr", AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.xLgr);
                        SqlParameter nro = new SqlParameter("@NRO", AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.nro);
                        SqlParameter xBairro = new SqlParameter("@xBairro", AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.xBairro);
                        SqlParameter cMun = new SqlParameter("@cMun", AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.cMun);
                        SqlParameter xMun = new SqlParameter("@xMun", AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.xMun);
                        SqlParameter UF = new SqlParameter("@UF", AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.UF);
                        SqlParameter CEP = new SqlParameter("@CEP", (AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.CEP == null) ? "-" : AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.CEP);
                        SqlParameter cPais = new SqlParameter("@cPais", (AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.cPais == null) ? "-" : AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.cPais);
                        SqlParameter xPais = new SqlParameter("@xPais", (AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.xPais == null) ? "-" : AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.xPais);
                        SqlParameter fone = new SqlParameter("@fone", (AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.fone == null) ? "-" : AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.fone);
                        SqlParameter IE = new SqlParameter("@ie", AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.IE == null ? "-" : AvalaraInfoNotas.nfeProc.NFe.infNFe.entrega.IE);


                        var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Entrega @ChaveNFE, @CNPJ, @xNome, @xLgr, @NRO, @xBairro, @cMun, @xMun, @UF," +
                            "@cPais, @xPais,@fone, @ie",
                            Chave, CNPJ, xNome, xLgr, nro, xBairro, cMun, xMun, UF, CEP, cPais, xPais, fone, IE).ToList();
                    }
                }



            }

            return 0;
        }

        //public int InsertDownloadNFeNotaXMLautXML(string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas, AvalaraInfoNotasLista AvalaraInfoNotasLista, int cont)
        //{
        //    using (DatabaseContext dbide = new DatabaseContext())
        //    {
        //        // insert conteudo xml autXML               

        //        if (cont == 2)
        //        {
        //            if (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.autXML != null)
        //            {
        //                SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
        //                SqlParameter CPF = new SqlParameter("@CPF", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.autXML.CPF);


        //                var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_autXML @ChaveNFE, @CPF",
        //                    Chave, CPF).ToList();
        //            }
        //        }
        //        else
        //        {
        //            if (AvalaraInfoNotas.nfeProc.NFe.infNFe.autXML != null)
        //            {
        //                SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
        //                SqlParameter CPF = new SqlParameter("@CPF", AvalaraInfoNotas.nfeProc.NFe.infNFe.autXML.CPF);


        //                var insertIde = dbide.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_autXML @ChaveNFE, @CPF",
        //                    Chave, CPF).ToList();
        //            }
        //        }



        //    }

        //    return 0;
        //}


        public int InsertDownloadNFeNotaXMLProdutosLista(string ChaveNFE, AvalaraInfoNotasLista AvalaraInfoNotas)
        {
            using (DatabaseContext dbprod = new DatabaseContext())
            {
                // insert conteudo xml produtos
                foreach (var item in AvalaraInfoNotas.nfeProc.NFe.infNFe.det)
                {


                    if (item.prod != null)
                    {

                        SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                        SqlParameter cProd = new SqlParameter("@cProd", item.prod.cProd);
                        SqlParameter cEAN = new SqlParameter("@cEAN", (item.prod.cEAN == null) ? "0" : item.prod.cEAN);
                        SqlParameter xProd = new SqlParameter("@xProd", item.prod.xProd);
                        SqlParameter NCM = new SqlParameter("@NCM", item.prod.NCM);
                        SqlParameter CEST = new SqlParameter("@CEST", (item.prod.CEST == null) ? "0" : item.prod.CEST);
                        SqlParameter CFOP = new SqlParameter("@CFOP", item.prod.CFOP);
                        SqlParameter uCom = new SqlParameter("@uCom", item.prod.uCom);
                        SqlParameter qCom = new SqlParameter("@qCom", item.prod.qCom);
                        SqlParameter vUnCom = new SqlParameter("@vUnCom", item.prod.vUnCom);
                        SqlParameter vProd = new SqlParameter("@vProd", item.prod.vProd);
                        SqlParameter cEANTrib = new SqlParameter("@cEANTrib", (item.prod.cEANTrib == null) ? "0" : item.prod.cEANTrib);
                        SqlParameter uTrib = new SqlParameter("@uTrib", item.prod.uTrib);
                        SqlParameter qTrib = new SqlParameter("@qTrib", item.prod.qTrib);
                        SqlParameter vUnTrib = new SqlParameter("@vUnTrib", item.prod.vUnTrib);
                        SqlParameter nItemPed = new SqlParameter("@nItemPed", (item.NItem == null) ? "0" : item.NItem);


                        SqlParameter cProdANP = new SqlParameter("@cProdANP", "0");
                        SqlParameter descANP = new SqlParameter("@descANP", "0");
                        SqlParameter CODIF = new SqlParameter("@CODIF", "0");
                        SqlParameter UFCons = new SqlParameter("@UFCons", "0");

                        if (item.prod.comb != null)
                        {
                            cProdANP = new SqlParameter("@cProdANP", (item.prod.comb.cProdANP == null) ? "0" : item.prod.comb.cProdANP);
                            descANP = new SqlParameter("@descANP", (item.prod.comb.descANP == null) ? "0" : item.prod.comb.descANP);
                            CODIF = new SqlParameter("@CODIF", (item.prod.comb.CODIF == null) ? "0" : item.prod.comb.CODIF);
                            UFCons = new SqlParameter("@UFCons", item.prod.comb.UFCons);


                        }
                        // SqlParameter infAdProd = new SqlParameter("@infAdProd", (item.infAdProd == null) ? "0" : item.infAdProd);
                        SqlParameter infAdProd = new SqlParameter("@infAdProd", "-");

                        SqlParameter orig = new SqlParameter("@orig", "0");
                        SqlParameter CST = new SqlParameter("@CST", "0");
                        SqlParameter modBC = new SqlParameter("@modBC", "0");
                        SqlParameter vBC = new SqlParameter("@vBC", "0");
                        SqlParameter pICMS = new SqlParameter("@pICMS", "0");
                        SqlParameter vICMS = new SqlParameter("@vICMS", "0");

                        if (item.imposto.ICMS != null)
                        {
                            if (item.imposto.ICMS.ICMS00 != null)
                            {
                                orig = new SqlParameter("@orig", item.imposto.ICMS.ICMS00.orig);
                                CST = new SqlParameter("@CST", item.imposto.ICMS.ICMS00.CST);
                                modBC = new SqlParameter("@modBC", item.imposto.ICMS.ICMS00.modBC);
                                vBC = new SqlParameter("@vBC", item.imposto.ICMS.ICMS00.vBC);
                                pICMS = new SqlParameter("@pICMS", item.imposto.ICMS.ICMS00.pICMS);
                                vICMS = new SqlParameter("@vICMS", item.imposto.ICMS.ICMS00.vICMS);
                            }
                        }

                        SqlParameter cEnq = new SqlParameter("@cEnq", "0");
                        SqlParameter IPINT_CST = new SqlParameter("@IPINT_CST", "0");
                        SqlParameter PISNT_CST = new SqlParameter("@PISNT_CST", "0");
                        SqlParameter COFINSNT_CST = new SqlParameter("@COFINSNT_CST", "0");

                        if (item.imposto.IPI != null)
                        {
                            cEnq = new SqlParameter("@cEnq", item.imposto.IPI.cEnq);
                            IPINT_CST = new SqlParameter("@IPINT_CST", (item.imposto.IPI.IPINT != null) ? item.imposto.IPI.IPINT.CST : "0");
                            PISNT_CST = new SqlParameter("@PISNT_CST", (item.imposto.PIS.PISOutr != null) ? item.imposto.PIS.PISOutr.CST : "0");
                            COFINSNT_CST = new SqlParameter("@COFINSNT_CST", (item.imposto.COFINS.COFINSOutr != null) ? item.imposto.COFINS.COFINSOutr.CST : "0");
                        }

                        SqlParameter cStat = new SqlParameter("@cStat", (AvalaraInfoNotas.nfeProc == null) ? "0" : AvalaraInfoNotas.nfeProc.protNFe.infProt.cStat);
                        SqlParameter xMotivo = new SqlParameter("@xMotivo", (AvalaraInfoNotas.nfeProc == null) ? "0" : AvalaraInfoNotas.nfeProc.protNFe.infProt.xMotivo.ToString());

                        var insertIde = dbprod.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Produtos @ChaveNFE, @cProd, @cEAN, @xProd, @NCM, @CEST, @CFOP, @uCom, @qCom," +
                            "@vUnCom, @vProd, @cEANTrib, @uTrib,   @qTrib, @vUnTrib, @nItemPed, @cProdANP,   @descANP, @CODIF, @UFCons, @infAdProd," +
                            " @orig, @CST, @modBC, @vBC, @pICMS, @vICMS, @cEnq, @IPINT_CST, @PISNT_CST, COFINSNT_CST , cStat, @xMotivo ",
                            Chave, cProd, cEAN, xProd, NCM, CEST, CFOP, uCom, qCom, vUnCom, vProd, cEANTrib, uTrib, qTrib,
                            vUnTrib, nItemPed, cProdANP, descANP, CODIF, UFCons, infAdProd, orig, CST, modBC, vBC, pICMS, vICMS, cEnq, IPINT_CST, PISNT_CST, COFINSNT_CST, cStat, xMotivo).ToList();

                        //var insertIde = dbprod.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Produtos @ChaveNFE, @cProd, @cEAN, @xProd",
                        //        Chave, cProd, cEAN, xProd).ToList();

                    }
                }


            }

            return 0;
        }
        public int InsertDownloadNFeNotaXMLProdutos(string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas)
        {
            using (DatabaseContext dbprod = new DatabaseContext())
            {
                // insert conteudo xml produtos

                if (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod != null)
                {

                    SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                    SqlParameter cProd = new SqlParameter("@cProd", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.cProd);
                    SqlParameter cEAN = new SqlParameter("@cEAN", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.cEAN == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.cEAN);
                    SqlParameter xProd = new SqlParameter("@xProd", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.xProd);
                    SqlParameter NCM = new SqlParameter("@NCM", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.NCM);
                    SqlParameter CEST = new SqlParameter("@CEST", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.CEST == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.CEST);
                    SqlParameter CFOP = new SqlParameter("@CFOP", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.CFOP);
                    SqlParameter uCom = new SqlParameter("@uCom", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.uCom);
                    SqlParameter qCom = new SqlParameter("@qCom", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.qCom);
                    SqlParameter vUnCom = new SqlParameter("@vUnCom", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.vUnCom);
                    SqlParameter vProd = new SqlParameter("@vProd", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.vProd);
                    SqlParameter cEANTrib = new SqlParameter("@cEANTrib", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.cEANTrib == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.cEANTrib);
                    SqlParameter uTrib = new SqlParameter("@uTrib", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.uTrib);
                    SqlParameter qTrib = new SqlParameter("@qTrib", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.qTrib);
                    SqlParameter vUnTrib = new SqlParameter("@vUnTrib", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.vUnTrib);
                    SqlParameter nItemPed = new SqlParameter("@nItemPed", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.nItemPed == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.nItemPed);

                    SqlParameter cProdANP = new SqlParameter("@cProdANP", "0");
                    SqlParameter descANP = new SqlParameter("@descANP", "0");
                    SqlParameter CODIF = new SqlParameter("@CODIF", "0");
                    SqlParameter UFCons = new SqlParameter("@UFCons", "0");

                    if (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.comb != null)
                    {
                        cProdANP = new SqlParameter("@cProdANP", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.comb.cProdANP == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.comb.cProdANP);
                        descANP = new SqlParameter("@descANP", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.comb.descANP == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.comb.descANP);
                        CODIF = new SqlParameter("@CODIF", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.comb.CODIF == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.comb.CODIF);
                        UFCons = new SqlParameter("@UFCons", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.prod.comb.UFCons);


                    }
                    // SqlParameter infAdProd = new SqlParameter("@infAdProd", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.infAdProd == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.det.infAdProd);
                    SqlParameter infAdProd = new SqlParameter("@infAdProd", "-");

                    SqlParameter orig = new SqlParameter("@orig", "0");
                    SqlParameter CST = new SqlParameter("@CST", "0");
                    SqlParameter modBC = new SqlParameter("@modBC", "0");
                    SqlParameter vBC = new SqlParameter("@vBC", "0");
                    SqlParameter pICMS = new SqlParameter("@pICMS", "0");
                    SqlParameter vICMS = new SqlParameter("@vICMS", "0");

                    if (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.ICMS != null)
                    {
                        if (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.ICMS.ICMS00 != null)
                        {
                            orig = new SqlParameter("@orig", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.ICMS.ICMS00.orig);
                            CST = new SqlParameter("@CST", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.ICMS.ICMS00.CST);
                            modBC = new SqlParameter("@modBC", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.ICMS.ICMS00.modBC);
                            vBC = new SqlParameter("@vBC", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.ICMS.ICMS00.vBC);
                            pICMS = new SqlParameter("@pICMS", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.ICMS.ICMS00.pICMS);
                            vICMS = new SqlParameter("@vICMS", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.ICMS.ICMS00.vICMS);
                        }
                    }

                    SqlParameter cEnq = new SqlParameter("@cEnq", "0");
                    SqlParameter IPINT_CST = new SqlParameter("@IPINT_CST", "0");
                    SqlParameter PISNT_CST = new SqlParameter("@PISNT_CST", "0");
                    SqlParameter COFINSNT_CST = new SqlParameter("@COFINSNT_CST", "0");

                    if (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.IPI != null)
                    {
                        cEnq = new SqlParameter("@cEnq", AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.IPI.cEnq);
                        IPINT_CST = new SqlParameter("@IPINT_CST", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.IPI.IPINT != null) ? AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.IPI.IPINT.CST : "0");
                        PISNT_CST = new SqlParameter("@PISNT_CST", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.PIS.PISOutr != null) ? AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.PIS.PISOutr.CST : "0");
                        COFINSNT_CST = new SqlParameter("@COFINSNT_CST", (AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.COFINS.COFINSOutr != null) ? AvalaraInfoNotas.nfeProc.NFe.infNFe.det.imposto.COFINS.COFINSOutr.CST : "0");
                    }

                    SqlParameter cStat = new SqlParameter("@cStat", (AvalaraInfoNotas.nfeProc == null) ? "0" : AvalaraInfoNotas.nfeProc.protNFe.infProt.cStat);
                    SqlParameter xMotivo = new SqlParameter("@xMotivo", (AvalaraInfoNotas.nfeProc == null) ? "0" : AvalaraInfoNotas.nfeProc.protNFe.infProt.xMotivo.ToString());

                    var insertIde = dbprod.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Produtos @ChaveNFE, @cProd, @cEAN, @xProd, @NCM, @CEST, @CFOP, @uCom, @qCom," +
                        "@vUnCom, @vProd, @cEANTrib, @uTrib,   @qTrib, @vUnTrib, @nItemPed, @cProdANP,   @descANP, @CODIF, @UFCons, @infAdProd," +
                        " @orig, @CST, @modBC, @vBC, @pICMS, @vICMS, @cEnq, @IPINT_CST, @PISNT_CST, COFINSNT_CST , @cStat, @xMotivo",
                        Chave, cProd, cEAN, xProd, NCM, CEST, CFOP, uCom, qCom, vUnCom, vProd, cEANTrib, uTrib, qTrib,
                        vUnTrib, nItemPed, cProdANP, descANP, CODIF, UFCons, infAdProd, orig, CST, modBC, vBC, pICMS, vICMS, cEnq, IPINT_CST, PISNT_CST, COFINSNT_CST, cStat, xMotivo).ToList();

                    //var insertIde = dbprod.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Produtos @ChaveNFE, @cProd, @cEAN, @xProd",
                    //        Chave, cProd, cEAN, xProd).ToList();

                }


            }

            return 0;
        }


        public int InsertDownloadNFeNotaXMLTotal(string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas, AvalaraInfoNotasLista AvalaraInfoNotasLista, int cont)
        {
            using (DatabaseContext dbprod = new DatabaseContext())
            {
                // insert conteudo xml Total

                if (cont == 2)
                {
                    if (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total != null)
                    {

                        SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                        SqlParameter totalvBC = new SqlParameter("@totalvBC", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vBC == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vBC);
                        SqlParameter totalvICMS = new SqlParameter("@totalvICMS", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vICMS == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vICMS);
                        SqlParameter vICMSDeson = new SqlParameter("@vICMSDeson", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vICMSDeson == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vICMSDeson);
                        SqlParameter vFCPUFDest = new SqlParameter("@vFCPUFDest", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFCPUFDest == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFCPUFDest);
                        SqlParameter vICMSUFDest = new SqlParameter("@vICMSUFDest", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vICMSUFDest == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vICMSUFDest);
                        SqlParameter vICMSUFRemet = new SqlParameter("@vICMSUFRemet", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vICMSUFRemet == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vICMSUFRemet);
                        SqlParameter vFCP = new SqlParameter("@vFCP", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFCP == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFCP);
                        SqlParameter vBCST = new SqlParameter("@vBCST", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vBCST);
                        SqlParameter vST = new SqlParameter("@vST", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vST);
                        SqlParameter vFCPST = new SqlParameter("@vFCPST", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFCPST == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFCPST);
                        SqlParameter vFCPSTRet = new SqlParameter("@vFCPSTRet", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFCPSTRet == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFCPSTRet);
                        SqlParameter totalvProd = new SqlParameter("@totalvProd", (AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vProd == null) ? "0" : AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vProd);
                        SqlParameter vFrete = new SqlParameter("@vFrete", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vFrete);
                        SqlParameter vSeg = new SqlParameter("@vSeg", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vSeg);
                        SqlParameter vDesc = new SqlParameter("@vDesc", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vDesc);
                        SqlParameter vII = new SqlParameter("@vII", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vII);
                        SqlParameter vIPI = new SqlParameter("@vIPI", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vIPI);
                        SqlParameter vPIS = new SqlParameter("@vPIS", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vPIS);
                        SqlParameter vCOFINS = new SqlParameter("@vCOFINS", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vCOFINS);
                        SqlParameter vOutro = new SqlParameter("@vOutro", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vOutro);
                        SqlParameter vNF = new SqlParameter("@vNF", AvalaraInfoNotasLista.nfeProc.NFe.infNFe.total.ICMSTot.vNF);
                        SqlParameter cStat = new SqlParameter("@cStat", (AvalaraInfoNotasLista.nfeProc == null) ? "0" : AvalaraInfoNotasLista.nfeProc.protNFe.infProt.cStat);
                        SqlParameter xMotivo = new SqlParameter("@xMotivo", (AvalaraInfoNotasLista.nfeProc == null) ? "0" : AvalaraInfoNotasLista.nfeProc.protNFe.infProt.xMotivo.ToString());

                        var insertIde = dbprod.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Total @ChaveNFE, @totalvBC, @totalvICMS, @vICMSDeson, @vFCPUFDest, @vICMSUFDest,"
                            + " @vICMSUFRemet, @vFCP, @vBCST , @vST, @vFCPST, @vFCPSTRet, @totalvProd,   @vFrete, @vSeg, @vDesc, @vII,   @vIPI, @vPIS, @vCOFINS, @vOutro," +
                            " @vNF  , @cStat, @xMotivo",
                            Chave, totalvBC, totalvICMS, vICMSDeson, vFCPUFDest, vICMSUFDest, vICMSUFRemet, vFCP, vBCST, vST, vFCPST, vFCPSTRet, totalvProd, vFrete,
                            vSeg, vDesc, vII, vIPI, vPIS, vCOFINS, vOutro, vNF, cStat, xMotivo).ToList();

                    }

                }
                else
                {

                    if (AvalaraInfoNotas.nfeProc.NFe.infNFe.total != null)
                    {

                        SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);
                        SqlParameter totalvBC = new SqlParameter("@totalvBC", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vBC);
                        SqlParameter totalvICMS = new SqlParameter("@totalvICMS", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vICMS == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vICMS);
                        SqlParameter vICMSDeson = new SqlParameter("@vICMSDeson", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vICMSDeson == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vICMSDeson);
                        SqlParameter vFCPUFDest = new SqlParameter("@vFCPUFDest", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFCPUFDest == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFCPUFDest);
                        SqlParameter vICMSUFDest = new SqlParameter("@vICMSUFDest", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vICMSUFDest == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vICMSUFDest);
                        SqlParameter vICMSUFRemet = new SqlParameter("@vICMSUFRemet", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vICMSUFRemet == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vICMSUFRemet);
                        SqlParameter vFCP = new SqlParameter("@vFCP", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFCP == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFCP);
                        SqlParameter vBCST = new SqlParameter("@vBCST", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vBCST);
                        SqlParameter vST = new SqlParameter("@vST", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vST);
                        SqlParameter vFCPST = new SqlParameter("@vFCPST", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFCPST == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFCPST);
                        SqlParameter vFCPSTRet = new SqlParameter("@vFCPSTRet", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFCPSTRet == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFCPSTRet);
                        SqlParameter totalvProd = new SqlParameter("@totalvProd", (AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vProd == null) ? "0" : AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vProd);
                        SqlParameter vFrete = new SqlParameter("@vFrete", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vFrete);
                        SqlParameter vSeg = new SqlParameter("@vSeg", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vSeg);
                        SqlParameter vDesc = new SqlParameter("@vDesc", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vDesc);
                        SqlParameter vII = new SqlParameter("@vII", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vII);
                        SqlParameter vIPI = new SqlParameter("@vIPI", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vIPI);
                        SqlParameter vPIS = new SqlParameter("@vPIS", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vPIS);
                        SqlParameter vCOFINS = new SqlParameter("@vCOFINS", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vCOFINS);
                        SqlParameter vOutro = new SqlParameter("@vOutro", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vOutro);
                        SqlParameter vNF = new SqlParameter("@vNF", AvalaraInfoNotas.nfeProc.NFe.infNFe.total.ICMSTot.vNF);
                        SqlParameter cStat = new SqlParameter("@cStat", (AvalaraInfoNotas.nfeProc == null) ? "0" : AvalaraInfoNotas.nfeProc.protNFe.infProt.cStat);
                        SqlParameter xMotivo = new SqlParameter("@xMotivo", (AvalaraInfoNotas.nfeProc == null) ? "0" : AvalaraInfoNotas.nfeProc.protNFe.infProt.xMotivo.ToString());

                        var insertIde = dbprod.Database.SqlQuery<Ide>("STO_I_Avalara_DownloadNFe_Total @ChaveNFE, @totalvBC, @totalvICMS, @vICMSDeson, @vFCPUFDest, @vICMSUFDest,"
                            + " @vICMSUFRemet, @vFCP, @vBCST , @vST, @vFCPST, @vFCPSTRet, @totalvProd,   @vFrete, @vSeg, @vDesc, @vII,   @vIPI, @vPIS, @vCOFINS, @vOutro," +
                            " @vNF  , @cStat, @xMotivo",
                            Chave, totalvBC, totalvICMS, vICMSDeson, vFCPUFDest, vICMSUFDest, vICMSUFRemet, vFCP, vBCST, vST, vFCPST, vFCPSTRet, totalvProd, vFrete,
                            vSeg, vDesc, vII, vIPI, vPIS, vCOFINS, vOutro, vNF, cStat, xMotivo).ToList();

                    }
                }




            }

            return 0;
        }


        public int InsertDownloadNFe(Root_Ava_DownloadNFe entrada, string ChaveNFE, AvalaraInfoNotas AvalaraInfoNotas, AvalaraInfoNotasLista AvalaraInfoNotasLista, int cont)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter cStat = new SqlParameter("@cStat", entrada.retDownloadNFe.cStat);
                SqlParameter dhResp = new SqlParameter("@dhResp", entrada.retDownloadNFe.dhResp);
                SqlParameter xMotivo = new SqlParameter("@xMotivo", entrada.retDownloadNFe.xMotivo);
                SqlParameter Chave = new SqlParameter("@ChaveNFE", ChaveNFE);



                var linha = db.Database.SqlQuery<Ava_DownloadNFe>("STO_I_Avalara_DownloadNFe @cStat, @dhResp, @xMotivo, @ChaveNFE", cStat, dhResp, xMotivo, Chave).ToList();

                if (linha.Count() > 0)
                {

                    SqlParameter idavalara = new SqlParameter("@Id_Avalara", linha.FirstOrDefault().Id);
                    SqlParameter vSefaz = new SqlParameter("@vSefaz", entrada.retDownloadNFe.retorno.vSefaz);
                    SqlParameter vIntegridade = new SqlParameter("@vIntegridade", entrada.retDownloadNFe.retorno.vIntegridade);
                    SqlParameter vCadastral = new SqlParameter("@vCadastral", entrada.retDownloadNFe.retorno.vCadastral.Replace("-", "").Trim());
                    SqlParameter Conteudo = new SqlParameter("@ConteudoXml", entrada.retDownloadNFe.retorno.Conteudo);

                    var insert = db.Database.SqlQuery<Avalara_RetornaChavesNFePelaDataEntrada>("STO_I_Avalara_RetornaDownloadNFe @Id_Avalara, @vSefaz, @vIntegridade, @vCadastral, @ConteudoXml", idavalara, vSefaz, vIntegridade, vCadastral, Conteudo).ToList();

                    var existe = db.Database.SqlQuery<Avalara_RetornaChavesNFePelaDataEntrada>("select * from Avalara_DownloadNFe_Emit where ChaveNfe =  '" + ChaveNFE + "'").ToList();
                    if (existe.Count() == 0)
                    {

                        InsertDownloadNFeNotaXMLIde(ChaveNFE, AvalaraInfoNotas, AvalaraInfoNotasLista, cont);
                        InsertDownloadNFeNotaXMLEmit(ChaveNFE, AvalaraInfoNotas, AvalaraInfoNotasLista, cont);
                        InsertDownloadNFeNotaXMLDest(ChaveNFE, AvalaraInfoNotas, AvalaraInfoNotasLista, cont);
                        InsertDownloadNFeNotaXMLEntrega(ChaveNFE, AvalaraInfoNotas, AvalaraInfoNotasLista, cont);
                        // InsertDownloadNFeNotaXMLautXML(ChaveNFE, AvalaraInfoNotas, AvalaraInfoNotasLista, cont);
                        if (cont == 2)
                            InsertDownloadNFeNotaXMLProdutosLista(ChaveNFE, AvalaraInfoNotasLista);
                        else
                            InsertDownloadNFeNotaXMLProdutos(ChaveNFE, AvalaraInfoNotas);

                        InsertDownloadNFeNotaXMLTotal(ChaveNFE, AvalaraInfoNotas, AvalaraInfoNotasLista, cont);

                    }


                    // insert conteudo xml Transp

                    //SqlParameter TranspmodFrete = new SqlParameter("@TranspmodFrete", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.modFrete);
                    //SqlParameter TranspCNPJ = new SqlParameter("@TranspCNPJ", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.transporta.CNPJ);
                    //SqlParameter TranspxNome = new SqlParameter("@TranspxNome", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.transporta.xNome);
                    //SqlParameter TranspIE = new SqlParameter("@TranspIE", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.transporta.IE);
                    //SqlParameter TranspxEnder = new SqlParameter("@TranspxEnder", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.transporta.xEnder);
                    //SqlParameter TranspxMun = new SqlParameter("@TranspxMun", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.transporta.xMun);
                    //SqlParameter TranspUF = new SqlParameter("@TranspUF", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.transporta.UF);
                    //SqlParameter TranspqVol = new SqlParameter("@TranspqVol", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.vol);
                    //SqlParameter TransppesoL = new SqlParameter("@TransppesoL", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.vol.pesoL);
                    //SqlParameter TransppesoB = new SqlParameter("@TransppesoB", AvalaraInfoNotas.nfeProc.NFe.infNFe.transp.vol.pesoB);

                    return 1;


                }

            }

            return 0;
        }


        public List<RetornoNfe> IobterInfoEntrada()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<RetornoNfe>("STO_S_AVALARA_RetornaChavesNFePelaDataEntrada").ToList();
                return linha;
            }
        }
    }
}
