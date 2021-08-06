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
    public class StatementClassificacaoBUS
    {
        StatementClassificacaoDAL dal = null;

        public StatementClassificacaoBUS()
        {
            dal = new StatementClassificacaoDAL();
        }

        public List<StatementClassificacao> Lista(string classificacao = null, string descricao = null, string situacao = null)
        {
            List<StatementClassificacao> lst = new List<StatementClassificacao>();
            try
            {
                lst = dal.Lista(classificacao, descricao, situacao).ToList();
            }
            catch (Exception e)
            {
                return null;
            }

            return lst;
        }

        public StatementClassificacao updateclassificacao(int idclassificacao = 0, string classificacao = null, string descricao = null, string situacao = null)
        {
            return dal.updateclassificacao(idclassificacao, classificacao, descricao, situacao);
        }

        public StatementClassificacao inserirclassificacao(string descricao = null, string situacao = null)
        {
            return dal.inserirclassificacao(descricao, situacao);
        }

        public StatementClassificacao excluirClassificacao(int idClassificacao = 0)
        {
            return dal.excluirclassificacao(idClassificacao);
        }
    }
}
