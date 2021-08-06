using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class StatementDepartamentoBUS
    {
        StatementDepartamentoDAL dal = null;

        public StatementDepartamentoBUS()
        {
            dal = new StatementDepartamentoDAL();
        }

        public List<StatementDepartamento> Lista(string situacao = null)
        {
            List<StatementDepartamento> lst = new List<StatementDepartamento>();
            try
            {
                lst = dal.Lista(situacao).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return lst;
        }
    }
}
