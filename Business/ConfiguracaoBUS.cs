using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class ConfiguracaoBUS
    {
        ConfiguracaoDAL dal = new ConfiguracaoDAL();

        public List<Configuracao> Lista(string projeto = null, string documento = null)
        {
            List<Configuracao> lst = dal.Lista(projeto, documento);
            return lst;
        }

        public List<Configuracao> ListaProjeto()
        {
            List<Configuracao> lst = dal.ListaProjeto().ToList();
            return lst;
        }

        public List<Configuracao> ListaDocumento()
        {
            List<Configuracao> lst = dal.ListaDocumento().ToList();
            return lst;
        }
    }
}
