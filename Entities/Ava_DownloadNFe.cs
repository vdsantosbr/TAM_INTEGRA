using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Ava_DownloadNFe
    {
        [JsonProperty("@versao")]

       
        public string Versao { get; set; }
        public string cStat { get; set; }
        public string xMotivo { get; set; }
        public DateTime dhResp { get; set; }
        public Retorno retorno { get; set; }
        public int Id { get; set; }

    }

    public class Retorno
    {
        public string vIntegridade { get; set; }
        public string vSefaz { get; set; }
        public string vCadastral { get; set; }
        public string Conteudo { get; set; }
    }

    public class Root_Ava_DownloadNFe
    {
        public Ava_DownloadNFe retDownloadNFe { get; set; }
    }
}
