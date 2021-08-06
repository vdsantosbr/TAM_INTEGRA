using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace Data
{
    public class ImportacaoFaturaDAL
    {
        public List<ImportacaoFatura> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string tipoFatura, string numProcesso, string numDI, string invoice, string situacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@FAT_DATA_FATURA_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@FAT_DATA_FATURA_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                SqlParameter pTipoFatura = new SqlParameter("@FAT_TIPO_FATURA", (tipoFatura == null) ? (object)DBNull.Value : tipoFatura);
                SqlParameter pNumProcesso = new SqlParameter("@FAT_COD_PROCESSO", (numProcesso == null) ? (object)DBNull.Value : numProcesso);
                SqlParameter pNumDI = new SqlParameter("@FAT_NUM_DI", (numDI == null) ? (object)DBNull.Value : numDI);
                SqlParameter pInvoice = new SqlParameter("@INVOICE", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<ImportacaoFatura>("EXEC STO_S_TR_IS_OUT_FATH_FILTRO @FAT_DATA_FATURA_INICIO, @FAT_DATA_FATURA_TERMINO, @FAT_TIPO_FATURA, @FAT_COD_PROCESSO, @FAT_NUM_DI, @INVOICE, @SITUACAO", pDataInicio, pDataFim, pTipoFatura, pNumProcesso, pNumDI, pInvoice, pSituacao).ToList();

                if (linha.Count > 0)
                {
                    return linha;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<ImportacaoFatura> Cancelar(int id_integracao = 0, string id_fatura = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdFatura = new SqlParameter("@FAT_FATURA_ID ", (id_fatura == null) ? (object)DBNull.Value : id_fatura);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFatura>("EXEC STO_U_Excluir_TR_IS_OUT_FATH_Excluir @ID_INTEGRACAO, @FAT_FATURA_ID", pIdIntegracao, pIdFatura).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    List<ImportacaoFatura> lst = new List<ImportacaoFatura>();
                    lst.Add(new ImportacaoFatura
                    {
                        //Comentario = e.Message,
                        //OBSERVACAO = "Erro"
                    });
                    return lst.ToList();
                }

            }
        }

        public List<ImportacaoFatura> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID", (sp_id == null) ? (object)DBNull.Value : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO", (sp_id_despesa_processo == null) ? (object)DBNull.Value : sp_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFatura>("EXEC STO_S_TR_OUT_NF_INFO @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdIntegracao, pSpId, pSpIdDespesas).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        //public List<ImportacaoFatura> Reprocessamento(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        //{
        //    using (DatabaseContext db = new DatabaseContext())
        //    {
        //        SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
        //        SqlParameter pIdBrokerSys = new SqlParameter("@ID_BrokerSys", (id_brokersys == 0) ? 0 : id_brokersys);
        //        SqlParameter pNumNFBrokerSys = new SqlParameter("@NUMNF_BrokerSys", (numnf_brokersys== 0) ? 0 : numnf_brokersys);

        //        try
        //        {
        //            try
        //            {
        //                var linha = db.Database.SqlQuery<ImportacaoFatura>("EXEC STO_S_TR_BS_OUT_NF_Reprocessar @ID_INTEGRACAO, @ID_BrokerSys, @NUMNF_BrokerSys", pIdIntegracao, pIdBrokerSys, pNumNFBrokerSys).ToList();
        //                if (linha.Count > 0)
        //                {
        //                    return linha;
        //                }
        //                else
        //                {
        //                    return null;
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                return new List<ImportacaoFatura>();
        //            }


        //        }
        //        catch (Exception e)
        //        {
        //            var erro = e.Message;
        //            return null;
        //        }

        //    }
        //}

        public List<ImportacaoFatura> Reprocessamento(int id_integracao = 0, string id_fatura = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdFatura = new SqlParameter("@FAT_FATURA_ID ", (id_fatura == null) ? (object)DBNull.Value : id_fatura);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFatura>("EXEC STO_U_TR_IS_OUT_FATH_Reprocessar @ID_INTEGRACAO, @FAT_FATURA_ID", pIdIntegracao, pIdFatura).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    List<ImportacaoFatura> lst = new List<ImportacaoFatura>();
                    lst.Add(new ImportacaoFatura
                    {
                        //Comentario = e.Message,
                        //OBSERVACAO = "Erro"
                    });
                    return lst.ToList();
                }

            }
        }

        public List<ImportacaoFatura> TiposDespesas()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFatura>("EXEC STO_S_TR_IS_OUT_SPDESP_Tipos").ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<ImportacaoFatura> Credor()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFatura>("EXEC STO_S_TR_IS_OUT_SPDESP_Credor").ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
    }
}
