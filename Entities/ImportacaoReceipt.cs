using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ImportacaoReceipt
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string strDataInicio { get; set; }
        public string strDataFim { get; set; }
        public int Id_Integracao { get; set; }
        public int Status { get; set; }
        public DateTime? Data { get; set; }
        public string Transaction_Number { get; set; }
        public string Invoice_Number { get; set; }
        public string Invoice_Date { get; set; }
        public string Customer { get; set; }
        public string Means_of_Transport { get; set; }
        public string Country_of_Shipping { get; set; }
        public string Warehouse { get; set; }
        public string Bill_of_Lading_Number { get; set; }
        public double? Total_Weight { get; set; }
        public double? Total_Value { get; set; }
        public string CodTmv { get; set; }
        public string NumeroMov { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string Observacao { get; set; }
        public List<ImportacaoReceipt> lstReceipts { get; set; }
    }
}
