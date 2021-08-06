using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.Globalization;

namespace Data
{
    public class ImportacaoSolPgtoDespesasDAL
    {
        public List<ImportacaoSolPgtoDespesas> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, int tpDoc, string numDoc, string processo, string nota, string situacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@SP_DATA_CADASTRO_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@SP_DATA_CADASTRO_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);
                SqlParameter pTpDoc = new SqlParameter("@ID_TIPO_DOCUMENTO", (tpDoc == 0) ? 0 : tpDoc);
                SqlParameter pNumDoc = new SqlParameter("@SP_NUM_DOCUMENTO", (numDoc == null) ? (object)DBNull.Value : numDoc);
                SqlParameter pCodProcesso = new SqlParameter("@SP_COD_PROCESSO", (processo == null) ? (object)DBNull.Value : processo);
                SqlParameter pNota = new SqlParameter("@FAT_NUM_FATURA", (nota == null) ? (object)DBNull.Value : nota);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", (situacao == null) ? "" : situacao);

                try { 
                var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC STO_S_TR_IS_OUT_SPDESP_CABECALHO_Filtro @SP_DATA_CADASTRO_INICIO, @SP_DATA_CADASTRO_TERMINO, @ID_TIPO_DOCUMENTO, @SP_NUM_DOCUMENTO, @SP_COD_PROCESSO, @FAT_NUM_FATURA, @SITUACAO", pDataInicio, pDataFim, pTpDoc, pNumDoc, pCodProcesso, pNota, pSituacao).ToList();

                if (linha.Count > 0)
                {
                    return linha;
                }
                else
                {
                    return null;
                }
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

        public List<ImportacaoSolPgtoDespesas> Edicao(int id_spdesp = 0, int id_integracao = 0, int sp_id = 0, int spd_id_despesa_processo = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdSpDesp = new SqlParameter("@ID_SPDESP", (id_spdesp == 0) ? (object)DBNull.Value : id_spdesp);
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pSpId = new SqlParameter("@SP_ID", (sp_id == 0) ? 0 : sp_id);
                SqlParameter pSpIdDespesas = new SqlParameter("@SPD_ID_DESPESA_PROCESSO", (spd_id_despesa_processo == 0) ? (object)DBNull.Value : spd_id_despesa_processo);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC STO_S_TR_IS_OUT_SPDESP_CABECALHO @ID_SPDESP, @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO", pIdSpDesp, pIdIntegracao, pSpId, pSpIdDespesas).ToList();

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

        public List<ImportacaoSolPgtoDespesas> Reprocessamento(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
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
                        var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC STO_U_TR_BS_OUT_NF_Reprocessar @ID_INTEGRACAO, @ID_BrokerSys, @NUMNF_BrokerSys", pIdIntegracao, pIdBrokerSys, pNumNFBrokerSys).ToList();
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
                        return new List<ImportacaoSolPgtoDespesas>();
                    }

                    
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        public List<ImportacaoSolPgtoDespesas> Excluir(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
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
                        var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC STO_U_TR_BS_OUT_NF_Cancelar @ID_INTEGRACAO, @ID_BrokerSys, @NUMNF_BrokerSys", pIdIntegracao, pIdBrokerSys, pNumNFBrokerSys).ToList();
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
                        return new List<ImportacaoSolPgtoDespesas>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        public List<ImportacaoSolPgtoDespesas> Emitir_NFE(int id_integracao = 0, string id_ne = "", string numnf_ne = "", string id_nd = null, string numnf_nd = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIDNE = new SqlParameter("@ID_NE", (id_ne == "") ? "" : id_ne);
                SqlParameter pNUMNFNE = new SqlParameter("@NUMNF_NE", (numnf_ne == "") ? "" : numnf_ne);
                SqlParameter pIDND = new SqlParameter("@NF_ID_ND", (id_nd == null) ? null : id_nd);
                SqlParameter pNUMNFND = new SqlParameter("@NF_NUMNF_ND", (id_ne == null) ? null : numnf_nd);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC STO_U_TR_BS_OUT_NF_Emissao @ID_INTEGRACAO,@ID_NE, @NUMNF_NE, @NF_ID_ND, @NF_NUMNF_ND", pIdIntegracao, pIDNE, pNUMNFNE, pIDND, pNUMNFND).ToList();
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
                        return new List<ImportacaoSolPgtoDespesas>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<ImportacaoSolPgtoDespesas> RecFisico(int id_integracao = 0, string id_ne = "", string numnf_ne = "", string id_nd = "", string numnf_nd = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIDNE = new SqlParameter("@ID_NE", (id_ne == "") ? "" : id_ne);
                SqlParameter pNUMNFNE = new SqlParameter("@NUMNF_NE", (numnf_ne == "") ? "" : numnf_ne);
                SqlParameter pIDND = new SqlParameter("@NF_ID_ND", (id_nd == null) ? null : id_nd);
                SqlParameter pNUMNFND = new SqlParameter("@NF_NUMNF_ND", (id_ne == null) ? null : numnf_nd);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC STO_U_TR_BS_OUT_NF_RecFisico @ID_INTEGRACAO,@ID_NE, @NUMNF_NE, @NF_ID_ND, @NF_NUMNF_ND", pIdIntegracao, pIDNE, pNUMNFNE, pIDND, pNUMNFND).ToList();
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
                        return new List<ImportacaoSolPgtoDespesas>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }

        public List<ImportacaoSolPgtoDespesas> Cancelar(int id_integracao = 0, string id_ne = "", string numnf_ne = "", string id_nd = "", string numnf_nd = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);
                SqlParameter pIDNE = new SqlParameter("@ID_NE", (id_ne == "") ? "" : id_ne);
                SqlParameter pNUMNFNE = new SqlParameter("@NUMNF_NE", (numnf_ne == "") ? "" : numnf_ne);
                SqlParameter pIDND = new SqlParameter("@NF_ID_ND", (id_nd == null) ? null : id_nd);
                SqlParameter pNUMNFND = new SqlParameter("@NF_NUMNF_ND", (id_ne == null) ? null : numnf_nd);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC STO_U_TR_BS_OUT_NF_Cancelar @ID_INTEGRACAO,@ID_NE, @NUMNF_NE, @NF_ID_ND, @NF_NUMNF_ND", pIdIntegracao, pIDNE, pNUMNFNE, pIDND, pNUMNFND).ToList();
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
                        return new List<ImportacaoSolPgtoDespesas>();
                    }


                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<ImportacaoSolPgtoDespesas> TipoDocumento()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC sto_sall_tr_is_out_spdesp_tipo_documento").ToList();

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
        public List<ImportacaoSolPgtoDespesas> Situacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC sto_sall_tr_is_out_spdesp_situacao").ToList();

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
        public List<ImportacaoSolPgtoDespesas> Filial()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC sto_s_rm_filial").ToList();

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
        public List<ImportacaoSolPgtoDespesas> Fornecedor()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC sto_s_rm_fornecedor").ToList();

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
        public List<ImportacaoSolPgtoDespesas> Moeda()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC sto_s_rm_moeda").ToList();

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
        public int SalvarDespesas(ImportacaoSolPgtoDespesas desp)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                DateTime data = new DateTime();
                int param_idspdesp = 0;
                var pIdSpDesp = new SqlParameter
                {
                    ParameterName = "@ID_SPDESP",
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                    Value = 0
                };

                SqlParameter pIdIntegracao = new SqlParameter();
                if (desp.Id_Integracao > 0)
                {
                    pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", (desp.Id_Integracao == 0) ? (object)DBNull.Value : desp.Id_Integracao);
                }
                else
                {
                    pIdIntegracao = new SqlParameter
                    {
                        ParameterName = "@ID_INTEGRACAO",
                        DbType = System.Data.DbType.Int32,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = 0
                    };
                }

                SqlParameter pIdDocumento = new SqlParameter("@ID_TIPO_DOCUMENTO", (desp.id_tp_documento_div == 0) ? (object)DBNull.Value : desp.id_tp_documento_div);
                SqlParameter pSpId = new SqlParameter("@SP_ID", (desp.Sp_ID == "" || desp.Sp_ID == null) ? "" : desp.Sp_ID);
                SqlParameter pProcesso = new SqlParameter("@SPD_ID_DESPESA_PROCESSO", (desp.Spd_Id_Despesa_Processo == "" || desp.Spd_Id_Despesa_Processo == null) ? "" : desp.Spd_Id_Despesa_Processo);
                SqlParameter pNumDocumento = new SqlParameter("@SP_NUM_DOCUMENTO", (desp.NumDocDiv == "" || desp.NumDocDiv == null) ? "" : desp.NumDocDiv);
                SqlParameter pCodProcesso = new SqlParameter("@SP_COD_PROCESSO", (desp.ProcessoDiv == "" || desp.ProcessoDiv == null) ? "" : desp.ProcessoDiv);
                SqlParameter pNumFatura = new SqlParameter("@FAT_NUM_FATURA", (desp.InvoiceDiv == "" | desp.InvoiceDiv == null) ? "" : desp.InvoiceDiv);
                SqlParameter pCodCredor = new SqlParameter("@SP_CODIGO_CREDOR_DESPESA", (desp.FornecedorDiv == "" || desp.FornecedorDiv == null) ? "" : desp.FornecedorDiv);
                SqlParameter pCodMoeda = new SqlParameter("@SP_COD_MOEDA", (desp.MoedaDiv == "") ? "" : desp.MoedaDiv);
                SqlParameter pHistorico = new SqlParameter("@HISTORICOCURTO", (desp.Justificativa == "" || desp.Justificativa == null) ? "" : desp.Justificativa);
                SqlParameter pCodFilial = new SqlParameter("@CODFILIAL", (desp.FilialDiv == 0) ? 0 : desp.FilialDiv);

                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                SqlParameter pDtCadastro = new SqlParameter("@SP_DATA_CADASTRO", System.Data.SqlDbType.DateTime);
                if (desp.DtEmissaoDiv.ToString() == "01/01/0001 00:00:00")
                {
                    pDtCadastro.Value = DBNull.Value;
                }
                else
                {
                    try
                    {
                        pDtCadastro.Value = DateTime.ParseExact(desp.DtEmissaoDiv, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    }catch(Exception)
                    {
                        pDtCadastro.Value = DateTime.ParseExact(desp.DtEmissaoDiv, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                    }
                }

                SqlParameter pDtVencimento = new SqlParameter("@SP_DATA_VENCIMENTO", System.Data.SqlDbType.DateTime);
                if (desp.VencimentoDiv.ToString() == "01/01/0001 00:00:00")
                {
                    pDtVencimento.Value = DBNull.Value;
                }
                else
                {
                    try
                    {
                        pDtVencimento.Value = DateTime.ParseExact(desp.VencimentoDiv, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        pDtVencimento.Value = DateTime.ParseExact(desp.VencimentoDiv, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                }

                SqlParameter pDtLiberacao = new SqlParameter("@SP_DATA_LIBERACAO", System.Data.SqlDbType.DateTime);
                if (desp.DtLiberacao == null)
                {
                    pDtLiberacao.Value = DBNull.Value;
                }
                else
                {
                    try
                    {
                        pDtLiberacao.Value = DateTime.ParseExact(desp.DtLiberacao, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        pDtLiberacao.Value = DateTime.ParseExact(desp.DtLiberacao, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                }

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoSolPgtoDespesas>("EXEC STO_I_TR_IS_OUT_SPDESP_CABECALHO @ID_SPDESP, @ID_TIPO_DOCUMENTO, @ID_INTEGRACAO, @SP_ID, @SPD_ID_DESPESA_PROCESSO, @SP_DATA_CADASTRO , @SP_DATA_VENCIMENTO , @SP_DATA_LIBERACAO , @SP_NUM_DOCUMENTO , @SP_COD_PROCESSO , @FAT_NUM_FATURA , @SP_CODIGO_CREDOR_DESPESA, @SP_COD_MOEDA , @HISTORICOCURTO , @CODFILIAL ",
                        pIdSpDesp, pIdDocumento, pIdIntegracao, pSpId, pProcesso, pDtCadastro, pDtVencimento, pDtLiberacao, pNumDocumento, pCodProcesso, pNumFatura, pCodCredor, pCodMoeda, pHistorico, pCodFilial).ToList();

                    if (linha != null)
                    {
                        foreach(var r in linha)
                        {
                            param_idspdesp = r.Id_SPDesp;
                        }

                        return param_idspdesp;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return 0;
                }

            }
        }

    }
}
