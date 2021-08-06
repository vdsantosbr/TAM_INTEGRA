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
    public class ImportacaoDocumentoBUS
    {
        ImportacaoDocumentoDAL dal = new ImportacaoDocumentoDAL();

        public List<ImportacaoDocumento> ListaFornecedor(int id_perfil)
        {
            List<ImportacaoDocumento> lst = dal.ListaFornecedor(id_perfil);
            return lst;
        }

        public List<ImportacaoDocumento> ListaProcesso(string formulario, int id_integracao_servidor = 0, int id_perfil = 0)
        {
            List<ImportacaoDocumento> lst = dal.ListaProcesso(formulario, id_integracao_servidor, id_perfil);
            return lst;
        }

        public List<ImportacaoDocumento> ListaMovimento(string formulario, int id_integracao_processo)
        {
            List<ImportacaoDocumento> lst = new List<ImportacaoDocumento>();
             lst = dal.ListaMovimento(formulario, id_integracao_processo);
            return lst;
        }
        public List<ImportacaoDocumento> Filtro(int id_Perfil, DateTime data_Inicio, DateTime data_Fim, string codTMV, string numeroMov, string situacao, int id_integracao = 0)
        {
            List<ImportacaoDocumento> lst = dal.Filtro(id_Perfil, data_Inicio, data_Fim, codTMV, numeroMov, situacao, id_integracao);
            return lst;
        }
        public List<ImportacaoDocumento> ItensPedido(int idMov)
        {
            List<ImportacaoDocumento> lst = dal.ItensPedido(idMov);
            return lst;
        }
        public List<ImportacaoDocumento> ItensPedidoEdicao(int idMov)
        {
            List<ImportacaoDocumento> lst = dal.ItensPedidoEdicao(idMov);
            return lst;
        }
        public List<ImportacaoDocumento> abrirDocumento(int idMov)
        {
            List<ImportacaoDocumento> lst = dal.abrirDocumento(idMov);
            return lst;
        }

        public List<ImportacaoDocumento> agendarIntegracao(int idMov)
        {
            List<ImportacaoDocumento> lst = dal.agendarIntegracao(idMov);
            return lst;
        }
        public List<ImportacaoDocumento> IntegracaoParametroValidar(int idMov)
        {
            List<ImportacaoDocumento> lst = dal.IntegracaoParametroValidar(idMov);
            return lst;
        }
    }
}
