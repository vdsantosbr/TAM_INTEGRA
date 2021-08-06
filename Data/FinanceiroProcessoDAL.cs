using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using System.Data.SqlClient;

namespace Data
{
    public class FinanceiroProcessoDAL
    {
        public List<FinanceiroProcesso> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string numProcesso, string numDI)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@DATA_INICIO ", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@DATA_TERMINO ", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                //SqlParameter pDeclaracao = new SqlParameter("@NF_TIPO_DEC_SISCOMEX ", (declaracao == 0) ? (object)DBNull.Value : declaracao);
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO  ", (numProcesso == null) ? (object)DBNull.Value : numProcesso);
                SqlParameter pNumDI = new SqlParameter("@NF_NUMERO_DI", (numDI == null) ? (object)DBNull.Value : numDI);

                var linha = db.Database.SqlQuery<FinanceiroProcesso>("EXEC STO_S_THOMSONREUTERS_PROCESSO @DATA_INICIO, @DATA_TERMINO, @NF_COD_PROCESSO, @NF_NUMERO_DI", pDataInicio, pDataFim, pCodProcesso, pNumDI).ToList();

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

        public List<FinanceiroProcesso> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID", (sp_id == null) ? (object)DBNull.Value : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO", (sp_id_despesa_processo == null) ? (object)DBNull.Value : sp_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroProcesso>("EXEC STO_S_TAMINTEGRA_SW_IS_OUT_SPDESP @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdIntegracao,pSpId, pSpIdDespesas).ToList();

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

        public List<FinanceiroProcesso> Reprocessamento(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID ", (sp_id == null) ? (object)DBNull.Value : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO  ", (sp_id_despesa_processo == null) ? (object)DBNull.Value : sp_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroProcesso>("EXEC STO_U_TAMINTEGRA_SW_IS_OUT_SPDESP_RMNUCLEUS @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdIntegracao, pSpId, sp_id_despesa_processo).ToList();

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

        public List<FinanceiroProcesso> TiposDespesas()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroProcesso>("EXEC STO_S_TR_IS_OUT_SPDESP_Tipos").ToList();

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
        public List<FinanceiroProcesso> Declaracao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroProcesso>("EXEC STO_S_THOMSONREUTERS_PROCESSO_TIPO_DECLARACAO").ToList();

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
        public List<FinanceiroProcesso> Guia(string codProcesso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProcesso == null) ? (object)DBNull.Value : codProcesso);

                    var linha = db.Database.SqlQuery<FinanceiroProcesso>("EXEC STO_S_THOMSONREUTERS_PROCESSO_DETALHAMENTO @NF_COD_PROCESSO", pCodProcesso).ToList();

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

        public List<FinanceiroGuiaFatura> InformeFatura(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaFatura>("EXEC STO_S_THOMSONREUTERS_PROCESSO_FATURA @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaFatura>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaImpostos> InformeImpostos(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaImpostos>("EXEC STO_S_THOMSONREUTERS_PROCESSO_IMPOSTOS @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaImpostos>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaDespesas> InformeDespesas(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaDespesas>("EXEC STO_S_THOMSONREUTERS_PROCESSO_DESPESAS @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaDespesas>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaEstoque> InformeEstoque(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaEstoque>("EXEC STO_S_THOMSONREUTERS_PROCESSO_NOTA_ESTOQUE @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaEstoque>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaEstoque> InformeEstoqueLACCTB(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaEstoque>("EXEC STO_S_THOMSONREUTERS_PROCESSO_NOTA_LACCTB @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaEstoque>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaHistorico> InformeHistorico(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaHistorico>("EXEC STO_S_THOMSONREUTERS_PROCESSO_HISTORICO @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaHistorico>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaDeclaracao> InformeDeclaracao(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaDeclaracao>("EXEC STO_S_THOMSONREUTERS_PROCESSO_DETALHAMENTO @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaDeclaracao>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaFinanceiro> InformeFinanceiro(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaFinanceiro>("EXEC STO_S_THOMSONREUTERS_PROCESSO_FINANCEIRO @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaFinanceiro>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaNotaCompl> InformeNotaCompl(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaNotaCompl>("EXEC STO_S_THOMSONREUTERS_PROCESSO_NOTA_COMPL @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaNotaCompl>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<FinanceiroGuiaFaturaPDC> InformeFaturaPDC(string codProceso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (codProceso == null) ? (object)DBNull.Value : codProceso);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroGuiaFaturaPDC>("EXEC STO_S_THOMSONREUTERS_PROCESSO_FATURA_PDC @NF_COD_PROCESSO", pCodProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroGuiaFaturaPDC>();
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
