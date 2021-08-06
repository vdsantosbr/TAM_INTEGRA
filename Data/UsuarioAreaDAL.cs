using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UsuarioAreaDAL
    {
        private DatabaseContext db = new DatabaseContext();
        private int retorno;

        public List<UsuarioArea> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<UsuarioArea>("EXEC SP_UsuarioArea NULL, NULL, NULL, NULL, NULL, 'Lista'").ToList();
            }
        }

        public UsuarioArea BuscaPorId(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pCor = new SqlParameter("@cor", DBNull.Value);
                SqlParameter pAdministrativa = new SqlParameter("@administrativa", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");
                var linha = db.Database.SqlQuery<UsuarioArea>("SP_UsuarioArea  @idUsuario, @id, @nome, @cor, @administrativa, @operacao", pUsu, pId, pNome, pCor, pAdministrativa, pOper).ToList();
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

        public UsuarioArea BuscaPorDuplicidade(UsuarioArea obj)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", obj.Id);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pCor = new SqlParameter("@cor", DBNull.Value);
                SqlParameter pAdministrativa = new SqlParameter("@administrativa", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorDuplicidade");
                var linha = db.Database.SqlQuery<UsuarioArea>("SP_UsuarioArea  @idUsuario, @id, @nome, @cor, @administrativa, @operacao", pUsu, pId, pNome, pCor, pAdministrativa, pOper).ToList();
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


        public List<UsuarioArea> BuscaAreaVenda(string idArea)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId = new SqlParameter("@id", idArea);
                var linha = db.Database.SqlQuery<UsuarioArea>("SP_UsuarioArea_Lista  @id", pId).ToList();
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

        public bool Insere(UsuarioArea obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor);
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pCor = new SqlParameter("@cor", (obj.Cor == null) ? (object)DBNull.Value : obj.Cor);
                SqlParameter pAdministrativa = new SqlParameter("@administrativa", obj.Administrativa);
                SqlParameter pOper = new SqlParameter("@operacao", "Insere");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioArea  @idUsuario, @id, @nome, @cor, @administrativa, @operacao", pUsu, pId, pNome, pCor, pAdministrativa, pOper);

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

        public bool Atualiza(UsuarioArea obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", idUsuarioAutor);
                SqlParameter pId = new SqlParameter("@id", obj.Id);
                SqlParameter pNome = new SqlParameter("@nome", obj.Nome);
                SqlParameter pCor = new SqlParameter("@cor", (obj.Cor == null) ? (object)DBNull.Value : obj.Cor);
                SqlParameter pAdministrativa = new SqlParameter("@administrativa", obj.Administrativa);
                SqlParameter pOper = new SqlParameter("@operacao", "Atualiza");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioArea  @idUsuario, @id, @nome, @cor, @administrativa, @operacao", pUsu, pId, pNome, pCor, pAdministrativa, pOper);

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
                SqlParameter pCor = new SqlParameter("@cor", DBNull.Value);
                SqlParameter pAdministrativa = new SqlParameter("@administrativa", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "Apaga");

                retorno = db.Database.ExecuteSqlCommand("SP_UsuarioArea  @idUsuario, @id, @nome, @cor, @administrativa, @operacao", pUsu, pId, pNome, pCor, pAdministrativa, pOper);

                return true;
            }
        }
    }
}
