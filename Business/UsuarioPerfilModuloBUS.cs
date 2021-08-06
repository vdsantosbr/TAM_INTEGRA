using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioPerfilModuloBUS
    {
        UsuarioPerfilModuloDAL dal = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        int retorno = 0;

        public UsuarioPerfilModuloBUS()
        {
            dal = new UsuarioPerfilModuloDAL();
        }

        public List<UsuarioPerfilModulo> Lista()
        {
            List<UsuarioPerfilModulo> lst = dal.Lista().OrderBy(obj => obj.Modulo).ToList();
            return lst;
        }

        public List<UsuarioPerfilModulo> BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public UsuarioPerfilModulo BuscaPorDuplicidade(UsuarioPerfilModulo obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public int Insere(UsuarioPerfilModulo obj, int idUsuarioAutor)
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

        public int Atualiza(UsuarioPerfilModulo obj, int idUsuarioAutor)
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

        public int Apaga(UsuarioPerfilModulo obj, int idUsuarioAutor)
        {
            if (dal.Apaga(obj, idUsuarioAutor))
            {
                retorno = 1;
            }
            return retorno;
        }

        public int ApagaModulosDoPerfil(int idPerfil, int idUsuarioAutor)
        {
            if (dal.ApagaModulosDoPerfil(idPerfil, idUsuarioAutor))
            {
                retorno = 1;
            }
            return retorno;
        }
    }
}
