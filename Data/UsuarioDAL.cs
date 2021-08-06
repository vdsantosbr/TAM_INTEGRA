using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UsuarioDAL
    {
        private DatabaseContext db = new DatabaseContext();
        private int retorno;

        public List<Usuario> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                return db.Database.SqlQuery<Usuario>("EXEC SP_Usuario NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'Lista'").ToList();
            }
        }

        public Usuario BuscaPorId(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pIdArea = new SqlParameter("@idArea", DBNull.Value);
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", DBNull.Value);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pSobrenome = new SqlParameter("@sobrenome", DBNull.Value);
                SqlParameter pLogin = new SqlParameter("@login", DBNull.Value);
                SqlParameter pEmail = new SqlParameter("@email", DBNull.Value);
                SqlParameter pVendedor = new SqlParameter("@vendedor", DBNull.Value);
                SqlParameter pexibirValorVenda = new SqlParameter("@exibirValorVenda", DBNull.Value);
                SqlParameter pExibirCorVerde = new SqlParameter("@exibirCorVerde", DBNull.Value);
                SqlParameter pAutorizarExportacao = new SqlParameter("@autorizarExportacao", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");
                var linha = db.Database.SqlQuery<Usuario>("SP_Usuario  @idUsuario, @id, @idArea, @idPerfil, @idSituacao, @nome, @sobrenome, @login, @email, @vendedor, @exibirValorVenda, @exibirCorVerde, @autorizarExportacao, @operacao", pUsu, pId, pIdArea, pIdPerfil, pIdSituacao, pNome, pSobrenome, pLogin, pEmail, pVendedor, pexibirValorVenda, pExibirCorVerde, pAutorizarExportacao, pOper).ToList();
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

        public Usuario BuscaPorIdPessoa(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", id);
                SqlParameter pIdDepartamento = new SqlParameter();
                pIdDepartamento.ParameterName = "@ID_DEPARTAMENTO";
                pIdDepartamento.SqlDbType = System.Data.SqlDbType.Int;
                pIdDepartamento.Direction = ParameterDirection.Input;
                pIdDepartamento.Value = 0;
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", "");

                var linha = db.Database.SqlQuery<Usuario>("STO_S_PESSOA  @ID_PESSOA, @ID_DEPARTAMENTO, @SITUACAO", pIdPessoa, pIdDepartamento, pSituacao).ToList();
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

        public Usuario BuscaPorLogin(string login)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.Database.Connection.Open();
                SqlParameter pLogin = new SqlParameter("@login", login);

                var linha = db.Database.SqlQuery<Usuario>("STO_S_PESSOA_LOGIN  @login", pLogin).ToList();
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

        public Usuario BuscaPorIdPerfil(int idPerfil)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdPerfil = new SqlParameter("@id_Perfil", idPerfil);

                var linha = db.Database.SqlQuery<Usuario>("STO_S_PERFIL_FUNCIONALIDADE_PERFIL  @id_Perfil", pIdPerfil).ToList();
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

        //public Usuario BuscaPorLogin(string login)
        //{
        //    using (DatabaseContext db = new DatabaseContext())
        //    {
        //        SqlParameter pLogin = new SqlParameter("@login", login);
        //        var linha = db.Database.SqlQuery<Usuario>("STO_S_PESSOA_LOGIN  @login", pLogin).ToList();
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


        public List<Usuario> BuscaPorPerfil(int idPerfil)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdArea = new SqlParameter("@idArea", DBNull.Value);
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", idPerfil);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pSobrenome = new SqlParameter("@sobrenome", DBNull.Value);
                SqlParameter pLogin = new SqlParameter("@login", DBNull.Value);
                SqlParameter pEmail = new SqlParameter("@email", DBNull.Value);
                SqlParameter pVendedor = new SqlParameter("@vendedor", DBNull.Value);
                SqlParameter pexibirValorVenda = new SqlParameter("@exibirValorVenda", DBNull.Value);
                SqlParameter pExibirCorVerde = new SqlParameter("@exibirCorVerde", DBNull.Value);
                SqlParameter pAutorizarExportacao = new SqlParameter("@autorizarExportacao", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorPerfil");
                var linhas = db.Database.SqlQuery<Usuario>("SP_Usuario  @idUsuario, @id, @idArea, @idPerfil, @idSituacao, @nome, @sobrenome, @login, @email, @vendedor, @exibirValorVenda, @exibirCorVerde, @autorizarExportacao, @operacao", pUsu, pId, pIdArea, pIdPerfil, pIdSituacao, pNome, pSobrenome, pLogin, pEmail, pVendedor, pexibirValorVenda, pExibirCorVerde, pAutorizarExportacao, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return new List<Usuario>();
                }
            }
        }

        public List<Usuario> BuscaPorArea(int idArea)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdArea = new SqlParameter("@idArea", idArea);
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", DBNull.Value);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pSobrenome = new SqlParameter("@sobrenome", DBNull.Value);
                SqlParameter pLogin = new SqlParameter("@login", DBNull.Value);
                SqlParameter pEmail = new SqlParameter("@email", DBNull.Value);
                SqlParameter pVendedor = new SqlParameter("@vendedor", DBNull.Value);
                SqlParameter pexibirValorVenda = new SqlParameter("@exibirValorVenda", DBNull.Value);
                SqlParameter pExibirCorVerde = new SqlParameter("@exibirCorVerde", DBNull.Value);
                SqlParameter pAutorizarExportacao = new SqlParameter("@autorizarExportacao", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorArea");
                var linhas = db.Database.SqlQuery<Usuario>("SP_Usuario  @idUsuario, @id, @idArea, @idPerfil, @idSituacao, @nome, @sobrenome, @login, @email, @vendedor, @exibirValorVenda, @exibirCorVerde, @autorizarExportacao, @operacao", pUsu, pId, pIdArea, pIdPerfil, pIdSituacao, pNome, pSobrenome, pLogin, pEmail, pVendedor, pexibirValorVenda, pExibirCorVerde, pAutorizarExportacao, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return new List<Usuario>();
                }
            }
        }

        public Usuario BuscaPorDuplicidade(Usuario obj)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", obj.Id);
                SqlParameter pIdArea = new SqlParameter("@idArea", obj.IdArea);
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", obj.IdPerfil);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pSobrenome = new SqlParameter("@sobrenome", obj.Sobrenome);
                SqlParameter pLogin = new SqlParameter("@login", obj.Login);
                SqlParameter pEmail = new SqlParameter("@email", obj.Email);
                SqlParameter pVendedor = new SqlParameter("@vendedor", DBNull.Value);
                SqlParameter pexibirValorVenda = new SqlParameter("@exibirValorVenda", DBNull.Value);
                SqlParameter pExibirCorVerde = new SqlParameter("@exibirCorVerde", DBNull.Value);
                SqlParameter pAutorizarExportacao = new SqlParameter("@autorizarExportacao", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorDuplicidade");
                var linha = db.Database.SqlQuery<Usuario>("SP_Usuario  @idUsuario, @id, @idArea, @idPerfil, @idSituacao, @nome, @sobrenome, @login, @email, @vendedor, @exibirValorVenda, @exibirCorVerde, @autorizarExportacao, @operacao", pUsu, pId, pIdArea, pIdPerfil, pIdSituacao, pNome, pSobrenome, pLogin, pEmail, pVendedor, pexibirValorVenda, pExibirCorVerde, pAutorizarExportacao, pOper).ToList();
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

        public bool Insere(Usuario obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor);
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdArea = new SqlParameter("@idArea", obj.IdArea);
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", obj.IdPerfil);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", obj.IdSituacao);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pSobrenome = new SqlParameter("@sobrenome", obj.Sobrenome);
                SqlParameter pLogin = new SqlParameter("@login", obj.Login);
                SqlParameter pEmail = new SqlParameter("@email", obj.Email);
                SqlParameter pOper = new SqlParameter("@operacao", "Insere");

                retorno = db.Database.ExecuteSqlCommand("SP_Usuario  @idUsuario, @id, @idArea, @idPerfil, @idSituacao, @nome, @sobrenome, @login, @email, @vendedor, @exibirValorVenda, @exibirCorVerde, @autorizarExportacao, @operacao", pUsu, pId, pIdArea, pIdPerfil, pIdSituacao, pNome, pSobrenome, pLogin, pEmail, pOper);

                if (retorno == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Atualiza(Usuario obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor);
                SqlParameter pId = new SqlParameter("@id", obj.Id);
                SqlParameter pIdArea = new SqlParameter("@idArea", obj.IdArea);
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", obj.IdPerfil);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", obj.IdSituacao);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pSobrenome = new SqlParameter("@sobrenome", obj.Sobrenome);
                SqlParameter pLogin = new SqlParameter("@login", obj.Login);
                SqlParameter pEmail = new SqlParameter("@email", obj.Email);

                SqlParameter pOper = new SqlParameter("@operacao", "Atualiza");

                retorno = db.Database.ExecuteSqlCommand("SP_Usuario  @idUsuario, @id, @idArea, @idPerfil, @idSituacao, @nome, @sobrenome, @login, @email, @vendedor, @exibirValorVenda, @exibirCorVerde, @autorizarExportacao, @operacao", pUsu, pId, pIdArea, pIdPerfil, pIdSituacao, pNome, pSobrenome, pLogin, pEmail, pOper);

                if (retorno == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Apaga(int id, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor);
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pIdArea = new SqlParameter("@idArea", DBNull.Value);
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", DBNull.Value);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pSobrenome = new SqlParameter("@sobrenome", DBNull.Value);
                SqlParameter pLogin = new SqlParameter("@login", DBNull.Value);
                SqlParameter pEmail = new SqlParameter("@email", DBNull.Value);
                SqlParameter pVendedor = new SqlParameter("@vendedor", DBNull.Value);
                SqlParameter pexibirValorVenda = new SqlParameter("@exibirValorVenda", DBNull.Value);
                SqlParameter pExibirCorVerde = new SqlParameter("@exibirCorVerde", DBNull.Value);
                SqlParameter pAutorizarExportacao = new SqlParameter("@autorizarExportacao", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "Apaga");

                retorno = db.Database.ExecuteSqlCommand("SP_Usuario  @idUsuario, @id, @idArea, @idPerfil, @idSituacao, @nome, @sobrenome, @login, @email, @vendedor, @exibirValorVenda, @exibirCorVerde, @autorizarExportacao, @operacao", pUsu, pId, pIdArea, pIdPerfil, pIdSituacao, pNome, pSobrenome, pLogin, pEmail, pVendedor, pexibirValorVenda, pExibirCorVerde, pAutorizarExportacao, pOper);

                if (retorno == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<Usuario> UsuarioAcesso(int id_pessoa = 0, int id_departamento = 0, string situacao = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Usuario> lst = new List<Usuario>();
                try
                {
                    SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", id_pessoa);
                    SqlParameter pIdDepartamento = new SqlParameter("@ID_DEPARTAMENTO", id_departamento);
                    SqlParameter pSituacao = new SqlParameter("@SITUACAO", situacao);

                    var linha = db.Database.SqlQuery<Usuario>("EXEC STO_S_PESSOA @ID_PESSOA, @ID_DEPARTAMENTO, @SITUACAO", pIdPessoa, pIdDepartamento, pSituacao).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return lst;
                    }
                }
                catch (Exception e)
                {
                    return lst;
                }
            }
        }
        public List<Perfil> Perfil()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Perfil> lst = new List<Perfil>();
                try
                {
                    SqlParameter pSituacao = new SqlParameter("@SITUACAO", "A");

                    var linha = db.Database.SqlQuery<Perfil>("EXEC STO_S_PERFIL @SITUACAO", pSituacao).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return lst;
                    }
                }
                catch (Exception e)
                {
                    return lst;
                }
            }
        }
        public List<Departamento> Departamento()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Departamento> lst = new List<Departamento>();
                try
                {
                    SqlParameter pSituacao = new SqlParameter("@SITUACAO", "A");

                    var linha = db.Database.SqlQuery<Departamento>("EXEC STO_S_DEPARTAMENTO @SITUACAO", pSituacao).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return lst;
                    }
                }
                catch (Exception e)
                {
                    return lst;
                }
            }
        }
        public List<Usuario> SalvarUsuario(int id_pessoa = 0, int id_perfil = 0, int id_departamento = 0, string login = "", string nome = "", string email = "",
            string telefone = "",  string administrador = "", string situacao = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Usuario> lst = new List<Usuario>();
                try
                {
                    SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", id_pessoa);
                    SqlParameter pIdPerfil = new SqlParameter("@ID_PERFIL", id_perfil);
                    SqlParameter pIdDepartamento = new SqlParameter("@ID_DEPARTAMENTO", id_departamento);
                    SqlParameter pLogin = new SqlParameter("@LOGIN", login);
                    SqlParameter pNome = new SqlParameter("@NOME", nome);
                    SqlParameter pEmail = new SqlParameter("@EMAIL", email);
                    SqlParameter pTelefone = new SqlParameter("@TELEFONE", "");
                    SqlParameter pAdministrador = new SqlParameter("@ADMINISTRADOR", administrador);
                    SqlParameter pSituacao = new SqlParameter("@SITUACAO", situacao);

                    if(id_pessoa > 0)
                    {
                        var linha = db.Database.SqlQuery<Usuario>("EXEC STO_U_PESSOA @ID_PESSOA, @ID_DEPARTAMENTO, @ID_PERFIL, @LOGIN, @NOME, @EMAIL, @TELEFONE, @ADMINISTRADOR, @SITUACAO",
                        pIdPessoa, pIdDepartamento, pIdPerfil, pLogin, pNome, pEmail, pTelefone, pAdministrador, pSituacao).ToList();

                        if (linha.Count > 0)
                        {
                            return linha;
                        }
                        else
                        {
                            return lst;
                        }
                    }
                    else
                    {
                        var linha = db.Database.SqlQuery<Usuario>("EXEC STO_I_PESSOA @ID_PESSOA, @ID_DEPARTAMENTO, @ID_PERFIL, @LOGIN, @NOME, @EMAIL, @TELEFONE, @ADMINISTRADOR, @SITUACAO",
                        pIdPessoa, pIdDepartamento, pIdPerfil, pLogin, pNome, pEmail, pTelefone, pAdministrador, pSituacao).ToList();

                        if (linha.Count > 0)
                        {
                            return linha;
                        }
                        else
                        {
                            return lst;
                        }
                    }
                }
                catch (Exception e)
                {
                    return lst;
                }
            }
        }
        public List<Perfil> MenuInicializacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Perfil> lst = new List<Perfil>();
                try
                {
                    var linha = db.Database.SqlQuery<Perfil>("EXEC sto_s_menu_inicializacao").ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return lst;
                    }
                }
                catch (Exception e)
                {
                    return lst;
                }
            }
        }
    }
}
