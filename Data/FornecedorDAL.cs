using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public class FornecedorDAL
    {
        public List<Fornecedor> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<Fornecedor>("EXEC STO_S_INTEGRACAO_PERFIL_FORNECEDOR NULL, NULL").ToList();
            }
        }

        public List<Fornecedor> ListaConta(int id_Perfil, string formulario)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId_Perfil = new SqlParameter("@id_Perfil", id_Perfil);
                SqlParameter pFormulario = new SqlParameter("@Formulario", formulario);
                var linha = db.Database.SqlQuery<Fornecedor>("EXEC STO_S_COMPRAS_FORNECEDOR @id_Perfil, @formulario", pId_Perfil, pFormulario).ToList();
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

        public List<Fornecedor> Lista(int id_Perfil, string formulario)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId_Perfil = new SqlParameter("@id_Perfil", id_Perfil);
                //SqlParameter pFormulario = new SqlParameter("@Formulario", formulario);
                var linha = db.Database.SqlQuery<Fornecedor>("EXEC STO_S_INTEGRACAO_SERVIDOR_POR_PERFIL @id_Perfil", pId_Perfil).ToList();
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
