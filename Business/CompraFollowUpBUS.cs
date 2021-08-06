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
    public class CompraFollowUpBUS
    {
        CompradoFollowUpDAL dal = null;

        public CompraFollowUpBUS()
        {
            dal = new CompradoFollowUpDAL();
        }

        public List<CompraFollowUp> Filtro(DateTime? data_Inicio, DateTime? data_Termino, string pedido, string aplicacao, string pn, string invoice, string conhecimento, string status_compra = "", string status_pedido = "", string processo = "")
        {
            List<CompraFollowUp> lst = dal.Filtro(data_Inicio, data_Termino, pedido, aplicacao, pn, invoice, conhecimento, status_compra, status_pedido, processo).ToList();
            return lst;
        }
        public List<CompraFollowUp> Informativo(int idmov = 0, int nseqitmov = 0, int idprd = 0)
        {
            List<CompraFollowUp> lst = dal.Informativo(idmov, nseqitmov, idprd).ToList();
            return lst;
        }
         public List<CompraFollowUp> Excel(DateTime? data_Inicio, DateTime? data_Termino, string pedido, string aplicacao, string pn, string invoice, string conhecimento, string status_compra = "", string status_pedido = "", string processo = "")
        {
            List<CompraFollowUp> lst = dal.Excel(data_Inicio, data_Termino, pedido, aplicacao, pn, invoice, conhecimento, status_compra, status_pedido, processo).ToList();
            return lst;
        }
        public List<CompraFollowUp> Salvar(int idmov = 0, int nseqitmov = 0, int idprd = 0, string status = "", DateTime? prazo = null, string observacao = "", string house = "", string processo = "")
        {
            List<CompraFollowUp> lst = dal.Salvar(idmov, nseqitmov, idprd, status, prazo, observacao, house, processo).ToList();
            return lst;
        }
        public List<CompraFollowUp> Importar(CompraFollowUp compra)
        {
            List<CompraFollowUp> lst = dal.Importar(compra).ToList();
            return lst;
        }
        public List<CompraFollowUp> Item(int idmov = 0, int nseqitmov = 0, int idprd = 0)
        {
            List<CompraFollowUp> lst = dal.Item(idmov, nseqitmov, idprd).ToList();
            return lst;
        }
    }
}
