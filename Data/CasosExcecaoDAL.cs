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
    public class CasosExcecaoDAL
    {
        public List<CasosExcecao> Exibir(int ano = 0, int mes = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ANO", ano);
                SqlParameter pMes = new SqlParameter("@MES", mes);

                var linha = db.Database.SqlQuery<CasosExcecao>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_ATUAL @ANO, @MES", pAno, pMes).ToList();

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
        public List<CasosExcecao> GridExcecoes(int ano = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<CasosExcecao>("EXEC STO_sall_tr_ci_out_vc_excecao").ToList();

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
        public List<CasosExcecao> AddExcecao(int id_excecao = 0, int id_pessoa = 0, string referencia = "", string observacao = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdExcecao = new SqlParameter();

                if (id_excecao > 0)
                {
                    pIdExcecao = new SqlParameter("@ID_EXCECAO", id_excecao);
                }
                else
                {
                    pIdExcecao.ParameterName = "@ID_EXCECAO";
                    pIdExcecao.SqlDbType = SqlDbType.Int;
                    pIdExcecao.Direction = ParameterDirection.Input;
                    pIdExcecao.Value = 0;
                }

                SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", id_pessoa);
                SqlParameter pReferencia = new SqlParameter("@REFERENCIA", referencia);
                SqlParameter pObservacao = new SqlParameter("@OBSERVACAO", observacao);

                var linha = db.Database.SqlQuery<CasosExcecao>("EXEC STO_I_TR_CI_OUT_VC_EXCECAO @ID_EXCECAO, @ID_PESSOA, @REFERENCIA, @OBSERVACAO",
                    pIdExcecao, pIdPessoa, pReferencia, pObservacao).ToList();

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
        public List<CasosExcecao> RemoverExcecao(int id_excecao = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdExcecao = new SqlParameter("@ID_EXCECAO", id_excecao);

                var linha = db.Database.SqlQuery<CasosExcecao>("EXEC STO_U_TR_CI_OUT_VC_EXCECAO @ID_EXCECAO", pIdExcecao).ToList();

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
