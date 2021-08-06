using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace Data
{
    public class ImportacaoSolPgtoDAL
    {
        public List<ImportacaoSolPgto> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string tipoDespesa, string codCredorDespesa, string processo, string situacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@SP_DATA_LIBERACAO_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@SP_DATA_LIBERACAO_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                SqlParameter pCodCredorDespesa = new SqlParameter("@SP_COD_CREDOR_DESPESA", (tipoDespesa == null) ? (object)DBNull.Value : tipoDespesa);
                SqlParameter pCodigoCredorDespesa = new SqlParameter("@SP_CODIGO_CREDOR_DESPESA ", (codCredorDespesa == null) ? (object)DBNull.Value : codCredorDespesa);
                SqlParameter pCodProcesso = new SqlParameter("@SP_COD_PROCESSO", (processo == null) ? (object)DBNull.Value : processo);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<ImportacaoSolPgto>("EXEC STO_S_TR_IS_OUT_SPDESP_FILTRO @SP_DATA_LIBERACAO_INICIO, @SP_DATA_LIBERACAO_TERMINO, @SP_COD_CREDOR_DESPESA, @SP_CODIGO_CREDOR_DESPESA, @SP_COD_PROCESSO, @SITUACAO", pDataInicio, pDataFim, pCodCredorDespesa, pCodigoCredorDespesa, pCodProcesso, pSituacao).ToList();

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
        public List<ImportacaoSolPgto> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID", (sp_id == null) ? (object)DBNull.Value : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO", (sp_id_despesa_processo == null) ? (object)DBNull.Value : sp_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgto>("EXEC STO_S_TR_OUT_NF_INFO @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdIntegracao, pSpId, pSpIdDespesas).ToList();

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

        public List<ImportacaoSolPgto> Reprocessar(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdBrokerSys = new SqlParameter("@ID_BrokerSys", (id_brokersys == 0) ? 0 : id_brokersys);
                SqlParameter pNumNFBrokerSys = new SqlParameter("@NUMNF_BrokerSys", (numnf_brokersys== 0) ? 0 : numnf_brokersys);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<ImportacaoSolPgto>("EXEC STO_S_TR_IS_OUT_SPDESP_Reprocessar @ID_INTEGRACAO, @ID_BrokerSys, @NUMNF_BrokerSys", pIdIntegracao, pIdBrokerSys, pNumNFBrokerSys).ToList();
                        if (linha.Count > 0)
                        {
                            return linha;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception)
                    {
                        return new List<ImportacaoSolPgto>();
                    }

                    
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        public List<ImportacaoSolPgto> Excluir(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIdBrokerSys = new SqlParameter("@ID_BrokerSys", (id_brokersys == 0) ? 0 : id_brokersys);
                SqlParameter pNumNFBrokerSys = new SqlParameter("@NUMNF_BrokerSys", (numnf_brokersys == 0) ? 0 : numnf_brokersys);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<ImportacaoSolPgto>("EXEC STO_S_TR_IS_OUT_SPDESP_Excluir @ID_INTEGRACAO, @ID_BrokerSys, @NUMNF_BrokerSys", pIdIntegracao, pIdBrokerSys, pNumNFBrokerSys).ToList();
                        if (linha.Count > 0)
                        {
                            return linha;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception)
                    {
                        return new List<ImportacaoSolPgto>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        public List<ImportacaoSolPgto> TiposDespesas()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgto>("EXEC STO_S_TR_IS_OUT_SPDESP_Tipos").ToList();

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
        public List<ImportacaoSolPgto> Credor()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgto>("EXEC STO_S_TR_IS_OUT_SPDESP_Credor").ToList();

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
