using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   
    public class Xml
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("@encoding")]
        public string Encoding { get; set; }
    }

    public class NFSe
    {
        public string NumeroNFe { get; set; }
        public DateTime DataEmissaoNFe { get; set; }
        public string CNPJ { get; set; }
    }

    public class RetornoTotalChaves
    {
        public List<NFSe> NFSe { get; set; }
    }

    public class RetChavesNfse
    {
        public string cStat { get; set; }
        public string xMotivo { get; set; }
        public DateTime dhResp { get; set; }
        public string Quantidade { get; set; }
        public RetornoTotalChaves retorno { get; set; }
    }

    public class RecebimentoAvalaraNfse
    {
        [JsonProperty("?xml")]
        public Xml Xml { get; set; }
        public RetChavesNfse retChavesNfse { get; set; }
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class ChaveNFe
{
    public string InscricaoPrestador { get; set; }
    public string NumeroNFe { get; set; }
    public string CodigoVerificacao { get; set; }
}

public class CPFCNPJPrestador
{
    public string CNPJ { get; set; }
}

public class ChaveRPS
{
    public string InscricaoPrestador { get; set; }
    public string NumeroRPS { get; set; }
}

public class EnderecoPrestador
{
    public string TipoLogradouro { get; set; }
    public string Logradouro { get; set; }
    public string NumeroEndereco { get; set; }
    public string ComplementoEndereco { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
}

public class CPFCNPJTomador
{
    public string CNPJ { get; set; }
}

public class EnderecoTomador
{
    public string TipoLogradouro { get; set; }
    public string Logradouro { get; set; }
    public string NumeroEndereco { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
}

public class NFe
{
    public ChaveNFe ChaveNFe { get; set; }
    public DateTime DataEmissaoNFe { get; set; }
    public CPFCNPJPrestador CPFCNPJPrestador { get; set; }
    public string RazaoSocialPrestador { get; set; }
    public EnderecoPrestador EnderecoPrestador { get; set; }
    public string StatusNFe { get; set; }
    public string TributacaoNFe { get; set; }
    public string OpcaoSimples { get; set; }
    public string ValorServicos { get; set; }
    public string CodigoServico { get; set; }
    public string AliquotaServicos { get; set; }
    public string NumeroLote { get; set; }
    public string ValorISS { get; set; }

    public string ValorCOFINS { get; set; }
    public string ValorINSS { get; set; }
    public string ValorCSLL { get; set; }
    public string ValorPIS { get; set; }
    public string ValorCredito { get; set; }
    public string ISSRetido { get; set; }
    public CPFCNPJTomador CPFCNPJTomador { get; set; }
    public string InscricaoMunicipalTomador { get; set; }
    public string RazaoSocialTomador { get; set; }
    
    public EnderecoTomador EnderecoTomador { get; set; }
    public string EmailTomador { get; set; }
    public string EmailPrestador { get; set; }
    public string Discriminacao { get; set; }
    public object FonteCargaTributaria { get; set; }

    public object TipoRPS { get; set; }
    public object DataEmissaoRPS { get; set; }
    public ChaveRPS ChaveRPS { get; set; }
    public string ValorIR { get; set; }
}

public class Conteudo
{
    public NFe NFe { get; set; }
}

public class NFSe
{
    [JsonProperty("@NumeroDocumento")]
    public string NumeroDocumento { get; set; }
    public string vIntegridade { get; set; }
    public string vSefaz { get; set; }
    public string vCadastral { get; set; }
    public Conteudo Conteudo { get; set; }
}

public class Retorno
{
    public NFSe NFSe { get; set; }
}

public class RetDownloadNFSes
{
    [JsonProperty("@versao")]
    public string Versao { get; set; }
    public string cStat { get; set; }
    public string xMotivo { get; set; }
    public DateTime dhResp { get; set; }
    public string Quantidade { get; set; }
    public Retorno retorno { get; set; }
   
}




public class RootRetDownloadNFSes
{
    public RetDownloadNFSes retDownloadNFSes { get; set; }
}


