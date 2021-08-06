using System.Collections.Generic;
using System.Linq;
using Entities;
using System.Data.SqlClient;
using System;

namespace Data
{
    public class ConfiguracaoDAL
    {
        public List<Configuracao> Lista(string projeto = null, string documento = null)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                SqlParameter pProjeto = new SqlParameter("@PROJETO ", (projeto == null) ? (object)System.DBNull.Value : projeto);
                SqlParameter pDocumento = new SqlParameter("@TIPO_DOCUMENTO ", (documento == null) ? (object)DBNull.Value : documento);

                return db.Database.SqlQuery<Configuracao>("EXEC STO_S_CONFIGURACAO @PROJETO, @TIPO_DOCUMENTO", pProjeto, pDocumento).ToList();
            }
        }

        public List<Configuracao> ListaProjeto()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<Configuracao>("EXEC STO_S_CONFIGURACAO_PROJETO").ToList();
            }
        }

        public List<Configuracao> ListaDocumento()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<Configuracao>("EXEC STO_S_CONFIGURACAO_TIPO_DOCUMENTO").ToList();
            }
        }
    }
}
