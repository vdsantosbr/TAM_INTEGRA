using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Business
{
    public class IntegracaoBUS
    {
        IntegracaoDAL dal = new IntegracaoDAL();
        int retorno = 0;

        public List<Integracao> BuscaIdIntegracaoProcessoBUS(string formulario, int id_integracao_servidor, int id_Perfil = 0)
        {
            List<Integracao> integracao = dal.BuscaIdIntegracaoProcesso(formulario, id_integracao_servidor, id_Perfil);
            return integracao;
        }

        public void Integracao(Integracao integracao)
        {
            //Integracao lst = dal.Integracao(integracao);
            //return lst;

            if (dal.Integracao(integracao))
            {
                retorno = 1;
            }

        }

        public Integracao IntegracaoImportacaoProduto(int idIntegracao = 0, int idIntegracaoProcesso = 0, string complemento = null, string tipo = null, string situacao = null, string observacao = null, int idPessoa = 0)
        {
            Integracao lst = dal.IntegracaoImportacaoProduto(0, idIntegracaoProcesso, null, null, null, null, idPessoa);
            return lst;
        }

        public Integracao IntegracaoParametroBUS(string origem, int idIntegracao, int idMov, int IDCFO = 0, int NSEQITMMOV = 0, int IDPRD = 0, string serial = "", string chave = "", int NF_ITE_NM_ADICAO = 0, int NF_ITE_NM_ITEM_ADICAO = 0, string final_destination = "", int idLan = 0)
        {
            Integracao lst = dal.IntegracaoParametro(origem, idIntegracao, idMov, IDCFO, NSEQITMMOV, IDPRD, serial, chave, NF_ITE_NM_ADICAO, NF_ITE_NM_ITEM_ADICAO, final_destination, idLan);
            return lst;
        }

        public Integracao DesbloquearPedidoBUS(int idIntegracao = 0, int idMov = 0, int idPessoa = 0)
        {
            Integracao lst = dal.DesbloquearPedido(idIntegracao, idMov, idPessoa);
            return lst;
        }

        public List<Integracao> IntegracaoProcesso(int idIntegracaoProcesso = 0)
        {
            List<Integracao> lst = dal.IntegracaoProcesso(idIntegracaoProcesso);
            return lst;
        }

    }
}
