using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioModuloBUS
    {
        UsuarioModuloDAL dal = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        int retorno = 0;

        public UsuarioModuloBUS()
        {
            dal = new UsuarioModuloDAL();
        }

        public List<UsuarioModulo> Lista()
        {
            List<UsuarioModulo> lst = dal.Lista().OrderBy(obj => obj.Nome).ToList();
            return lst;
        }

        public UsuarioModulo BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public UsuarioModulo BuscaPorDuplicidade(UsuarioModulo obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public int Insere(UsuarioModulo obj, int idUsuarioAutor)
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

        public int Atualiza(UsuarioModulo obj, int idUsuarioAutor)
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

        public int Apaga(int id, int idUsuarioAutor)
        {
            if (dal.Apaga(id, idUsuarioAutor))
            {
                retorno = 1;
            }
            return retorno;
        }
    }
}
