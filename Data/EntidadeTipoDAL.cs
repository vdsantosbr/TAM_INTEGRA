using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EntidadeTipoDAL
    {
        private DatabaseContext db = new DatabaseContext();
        private int retorno;

        public List<EntidadeTipo> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<EntidadeTipo>("EXEC SP_EntidadeTipo NULL, NULL, NULL, 'Lista'").ToList();
            }
        }

        public EntidadeTipo BuscaPorId(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");
                var linha = db.Database.SqlQuery<EntidadeTipo>("SP_EntidadeTipo  @idUsuario, @id, @nome, @operacao", pUsu, pId, pNome, pOper).ToList();
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

        public List<EntidadeTipo> BuscaPorIdLista(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");
                var linha = db.Database.SqlQuery<EntidadeTipo>("SP_EntidadeTipo  @idUsuario, @id, @nome, @operacao", pUsu, pId, pNome, pOper).ToList();
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

        public EntidadeTipo BuscaPorDuplicidade(EntidadeTipo obj)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", obj.Id);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorDuplicidade");
                var linha = db.Database.SqlQuery<EntidadeTipo>("SP_EntidadeTipo  @idUsuario, @id, @nome, @operacao", pUsu, pId, pNome, pOper).ToList();
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

        public bool Insere(EntidadeTipo obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor);
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pOper = new SqlParameter("@operacao", "Insere");

                retorno = db.Database.ExecuteSqlCommand("SP_EntidadeTipo  @idUsuario, @id, @nome, @operacao", pUsu, pId, pNome, pOper);

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

        public bool Atualiza(EntidadeTipo obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor);
                SqlParameter pId = new SqlParameter("@id", obj.Id);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pOper = new SqlParameter("@operacao", "Atualiza");

                retorno = db.Database.ExecuteSqlCommand("SP_EntidadeTipo  @idUsuario, @id, @nome, @operacao", pUsu, pId, pNome, pOper);

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
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "Apaga");

                retorno = db.Database.ExecuteSqlCommand("SP_EntidadeTipo  @idUsuario, @id, @nome, @operacao", pUsu, pId, pNome, pOper);

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
