using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class FinanceiroCambioSYSBUS
    {
        FinanceiroCambioSYSDAL dal = new FinanceiroCambioSYSDAL();

        public List<FinanceiroCambioSYS> Filtro(DateTime dataInicioDT, DateTime dataTerminoDT)
        {
            List<FinanceiroCambioSYS> lst = dal.Filtro(dataInicioDT, dataTerminoDT);
            return lst;
        }
        public List<FinanceiroCambioSYS> GuiaBaixaInsert(int id_pessoa = 0)
        {
            List<FinanceiroCambioSYS> lst = dal.GuiaBaixaInsert(id_pessoa);
            return lst;
        }
        public List<FinanceiroCambioSYS> DownloadTxt(int id_integracao = 0)
        {
            List<FinanceiroCambioSYS> lst = dal.DownloadTxt(id_integracao);
            return lst;
        }
        public List<FinanceiroCambioSYS> DownloadExcel(int id_integracao = 0)
        {
            List<FinanceiroCambioSYS> lst = dal.DownloadExcel(id_integracao);
            return lst;
        }
        public List<FinanceiroCambioSYS> ExcelIS(int id_integracao = 0)
        {
            List<FinanceiroCambioSYS> lst = dal.ExcelIS(id_integracao);
            return lst;
        }

    }
}
