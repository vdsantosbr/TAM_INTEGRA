using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class EnderecoCidadeBUS
    {
        EnderecoCidadeDAL dal = null;

        public EnderecoCidadeBUS()
        {
            dal = new EnderecoCidadeDAL();
        }

        public List<EnderecoCidade> Lista()
        {
            List<EnderecoCidade> lst = dal.Lista().OrderBy(obj => obj.Nome).ToList();
            return lst;
        }

        public EnderecoCidade BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public List<EnderecoCidade> BuscaPorNome(string nome)
        {
            return dal.BuscaPorNome(nome);
        }

        public EnderecoCidade BuscaPorBairro(int idBairro)
        {
            return dal.BuscaPorBairro(idBairro);
        }

        public List<EnderecoCidade> BuscaPorEstado(int idEstado)
        {
            return dal.BuscaPorEstado(idEstado).OrderBy(obj => obj.UF).ToList();
        }

        public List<EnderecoCidade> BuscaPorEstadoUF(string idEstado)
        {
            return dal.BuscaPorEstadoUF(idEstado).OrderBy(obj => obj.UF).ToList();
        }

        public List<EnderecoCidade> BuscaPorEntidade(int idEntidade)
        {
            return dal.BuscaPorEntidade(idEntidade);
        }

        public List<EnderecoCidade> BuscaPorRegiao(int idRegiao)
        {
            return dal.BuscaPorRegiao(idRegiao);
        }
    }
}
