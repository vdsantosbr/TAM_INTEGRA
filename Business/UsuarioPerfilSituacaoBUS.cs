using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioPerfilSituacaoBUS
    {
        UsuarioPerfilSituacaoDAL dal = null;
        UsuarioPerfilDAL dalPerfil = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        //3 = Perfil vinculado
        int retorno = 0;

        public UsuarioPerfilSituacaoBUS()
        {
            dal = new UsuarioPerfilSituacaoDAL();
            dalPerfil = new UsuarioPerfilDAL();
        }

        public List<UsuarioPerfilSituacao> Lista()
        {
            List<UsuarioPerfilSituacao> lst = dal.Lista().OrderBy(obj => obj.Nome).ToList();
            return lst;
        }

        public UsuarioPerfilSituacao BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public UsuarioPerfilSituacao BuscaPorDuplicidade(UsuarioPerfilSituacao obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public int Insere(UsuarioPerfilSituacao obj, int idUsuarioAutor)
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

        public int Atualiza(UsuarioPerfilSituacao obj, int idUsuarioAutor)
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
            if (dalPerfil.BuscaPorSituacao(idSituacao).Count == 0)
            {
                if (dal.Apaga(idSituacao, idUsuarioAutor))
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
    }
}
