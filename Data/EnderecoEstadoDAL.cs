using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EnderecoEstadoDAL
    {
        private DatabaseContext db = new DatabaseContext();

        public List<EnderecoEstado> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linhas = db.Database.SqlQuery<EnderecoEstado>("EXEC SP_EnderecoEstado NULL, NULL, NULL, NULL, NULL, 'Lista'").ToList();
                return linhas;
            }
        }

        public EnderecoEstado BuscaPorId(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pIdCidade = new SqlParameter("@idCidade", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorId");

                var linha = db.Database.SqlQuery<EnderecoEstado>("SP_EnderecoEstado  @idUsuario, @id, @idCidade, @idRegiao, @nome, @operacao", pUsu, pId, pIdCidade, pIdRegiao, pNome, pOper).ToList();
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

        public EnderecoEstado BuscaPorIdEnt(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", id);
                SqlParameter pIdCidade = new SqlParameter("@idCidade", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorIdEnt");

                var linha = db.Database.SqlQuery<EnderecoEstado>("SP_EnderecoEstado  @idUsuario, @id, @idCidade, @idRegiao, @nome, @operacao", pUsu, pId, pIdCidade, pIdRegiao, pNome, pOper).ToList();
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

        public EnderecoEstado BuscaPorCidade(int idCidade)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdCidade = new SqlParameter("@idCidade", idCidade);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", DBNull.Value);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorCidade");

                var linha = db.Database.SqlQuery<EnderecoEstado>("SP_EnderecoEstado  @idUsuario, @id, @idCidade, @idRegiao, @nome, @operacao", pUsu, pId, pIdCidade, pIdRegiao, pNome, pOper).ToList();
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

        public List<EnderecoEstado> BuscaPorRegiao(int idRegiao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pUsu = new SqlParameter("@idUsuario", 1); //Administrador
                SqlParameter pId = new SqlParameter("@id", DBNull.Value);
                SqlParameter pIdCidade = new SqlParameter("@idCidade", DBNull.Value);
                SqlParameter pIdRegiao = new SqlParameter("@idRegiao", idRegiao);
                SqlParameter pNome = new SqlParameter("@nome", DBNull.Value);
                SqlParameter pOper = new SqlParameter("@operacao", "BuscaPorRegiao");

                var linhas = db.Database.SqlQuery<EnderecoEstado>("SP_EnderecoEstado  @idUsuario, @id, @idCidade, @idRegiao, @nome, @operacao", pUsu, pId, pIdCidade, pIdRegiao, pNome, pOper).ToList();
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
