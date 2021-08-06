using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Data;

namespace Business
{
    public class ContabilizacaoVariacaoCambialBUS
    {
        ContabilizacaoVariacaoCambialDAL dal = new ContabilizacaoVariacaoCambialDAL();

        public List<ContabilizacaoVariacaoCambial> Filtro(int ano, int mes, string codProcesso, string invoice, string tipo, string classificacao)
        {
            List<ContabilizacaoVariacaoCambial> lst = dal.Filtro(ano, mes, codProcesso, invoice, tipo, classificacao);
            return lst;
        }

        //public List<ImportacaoFatura> Cancelar(int id_integracao = 0, string fat_fatura_id = null)
        //{
        //    List<ImportacaoFatura> lst = dal.Cancelar(id_integracao, fat_fatura_id).ToList();
        //    return lst;
        //}

        public List<ContabilizacaoVariacaoCambial> Reprocessar(int id_integracao = 0, string id_lancamento = null)
        {
            List<ContabilizacaoVariacaoCambial> lst = dal.Reprocessar(id_integracao, id_lancamento);
            return lst;
        }

        public List<ContabilizacaoVariacaoCambial> Excluir(int id_integracao = 0, string id_lancamento = null)
        {
            List<ContabilizacaoVariacaoCambial> lst = dal.Excluir(id_integracao, id_lancamento);
            return lst;
        }
    }
}
