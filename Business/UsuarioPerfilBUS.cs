using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioPerfilBUS
    {
        UsuarioPerfilDAL dal = null;
        UsuarioDAL dalUsuario = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        //3 = Usuário vinculado
        int retorno = 0;

        public UsuarioPerfilBUS()
        {
            dal = new UsuarioPerfilDAL();
            dalUsuario = new UsuarioDAL();
        }

        public List<UsuarioPerfil> Lista()
        {
            List<UsuarioPerfil> lst = dal.Lista().OrderBy(obj => obj.Nome).ToList();
            return lst;
        }

        public UsuarioPerfil BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public UsuarioPerfil BuscaPorDuplicidade(UsuarioPerfil obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public int Insere(UsuarioPerfil obj, int idUsuarioAutor)
        {
            //Validação de duplicidade
            if (dal.BuscaPorDuplicidade(obj) == null)
            {
                retorno = dal.Insere(obj, idUsuarioAutor);
            }

            return retorno;
        }

        public int Atualiza(UsuarioPerfil obj, int idUsuarioAutor)
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

        public int Apaga(int idPerfil, int idUsuarioAutor)
        {
            if (dalUsuario.BuscaPorPerfil(idPerfil).Count == 0)
            {
                if (dal.Apaga(idPerfil, idUsuarioAutor))
                {
                    retorno = 1;
                }
            }
            else
            {
                retorno = 3;
            }
            return retorno;
        }

        public void RemoveModulosExistentes(ref List<UsuarioModulo> lstUm, List<UsuarioPerfilModulo> lstUpm)
        {
            foreach (UsuarioPerfilModulo upm in lstUpm)
            {
                foreach (UsuarioModulo um in lstUm.ToList())
                {
                    if (upm.IdModulo == um.Id)
                    {
                        lstUm.Remove(um);
                    }
                }
            }
        }
    }
}
