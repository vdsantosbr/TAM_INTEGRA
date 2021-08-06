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
    public class ContaContabilBUS
    {
        ContaContabilDAL dal = new ContaContabilDAL();

        public List<ContaContabil> Exibir()
        {
            List<ContaContabil> lst = new List<ContaContabil>();
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
        public List<Conta> Contas()
        {
            List<Conta> lst = new List<Conta>();
            try
            {
                lst = dal.Contas();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Conta> SalvarConta(int id_conta = 0, string cod_conta = "", int id_pessoa = 0)
        {
            List<Conta> lst = new List<Conta>();
            try
            {
                lst = dal.SalvarConta(id_conta, cod_conta, id_pessoa);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
    }
}
