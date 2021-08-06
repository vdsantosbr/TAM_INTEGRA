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
    public class FechamentoContabilDAL
    {
        public List<FechamentoContabil> Filtro(int ano = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ANO", (ano == 0) ? (object)DBNull.Value : ano);

                try
                {
                    var linha = db.Database.SqlQuery<FechamentoContabil>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_Filtro @ANO", pAno).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }catch(Exception e)
                {
                    return null;
                }
            }
        }
        public List<FechamentoContabil> Fechamento_Novo()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<FechamentoContabil>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_Novo").ToList();

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
        public List<FechamentoContabil> Fechamento(int id_fechamento = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdFechamento = new SqlParameter("@ID_FECHAMENTO", id_fechamento);
                var linha = db.Database.SqlQuery<FechamentoContabil>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO @ID_FECHAMENTO", pIdFechamento).ToList();

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
        public List<FechamentoContabil> Salvar(int id_fechamento = 0, int id_integracao = 0, string fechamento = "", DateTime dt_contabil = default(DateTime), string situacao = "", int id_pessoa = 0, string observacao = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdFechamento = new SqlParameter();

                if (id_fechamento > 0)
                {
                   pIdFechamento = new SqlParameter("@ID_FECHAMENTO", id_fechamento);
                }
                else
                {
                    pIdFechamento.ParameterName = "@ID_FECHAMENTO";
                    pIdFechamento.SqlDbType = SqlDbType.Int;
                    pIdFechamento.Direction = ParameterDirection.Input;
                    pIdFechamento.Value = 0;
                }

                SqlParameter pIdIntegracao = new SqlParameter();

                if (id_integracao > 0)
                {
                    pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                }
                else
                {
                    pIdIntegracao.ParameterName = "@ID_INTEGRACAO";
                    pIdIntegracao.SqlDbType = SqlDbType.Int;
                    pIdIntegracao.Direction = ParameterDirection.Input;
                    pIdIntegracao.Value = 0;
                }

                SqlParameter pFechamento = new SqlParameter("@FECHAMENTO", fechamento);
                SqlParameter pDtContabil = new SqlParameter("@DATA_CONTABILIZACAO", dt_contabil);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", situacao);
                SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", id_pessoa);
                SqlParameter pObservacao = new SqlParameter("@OBSERVACAO", observacao);


                var linha = db.Database.SqlQuery<FechamentoContabil>("EXEC STO_I_TR_CI_OUT_VC_FECHAMENTO @ID_FECHAMENTO, @ID_FECHAMENTO, @FECHAMENTO, @DATA_CONTABILIZACAO, @SITUACAO, @ID_PESSOA, @OBSERVACAO", 
                    pIdFechamento, pIdIntegracao, pFechamento, pDtContabil, pSituacao, pIdPessoa, pObservacao).ToList();

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
        public List<IntegracaoCambioSys> GridCambioSys(int mes = 0, int ano = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdFechamento = new SqlParameter();

                SqlParameter pAno = new SqlParameter("@ANO", ano);
                SqlParameter pMes = new SqlParameter("@MES", mes);

                var linha = db.Database.SqlQuery<IntegracaoCambioSys>("EXEC STO_S_TR_CI_OUT_VC_CAMBIOSYS @ANO, @MES",
                     pAno, pMes).ToList();

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
        public List<FechamentoContabil> FechamentoAno()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<FechamentoContabil>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_ANO").ToList();

                if (linha.Count() > 0)
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
