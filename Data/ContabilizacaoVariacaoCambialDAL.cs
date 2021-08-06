using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ContabilizacaoVariacaoCambialDAL
    {
        public List<ContabilizacaoVariacaoCambial> Filtro(int ano, int mes, string codProcesso, string invoice, string tipo, string classificacao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ano", (ano == 0) ? 0 : ano);
                SqlParameter pMes = new SqlParameter("@mes", (mes == 0) ? 0 : mes);
                SqlParameter PCodProcesso = new SqlParameter("@codProcesso", (codProcesso == null) ? (object)DBNull.Value : codProcesso);
                SqlParameter PInvoice = new SqlParameter("@invoice", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pTipo = new SqlParameter("@tipo", (invoice == null) ? (object)DBNull.Value : invoice);
                SqlParameter pClassificacao = new SqlParameter("@classificacao", (classificacao == null) ? (object)DBNull.Value : classificacao);

                var linha = db.Database.SqlQuery<ContabilizacaoVariacaoCambial>("EXEC STO_S_TR_CI_OUT_CICONTAB_VC_Filtro @ano, @mes, @codProcesso, @invoice, @tipo, @classificacao", pAno, pMes, PCodProcesso, PInvoice, pTipo, pClassificacao).ToList();
                if (linha.Count > 0)
                {
                    return linha;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<ContabilizacaoVariacaoCambial> Reprocessar(int id_integracao = 0, string id_lancamento = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? 0 : id_integracao);
                SqlParameter pIdLancamento = new SqlParameter("@id_lancamento", (id_lancamento == null) ? "" : id_lancamento);

                var linha = db.Database.SqlQuery<ContabilizacaoVariacaoCambial>("EXEC STO_I_TR_CI_OUT_CICONTAB_VC_Reprocessar @id_integracao, @id_lancamento", pIdIntegracao, pIdLancamento).ToList();
                if (linha.Count > 0)
                {
                    return linha;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<ContabilizacaoVariacaoCambial> Excluir(int id_integracao = 0, string id_lancamento = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? 0 : id_integracao);
                SqlParameter pIdLancamento = new SqlParameter("@id_lancamento", (id_lancamento == null) ? "" : id_lancamento);

                var linha = db.Database.SqlQuery<ContabilizacaoVariacaoCambial>("EXEC STO_I_TR_CI_OUT_CICONTAB_VC_Excluir @id_integracao, @id_lancamento", pIdIntegracao, pIdLancamento).ToList();
                if (linha.Count > 0)
                {
                    return linha;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
