using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class EstoqueBUS
    {
        EstoqueDAL dal = new EstoqueDAL();

        public List<Estoque> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, string declaracao, string processo, string nota, string situacao)
        {
            List<Estoque> lst = dal.Filtro(dataInicioDT, dataTerminoDT, declaracao, processo, nota, situacao);
            return lst;
        }
        public List<Estoque> Informe(int id_integracao = 0, string sp_id = null, string sp_id_despesa_processo = null)
        {
            List<Estoque> lst = dal.Informe(id_integracao, sp_id, sp_id_despesa_processo).ToList();
            return lst;
        }

        public List<Estoque> Reprocessar(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            List<Estoque> lst = dal.Reprocessamento(id_integracao, id_brokersys, numnf_brokersys);
            return lst;
        }
        public List<Estoque> Excluir(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            List<Estoque> lst = dal.Excluir(id_integracao, id_brokersys, numnf_brokersys);
            return lst;
        }
        public List<Estoque> Emitir_NFE(string nf_cod_processo = "")
        {
            List<Estoque> lst = dal.Emitir_NFE(nf_cod_processo);
            return lst;
        }
        public List<Estoque> RecFisico(string nf_cod_processo = "", string tp_nf = "")
        {
            List<Estoque> lst = dal.RecFisico(nf_cod_processo, tp_nf);
            return lst;
        }
        public List<Estoque> Cancelar(string nf_cod_processo = "", string tp_nf = "")
        {
            List<Estoque> lst = dal.Cancelar(nf_cod_processo, tp_nf);
            return lst;
        }
        public List<Estoque> TiposDespesas()
        {
            List<Estoque> lst = dal.TiposDespesas();
            return lst;
        }
        public List<Estoque> Credor()
        {
            List<Estoque> lst = dal.Credor();
            return lst;
        }
    }
}
