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
    public class ImportacaoDocumentoDAL
    {
        public List<ImportacaoDocumento> ListaFornecedor(int id_Perfil)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId_Perfil = new SqlParameter("@id_Perfil", id_Perfil);
                var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_INTEGRACAO_SERVIDOR_POR_PERFIL @id_Perfil", pId_Perfil).ToList();
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

        public List<ImportacaoDocumento> ListaProcesso(string formulario, int id_integracao_servidor, int id_Perfil = 0)
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

                SqlParameter pIntegracao_Servidor = new SqlParameter();
                pIntegracao_Servidor.ParameterName = "@id_Integracao_Servidor";
                pIntegracao_Servidor.SqlDbType = System.Data.SqlDbType.Int;
                pIntegracao_Servidor.Direction = ParameterDirection.Input;
                pIntegracao_Servidor.Value = 0;

                SqlParameter pFormulario = new SqlParameter("@formulario", (formulario == null) ? null : formulario);
                //SqlParameter pIntegracao_Servidor = new SqlParameter("@id_Integracao_Servidor", (id_integracao_servidor == 0) ? 0 : id_integracao_servidor);
                SqlParameter pId_Perfil = new SqlParameter("@id_Perfil", (id_Perfil == 0) ? 0 : id_Perfil);

                var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_INTEGRACAO_PROCESSO_FORMULARIO @formulario, @id_integracao_servidor,@Id_perfil", pFormulario, pIntegracao_Servidor, pId_Perfil).ToList();

                try
                {
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
                    return new List<ImportacaoDocumento>();
                }
               
            }
        }

        public List<ImportacaoDocumento> ListaMovimento(string formulario, int id_integracao_processo)
        {
            List<ImportacaoDocumento> x = new List<ImportacaoDocumento>();
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pFormulario = new SqlParameter("@Formulario", "frmImportacaoDocumento");
                SqlParameter pIntegracaoProcesso = new SqlParameter("@id_Integracao_Processo", (id_integracao_processo == 0) ? 0 : id_integracao_processo);
                

                var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_COMPRAS_FORMULARIO  @Formulario, @ID_INTEGRACAO_PROCESSO ", pFormulario, pIntegracaoProcesso).ToList();


                    if (linha == null)
                    {
                        x.Add(new ImportacaoDocumento());
                    }


                if (linha.Count > 0)
                {
                    return linha;
                }
                else
                {
                    return x;
                }
            }
        }

        public List<ImportacaoDocumento> Filtro(int id_Perfil, DateTime data_Inicio, DateTime data_Fim, string codTMV, string numeroMov, string situacao, int id_integracao = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId_Perfil = new SqlParameter("@id_Perfil", (id_Perfil == 0) ? 0 : id_Perfil);
                SqlParameter pFormulario = new SqlParameter("@Formulario", "frmImportacaoDocumento");
                SqlParameter pData_Inicio = new SqlParameter("@Data_Inicio", data_Inicio);
                SqlParameter pData_Fim = new SqlParameter("@Data_Termino", data_Fim);
                SqlParameter pCodTMV = new SqlParameter("@codTMV", (codTMV == null) ? (object)DBNull.Value : codTMV);
                SqlParameter pNumeroMov = new SqlParameter("@numeroMov", (numeroMov == null) ? (object) DBNull.Value : numeroMov);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);

                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_RM_DOCUMENTO_SOFTWAY  @Data_Inicio, @Data_Termino, @codTMV, @numeroMov, @situacao, @id_integracao", pData_Inicio, pData_Fim, pCodTMV, pNumeroMov, pSituacao, pIdIntegracao).ToList();
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
                catch(Exception e)
                {
                    var erro = e.Message;
                    return null;
                }
                
            }
        }
        public List<ImportacaoDocumento> ItensPedido(int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IdMov", (idMov == 0) ? 0 : idMov);
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_RM_DOCUMENTO_SOFTWAY_ITEM @idMov", pIdMov).ToList();
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

        public List<ImportacaoDocumento> ItensPedidoEdicao(int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IdMov", (idMov == 0) ? 0 : idMov);
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_RM_DOCUMENTO_EDICAO @idMov", pIdMov).ToList();
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
        public List<ImportacaoDocumento> abrirDocumento(int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IdMov", (idMov == 0) ? 0 : idMov);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_RM_DOCUMENTO @idMov", pIdMov).ToList();
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

        public List<ImportacaoDocumento> agendarIntegracao(int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IdMov", (idMov == 0) ? 0 : idMov);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_RM_DOCUMENTO @idMov", pIdMov).ToList();
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
        public List<ImportacaoDocumento> IntegracaoParametroValidar(int id_mov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IDMOV", id_mov);
                var linha = db.Database.SqlQuery<ImportacaoDocumento>("EXEC STO_S_INTEGRACAO_PARAMETRO_Validar @IDMOV", pIdMov).ToList();
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

    }
}
