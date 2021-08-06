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
    public class StatementContaDAL
    {
        public List<StatementContas> Lista(string conta = null, string descricao = null, string situacao = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pConta = new SqlParameter("@conta", (conta == null) ? (object)DBNull.Value : conta);
                SqlParameter pDescricao = new SqlParameter("@descricao", (descricao == null) ? (object)DBNull.Value : descricao);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<StatementContas>("EXEC STO_S_FIN_CONTA @conta, @descricao, @situacao", pConta, pDescricao, pSituacao).ToList();
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

        public StatementContas updateConta(int idConta = 0, string conta = null, string descricao = null, string situacao = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConta = new SqlParameter("@idConta", idConta);
                SqlParameter pConta = new SqlParameter("@conta", (conta == null) ? (object)DBNull.Value : conta);
                SqlParameter pDescricao = new SqlParameter("@descricao", (descricao == null) ? (object)DBNull.Value : descricao);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);

                var linha = db.Database.SqlQuery<StatementContas>("EXEC STO_U_FIN_CONTA @idConta, @conta, @descricao, @situacao", pIdConta, pConta, pDescricao, pSituacao).ToList();
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

        public StatementContas excluirConta(int idConta = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConta = new SqlParameter("@idConta", idConta);

                var linha = db.Database.SqlQuery<StatementContas>("EXEC STO_D_FIN_CONTA @idConta", pIdConta).ToList();
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

        public StatementContas inserirConta(string conta = null, string descricao = null, string situacao = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pConta = new SqlParameter("@conta", (conta == null) ? (object)DBNull.Value : conta);
                SqlParameter pDescricao = new SqlParameter("@descricao", (descricao == null) ? (object)DBNull.Value : descricao);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? (object)DBNull.Value : situacao);


                var pIdConta = new SqlParameter
                {
                    ParameterName = "id_Conta",
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output
                };

                try
                {
                    var linha = db.Database.SqlQuery<StatementContas>("EXEC STO_I_FIN_CONTA @id_conta,@conta, @descricao, @situacao", pIdConta, pConta, pDescricao, pSituacao).ToList();
                    if (linha.Count > 0)
                    {
                        return linha.First();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    StatementContas res = new StatementContas();
                    res.Observacao = e.Message;
                    return res;
                }
            }
        }
    }
}
