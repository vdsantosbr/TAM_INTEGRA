using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioAreaBUS
    {
        UsuarioAreaDAL dal = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        int retorno = 0;

        public UsuarioAreaBUS()
        {
            dal = new UsuarioAreaDAL();
        }

        public List<UsuarioArea> Lista()
        {
            List<UsuarioArea> lst = dal.Lista().OrderBy(obj => obj.Nome).ToList();
            return lst;
        }

        public UsuarioArea BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public UsuarioArea BuscaPorDuplicidade(UsuarioArea obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public List<UsuarioArea> BuscaAreaVenda(string idArea)
        {
            return dal.BuscaAreaVenda(idArea);
        }

        public int Insere(UsuarioArea obj, int idUsuarioAutor)
        {
            //Validação de duplicidade
            if (dal.BuscaPorDuplicidade(obj) != null)
            {
                retorno = 2;
            }
            else
            {
                if (dal.Insere(obj, idUsuarioAutor))
                {
                    retorno = 1;
                }
            }
            return retorno;
        }

        public int Atualiza(UsuarioArea obj, int idUsuarioAutor)
        {
            //Validação de duplicidade
            if (dal.BuscaPorDuplicidade(obj) != null)
            {
                retorno = 2;
            }
            else
            {
                if (dal.Atualiza(obj, idUsuarioAutor))
                {
                    retorno = 1;
                }
            }
            return retorno;
        }

        public int Apaga(int idSituacao, int idUsuarioAutor)
        {
            if (dal.Apaga(idSituacao, idUsuarioAutor))
            {
                retorno = 1;
            }
            return retorno;
        }
    }
}
