using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace Data
{
    public class EstoqueDAL
    {
        public List<Estoque> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string declaracao, string processo, string nota, string situacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@DATA_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@DATA_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                SqlParameter pDeclaracao = new SqlParameter("@NF_TIPO_DEC_SISCOMEX", (declaracao == null) ? "" : declaracao);
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (processo == null) ? (object)DBNull.Value : processo);
                SqlParameter pNota = new SqlParameter("@NF_NUMNF", (nota == null) ? (object)DBNull.Value : nota);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", (situacao == null) ? "" : situacao);

                var linha = db.Database.SqlQuery<Estoque>("EXEC STO_S_TR_BS_OUT_NF_Filtro @DATA_INICIO, @DATA_TERMINO, @NF_TIPO_DEC_SISCOMEX, @NF_COD_PROCESSO, @NF_NUMNF, @SITUACAO", pDataInicio, pDataFim, pDeclaracao, pCodProcesso, pNota, pSituacao).ToList();

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

        public List<Estoque> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID", (sp_id == null) ? (object)DBNull.Value : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO", (sp_id_despesa_processo == null) ? (object)DBNull.Value : sp_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<Estoque>("EXEC STO_S_TR_OUT_NF_INFO @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdIntegracao, pSpId, pSpIdDespesas).ToList();

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

        public List<Estoque> Reprocessamento(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
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
                        var linha = db.Database.SqlQuery<Estoque>("EXEC STO_U_TR_BS_OUT_NF_Reprocessar @ID_INTEGRACAO, @ID_BrokerSys, @NUMNF_BrokerSys", pIdIntegracao, pIdBrokerSys, pNumNFBrokerSys).ToList();
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
                        return new List<Estoque>();
                    }

                    
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        public List<Estoque> Excluir(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
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
                        var linha = db.Database.SqlQuery<Estoque>("EXEC STO_U_TR_BS_OUT_NF_Cancelar @ID_INTEGRACAO, @ID_BrokerSys, @NUMNF_BrokerSys", pIdIntegracao, pIdBrokerSys, pNumNFBrokerSys).ToList();
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
                        return new List<Estoque>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        public List<Estoque> Emitir_NFE(string nf_cod_processo = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                //SqlParameter pIDNE = new SqlParameter("@ID_NE", (id_ne == "") ? "" : id_ne);
                //SqlParameter pNUMNFNE = new SqlParameter("@NUMNF_NE", (numnf_ne == "") ? "" : numnf_ne);
                //SqlParameter pIDND = new SqlParameter("@NF_ID_ND", (id_nd == null) ? null : id_nd);
                //SqlParameter pNUMNFND = new SqlParameter("@NF_NUMNF_ND", (id_ne == null) ? null : numnf_nd);
                //SqlParameter pIDBSOUT = new SqlParameter("@ID_TR_BS_OUT_NF", (id_tr_bs_out_nf == 0) ? 0 : id_tr_bs_out_nf);
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (nf_cod_processo == "") ? "" : nf_cod_processo);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<Estoque>("EXEC STO_U_TR_BS_OUT_NF_Emissao @NF_COD_PROCESSO", pCodProcesso).ToList();
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
                        return new List<Estoque>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<Estoque> RecFisico(string nf_cod_processo = "", string tp_nf = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                //SqlParameter pIDNE = new SqlParameter("@ID_NE", (id_ne == "") ? "" : id_ne);
                //SqlParameter pNUMNFNE = new SqlParameter("@NUMNF_NE", (numnf_ne == "") ? "" : numnf_ne);
                //SqlParameter pIDND = new SqlParameter("@NF_ID_ND", (id_nd == null) ? null : id_nd);
                //SqlParameter pNUMNFND = new SqlParameter("@NF_NUMNF_ND", (id_ne == null) ? null : numnf_nd);
                //SqlParameter pIDBSOUT = new SqlParameter("@ID_TR_BS_OUT_NF", (id_tr_bs_out_nf == 0) ? 0 : id_tr_bs_out_nf);
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (nf_cod_processo == "") ? "" : nf_cod_processo);
                SqlParameter pTpNF = new SqlParameter("@NF_TPNOTA", (tp_nf == "") ? "" : tp_nf);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<Estoque>("EXEC STO_U_TR_BS_OUT_NF_RecFisico @NF_COD_PROCESSO", pCodProcesso).ToList();
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
                        return new List<Estoque>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        public List<Estoque> Cancelar(string nf_cod_processo = "", string tp_nf = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                //SqlParameter pIDNE = new SqlParameter("@ID_NE", (id_ne == "") ? "" : id_ne);
                //SqlParameter pNUMNFNE = new SqlParameter("@NUMNF_NE", (numnf_ne == "") ? "" : numnf_ne);
                //SqlParameter pIDND = new SqlParameter("@NF_ID", (id_nd == null) ? null : id_nd);
                //SqlParameter pNUMNFND = new SqlParameter("@NF_NUMNF", (id_ne == null) ? null : numnf_nd);
                //SqlParameter pIDBSOUT = new SqlParameter("@ID_TR_BS_OUT_NF", (id_tr_bs_out_nf == 0) ? 0 : id_tr_bs_out_nf);
                SqlParameter pCodProcesso = new SqlParameter("@NF_COD_PROCESSO", (nf_cod_processo == "") ? "" : nf_cod_processo);
                SqlParameter pTpNF = new SqlParameter("@NF_TPNOTA", (tp_nf == "") ? "" : tp_nf);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<Estoque>("EXEC STO_U_TR_BS_OUT_NF_Cancelar @NF_COD_PROCESSO, @NF_TPNOTA", pCodProcesso, pTpNF).ToList();
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
                        return new List<Estoque>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<Estoque> TiposDespesas()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<Estoque>("EXEC STO_S_TR_IS_OUT_SPDESP_Tipos").ToList();

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
        public List<Estoque> Credor()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<Estoque>("EXEC STO_S_TR_IS_OUT_SPDESP_Credor").ToList();

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
