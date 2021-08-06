using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class SituacaoBUS
    {
        SituacaoDAL dal = new SituacaoDAL();

        public List<Situacao> ListatBUS()
        {
            List<Situacao> situacao = dal.Lista();
            return situacao;
        }
    }
}
