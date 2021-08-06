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
    public class ImportacaoFornecedorBUS
    {
        ImportacaoFornecedorDAL dal = new ImportacaoFornecedorDAL();

        public List<ImportacaoFornecedor> ListaFornecedor(int id_perfil)
        {
            List<ImportacaoFornecedor> lst = dal.ListaFornecedor(id_perfil);
            return lst;
        }

        public List<ImportacaoFornecedor> ListaProcesso(string formulario, int id_integracao_servidor = 0, int id_pessoa = 0)
        {
            List<ImportacaoFornecedor> lst = dal.ListaProcesso(formulario, id_integracao_servidor, id_pessoa);
            return lst;
        }

        public List<ImportacaoFornecedor> ListaMovimento(string formulario, int id_integracao_processo)
        {
            List<ImportacaoFornecedor> lst = new List<ImportacaoFornecedor>();
            lst = dal.ListaMovimento(formulario, id_integracao_processo);
            return lst;
        }
        public List<ImportacaoFornecedor> Filtro(int id_integracao = 0,string codigo = "", string nomeFantasia = "")
        {
            List<ImportacaoFornecedor> lst = dal.Filtro(id_integracao, codigo, nomeFantasia);
            return lst;
        }
        public List<ImportacaoFornecedor> ItensPedido(int idMov)
        {
            List<ImportacaoFornecedor> lst = dal.ItensPedido(idMov);
            return lst;
        }
        public List<ImportacaoFornecedor> ItensPedidoEdicao(int idMov)
        {
            List<ImportacaoFornecedor> lst = dal.ItensPedidoEdicao(idMov);
            return lst;
        }
        public List<ImportacaoFornecedor> abrirDocumento(int idMov)
        {
            List<ImportacaoFornecedor> lst = dal.abrirDocumento(idMov);
            return lst;
        }

        public List<ImportacaoFornecedor> agendarIntegracao(int idMov)
        {
            List<ImportacaoFornecedor> lst = dal.agendarIntegracao(idMov);
            return lst;
        }
    }
}
