using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class IntegracaoDAL
    {
        int retorno = 0;
        //public Integracao BuscaIdIntegracaoProcesso(string formulario)
        //{
        //    using (DatabaseContext db = new DatabaseContext())
        //    {
        //        SqlParameter pIdIntegracaoProcesso = new SqlParameter("@FORMULARIO", formulario);

        //        var linha =  db.Database.SqlQuery<Integracao>("EXEC STO_S_INTEGRACAO_PROCESSO_FORMULARIO @FORMULARIO", pIdIntegracaoProcesso).ToList();

        //        if (linha.Count > 0)
        //        {
        //            return linha.First();
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
        public List<Integracao> BuscaIdIntegracaoProcesso(string formulario, int id_integracao_servidor, int id_Perfil = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //if(situacao == null)
                //{
                //    situacao = "";
                //}
                //SqlParameter pId_Integracao_Processo = new SqlParameter("@id_Integracao_Processo", (id_Integracao_Processo == 0) ? 0 : id_Integracao_Processo);
                //SqlParameter pIntegracao_Servidor = new SqlParameter("@id_Integracao_Servidor", (id_Integracao_Servidor == 0) ? 0 : id_Integracao_Servidor);
                //SqlParameter pSituacao = new SqlParameter("@Situacao", (situacao == null) ? (object)DBNull.Value : situacao);

                SqlParameter pFormulario = new SqlParameter("@formulario", (formulario == null) ? null : formulario);
                SqlParameter pIntegracao_Servidor = new SqlParameter("@id_Integracao_Servidor", (id_integracao_servidor == 0) ? 0 : id_integracao_servidor);
                SqlParameter pId_Perfil = new SqlParameter("@id_Perfil", (id_Perfil == 0) ? 0 : id_Perfil);

                var linha = db.Database.SqlQuery<Integracao>("EXEC STO_S_INTEGRACAO_PROCESSO_FORMULARIO @formulario, @id_integracao_servidor,@Id_perfil", pFormulario, pIntegracao_Servidor, pId_Perfil).ToList();
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
        public bool Integracao(Integracao integracao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //SqlParameter pidIntegracao = new SqlParameter("@ID_INTEGRACAO", integracao.Id_integracao);
                //pidIntegracao.ParameterName = "@ID_INTEGRACAO";
                //pidIntegracao.Direction = ParameterDirection.Output;
                //pidIntegracao.SqlDbType = SqlDbType.Int;

                SqlParameter pIdentity = new SqlParameter();
                pIdentity.ParameterName = "@ID_INTEGRACAO";
                pIdentity.Direction = ParameterDirection.Output;
                pIdentity.SqlDbType = SqlDbType.Int;

                SqlParameter pidIntegracaoProcesso = new SqlParameter("@id_Integracao_Processo", integracao.Id_integracao_Processo);
                SqlParameter pcomplemento = new SqlParameter("@complemento", integracao.Complemento);
                SqlParameter pTipo = new SqlParameter("@tipo", integracao.Tipo);
                SqlParameter pSituacao = new SqlParameter("@situacao", integracao.Situacao);
                SqlParameter pObservacao = new SqlParameter("@observacao", integracao.Observacao);
                SqlParameter pIdPessoa = new SqlParameter("@id_Pessoa", integracao.Id_Pessoa);
                SqlParameter pIdMov = new SqlParameter("@idMov", integracao.IdMov);

                //try
                //{
                    var linha = db.Database.ExecuteSqlCommand("EXEC STO_I_INTEGRACAO @ID_INTEGRACAO out, @id_Integracao_Processo, @complemento, @tipo, @situacao, @observacao, @id_Pessoa, @idMov",
                   pIdentity, pidIntegracaoProcesso, pcomplemento, pTipo, pSituacao, pObservacao, pIdPessoa, pIdMov);

                    integracao.Id_integracao = (int)pIdentity.Value;

                    if (linha == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                //}
                //catch(Exception e)
                //{
                //    var erro = e.Message;
                //    return false;
                //}
               


            }
        }

        public Integracao IntegracaoImportacaoProduto(int idIntegracao = 0, int idIntegracaoProcesso = 0, string complemento = null, string tipo = null, string situacao = null, string observacao = null, int idPessoa = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pidIntegracao = new SqlParameter("@ID_INTEGRACAO", DBNull.Value);
                SqlParameter pidIntegracaoProcesso = new SqlParameter("@id_Integracao_Processo", idIntegracaoProcesso);
                SqlParameter pcomplemento = new SqlParameter("@complemento", "CADASTRO");
                SqlParameter pTipo = new SqlParameter("@tipo", "CADASTRO");
                SqlParameter pSituacao = new SqlParameter("@situacao", "AG");
                SqlParameter pObservacao = new SqlParameter("@observacao", DBNull.Value);
                SqlParameter pIdPessoa = new SqlParameter("@id_Pessoa", idPessoa);

                var linha = db.Database.SqlQuery<Integracao>("EXEC STO_I_INTEGRACAO @ID_INTEGRACAO, @id_Integracao_Processo, @complemento, @tipo, @situacao, @observacao, @id_Pessoa",
                    pidIntegracao, pidIntegracaoProcesso, pcomplemento, pTipo, pSituacao, pObservacao, pIdPessoa).ToList();

                if (linha.Count > 0)
                {
                    return linha.First();
                }
                else
                {
                    return null;
                }
            }
        }

        public Integracao IntegracaoParametro(string origem , int idIntegracao, int idMov, int IDCFO = 0, int NSEQITMMOV = 0, int IDPRD = 0, string serial = "", string chave = "", int NF_ITE_NM_ADICAO = 0, int NF_ITE_NM_ITEM_ADICAO = 0,string final_destination = "", int idLan = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //SqlParameter pDATA_RESPOSTA = new SqlParameter("@DATA_RESPOSTA", SqlDbType.DateTime);
                //pDATA_RESPOSTA.Direction = ParameterDirection.Input;
                //pDATA_RESPOSTA.IsNullable = true;
                //if (obj.DATA_RESPOSTA == null)
                //{
                //    pDATA_RESPOSTA.Value = DBNull.Value;
                //}
                //else
                //{
                //    pDATA_RESPOSTA.Value = obj.DATA_RESPOSTA;
                //}


                SqlParameter pidIntegracao = new SqlParameter("@ID_INTEGRACAO", idIntegracao);
                SqlParameter pidMov = new SqlParameter("@idMov", idMov);
                SqlParameter pidCFO = new SqlParameter("@idCFO", (IDCFO) == 0 ? (object)DBNull.Value : IDCFO);
                SqlParameter pidPRD = new SqlParameter("@IDPRD", (IDPRD) == 0 ? (object)DBNull.Value : IDPRD);
                SqlParameter pSerial = new SqlParameter("@SERIAL", (serial) == "" ? (object)DBNull.Value : serial);
                SqlParameter pChave = new SqlParameter("@CHAVE", (chave) == "" ? (object)DBNull.Value : chave);

                SqlParameter pNSEQITMMOV = new SqlParameter("@NSEQITMMOV", (NSEQITMMOV) == 0 ? (object)DBNull.Value : NSEQITMMOV);
                SqlParameter pNF_ITE_NM_ADICAO = new SqlParameter("@NF_ITE_NM_ADICAO", (NF_ITE_NM_ADICAO) == 0 ? (object)DBNull.Value : NF_ITE_NM_ADICAO);
                SqlParameter pNF_ITE_NM_ITEM_ADICAO = new SqlParameter("@NF_ITE_NM_ITEM_ADICAO", (NF_ITE_NM_ITEM_ADICAO) == 0 ? (object)DBNull.Value : NF_ITE_NM_ITEM_ADICAO);

                SqlParameter pOrigem = new SqlParameter("@origem", (origem) == null ? (object)DBNull.Value : origem);
                SqlParameter pfreight_forwarder = new SqlParameter("@freight_forwarder", "");
                //SqlParameter pfinal_destination = new SqlParameter("@final_destination", (object)DBNull.Value);
                
                SqlParameter pfinal_destination = new SqlParameter("@final_destination", SqlDbType.NVarChar);

                pfinal_destination.Direction = ParameterDirection.Input;
                pfinal_destination.IsNullable = true;
                if (final_destination == null)
                {
                    pfinal_destination.Value = DBNull.Value;
                }
                else
                {
                    pfinal_destination.Value = final_destination;
                }

                SqlParameter pPayment_method = new SqlParameter("@payment_method", "");
                SqlParameter pShipping_reference = new SqlParameter("@shipping_reference", "");
                SqlParameter pShipping_reference_compl = new SqlParameter("@shipping_reference_compl", "");
                SqlParameter pShipping_reference_carrier = new SqlParameter("@shipping_reference_carrier", "");
                SqlParameter pContact_name = new SqlParameter("@contact_name", "");
                SqlParameter pContact_phone = new SqlParameter("@contact_phone", "");
                SqlParameter pExport_information = new SqlParameter("@export_information", "");
                SqlParameter pContact_email = new SqlParameter("@contact_email", "");
                SqlParameter pOrder_note = new SqlParameter("@order_note", "");
                SqlParameter pDestination_country = new SqlParameter("@destination_country", "");
                SqlParameter pAll_parts_are_in_stock = new SqlParameter("@all_parts_are_in_stock", "");
                SqlParameter pIdLan = new SqlParameter("@idLan", (idLan) == 0 ? (object)DBNull.Value : idLan);

                try
                {
                    var linha = db.Database.SqlQuery<Integracao>("EXEC STO_I_INTEGRACAO_PARAMETRO @ID_INTEGRACAO, @idMov, @IDCFO, @NSEQITMMOV, @IDPRD, @SERIAL, @CHAVE, @NF_ITE_NM_ADICAO, @NF_ITE_NM_ITEM_ADICAO,  @origem, @freight_forwarder, @final_destination, @payment_method, @shipping_reference, @shipping_reference_compl,@shipping_reference_carrier,@contact_name,@contact_email,@contact_phone,@export_information,@order_note,@destination_country,@all_parts_are_in_stock, @IDLAN",
                    pidIntegracao, pidMov, pidCFO, pNSEQITMMOV, pidPRD, pSerial, pChave, pNF_ITE_NM_ADICAO, pNF_ITE_NM_ITEM_ADICAO, pOrigem, pfreight_forwarder, pfinal_destination, pPayment_method, pShipping_reference, pShipping_reference_compl, pShipping_reference_carrier, pContact_name, pContact_email, pContact_phone, pExport_information, pOrder_note, pDestination_country, pAll_parts_are_in_stock, pIdLan).ToList();

                    if (linha.Count > 0)
                    {
                        return linha.First();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception e)
                {
                    var erro = e.Message;
                    return null;
                }
            }
        }

        public Integracao DesbloquearPedido(int idIntegracao = 0, int idMov = 0, int idPessoa = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pidIntegracao = new SqlParameter("@ID_INTEGRACAO", idIntegracao);
                SqlParameter pIdMov = new SqlParameter("@idMov", idMov);
                SqlParameter pIdPessoa = new SqlParameter("@id_Pessoa", idPessoa);

                var linha = db.Database.SqlQuery<Integracao>("EXEC STO_U_INTEGRACAO_DESBLOQUEAR @ID_INTEGRACAO, @idMov, @id_Pessoa",
                    pidIntegracao, pIdMov, pIdPessoa).ToList();

                if (linha.Count > 0)
                {
                    return linha.First();
                }
                else
                {
                    return null;
                }
            }
        }
        public List<Integracao> IntegracaoProcesso(int idIntegracaoProcesso = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {


                SqlParameter pidIntegracaoProcesso = new SqlParameter("@ID_INTEGRACAO_PROCESSO", idIntegracaoProcesso);
                SqlParameter pIntegracaoServidor = new SqlParameter("@ID_INTEGRACAO_SERVIDOR", DBNull.Value);
                //pIntegracaoServidor.DbType = DbType.Int16;
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", "");
                pSituacao.DbType = DbType.String;


                try
                {

                    var linha = db.Database.SqlQuery<Integracao>("EXEC STO_S_INTEGRACAO_PROCESSO @ID_INTEGRACAO_PROCESSO, @ID_INTEGRACAO_SERVIDOR, @SITUACAO",
                        pidIntegracaoProcesso, pIntegracaoServidor, pSituacao).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception e)
                {
                    var erro = e.Message;
                    return null;
                }
                
            }
        }


    }
}
