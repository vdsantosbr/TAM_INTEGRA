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
    public class FechamentoContabilBUS
    {
        FechamentoContabilDAL dal = new FechamentoContabilDAL();

        public List<FechamentoContabil> Filtro (int ano = 0)
        {
            List<FechamentoContabil> lst = new List<FechamentoContabil>();
            try
            {
                lst = dal.Filtro(ano);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<FechamentoContabil> Fechamento_Novo()
        {
            List<FechamentoContabil> lst = new List<FechamentoContabil>();
            try
            {
                lst = dal.Fechamento_Novo();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<FechamentoContabil> Fechamento(int id_fechamento = 0)
        {
            List<FechamentoContabil> lst = new List<FechamentoContabil>();
            try
            {
                lst = dal.Fechamento(id_fechamento);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<FechamentoContabil> Salvar(int id_fechamento = 0, int id_integracao = 0, string fechamento = "", DateTime dt_contabil = default(DateTime), string situacao = "", int id_pessoa = 0, string observacao = "")
        {
            List<FechamentoContabil> lst = new List<FechamentoContabil>();
            try
            {
                lst = dal.Salvar(id_fechamento, id_integracao, fechamento, dt_contabil, situacao, id_pessoa, observacao);
            }
            catch (Exception e)
            {
                lst.Add(new FechamentoContabil
                {
                    Mensagem = e.Message
                });
            }
            return lst;
        }
        public List<IntegracaoCambioSys> GridCambioSys(int mes = 0, int ano = 0)
        {
            List<IntegracaoCambioSys> lst = new List<IntegracaoCambioSys>();
            try
            {
                lst = dal.GridCambioSys(mes, ano);
            }
            catch (Exception e)
            {
                lst.Add(new IntegracaoCambioSys
                {
                    //Mensagem = e.Message
                });
            }
            return lst;
        }
        public List<FechamentoContabil> FechamentoAno()
        {
            List<FechamentoContabil> lst = new List<FechamentoContabil>();
            try
            {
                lst = dal.FechamentoAno();
            }
            catch (Exception e)
            {
                lst.Add(new FechamentoContabil
                {
                    //Mensagem = e.Message
                });
            }
            return lst;
        }
    }
}
