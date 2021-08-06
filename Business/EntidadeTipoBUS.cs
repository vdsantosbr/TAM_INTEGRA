using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class EntidadeTipoBUS
    {
        EntidadeTipoDAL dal = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        int retorno = 0;

        public EntidadeTipoBUS()
        {
            dal = new EntidadeTipoDAL();
        }

        public List<EntidadeTipo> Lista()
        {
            List<EntidadeTipo> lst = dal.Lista().OrderBy(obj => obj.Nome).ToList();
            return lst;
        }

        public EntidadeTipo BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public EntidadeTipo BuscaPorDuplicidade(EntidadeTipo obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public int Insere(EntidadeTipo obj, int idUsuarioAutor)
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

        public int Atualiza(EntidadeTipo obj, int idUsuarioAutor)
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

        public int Apaga(int idTipo, int idUsuarioAutor)
        {
            if (dal.Apaga(idTipo, idUsuarioAutor))
            {
                retorno = 1;
            }

            //if (dalMotorizacao.BuscaPorTipo(idTipo).Count == 0)
            //{

            //}
            //else
            //{
            //    retorno = 3;
            //}
            return retorno;
        }

        public List<EntidadeTipo> BuscaTipo(int id)
        {
            List<EntidadeTipo> lst = dal.BuscaPorIdLista(id).OrderBy(obj => obj.Nome).ToList();
            return lst;
        }
    }
}
