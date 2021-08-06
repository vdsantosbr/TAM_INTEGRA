using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ImportReceiptInformativo
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public int Id_Integracao { get; set; }
        public string Status { get; set; }
        public DateTime? Data { get; set; }
        public string Transaction_Number { get; set; }
        public string Invoice_Number { get; set; }
        public string Invoice_Date { get; set; }
        public string Customer { get; set; }
        public string Means_of_Transport { get; set; }
        public string Country_of_Shipping { get; set; }
        public string Warehouse { get; set; }
        public string Bill_of_Lading_Number { get; set; }
        public string Total_Weight { get; set; }
        public string Total_Value { get; set; }
        public string CodLoc { get; set; }
        public string Serie { get; set; }
        public string CodTMV { get; set; }
        public string NumeroMov { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Currency_Code { get; set; }
        public string Order_ID { get; set; }
        public string Transaction_Line { get; set; }
        public string Part_Number { get; set; }
        public double? Quantity { get; set; }
        public double? Value_Per_Unit { get; set; }
        public string Unit_of_Meansurement { get; set; }
        public string Observacao { get; set; }
    }
}
