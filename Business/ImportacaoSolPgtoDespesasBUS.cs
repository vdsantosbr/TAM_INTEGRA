using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class ImportacaoSolPgtoDespesasBUS
    {
        ImportacaoSolPgtoDespesasDAL dal = new ImportacaoSolPgtoDespesasDAL();

        public List<ImportacaoSolPgtoDespesas> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT, int tpDoc, string numDoc, string processo, string nota, string situacao)
        {
            List<ImportacaoSolPgtoDespesas> lst = dal.Filtro(dataInicioDT, dataTerminoDT, tpDoc, numDoc, processo, nota, situacao);
            return lst;
        }
        public List<ImportacaoSolPgtoDespesas> Edicao(int id_spdesp = 0, int id_integracao = 0, int sp_id = 0, int spd_id_despesa_processo = 0)

        {
            List<ImportacaoSolPgtoDespesas> lst = new List<ImportacaoSolPgtoDespesas>();
            lst = dal.Edicao(id_spdesp, id_integracao, sp_id, spd_id_despesa_processo);
            return lst;
        }

        public List<ImportacaoSolPgtoDespesas> Reprocessar(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            List<ImportacaoSolPgtoDespesas> lst = dal.Reprocessamento(id_integracao, id_brokersys, numnf_brokersys);
            return lst;
        }
        public List<ImportacaoSolPgtoDespesas> Excluir(int id_integracao = 0, int id_brokersys = 0, int numnf_brokersys = 0)
        {
            List<ImportacaoSolPgtoDespesas> lst = dal.Excluir(id_integracao, id_brokersys, numnf_brokersys);
            return lst;
        }
        //public List<ImportacaoSolPgtoDespesas> Emitir_NFE(int id_Integracao = 0, string id_ne = "", string numnf_ne = "", string id_nd = "", string numnf_nd = "")
        //{
        //    List<ImportacaoSolPgtoDespesas> lst = dal.Emitir_NFE(id_Integracao, id_ne, numnf_ne, id_nd, numnf_nd);
        //    return lst;
        //}
        //public List<ImportacaoSolPgtoDespesas> RecFisico(int id_Integracao = 0, string id_ne = "", string numnf_ne = "", string id_nd = "", string numnf_nd = "")
        //{
        //    List<ImportacaoSolPgtoDespesas> lst = dal.RecFisico(id_Integracao, id_ne, numnf_ne, id_nd, numnf_nd);
        //    return lst;
        //}
        //public List<ImportacaoSolPgtoDespesas> Cancelar(int id_Integracao = 0, string id_ne = "", string numnf_ne = "", string id_nd = "", string numnf_nd = "")
        //{
        //    List<ImportacaoSolPgtoDespesas> lst = dal.Cancelar(id_Integracao, id_ne, numnf_ne, id_nd, numnf_nd);
        //    return lst;
        //}
        public List<ImportacaoSolPgtoDespesas> TipoDocumento()
        {
            List<ImportacaoSolPgtoDespesas> lst = dal.TipoDocumento();
            return lst;
        }
        public List<ImportacaoSolPgtoDespesas> Situacao()
        {
            List<ImportacaoSolPgtoDespesas> lst = dal.Situacao();
            return lst;
        }
        public List<ImportacaoSolPgtoDespesas> Filial()
        {
            List<ImportacaoSolPgtoDespesas> lst = dal.Filial();
            return lst;
        }
        public List<ImportacaoSolPgtoDespesas> Fornecedor()
        {
            List<ImportacaoSolPgtoDespesas> lst = dal.Fornecedor();
            return lst;
        }
        public List<ImportacaoSolPgtoDespesas> Moeda()
        {
            List<ImportacaoSolPgtoDespesas> lst = dal.Moeda();
            return lst;
        }
        public int Salvar(ImportacaoSolPgtoDespesas despesas)

        {
            int id_spdesp = 0;
            id_spdesp = dal.SalvarDespesas(despesas);
            return id_spdesp;
        }

    }
}
