using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class HomeDAL
    {
        public List<Home> Lista(DateTime dataInicio, DateTime dataFim, string numeroMov = null, string numProcesso = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@Data_Inicio", dataInicio);
                SqlParameter pDataFim = new SqlParameter("@Data_Fim", dataFim);
                SqlParameter pNumeroPedido = new SqlParameter("@Num_pedido", (numeroMov == null) ? (object)DBNull.Value : numeroMov);
                SqlParameter pNumeroProcesso = new SqlParameter("@Num_processo", (numProcesso == null) ? (object)DBNull.Value : numProcesso);

                try
                {
                    var linha = db.Database.SqlQuery<Home>("EXEC STO_S_INTEGRACAO_WORKFLOW @Data_Inicio, @Data_Fim, @Num_pedido, @Num_processo", pDataInicio, pDataFim, pNumeroPedido, pNumeroProcesso).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    var erro = e.Message;
                    return null;
                }
                
            }
        }
    }
}

