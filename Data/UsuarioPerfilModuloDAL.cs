using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UsuarioPerfilModuloDAL
    {
        private DatabaseContext db = new DatabaseContext();
        private int retorno;

        public List<UsuarioPerfilModulo> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<UsuarioPerfilModulo>("EXEC SP_UsuarioPerfilModulo NULL, NULL, NULL, 'Lista'").ToList();
            }
        }

        public List<UsuarioPerfilModulo> BuscaPorId(int idPerfil)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", idPerfil);
                SqlParameter pIdModulo = new SqlParameter("@idModulo", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");
                var linhas = db.Database.SqlQuery<UsuarioPerfilModulo>("SP_UsuarioPerfilModulo  @idUsuario, @idPerfil, @idModulo, @operacao", pUsu, pIdPerfil, pIdModulo, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return new List<UsuarioPerfilModulo>();
                }
            }
        }

        public List<UsuarioPerfilModulo> BuscaPorIdPerfilPessoa(int idPerfil)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdPerfil = new SqlParameter("@id_Perfil", idPerfil);

                var linhas = db.Database.SqlQuery<UsuarioPerfilModulo>("STO_S_PERFIL_FUNCIONALIDADE_PERFIL  @id_Perfil", pIdPerfil).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return new List<UsuarioPerfilModulo>();
                }
            }
        }

        public UsuarioPerfilModulo BuscaPorDuplicidade(UsuarioPerfilModulo obj)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", obj.IdPerfil);
                SqlParameter pIdModulo = new SqlParameter("@idModulo", obj.IdModulo);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorDuplicidade");
                var linha = db.Database.SqlQuery<UsuarioPerfilModulo>("SP_UsuarioPerfilModulo  @idUsuario, @idPerfil, @idModulo, @operacao", pUsu, pIdPerfil, pIdModulo, pOper).ToList();
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

        public bool Insere(UsuarioPerfilModulo obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor); 
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", obj.IdPerfil);
                SqlParameter pIdModulo = new SqlParameter("@idModulo", obj.IdModulo);
                SqlParameter pOper = new SqlParameter("@operacao", "Insere");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioPerfilModulo  @idUsuario, @idPerfil, @idModulo, @operacao", pUsu, pIdPerfil, pIdModulo, pOper);

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

        public bool Atualiza(UsuarioPerfilModulo obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor); 
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", obj.IdPerfil);
                SqlParameter pIdModulo = new SqlParameter("@idModulo", obj.IdModulo);
                SqlParameter pOper = new SqlParameter("@operacao", "Atualiza");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioPerfilModulo  @idUsuario, @idPerfil, @idModulo, @operacao", pUsu, pIdPerfil, pIdModulo, pOper);

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

        public bool Apaga(UsuarioPerfilModulo obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor); 
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", obj.IdPerfil);
                SqlParameter pIdModulo = new SqlParameter("@idModulo", obj.IdModulo);
                SqlParameter pOper = new SqlParameter("@operacao", "Apaga");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioPerfilModulo  @idUsuario, @idPerfil, @idModulo, @operacao", pUsu, pIdPerfil, pIdModulo, pOper);

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

        public bool ApagaModulosDoPerfil(int idPerfil, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor);
                SqlParameter pIdPerfil = new SqlParameter("@idPerfil", idPerfil);
                SqlParameter pIdModulo = new SqlParameter("@idModulo", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "ApagaModulosDoPerfil");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioPerfilModulo  @idUsuario, @idPerfil, @idModulo, @operacao", pUsu, pIdPerfil, pIdModulo, pOper);

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
