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
    public class StatementContasBUS
    {
        StatementContaDAL dal = null;

        public StatementContasBUS()
        {
            dal = new StatementContaDAL();
        }

        public List<StatementContas> Lista(string conta = null, string descricao = null, string situacao = null)
        {
            List<StatementContas> lst = new List<StatementContas>();
            try
            {
                lst = dal.Lista(conta, descricao, situacao).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
            
            return lst;
        }

        public StatementContas updateConta (int idConta = 0, string conta = null, string descricao = null, string situacao = null)
        {
            return dal.updateConta(idConta, conta, descricao, situacao);
        }

        public StatementContas inserirConta(string conta = null, string descricao = null, string situacao = null)
        {
            return dal.inserirConta(conta, descricao, situacao);
        }

        public StatementContas excluirConta(int idConta = 0)
        {
            return dal.excluirConta(idConta);
        }
    }
}
