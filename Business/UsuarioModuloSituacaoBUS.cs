using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioModuloSituacaoBUS
    {
        UsuarioModuloSituacaoDAL dal = null;
        UsuarioModuloDAL dalModulo = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        int retorno = 0;

        public UsuarioModuloSituacaoBUS()
        {
            dal = new UsuarioModuloSituacaoDAL();
            dalModulo = new UsuarioModuloDAL();
        }

        public List<UsuarioModuloSituacao> Lista()
        {
            List<UsuarioModuloSituacao> lst = dal.Lista().OrderBy(obj=>obj.Nome).ToList();
            return lst;
        }

        public UsuarioModuloSituacao BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public UsuarioModuloSituacao BuscaPorDuplicidade(UsuarioModuloSituacao obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public int Insere(UsuarioModuloSituacao obj, int idUsuarioAutor)
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

        public int Atualiza(UsuarioModuloSituacao obj, int idUsuarioAutor)
        {
            //Validação de duplicidade
            if (dal.BuscaPorDuplicidade(obj) != null)
            {
                retorno = 2;
            }
            else
            {
                if(dal.Atualiza(obj, idUsuarioAutor))
                {
                    retorno = 1;
                }
            }
            return retorno;   
        }

        public int Apaga(int idSituacao, int idUsuarioAutor)
        {
            if (dalModulo.BuscaPorSituacao(idSituacao).Count == 0)
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
