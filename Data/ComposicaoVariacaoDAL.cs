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
    public class ComposicaoVariacaoDAL
    {
        public List<ComposicaoVariacao> Exibir(int ano = 0, int mes = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ANO", ano);
                SqlParameter pMes = new SqlParameter("@MES", mes);

                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_S_TR_CI_OUT_VC_FECHAMENTO_ATUAL @ANO, @MES", pAno, pMes).ToList();

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
        public List<ComposicaoVariacao> Situacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_SALL_TR_CI_OUT_VC_COMPOSICAO_SITUACAO").ToList();

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
        public List<ComposicaoVariacao> Qualificacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_SALL_TR_CI_OUT_VC_COMPOSICAO_QUALIFICACAO").ToList();

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
        public List<ComposicaoVariacao> Tipo_vc()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_SALL_TR_CI_OUT_VC_COMPOSICAO_TIPO_VC").ToList();

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
        public List<ComposicaoVariacao> Tipo_fatura()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_SALL_TR_CI_OUT_VC_COMPOSICAO_TIPO_FATURA").ToList();

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
        public List<ComposicaoVariacao> Conta_debito(int id_integracao = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);

                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_SALL_TR_CI_OUT_VC_COMPOSICAO_CONTA_DEBITO @ID_INTEGRACAO", pIdIntegracao).ToList();

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

        public List<ComposicaoVariacao> Conta_credito(int id_integracao = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);

                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_SALL_TR_CI_OUT_VC_COMPOSICAO_CONTA_CREDITO @ID_INTEGRACAO", pIdIntegracao).ToList();

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
        public List<ComposicaoVariacao> GridContabilizacao(int id_integracao = 0, string situacao = "", string qualificacao = "", string tipo_vc = "", string tipo_fatura = "",
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

                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_S_TR_CI_OUT_VC_COMPOSICAO_Resumo  @ID_INTEGRACAO, @SITUACAO, @QUALIFICACAO, @TIPO_VC, @TIPO_FATURA, @CONTA_DEBITO, @CONTA_CREDITO, @COD_PROCESSO, @REFERENCIA",
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
        public List<ComposicaoVariacao> GridContabilizacaoFiltro(int id_integracao = 0, string situacao = "", string qualificacao = "", string tipo_vc = "", string tipo_fatura = "",
    string conta_debito = "", string conta_credito = "", string cod_processo = "", string referencia = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                SqlParameter pSituacao = new SqlParameter("@SITUACAO", situacao);
                SqlParameter pQualificacao = new SqlParameter("@QUALIFICACAO", qualificacao);
                SqlParameter pTipoFatura = new SqlParameter("@TIPO_FATURA", tipo_fatura);
                SqlParameter pTipoVc = new SqlParameter("@TIPO_VC", tipo_vc);
                SqlParameter pContaDebito = new SqlParameter("@CONTA_DEBITO", conta_debito);
                SqlParameter pContaCredito = new SqlParameter("@CONTA_CREDITO", conta_credito);
                SqlParameter PCodProcesso = new SqlParameter("@COD_PROCESSO", cod_processo);
                SqlParameter pReferencia = new SqlParameter("@REFERENCIA", referencia);

                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_S_TR_CI_OUT_VC_COMPOSICAO_Filtro  @ID_INTEGRACAO, @SITUACAO, @QUALIFICACAO, @TIPO_VC, @TIPO_FATURA, @CONTA_DEBITO, @CONTA_CREDITO, @COD_PROCESSO, @REFERENCIA",
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
        public List<ComposicaoVariacao> PopupReferencia(int id_integracao = 0, int id_lancamento = 0, string chave_lancamento = "", int id_entidade = 0, 
            string referencia = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                SqlParameter pIdLancamento = new SqlParameter("@ID_LANCAMENTO", id_lancamento);
                SqlParameter pIdEntidade = new SqlParameter("@ID_ENTIDADE", id_entidade);
                SqlParameter pChave = new SqlParameter("@CHAVE_LANCAMENTO", chave_lancamento);
                SqlParameter pReferencia = new SqlParameter("@REFERENCIA1", referencia);

                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_S_TR_CI_OUT_VC_COMPOSICAO  @ID_INTEGRACAO, @ID_LANCAMENTO, @CHAVE_LANCAMENTO, @ID_ENTIDADE, @REFERENCIA1",
                    pIdIntegracao, pIdLancamento, pChave, pIdEntidade, pReferencia).ToList();

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
        public List<ComposicaoVariacao> AddExcecao(int id_excecao = 0, int id_pessoa = 0, string referencia = "", string observacao = "")
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

                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_I_TR_CI_OUT_VC_EXCECAO  @ID_EXCECAO, @ID_PESSOA, @REFERENCIA, @OBSERVACAO",
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
        public List<ComposicaoVariacao> Salvar(int id_integracao = 0, int id_lancamento = 0, string chave_lancamento = "", int id_entidade = 0, string referencia = "", int tp_fatura = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                SqlParameter pIdLancamento = new SqlParameter("@ID_LANCAMENTO", id_lancamento);
                SqlParameter pIdEntidade = new SqlParameter("@ID_ENTIDADE", id_entidade);
                SqlParameter pTpFatura = new SqlParameter("@FAT_TIPO_FATURA", tp_fatura);
                SqlParameter pReferencia = new SqlParameter("@REFERENCIA1", referencia);
                SqlParameter pChave = new SqlParameter("@CHAVE_LANCAMENTO", chave_lancamento);

                var linha = db.Database.SqlQuery<ComposicaoVariacao>("EXEC STO_U_TR_CI_OUT_VC_COMPOSICAO  @ID_INTEGRACAO, @ID_LANCAMENTO, @CHAVE_LANCAMENTO, @ID_ENTIDADE, @REFERENCIA1, @FAT_TIPO_FATURA",
                    pIdIntegracao, pIdLancamento, pChave, pIdEntidade, pReferencia, pTpFatura).ToList();

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
