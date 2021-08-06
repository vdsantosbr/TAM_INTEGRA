using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EnderecoCidadeDAL
    {
        private DatabaseContext db = new DatabaseContext();

        public List<EnderecoCidade> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<EnderecoCidade>("EXEC SP_EnderecoCidade NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Lista'").ToList();
            }
        }

        public EnderecoCidade BuscaPorId(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pIdBairro = new SqlParameter("@idBairro", DBNull.Value);
                SqlParameter pIdEstado = new SqlParameter("@idEstado", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pDDD = new SqlParameter("@DDD", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");

                var linha = db.Database.SqlQuery<EnderecoCidade>("SP_EnderecoCidade  @idUsuario, @id, @idBairro, @idEstado, @idRegiao, @DDD, @nome, @operacao", pUsu, pId, pIdBairro, pIdEstado, pIdRegiao, pDDD, pNome, pOper).ToList();
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

        public List<EnderecoCidade> BuscaPorNome(string nome)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdBairro = new SqlParameter("@idBairro", DBNull.Value);
                SqlParameter pIdEstado = new SqlParameter("@idEstado", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pDDD = new SqlParameter("@DDD", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", nome);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorNome");

                var linhas = db.Database.SqlQuery<EnderecoCidade>("SP_EnderecoCidade  @idUsuario, @id, @idBairro, @idEstado, @idRegiao, @DDD, @nome, @operacao", pUsu, pId, pIdBairro, pIdEstado, pIdRegiao, pDDD, pNome, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return null;
                }
            }
        }

        public EnderecoCidade BuscaPorBairro(int idBairro)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdBairro = new SqlParameter("@idBairro", idBairro);
                SqlParameter pIdEstado = new SqlParameter("@idEstado", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pDDD = new SqlParameter("@DDD", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorBairro");

                var linha = db.Database.SqlQuery<EnderecoCidade>("SP_EnderecoCidade  @idUsuario, @id, @idBairro, @idEstado, @idRegiao, @DDD, @nome, @operacao", pUsu, pId, pIdBairro, pIdEstado, pIdRegiao, pDDD, pNome, pOper).ToList();
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

        public List<EnderecoCidade> BuscaPorEstado(int idEstado)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdBairro = new SqlParameter("@idBairro", DBNull.Value);
                SqlParameter pIdEstado = new SqlParameter("@idEstado", idEstado);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pDDD = new SqlParameter("@DDD", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorEstado");

                var linhas = db.Database.SqlQuery<EnderecoCidade>("SP_EnderecoCidade  @idUsuario, @id, @idBairro, @idEstado, @idRegiao, @DDD, @nome, @operacao", pUsu, pId, pIdBairro, pIdEstado, pIdRegiao, pDDD, pNome, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<EnderecoCidade> BuscaPorEstadoUF(string uf)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdBairro = new SqlParameter("@idBairro", DBNull.Value);
                SqlParameter pUF = new SqlParameter("@idEstado", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pDDD = new SqlParameter("@DDD", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", uf);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorEstadoUF");

                var linhas = db.Database.SqlQuery<EnderecoCidade>("SP_EnderecoCidade  @idUsuario, @id, @idBairro, @idEstado, @idRegiao, @DDD, @nome, @operacao", pUsu, pId, pIdBairro, pUF, pIdRegiao, pDDD, pNome, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<EnderecoCidade> BuscaPorEntidade(int idEntidade)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pidEntidade = new SqlParameter("@idEntidade", idEntidade);
                SqlParameter pIdBairro = new SqlParameter("@idBairro", DBNull.Value);
                SqlParameter pIdEstado = new SqlParameter("@idEstado", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pDDD = new SqlParameter("@DDD", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorEntidade");

                var linhas = db.Database.SqlQuery<EnderecoCidade>("SP_EnderecoCidade  @idUsuario, @id, @idEntidade, @idBairro, @idEstado, @idRegiao, @DDD, @nome, @operacao", pUsu, pId, pidEntidade, pIdBairro, pIdEstado, pIdRegiao, pDDD, pNome, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<EnderecoCidade> BuscaPorRegiao(int idRegiao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdBairro = new SqlParameter("@idBairro", DBNull.Value);
                SqlParameter pIdEstado = new SqlParameter("@idEstado", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", idRegiao);
                SqlParameter pDDD = new SqlParameter("@DDD", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorRegiao");

                var linhas = db.Database.SqlQuery<EnderecoCidade>("SP_EnderecoCidade  @idUsuario, @id, @idBairro, @idEstado, @idRegiao, @DDD, @nome, @operacao", pUsu, pId, pIdBairro, pIdEstado, pIdRegiao, pDDD, pNome, pOper).ToList();
                if (linhas.Count > 0)
                {
                    return linhas;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
