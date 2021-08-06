using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public  class Recebimento_Tipo_Movimento_Avalara
    {
        public string CODTMV { get; set; }
        public string CODTMVFAT{ get; set; }
        public string NOME { get; set; }
    
    }

    public class Recebimento_Tipo_Movimento_Remessa_Avalara
    {
        
        public string NOME { get; set; }
        public string CODTMV { get; set; }
    }

    public class Recebimento_Class_Fin_Avalara
    {
        public string CODTB2FLX { get; set; }
        public string DESCRICAO { get; set; }
    }

    public class Recebimento_Forma_Pagto_Avalara
    {
        public string CODTB3FLX { get; set; }
        public string DESCRICAO { get; set; }
    }
}
