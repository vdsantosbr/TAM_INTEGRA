using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Data
{
    public class FinanceiroCambioSYSDAL
    {
        public List<FinanceiroCambioSYS> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@DATA_INICIO", (dataInicioDT == null) ? (object)DBNull.Value : dataInicioDT);
                SqlParameter pDataFim = new SqlParameter("@DATA_TERMINO", (dataTerminoDT == null) ? (object)DBNull.Value : dataTerminoDT);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroCambioSYS>("EXEC STO_S_TR_CI_OUT_CIRMFLUXUS_ARQUIVO @DATA_INICIO, @DATA_TERMINO", pDataInicio, pDataFim).ToList();

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
                    List<FinanceiroCambioSYS> lst = new List<FinanceiroCambioSYS>();
                    lst.Add(new FinanceiroCambioSYS
                    {
                        Mensagem = e.Message
                    });
                    return lst;

                }
            }
        }
        public List<FinanceiroCambioSYS> GuiaBaixaInsert(int id_pessoa)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroCambioSYS>("EXEC STO_I_TR_CI_OUT_CIRMFLUXUS_TEXTO @ID_PESSOA", pIdPessoa).ToList();

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
                    return new List<FinanceiroCambioSYS>();
                }
              
            }
        }
        public List<FinanceiroCambioSYS> GuiaBaixaSelect(int id_pessoa)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pDataInicio = new SqlParameter("@ID_PESSOA", (id_pessoa == 0) ? (object)DBNull.Value : id_pessoa);

                var linha = db.Database.SqlQuery<FinanceiroCambioSYS>("EXEC STO_I_TR_CI_OUT_CIRMFLUXUS_TEXTO @ID_PESSOA", id_pessoa).ToList();

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
        public List<FinanceiroCambioSYS> DownloadTxt(int id_integracao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);

                var linha = db.Database.SqlQuery<FinanceiroCambioSYS>("EXEC STO_S_TR_CI_OUT_CIRMFLUXUS_TEXTO @ID_INTEGRACAO", pIdIntegracao).ToList();

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
        public List<FinanceiroCambioSYS> DownloadExcel(int id_integracao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);

                var linha = db.Database.SqlQuery<FinanceiroCambioSYS>("EXEC STO_S_TR_CI_OUT_CIRMFLUXUS @ID_INTEGRACAO", pIdIntegracao).ToList();

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
        public List<FinanceiroCambioSYS> ExcelIS(int id_integracao)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                SqlParameter pIdIntegracao = new SqlParameter("@ID_INTEGRACAO", (id_integracao == 0) ? (object)DBNull.Value : id_integracao);

                try
                {
                    var linha = db.Database.SqlQuery<FinanceiroCambioSYS>("EXEC STO_S_TR_CI_OUT_CIRMFLUXUS_EXCELIS @ID_INTEGRACAO", pIdIntegracao).ToList();

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
                    return new List<FinanceiroCambioSYS>();
                }

            }
        }

    }
 }

