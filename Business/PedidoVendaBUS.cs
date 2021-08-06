using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace Business
{
    public class PedidoVendaBUS
    {
        PedidoVendaDAL dal = null;

        public PedidoVendaBUS()
        {
            dal = new PedidoVendaDAL();
        }

        public List<PedidoVenda> Lista()
        {
            List<PedidoVenda> lst = dal.Lista().ToList();
            return lst;
        }

        public List<PedidoVenda> PedidoCompraLista(DateTime? data_Inicio, DateTime? data_Termino, string codCFO, string codVen, string codTmv, string numeroMov, int idPerfil,
            string formulario, string situacao, int id_integracao, int idMov)
        {
            List<PedidoVenda> lst = dal.PedidoCompraLista(data_Inicio, data_Termino, codCFO, codVen, codTmv, numeroMov, idPerfil, formulario, situacao, id_integracao, idMov).ToList();
            return lst;
        }
        public List<PedidoVenda> TipoMovimento()
        {
            List<PedidoVenda> lst = dal.TipoAprovacao().ToList();
            return lst;
        }
        public List<PedidoVenda> Pedido(string tipo_movimento = "", string pedido = "")
        {
            List<PedidoVenda> lst = dal.Pedido(tipo_movimento, pedido).ToList();
            return lst;
        }
        public List<PedidoVenda> Item(string tipo_movimento = "", string pedido = "")
        {
            List<PedidoVenda> lst = dal.Item(tipo_movimento, pedido).ToList();
            return lst;
        }
        public List<PedidoVenda> Aprovacao(string tipo_movimento = "", string pedido = "")
        {
            List<PedidoVenda> lst = dal.Aprovacao(tipo_movimento, pedido).ToList();
            return lst;
        }
        public List<PedidoVenda> Justificativa(string tipo_movimento = "", string pedido = "")
        {
            List<PedidoVenda> lst = dal.Justificativa(tipo_movimento, pedido).ToList();
            return lst;
        }
        public Integracao CancelarPedidoBUS(int idMov = 0, int idPessoa = 0, string justificativa = "")
        {
            Integracao lst = dal.CancelarPedido(idMov, idPessoa, justificativa);
            return lst;
        }
        public Integracao EditarPedidoBUS(int idMov = 0, int idPessoa = 0, int motivo = 0, string justificativa = "")
        {
            Integracao lst = dal.EditarPedido(idMov, idPessoa, motivo, justificativa);
            return lst;
        }
        public Integracao LiberarPedidoBUS(int idLiberacaoPO = 0, int motivo = 0, int idMov = 0, int idPessoa = 0, string justificativa = "")
        {
            Integracao lst = dal.LiberarPedido(idLiberacaoPO, motivo, idMov, idPessoa, justificativa);
            return lst;
        }
        public List<Motivo> Motivo()
        {
            List<Motivo> lst = dal.Motivo().ToList();
            return lst;
        }
        public List<PedidoVenda> Fornecedor()
        {
            List<PedidoVenda> lst = dal.Fornecedor();
            return lst;
        }
        public List<PedidoVenda> Movimento(int id_integracao_processo = 0)
        {
            List<PedidoVenda> lst = dal.Movimento(id_integracao_processo);
            return lst;
        }
        public List<PedidoVenda> GerarPedidoVenda(int id_integracao_processo = 0, int id_integracao_servidor = 0, int idmov = 0, int id_pessoa = 0)
        {
            List<PedidoVenda> lst = dal.GerarPedidoVenda(id_integracao_processo, id_integracao_servidor, idmov, id_pessoa);
            return lst;
        }
        public List<PedidoVenda> PedidoVenda(int id_integracao_processo = 0, int id_integracao_servidor = 0, int idmov = 0)
        {
            List<PedidoVenda> lst = dal.PedidoVenda(id_integracao_processo, id_integracao_servidor, idmov);
            return lst;
        }
    }
}
