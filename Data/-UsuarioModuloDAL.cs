using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UsuarioModuloDAL
    {
        private DatabaseContext db = new DatabaseContext();
        private int retorno;

        public List<UsuarioModulo> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<UsuarioModulo>("EXEC SP_UsuarioModulo NULL, NULL, NULL, NULL, NULL, 'Lista'").ToList();
            }
        }

        public UsuarioModulo BuscaPorId(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pDescricao = new SqlParameter("@descricao", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");
                var linha = db.Database.SqlQuery<UsuarioModulo>("SP_UsuarioModulo  @idUsuario, @id, @idSituacao, @nome, @descricao, @operacao", pUsu, pId, pIdSituacao, pNome, pDescricao, pOper).ToList();
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

        public List<UsuarioModulo> BuscaPorSituacao(int idSituacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", idSituacao);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pDescricao = new SqlParameter("@descricao", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorSituacao");
                var linhas = db.Database.SqlQuery<UsuarioModulo>("SP_UsuarioModulo  @idUsuario, @id, @idSituacao, @nome, @descricao, @operacao", pUsu, pId, pIdSituacao, pNome, pDescricao, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return new List<UsuarioModulo>();
                }
            }
        }

        public UsuarioModulo BuscaPorDuplicidade(UsuarioModulo obj)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", obj.Id);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", obj.IdSituacao);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pDescricao = new SqlParameter("@descricao", string.IsNullOrEmpty(obj.Descricao) ? (object)DBNull.Value : obj.Descricao);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorDuplicidade");
                var linha = db.Database.SqlQuery<UsuarioModulo>("SP_UsuarioModulo  @idUsuario, @id, @idSituacao, @nome, @descricao, @operacao", pUsu, pId, pIdSituacao, pNome, pDescricao, pOper).ToList();
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

        public bool Insere(UsuarioModulo obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor); 
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", obj.IdSituacao);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pDescricao = new SqlParameter("@descricao", string.IsNullOrEmpty(obj.Descricao) ? (object)DBNull.Value : obj.Descricao);
                SqlParameter pOper = new SqlParameter("@operacao", "Insere");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioModulo  @idUsuario, @id, @idSituacao, @nome, @descricao, @operacao", pUsu, pId, pIdSituacao, pNome, pDescricao, pOper);

                if(retorno == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
        }

        public bool Atualiza(UsuarioModulo obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor); 
                SqlParameter pId = new SqlParameter("@id", obj.Id);
                SqlParameter pIdSituacao = new SqlParameter("@idSituacao", obj.IdSituacao);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pDescricao = new SqlParameter("@descricao", string.IsNullOrEmpty(obj.Descricao) ? (object)DBNull.Value : obj.Descricao);
                SqlParameter pOper = new SqlParameter("@operacao", "Atualiza");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioModulo  @idUsuario, @id, @idSituacao, @nome, @descricao, @operacao", pUsu, pId, pIdSituacao, pNome, pDescricao, pOper);

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
                SqlParameter pDescricao = new SqlParameter("@descricao", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "Apaga");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioModulo  @idUsuario, @id, @idSituacao, @nome, @descricao, @operacao", pUsu, pId, pIdSituacao, pNome, pDescricao, pOper);

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
