using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using System.Data.SqlClient;

namespace Data
{
    public class FinanceiroFaturasDAL
    {
        public List<FinanceiroFaturas> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, int faturamento, string numDI, string invoice, string numProcesso, string situacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@FAT_DATA_FATURA_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@FAT_DATA_FATURA_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                SqlParameter pTipoFatura = new SqlParameter("@FAT_TIPO_FATURA", (faturamento == 0) ? (object)DBNull.Value : faturamento);
                SqlParameter pNumProcesso = new SqlParameter("@FAT_COD_PROCESSO", (numProcesso == null) ? (object)DBNull.Value : numProcesso);
                SqlParameter pNumDI = new SqlParameter("@FAT_NUM_DI", (numDI == null) ? (object)DBNull.Value : numDI);
                SqlParameter pInvoice = new SqlParameter("@INVOICE", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<FinanceiroFaturas>("EXEC STO_S_TR_IS_OUT_FATH_FILTRO @FAT_DATA_FATURA_INICIO, @FAT_DATA_FATURA_TERMINO, @FAT_TIPO_FATURA, @FAT_COD_PROCESSO, @FAT_NUM_DI, @INVOICE, @SITUACAO", pDataInicio, pDataFim, pTipoFatura, pNumProcesso, pNumDI, pInvoice, pSituacao).ToList();

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

        public List<FinanceiroFaturas> Informe(int id_integracao = 0, string id_fatura = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdFatura = new SqlParameter("@FAT_FATURA_ID", (id_fatura == null) ? (object)DBNull.Value : id_fatura);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroFaturas>("EXEC STO_S_TR_IS_OUT_FATH_Info @ID_INTEGRACAO, @FAT_FATURA_ID ", pIdIntegracao, pIdFatura).ToList();

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

        public List<FinanceiroFaturas> Reprocessamento(int id_integracao = 0, string id_fatura = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdFatura = new SqlParameter("@FAT_FATURA_ID ", (id_fatura == null) ? (object)DBNull.Value : id_fatura);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroFaturas>("EXEC STO_U_TR_IS_OUT_FATH_Reprocessar @ID_INTEGRACAO, @FAT_FATURA_ID", pIdIntegracao, pIdFatura).ToList();

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
                    List<FinanceiroFaturas> lst = new List<FinanceiroFaturas>();
                    lst.Add(new FinanceiroFaturas
                    {
                        //Comentario = e.Message,
                        //OBSERVACAO = "Erro"
                    });
                    return lst.ToList();
                }

            }
        }

        public List<FinanceiroFaturas> Excluir(int id_integracao = 0, string id_fatura = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdFatura = new SqlParameter("@FAT_FATURA_ID ", (id_fatura == null) ? (object)DBNull.Value : id_fatura);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroFaturas>("EXEC STO_U_TR_IS_OUT_FATH_Excluir @ID_INTEGRACAO, @FAT_FATURA_ID", pIdIntegracao, pIdFatura).ToList();

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
                    List<FinanceiroFaturas> lst = new List<FinanceiroFaturas>();
                    lst.Add(new FinanceiroFaturas
                    {
                        //Comentario = e.Message,
                        //OBSERVACAO = "Erro"
                    });
                    return lst.ToList();
                }

            }
        }

        public List<FinanceiroFaturas> TiposDespesas()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroFaturas>("EXEC STO_S_TAMINTEGRA_SW_NFE_SPDESP_TIPOS").ToList();

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
        public List<FinanceiroFaturas> Credor()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroFaturas>("EXEC STO_S_TAMINTEGRA_SW_NFE_SPDESP_CREDOR").ToList();

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
