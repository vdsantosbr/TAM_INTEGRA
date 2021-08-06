using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ComposicaoVariacaoBUS
    {
        ComposicaoVariacaoDAL dal = new ComposicaoVariacaoDAL();

        public List<ComposicaoVariacao> Fechamento_Novo()
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.Exibir();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> Situacao()
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.Situacao();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> Qualificacao()
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.Qualificacao();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> Tipo_vc()
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.Tipo_vc();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> Tipo_fatura()
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.Tipo_fatura();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> Conta_debito(int id_integracao = 0)
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.Conta_debito(id_integracao);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> Conta_credito(int id_integracao = 0)
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.Conta_credito(id_integracao);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> GridContabilizacao(int id_integracao = 0, string situacao = "", string qualificacao = "", string tipo_vc = "", string tipo_fatura = "",
            string conta_debito = "", string conta_credito = "", string cod_processo = "", string referencia = "")
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.GridContabilizacao(id_integracao, situacao, qualificacao, tipo_vc, tipo_fatura, conta_debito, conta_credito, cod_processo, referencia);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> GridContabilizacaoFiltro(int id_integracao = 0, string situacao = "", string qualificacao = "", string tipo_vc = "", string tipo_fatura = "",
    string conta_debito = "", string conta_credito = "", string cod_processo = "", string referencia = "")
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.GridContabilizacaoFiltro(id_integracao, situacao, qualificacao, tipo_vc, tipo_fatura, conta_debito, conta_credito, cod_processo, referencia);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> PopupReferencia(int id_integracao = 0, int id_lancamento = 0, string chave_lancamento = "", int id_entidade = 0,
    string referencia = "")
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.PopupReferencia(id_integracao, id_lancamento, chave_lancamento, id_entidade, referencia);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> AddExcecao(int id_excecao = 0, int id_pessoa = 0, string referencia = "", string observacao = "")
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.AddExcecao(id_excecao, id_pessoa, referencia, observacao);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<ComposicaoVariacao> Salvar(int id_integracao = 0, int id_lancamento = 0, string chave_lancamento = "", int id_entidade = 0, string referencia = "", int tp_fatura = 0)
        {
            List<ComposicaoVariacao> lst = new List<ComposicaoVariacao>();
            try
            {
                lst = dal.Salvar(id_integracao, id_lancamento, chave_lancamento, id_entidade, referencia, tp_fatura);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }

    }
}
