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
    public class StatementClassificacaoDAL
    {
        public List<StatementClassificacao> Lista(string classificacao = null, string descricao = null, string situacao = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                //SqlParameter pClassificacao = new SqlParameter("@classificacao", (classificacao == null) ? (object)DBNull.Value : classificacao);
                SqlParameter pDescricao = new SqlParameter("@descricao", (descricao == null) ? (object)DBNull.Value : descricao);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<StatementClassificacao>("EXEC STO_S_FIN_CLASSIFICACAO @descricao, @situacao", pDescricao, pSituacao).ToList();
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

        public StatementClassificacao updateclassificacao(int idclassificacao = 0, string classificacao = null, string descricao = null, string situacao = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdclassificacao = new SqlParameter("@idclassificacao", idclassificacao);
                SqlParameter pDescricao = new SqlParameter("@descricao", (descricao == null) ? (object)DBNull.Value : descricao);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<StatementClassificacao>("EXEC STO_U_FIN_CLASSIFICACAO @idclassificacao, @descricao, @situacao", pIdclassificacao, pDescricao, pSituacao).ToList();
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

        public StatementClassificacao excluirclassificacao(int idclassificacao = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdclassificacao = new SqlParameter("@idclassificacao", idclassificacao);

                var linha = db.Database.SqlQuery<StatementClassificacao>("EXEC STO_D_FIN_CLASSIFICACAO @idclassificacao", pIdclassificacao).ToList();
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

        public StatementClassificacao inserirclassificacao(string descricao = null, string situacao = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDescricao = new SqlParameter("@descricao", (descricao == null) ? (object)DBNull.Value : descricao);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);

                var pIdClassificacao = new SqlParameter
                {
                    ParameterName = "id_Classificacao",
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };

                var linha = db.Database.SqlQuery<StatementClassificacao>("EXEC STO_I_FIN_CLASSIFICACAO @id_Classificacao, @descricao, @situacao", pIdClassificacao, pDescricao, pSituacao).ToList();
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
    }
}
