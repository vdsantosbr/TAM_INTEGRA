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
    public class ImportacaoProdutoBUS
    {
        ImportacaoProdutoDAL dal = new ImportacaoProdutoDAL();
        public List<ImportacaoProduto> ListaProduto(int id_integracao = 0, string codTMV = null, string numeroMov = null, string partNumber = null)
        {
            List<ImportacaoProduto> lst = dal.ListaProduto(id_integracao, codTMV, numeroMov, partNumber);
            return lst;
        }
    }
}
