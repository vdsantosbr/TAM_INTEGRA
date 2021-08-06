using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class TipoMovimentoDAL
    {
        public List<TipoMovimento> Lista(string formulario, int id_integracao_processo)
        {
            List<TipoMovimento> x = new List<TipoMovimento>();
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pFormulario = new SqlParameter("@Formulario", formulario);
                SqlParameter pIntegracaoProcesso = new SqlParameter("@id_Integracao_Processo", (id_integracao_processo == 0) ? 0 : id_integracao_processo);


                var linha = db.Database.SqlQuery<TipoMovimento>("EXEC STO_S_COMPRAS_FORMULARIO  @Formulario, @ID_INTEGRACAO_PROCESSO ", pFormulario, pIntegracaoProcesso).ToList();


                if (linha == null)
                {
                    x.Add(new TipoMovimento());
                }


                if (linha.Count > 0)
                {
                    return linha;
                }
                else
                {
                    return x;
                }
            }
        }
    }
}
