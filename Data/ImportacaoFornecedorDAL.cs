using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Data
{
    public class ImportacaoFornecedorDAL
    {
        public List<ImportacaoFornecedor> ListaFornecedor(int id_Perfil)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId_Perfil = new SqlParameter("@id_Perfil", id_Perfil);
                var linha = db.Database.SqlQuery<ImportacaoFornecedor>("EXEC STO_S_INTEGRACAO_SERVIDOR_POR_PERFIL @id_Perfil", pId_Perfil).ToList();
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

        public List<ImportacaoFornecedor> ListaProcesso(string formulario, int id_integracao_servidor, int id_Perfil = 0)
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

                var linha = db.Database.SqlQuery<ImportacaoFornecedor>("EXEC STO_S_INTEGRACAO_PROCESSO_FORMULARIO @formulario, @id_integracao_servidor,@Id_perfil", pFormulario, pIntegracao_Servidor, pId_Perfil).ToList();
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

        public List<ImportacaoFornecedor> ListaMovimento(string formulario, int id_integracao_processo)
        {
            List<ImportacaoFornecedor> x = new List<ImportacaoFornecedor>();
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pFormulario = new SqlParameter("@Formulario", "frmImportacaoDocumento");
                SqlParameter pIntegracaoProcesso = new SqlParameter("@id_Integracao_Processo", (id_integracao_processo == 0) ? 0 : id_integracao_processo);


                var linha = db.Database.SqlQuery<ImportacaoFornecedor>("EXEC STO_S_COMPRAS_FORMULARIO  @Formulario, @ID_INTEGRACAO_PROCESSO ", pFormulario, pIntegracaoProcesso).ToList();


                if (linha == null)
                {
                    x.Add(new ImportacaoFornecedor());
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

        public List<ImportacaoFornecedor> Filtro(int id_integracao = 0, string codigo = null, string nomeFantasia = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao= new SqlParameter("@ID_INTEGRACAO", (id_integracao == 0) ? 0: id_integracao);
                SqlParameter pCodTMV = new SqlParameter("@CODCFO", (codigo == null) ? (object)DBNull.Value : codigo);
                SqlParameter pNumeroMov = new SqlParameter("@NOMEFANTASIA", (nomeFantasia == null) ? (object)DBNull.Value : nomeFantasia);


                try
                {
                    try
                    {
                        var linha = db.Database.SqlQuery<ImportacaoFornecedor>("EXEC STO_S_RM_IMPORTSYS_FORNECEDOR  @ID_INTEGRACAO, @CODCFO, @NOMEFANTASIA", pIdIntegracao, pCodTMV, pNumeroMov).ToList();
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
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }

            }
        }
        public List<ImportacaoFornecedor> ItensPedido(int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IdMov", (idMov == 0) ? 0 : idMov);
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFornecedor>("EXEC STO_S_RM_DOCUMENTO_SOFTWAY_ITEM @idMov", pIdMov).ToList();
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

        public List<ImportacaoFornecedor> ItensPedidoEdicao(int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IdMov", (idMov == 0) ? 0 : idMov);
                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFornecedor>("EXEC STO_S_RM_DOCUMENTO_EDICAO @idMov", pIdMov).ToList();
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
        public List<ImportacaoFornecedor> abrirDocumento(int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IdMov", (idMov == 0) ? 0 : idMov);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFornecedor>("EXEC STO_S_RM_DOCUMENTO @idMov", pIdMov).ToList();
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

        public List<ImportacaoFornecedor> agendarIntegracao(int idMov = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdMov = new SqlParameter("@IdMov", (idMov == 0) ? 0 : idMov);

                try
                {
                    var linha = db.Database.SqlQuery<ImportacaoFornecedor>("EXEC STO_S_RM_DOCUMENTO @idMov", pIdMov).ToList();
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
