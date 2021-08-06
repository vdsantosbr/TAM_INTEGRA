using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CompradorBUS
    {
        CompradorDAL dal = null;

        public CompradorBUS()
        {
            dal = new CompradorDAL();
        }

        public List<Compra> Lista()
        {
            List<Compra> lst = dal.Lista().ToList();
            return lst;
        }

        public List<Compra> PedidoCompraLista(DateTime? data_Inicio, DateTime? data_Termino, string codCFO, string codVen, string codTmv, string numeroMov, int idPerfil,
            string formulario, string situacao, int id_integracao, int idMov)
        {
            List<Compra> lst = dal.PedidoCompraLista(data_Inicio, data_Termino, codCFO, codVen, codTmv, numeroMov, idPerfil, formulario, situacao, id_integracao, idMov).ToList();
            return lst;
        }
    }
}
