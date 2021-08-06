using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   

    public class RetornoNfe
    {       
        public List<string> ChaveNFe { get; set; }
        public int id { get; set; }
        public string ChaveNFeBanco { get; set; }
    }

    public class RetChavesNfe
    {
        [JsonProperty("@versao")]

       
        public string Versao { get; set; }
        public int cStat { get; set; }
        public string xMotivo { get; set; }
        public DateTime dhResp { get; set; }
        public int Quantidade { get; set; }
        public RetornoNfe retorno { get; set; }
        public int Id { get; set; }
    }

    public class Avalara_RetornaChavesNFePelaDataEntrada
    {
        public RetChavesNfe retChavesNfe { get; set; }
    }
}
