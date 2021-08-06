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
    public class FornecedorBUS
    {
        FornecedorDAL dal = null;

        public FornecedorBUS()
        {
            dal = new FornecedorDAL();
        }

        public List<Fornecedor> Lista()
        {
            List<Fornecedor> lst = dal.Lista().OrderBy(obj => obj.Nome).ToList();
            return lst;
        }

        public List<Fornecedor> Lista(int id_Perfil, string formulario)
        {
            List<Fornecedor> lst = dal.Lista(id_Perfil, formulario);
            return lst;
        }



            public List<Fornecedor> ListaConta(int id_Perfil, string formulario)
        {
            List<Fornecedor> lst = dal.ListaConta(id_Perfil, formulario);
            return lst;
        }
    }
}
