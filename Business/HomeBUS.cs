using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class HomeBUS
    {
        HomeDAL dal = new HomeDAL();

        public List<Home> Lista(DateTime dataInicio, DateTime dataFim, string numeroMov, string numProcesso)
        {
            List <Home> lst = dal.Lista(dataInicio, dataFim, numeroMov, numProcesso);
            return lst;
        }
    }
}
