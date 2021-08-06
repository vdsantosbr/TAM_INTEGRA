using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.Data;

namespace Data
{
    public class ContaContabilDAL
    {
        public List<ContaContabil> Exibir()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<ContaContabil>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_Atual").ToList();

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
        public List<Conta> Contas()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<Conta>("EXEC STO_SALL_TR_CI_OUT_VC_CONTACONTABIL").ToList();

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
        public List<Conta> SalvarConta(int id_conta = 0, string cod_conta = "", int id_pessoa = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConta = new SqlParameter("@ID_CONTACONTABIL", id_conta);
                SqlParameter pCodConta = new SqlParameter("@CODCONTA", cod_conta);
                SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", id_pessoa);

                var linha = db.Database.SqlQuery<Conta>("EXEC STO_U_TR_CI_OUT_VC_CONTACONTABIL @ID_CONTACONTABIL, @CODCONTA, @ID_PESSOA", pIdConta, pCodConta, pIdPessoa).ToList();

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
    }
}
