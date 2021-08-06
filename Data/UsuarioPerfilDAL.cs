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
    public class UsuarioPerfilDAL
    {
        private DatabaseContext db = new DatabaseContext();
        private int retorno;

        public List<UsuarioPerfil> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<UsuarioPerfil>("EXEC SP_UsuarioPerfil NULL, NULL, NULL, NULL, NULL, 'Lista', NULL").ToList();
            }
        }

        public UsuarioPerfil BuscaPorId(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pVisaoGlobal = new SqlParameter("@visaoGlobal", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");

                SqlParameter pIdentity = new SqlParameter();
                pIdentity.ParameterName = "@identity";
                pIdentity.Direction = ParameterDirection.Output;
                pIdentity.SqlDbType = SqlDbType.Int;

                var linha = db.Database.SqlQuery<UsuarioPerfil>("SP_UsuarioPerfil  @idUsuario, @id, @idSituacao, @nome, @visaoGlobal, @operacao, @identity OUT", pUsu, pId, pIdSituacao, pNome, pVisaoGlobal, pOper, pIdentity).ToList();
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

        public List<UsuarioPerfil> BuscaPorSituacao(int idSituacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", idSituacao);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pVisaoGlobal = new SqlParameter("@visaoGlobal", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorSituacao");

                SqlParameter pIdentity = new SqlParameter();
                pIdentity.ParameterName = "@identity";
                pIdentity.Direction = ParameterDirection.Output;
                pIdentity.SqlDbType = SqlDbType.Int;

                var linhas = db.Database.SqlQuery<UsuarioPerfil>("SP_UsuarioPerfil  @idUsuario, @id, @idSituacao, @nome, @visaoGlobal, @operacao, @identity OUT", pUsu, pId, pIdSituacao, pNome, pVisaoGlobal, pOper, pIdentity).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return new List<UsuarioPerfil>();
                }
            }
        }

        public UsuarioPerfil BuscaPorDuplicidade(UsuarioPerfil obj)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", obj.Id_Perfil);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", obj.IdSituacao);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pVisaoGlobal = new SqlParameter("@visaoGlobal", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorDuplicidade");

                SqlParameter pIdentity = new SqlParameter();
                pIdentity.ParameterName = "@identity";
                pIdentity.Direction = ParameterDirection.Output;
                pIdentity.SqlDbType = SqlDbType.Int;

                var linha = db.Database.SqlQuery<UsuarioPerfil>("SP_UsuarioPerfil  @idUsuario, @id, @idSituacao, @nome, @visaoGlobal, @operacao, @identity OUT", pUsu, pId, pIdSituacao, pNome, pVisaoGlobal, pOper, pIdentity).ToList();
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

        public int Insere(UsuarioPerfil obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor); 
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", obj.IdSituacao);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pVisaoGlobal = new SqlParameter("@visaoGlobal", obj.VisaoGlobal);
                SqlParameter pOper = new SqlParameter("@operacao", "Insere");

                SqlParameter pIdentity = new SqlParameter();
                pIdentity.ParameterName = "@identity";
                pIdentity.Direction = ParameterDirection.Output;
                pIdentity.SqlDbType = SqlDbType.Int;

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioPerfil  @idUsuario, @id, @idSituacao, @nome, @visaoGlobal, @operacao, @identity OUT", pUsu, pId, pIdSituacao, pNome, pVisaoGlobal, pOper, pIdentity);

                return (int)pIdentity.Value;
            }
        }

        public bool Atualiza(UsuarioPerfil obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor); 
                SqlParameter pId = new SqlParameter("@id", obj.Id_Perfil);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", obj.IdSituacao);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pVisaoGlobal = new SqlParameter("@visaoGlobal", obj.VisaoGlobal);
                SqlParameter pOper = new SqlParameter("@operacao", "Atualiza");

                SqlParameter pIdentity = new SqlParameter();
                pIdentity.ParameterName = "@identity";
                pIdentity.Direction = ParameterDirection.Output;
                pIdentity.SqlDbType = SqlDbType.Int;

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioPerfil  @idUsuario, @id, @idSituacao, @nome, @visaoGlobal, @operacao, @identity OUT", pUsu, pId, pIdSituacao, pNome, pVisaoGlobal, pOper, pIdentity);

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
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pVisaoGlobal = new SqlParameter("@visaoGlobal", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "Apaga");

                SqlParameter pIdentity = new SqlParameter();
                pIdentity.ParameterName = "@identity";
                pIdentity.Direction = ParameterDirection.Output;
                pIdentity.SqlDbType = SqlDbType.Int;

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioPerfil  @idUsuario, @id, @idSituacao, @nome, @visaoGlobal, @operacao, @identity OUT", pUsu, pId, pIdSituacao, pNome, pVisaoGlobal, pOper, pIdentity);

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
    }
}
