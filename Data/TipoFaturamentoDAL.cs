using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Data
{
    public class TipoFaturamentoDAL
    {
        public List<TipoFaturamento> ListaTipoFaturamento()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<TipoFaturamento>("EXEC STO_S_CAVOK_TIPO_FATURAMENTO").ToList();
                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception e)
                {
                    return new List<TipoFaturamento>();
                }
            }
        }
    }
}
