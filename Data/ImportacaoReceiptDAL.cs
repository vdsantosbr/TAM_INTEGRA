using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ImportacaoReceiptDAL
    {
        public List<ImportacaoReceipt> Filtro(int status = 0, DateTime dt_inicio = new DateTime(), DateTime dt_fim = new DateTime(), string transaction_type = "", string transaction_number = "",
            string bill_of_lading_number = "", string part_number = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pStatus = new SqlParameter("@STATUS", status);
                SqlParameter pDtInicio = new SqlParameter("@DATA_INICIO", dt_inicio);
                SqlParameter pDtFim = new SqlParameter("@DATA_TERMINO", dt_fim);
                SqlParameter pTransactionType = new SqlParameter("@TRANSACTION_TYPE", transaction_type);
                SqlParameter pTransactionNumber = new SqlParameter("@TRANSACTION_NUMBER", transaction_number);
                SqlParameter pBill = new SqlParameter("@BILL_OF_LADING_NUMBER", bill_of_lading_number);
                SqlParameter pPartNumber = new SqlParameter("@PART_NUMBER", part_number);

                var linha = db.Database.SqlQuery<ImportacaoReceipt>("EXEC STO_S_TEXTRON_TRANSACTION_Filtro @STATUS, @DATA_INICIO, @DATA_TERMINO, @TRANSACTION_TYPE, @TRANSACTION_NUMBER, @BILL_OF_LADING_NUMBER, @PART_NUMBER", 
                    pStatus, pDtInicio, pDtFim, pTransactionType, pTransactionNumber, pBill, pPartNumber).ToList();

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
        public List<ImportacaoReceipt> ConfirmarConferencia(int id_integracao = 0, int transaction_number = 0, string bill_of_lading_number = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                SqlParameter pTransactionNumber  = new SqlParameter("@TRANSACTION_NUMBER", transaction_number);
                SqlParameter pBill = new SqlParameter("@BILL_OF_LADING_NUMBER ", bill_of_lading_number);

                var linha = db.Database.SqlQuery<ImportacaoReceipt>("EXEC STO_U_TEXTRON_TRANSACTION_Confirmar @ID_INTEGRACAO, @TRANSACTION_NUMBER, @BILL_OF_LADING_NUMBER",
                    pIdIntegracao, pTransactionNumber, pBill).ToList();

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
        public List<ImportReceiptInformativo> Informativo(int id_integracao = 0, int transaction_number = 0, string bill_of_lading_number = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", id_integracao);
                SqlParameter pTransactionNumber = new SqlParameter("@TRANSACTION_NUMBER", transaction_number);
                SqlParameter pBill = new SqlParameter("@BILL_OF_LADING_NUMBER ", bill_of_lading_number);

                try
                {
                    var linha = db.Database.SqlQuery<ImportReceiptInformativo>("EXEC STO_S_TEXTRON_TRANSACTION_info @ID_INTEGRACAO, @TRANSACTION_NUMBER, @BILL_OF_LADING_NUMBER",
                    pIdIntegracao, pTransactionNumber, pBill).ToList();

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
                    List<ImportReceiptInformativo> lst = new List<ImportReceiptInformativo>();
                    foreach(var r in lst)
                    {
                        r.Observacao = e.Message;
                    }
                    return lst;
                }
            }
        }
    }
}
