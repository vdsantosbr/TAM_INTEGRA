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
    public class ContabilizacaoBUS
    {
        ContabilizacaoDAL dal = new ContabilizacaoDAL();

        public List<Contabilizacao> Exibir(int ano = 0, int mes = 0)
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.Exibir(ano, mes);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Contabilizacao> GridContabilizacaoFiltro(int id_integracao = 0, string situacao = "", string qualificacao = "", string tipo_vc = "", string tipo_fatura = "",
string conta_debito = "", string conta_credito = "", string cod_processo = "", string referencia = "")
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
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
        public List<Contabilizacao> GridRegistroInconsistente(int id_integracao = 0, int id_fechamento = 0)
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.GridRegistroInconsistente(id_integracao, id_fechamento);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Contabilizacao> GridRegistrosErro(int id_integracao = 0, int id_fechamento = 0)
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.GridRegistrosErro(id_integracao, id_fechamento);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Contabilizacao> FormatarContabilizacao(int id_fechamento = 0)
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.FormatarContabilizacao(id_fechamento);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Contabilizacao> IntegrarrContabilizacao(int id_fechamento = 0)
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.IntegrarContabilizacao(id_fechamento);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Contabilizacao> ExcluirContabilizacao(int id_fechamento = 0)
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.ExcluirContabilizacao(id_fechamento);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Contabilizacao> ExportarContabilizacao(int ano = 0, int mes = 0)
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.ExportarContabilizacao(ano, mes);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Contabilizacao> FechamentoAno()
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.FechamentoAno();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Contabilizacao> FechamentoMes(int ano = 0)
        {
            List<Contabilizacao> lst = new List<Contabilizacao>();
            try
            {
                lst = dal.FechamentoMes(ano);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
    }
}
