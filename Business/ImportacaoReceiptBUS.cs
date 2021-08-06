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
    public class ImportacaoReceiptBUS
    {
        ImportacaoReceiptDAL dal = new ImportacaoReceiptDAL();

        public List<ImportacaoReceipt> Filtro(int status = 0, DateTime dt_inicio = new DateTime(), DateTime dt_fim = new DateTime(), string transaction_type = "", string transaction_number = "",
            string bill_of_lading_number = "", string part_number = "")
        {
            List<ImportacaoReceipt> lst = new List<ImportacaoReceipt>();
            lst = dal.Filtro(status, dt_inicio, dt_fim, transaction_type, transaction_number, bill_of_lading_number, part_number);

            return lst;
        }
        public List<ImportacaoReceipt> ConfirmarConferencia(int id_integracao = 0, int transaction_number = 0, string bill_of_lading_number = "")
        {
            List<ImportacaoReceipt> lst = new List<ImportacaoReceipt>();
            lst = dal.ConfirmarConferencia(id_integracao, transaction_number, bill_of_lading_number);

            return lst;
        }
        public List<ImportReceiptInformativo> Informativo(int id_integracao = 0, int transaction_number = 0, string bill_of_lading_number = "")
        {
            List<ImportReceiptInformativo> lst = new List<ImportReceiptInformativo>();
            lst = dal.Informativo(id_integracao, transaction_number, bill_of_lading_number);

            return lst;
        }
    }
}
