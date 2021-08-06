using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using System.Data.SqlClient;

namespace Data
{
    public class FinanceiroDespesasDAL
    {
        public List<FinanceiroDespesas> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, int faturamento, string codCredorDespesa = "", string codigoCredorDespesa = "", string processo = "", string situacao = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@SP_DATA_LIBERACAO_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@SP_DATA_LIBERACAO_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                SqlParameter pCodCredorDespesa = new SqlParameter("@SP_COD_CREDOR_DESPESA", (codCredorDespesa == null) ? "" : codCredorDespesa);
                SqlParameter pCodigoCredorDespesa = new SqlParameter("@SP_CODIGO_CREDOR_DESPESA ", (codigoCredorDespesa == null) ? "" : codigoCredorDespesa);
                SqlParameter pCodProcesso = new SqlParameter("@SP_COD_PROCESSO", (processo == null) ? (object)DBNull.Value : processo);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<FinanceiroDespesas>("EXEC STO_S_TR_IS_OUT_SPDESP_FILTRO @SP_DATA_LIBERACAO_INICIO, @SP_DATA_LIBERACAO_TERMINO, @SP_COD_CREDOR_DESPESA, @SP_CODIGO_CREDOR_DESPESA, @SP_COD_PROCESSO, @SITUACAO", pDataInicio, pDataFim, pCodCredorDespesa, pCodigoCredorDespesa, pCodProcesso, pSituacao).ToList();

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

        public List<FinanceiroDespesas> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID", (sp_id == null) ? (object)DBNull.Value : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO", (sp_id_despesa_processo == null) ? (object)DBNull.Value : sp_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroDespesas>("EXEC STO_S_TR_IS_OUT_SPDESP_Info @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdIntegracao,pSpId, pSpIdDespesas).ToList();

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

        public List<FinanceiroDespesas> Reprocessamento(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID ", (sp_id == null) ? (object)DBNull.Value : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO  ", (sp_id_despesa_processo == null) ? (object)DBNull.Value : sp_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroDespesas>("EXEC STO_U_TR_IS_OUT_SPDESP_Reprocessar @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdIntegracao, pSpId, sp_id_despesa_processo).ToList();

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

        public List<FinanceiroDespesas> Excluir(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID ", (sp_id == null) ? (object)DBNull.Value : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO  ", (sp_id_despesa_processo == null) ? (object)DBNull.Value : sp_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroDespesas>("EXEC STO_U_TR_IS_OUT_SPDESP_Excluir @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdIntegracao, pSpId, sp_id_despesa_processo).ToList();

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

        public List<FinanceiroDespesas> TiposDespesas()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroDespesas>("EXEC STO_S_TR_IS_OUT_SPDESP_Tipos").ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<FinanceiroDespesas>();
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return new List<FinanceiroDespesas>();
                }

            }
        }
        public List<FinanceiroDespesas> Credor()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroDespesas>("EXEC STO_S_TR_IS_OUT_SPDESP_Credor").ToList();

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
