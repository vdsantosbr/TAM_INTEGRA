using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class EnderecoEstadoBUS
    {
        EnderecoEstadoDAL dal = null;

        public EnderecoEstadoBUS()
        {
            dal = new EnderecoEstadoDAL();
        }

        public List<EnderecoEstado> Lista()
        {
            List<EnderecoEstado> lst = dal.Lista().OrderBy(obj => obj.UF).ToList();
            return lst;
        }

        public EnderecoEstado BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public EnderecoEstado BuscaPorIdEnt(int id)
        {
            return dal.BuscaPorIdEnt(id);
        }

        public EnderecoEstado BuscaPorCidade(int idCidade)
        {
            return dal.BuscaPorCidade(idCidade);
        }

        public List<EnderecoEstado> BuscaPorRegiao(int idRegiao)
        {
            return dal.BuscaPorRegiao(idRegiao).OrderBy(obj => obj.Nome).ToList();
        }
    }
}
