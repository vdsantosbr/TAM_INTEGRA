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
    public class CavokBUS
    {
        CavokDAL dal = new CavokDAL();

        public List<Cavok> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, int faturamento, string numeroMov, string situacao)
        {
            List<Cavok> lst = dal.Filtro(dataInicioDT, dataTerminoDT, faturamento, numeroMov, situacao);
            return lst;
        }
        public List<Cavok> Informe(int id_integracao = 0, int id_fatura = 0)
        {
            List<Cavok> lst = dal.Informe(id_integracao, id_fatura).ToList();
            return lst;
        }

        public List<Cavok> Reprocessar(int id_integracao = 0, int id_fatura = 0, int id_pessoa = 0)
        {
            List<Cavok> lst = dal.Reprocessamento(id_integracao, id_fatura, id_pessoa);
            return lst;
        }
    }
}
