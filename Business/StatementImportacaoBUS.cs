using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class StatementImportacaoBUS
    {
        private int retorno = 0;

        StatementImportacaoDAL dal = new StatementImportacaoDAL();
        public int Insere(StatementImportacao obj, int idUsuarioAutor)
        {
            if (dal.Insere(obj, idUsuarioAutor))
            {
                retorno = 1;
            }
            return retorno;
        }

        public StatementImportacao SalvarStatement(string id_conta, DateTime dataBase, int id_pessoa)
        {
            StatementImportacao lst = dal.SalvarStatement(id_conta, dataBase, id_pessoa);
            return lst;
        }

        public bool ExcluirStatement(int id_statement, int id_pessoa)
        {
            return dal.ExcluirStatement(id_statement, id_pessoa);
        }

        public bool ExcluirConciliacao(int id_conciliacao, int id_pessoa)
        {
            return dal.ExcluirConciliacao(id_conciliacao, id_pessoa);
        }

        public bool ExcluirRMFluxus(int id_RM_Fluxus, int id_pessoa)
        {
            return dal.ExcluirRMFluxus(id_RM_Fluxus, id_pessoa);
        }

        public List<StatementImportacao> Lista(int ano, int mes)
        {
            List<StatementImportacao> lst = dal.Lista(ano, mes);
            return lst;
        }

        public List<StatementImportacao> Download(int id_statement)
        {
            List<StatementImportacao> lst = dal.Download(id_statement);
            return lst;
        }
    }
}
