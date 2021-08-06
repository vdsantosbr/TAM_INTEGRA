using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
   public class CompradorDAL
    {
        private DatabaseContext db = new DatabaseContext();
        public List<Compra> Lista()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Database.SqlQuery<Compra>("EXEC STO_S_RM_COMPRADOR").ToList();
            }
        }

        public List<Compra> PedidoCompraLista(DateTime? data_Inicio, DateTime? data_Termino, string codCFO, string codVen, string codTmv, string numeroMov, int idPerfil,
            string formulario, string situacao, int id_integracao, int idMov)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                
                SqlParameter pDataInicio = new SqlParameter("@data_Inicio", (data_Inicio == null) ? (object)DBNull.Value : data_Inicio);
                SqlParameter pDataTermino = new SqlParameter("@data_Termino", (data_Termino == null) ? (object)DBNull.Value : data_Termino);
                SqlParameter pCodCFO = new SqlParameter("@codCFO", (codCFO == null) ? "" : codCFO);
                SqlParameter pCodVen = new SqlParameter("@codVen", (codVen == null) ? "" : codVen);
                SqlParameter pCodTmv = new SqlParameter("@codTmv", (codTmv == null) ? "" : codTmv);
                SqlParameter pNumeroMov = new SqlParameter("@numeroMov", (numeroMov == null) ? "" : numeroMov);
                SqlParameter pIdPerfil = new SqlParameter("@id_Perfil", (idPerfil == 0) ? (object)DBNull.Value : idPerfil);
                SqlParameter pFormulario = new SqlParameter("@formulario", (formulario == null) ? (object)DBNull.Value : formulario);
                SqlParameter pSituacao = new SqlParameter("@situacao", (situacao == null) ? "" : situacao);
                SqlParameter pIdIntegracao = new SqlParameter("@id_integracao", (id_integracao == 0) ? 0 : id_integracao);
                SqlParameter pIdMov = new SqlParameter("@idMov", (idMov == 0) ? 0 : idMov);
                //db.Database.CommandTimeout = 120;


                try
                {
                    var linha = db.Database.SqlQuery<Compra>("STO_S_RM_PEDIDO_COMPRA @data_Inicio, @data_Termino, @codCFO, @codVen, @codTmv, @numeroMov, @id_Perfil,@formulario,@situacao,@id_integracao,@idMov", pDataInicio, pDataTermino, pCodCFO, pCodVen, pCodTmv, pNumeroMov, pIdPerfil, pFormulario, pSituacao, pIdIntegracao, pIdMov).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return new List<Compra>();
                    }
                }
                catch(Exception e)
                {
                    var erro = e.Message;
                    return new List<Compra>();
                }
                //var linha = db.Database.SqlQuery<Comprador>("exec STO_S_RM_PEDIDO_COMPRA NULL,  NULL,  NULL,  NULL,  NULL,  NULL,  NULL,  NULL,  NULL").ToList();

                //if (linha.Count > 0)
                //{
                //    return linha;
                //}
                //else
                //{
                //    return null;
                //}
            }
        }
    }
}
