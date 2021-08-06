using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioSituacaoBUS
    {
        UsuarioSituacaoDAL dal = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        int retorno = 0;

        public UsuarioSituacaoBUS()
        {
            dal = new UsuarioSituacaoDAL();
        }

        public List<UsuarioSituacao> Lista()
        {
            List<UsuarioSituacao> lst = dal.Lista().OrderBy(obj => obj.Nome).ToList();
            return lst;
        }

        public UsuarioSituacao BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public UsuarioSituacao BuscaPorDuplicidade(UsuarioSituacao obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public int Insere(UsuarioSituacao obj, int idUsuarioAutor)
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

        public int Atualiza(UsuarioSituacao obj, int idUsuarioAutor)
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
