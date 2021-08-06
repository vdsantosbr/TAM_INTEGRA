using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.Data;

namespace Data
{
    public class StatementImportacaoDAL
    {
        private int retorno;
        public bool Insere(StatementImportacao obj, int idUsuarioAutor)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId_Statement = new SqlParameter("@id_statement", obj.Id_Statement);
                SqlParameter pInvoice = new SqlParameter("@invoice", obj.Invoice);
                SqlParameter pDocType = new SqlParameter("@doc_type", obj.Doc_Type);
                SqlParameter pCurrency = new SqlParameter("@currency", (obj.Currency == null) ? (object)DBNull.Value : obj.Currency);
                SqlParameter pAmount = new SqlParameter("@amount", obj.Amount);
                SqlParameter pAircraft = new SqlParameter("@Aircraft", (obj.Aircraft == null) ? (object)DBNull.Value : obj.Aircraft);
                SqlParameter pPOnumber = new SqlParameter("@po_number", obj.Po_Number);
                SqlParameter pSO = new SqlParameter("@Sales_Order", obj.So_Ref);
                //SqlParameter pDt_Doc = new SqlParameter("@doc_date", obj.Doc_Date);

                SqlParameter pDt_Doc = new SqlParameter("@doc_date", obj.Doc_Date == null ? (object)DBNull.Value : obj.Doc_Date);
                pDt_Doc.IsNullable = true;
                pDt_Doc.Direction = ParameterDirection.Input;
                pDt_Doc.SqlDbType = SqlDbType.DateTime;

                SqlParameter pNet_Due_Date = new SqlParameter("@net_due_date", obj.NET_DUE_DT == null ? (object)DBNull.Value : obj.NET_DUE_DT);
                pNet_Due_Date.IsNullable = true;
                pNet_Due_Date.Direction = ParameterDirection.Input;
                pNet_Due_Date.SqlDbType = SqlDbType.DateTime;


                //SqlParameter pNet_Due_Date = new SqlParameter("@net_due_date", obj.NET_DUE_DT);
                //SqlParameter pPaid_Date = new SqlParameter { 
                //                                                ParameterName = "@Paid_Date",
                //                                                SqlDbType = SqlDbType.DateTime,
                //                                                Value = obj.Paid_Date == (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue ? (object)DBNull.Value : obj.Paid_Date
                //                                            };
                //SqlParameter pPaid_Date = new SqlParameter("@Paid_Date", DBNull.Value);
                SqlParameter pPaid_Date = new SqlParameter("@Paid_Date", DBNull.Value);
                pPaid_Date.IsNullable = true;
                pPaid_Date.Direction = ParameterDirection.Input;
                pPaid_Date.SqlDbType = SqlDbType.DateTime;

                try
                {
                    retorno = db.Database.ExecuteSqlCommand("STO_I_FIN_STATEMENT_DETAIL  @id_statement, @invoice, @doc_type, @currency, @amount, @Aircraft, @po_number,@Sales_Order, @doc_date, @net_due_date,@paid_date",
                                                         pId_Statement, pInvoice, pDocType, pCurrency, pAmount, pAircraft, pPOnumber, pSO, pDt_Doc, pNet_Due_Date, pPaid_Date);

                    if (retorno == 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception e)
                {
                    var erro = e.Message;
                    return false;
                }

                
            }
        }

        public StatementImportacao SalvarStatement(string id_conta, DateTime dataBase, int id_pessoa)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pId_conta = new SqlParameter("@id_conta", id_conta);
                SqlParameter pData_Base = new SqlParameter("@data_base", dataBase);
                SqlParameter pId_Pessoa = new SqlParameter("@id_pessoa", id_pessoa);

                var linha = db.Database.SqlQuery<StatementImportacao>("EXEC STO_I_FIN_STATEMENT @id_conta, @data_base, @id_pessoa", pId_conta, pData_Base, pId_Pessoa).ToList();
                if (linha.Count > 0)
                {
                    return linha.First();
                }
                else
                {
                    return null;
                }
            }
        }

        public bool ExcluirStatement(int id_statement, int id_pessoa)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdStatement = new SqlParameter("@id_statement", id_statement);
                SqlParameter pId_Pessoa = new SqlParameter("@id_pessoa", id_pessoa);
                var linha = db.Database.SqlQuery<StatementImportacao>("EXEC STO_D_FIN_STATEMENT @id_statement, @id_pessoa", pIdStatement, pId_Pessoa).ToList();

                if (retorno == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public bool ExcluirConciliacao(int id_conciliacao, int id_pessoa)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdConciliacao = new SqlParameter("@id_conciliacao", id_conciliacao);
                SqlParameter pId_Pessoa = new SqlParameter("@id_pessoa", id_pessoa);
                var linha = db.Database.SqlQuery<StatementImportacao>("EXEC STO_D_FIN_CONCILIACAO @id_conciliacao, @id_pessoa", pIdConciliacao, pId_Pessoa).ToList();

                if (retorno == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public bool ExcluirRMFluxus(int id_RM_Fluxus, int id_pessoa)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdRMFluxus = new SqlParameter("@id_conciliacao", id_RM_Fluxus);
                SqlParameter pId_Pessoa = new SqlParameter("@id_pessoa", id_pessoa);
                var linha = db.Database.SqlQuery<StatementImportacao>("EXEC STO_D_FIN_RM_FLUXUS @id_rm_fluxus, @id_pessoa", pIdRMFluxus, pId_Pessoa).ToList();

                if (retorno == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public List<StatementImportacao> Lista(int ano = 0, int mes = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pAno = new SqlParameter("@ano",  (ano > 0) ? ano : 0);
                SqlParameter pMes = new SqlParameter("@mes", (mes > 0) ? mes : 0);

                var linha = db.Database.SqlQuery<StatementImportacao>("EXEC STO_S_FIN_STATEMENT @ANO, @MES", pAno, pMes).ToList();
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

        public List<StatementImportacao> Download(int id_statement)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdStatement = new SqlParameter("@id_statement", id_statement);
                var linha = db.Database.SqlQuery<StatementImportacao>("EXEC STO_S_FIN_STATEMENT_DETAIL @id_statement", pIdStatement).ToList();

                try
                {
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
                    var erro = e.Message;
                    return null;
                }
            }
        }
    }
}

