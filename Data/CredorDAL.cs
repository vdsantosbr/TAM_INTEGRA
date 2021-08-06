using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using System.Data.SqlClient;

namespace Data
{
    public class CredorDAL
    {
        public List<Credor> Credor()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    var linha = db.Database.SqlQuery<Credor>("EXEC STO_S_TR_IS_OUT_SPDESP_Credor").ToList();

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
