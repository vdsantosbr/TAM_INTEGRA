using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CasosExcecaoBUS
    {
        CasosExcecaoDAL dal = new CasosExcecaoDAL();

        public List<CasosExcecao> Exibir()
        {
            List<CasosExcecao> lst = new List<CasosExcecao>();
            try
            {
                lst = dal.Exibir();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<CasosExcecao> GridExcecoes()
        {
            List<CasosExcecao> lst = new List<CasosExcecao>();
            try
            {
                lst = dal.GridExcecoes();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<CasosExcecao> AddExcecoes(int id_excecao = 0, int id_pessoa = 0, string referencia = "", string observacao = "")
        {
            List<CasosExcecao> lst = new List<CasosExcecao>();
            try
            {
                lst = dal.AddExcecao(id_excecao, id_pessoa, referencia, observacao);
            }
            catch (Exception e)
            {                lst.Add(new CasosExcecao
                {
                    Mensagem = e.Message
                });
            }
            return lst;
        }
        public List<CasosExcecao> RemoverExcecao(int id_excecao = 0)
        {
            List<CasosExcecao> lst = new List<CasosExcecao>();
            try
            {
                lst = dal.RemoverExcecao(id_excecao);
            }
            catch (Exception e)
            {
                lst.Add(new CasosExcecao
                {
                    Mensagem = e.Message
                });
            }
            return lst;
        }
    }
}
