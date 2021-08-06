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
    public class ContabilizacaoDAL
    {
        public List<Contabilizacao> Exibir(int ano =0, int mes = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ANO", ano);
                SqlParameter pMes = new SqlParameter("@MES", mes);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_ATUAL @ANO, @MES", pAno, pMes).ToList();

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
        public List<Contabilizacao> GridContabilizacao(int id_integracao = 0, string situacao = "", string qualificacao = "", string tipo_vc = "", string tipo_fatura = "",
    string conta_debito = "", string conta_credito = "", string cod_processo = "", string referencia = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                cod_processo = cod_processo.Trim();

                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", situacao);
                SqlParameter pQualificacao = new SqlParameter("@QUALIFICACAO", qualificacao);
                SqlParameter pTipoFatura = new SqlParameter("@TIPO_FATURA", tipo_fatura);
                SqlParameter pTipoVc = new SqlParameter("@TIPO_VC", tipo_vc);
                SqlParameter pContaDebito = new SqlParameter("@CONTA_DEBITO", conta_debito);
                SqlParameter pContaCredito = new SqlParameter("@CONTA_CREDITO", conta_credito);
                SqlParameter PCodProcesso = new SqlParameter("@COD_PROCESSO", cod_processo);
                SqlParameter pReferencia = new SqlParameter("@REFERENCIA", referencia);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_S_TR_CI_OUT_VC_COMPOSICAO_Resumo  @ID_INTEGRACAO, @SITUACAO, @QUALIFICACAO, @TIPO_VC, @TIPO_FATURA, @CONTA_DEBITO, @CONTA_CREDITO, @COD_PROCESSO, @REFERENCIA",
                    pIdIntegracao, pSituacao, pQualificacao, pTipoVc, pTipoFatura, pContaDebito, pContaCredito, PCodProcesso, pReferencia).ToList();

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
        public List<Contabilizacao> GridRegistroInconsistente(int id_integracao = 0, int id_fechamento = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                SqlParameter pIdFechamento = new SqlParameter("@ID_FECHAMENTO", id_fechamento);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_S_TR_CI_OUT_VC_RegistroInconsistente  @ID_FECHAMENTO, @ID_INTEGRACAO",
                    pIdFechamento, pIdIntegracao).ToList();

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
        public List<Contabilizacao> GridRegistrosErro(int id_integracao = 0, int id_fechamento = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                SqlParameter pIdFechamento = new SqlParameter("@ID_FECHAMENTO", id_fechamento);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_S_TR_CI_OUT_VC_RegistroErro @ID_FECHAMENTO, @ID_INTEGRACAO",
                    pIdFechamento, pIdIntegracao).ToList();

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
        public List<Contabilizacao> FormatarContabilizacao(int id_fechamento = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdFechamento = new SqlParameter("@ID_FECHAMENTO", id_fechamento);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_EXE_INICIAR_JOB_VC_Formatar @ID_FECHAMENTO",
                    pIdFechamento).ToList();

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
        public List<Contabilizacao> IntegrarContabilizacao(int id_fechamento = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdFechamento = new SqlParameter("@ID_FECHAMENTO", id_fechamento);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_EXE_INICIAR_JOB_VC_Integrar @ID_FECHAMENTO", pIdFechamento).ToList();

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
        public List<Contabilizacao> ExcluirContabilizacao(int id_fechamento = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdFechamento = new SqlParameter("@ID_FECHAMENTO", id_fechamento);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_EXE_INICIAR_JOB_VC_Excluir @ID_FECHAMENTO", pIdFechamento).ToList();

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
        public List<Contabilizacao> ExportarContabilizacao(int ano = 0, int mes = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ANO", ano);
                SqlParameter pMes = new SqlParameter("@MES", mes);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_S_TR_CI_OUT_VC_Relatorio  @ANO, @MES", pAno, pMes).ToList();

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
        public List<Contabilizacao> FechamentoAno()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_ANO").ToList();

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
        public List<Contabilizacao> FechamentoMes(int ano = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ANO", ano);

                var linha = db.Database.SqlQuery<Contabilizacao>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_MES  @ANO", pAno).ToList();

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
