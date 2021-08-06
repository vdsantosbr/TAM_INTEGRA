using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace Business
{
    public class CompraAprovacaoBUS
    {
        CompraAprovacaoDAL dal = null;

        public CompraAprovacaoBUS()
        {
            dal = new CompraAprovacaoDAL();
        }

        public List<CompraAprovacao> Lista()
        {
            List<CompraAprovacao> lst = dal.Lista().ToList();
            return lst;
        }

        public List<CompraAprovacao> PedidoCompraLista(DateTime? data_Inicio, DateTime? data_Termino, string codCFO, string codVen, string codTmv, string numeroMov, int idPerfil,
            string formulario, string situacao, int id_integracao, int idMov)
        {
            List<CompraAprovacao> lst = dal.PedidoCompraLista(data_Inicio, data_Termino, codCFO, codVen, codTmv, numeroMov, idPerfil, formulario, situacao, id_integracao, idMov).ToList();
            return lst;
        }
        public List<CompraAprovacao> TipoMovimento()
        {
            List<CompraAprovacao> lst = dal.TipoAprovacao().ToList();
            return lst;
        }
        public List<CompraAprovacao> Pedido(string tipo_movimento = "", string pedido = "")
        {
            List<CompraAprovacao> lst = dal.Pedido(tipo_movimento, pedido).ToList();
            return lst;
        }
        public List<CompraAprovacao> Item(string tipo_movimento = "", string pedido = "")
        {
            List<CompraAprovacao> lst = dal.Item(tipo_movimento, pedido).ToList();
            return lst;
        }
        public List<CompraAprovacao> Aprovacao(string tipo_movimento = "", string pedido = "")
        {
            List<CompraAprovacao> lst = dal.Aprovacao(tipo_movimento, pedido).ToList();
            return lst;
        }
        public List<CompraAprovacao> Justificativa(string tipo_movimento = "", string pedido = "")
        {
            List<CompraAprovacao> lst = dal.Justificativa(tipo_movimento, pedido).ToList();
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
    }
}
