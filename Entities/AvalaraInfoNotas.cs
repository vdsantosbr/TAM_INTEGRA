using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Ide
    {
        public string cUF { get; set; }
        public string cNF { get; set; }
        public string natOp { get; set; }
        public string indPag { get; set; }
        public string mod { get; set; }
        public string serie { get; set; }
        public string nNF { get; set; }
        public string dhEmi { get; set; }
        public string dhSaiEnt { get; set; }
        public string tpNF { get; set; }
        public string idDest { get; set; }
        public string cMunFG { get; set; }
        public string tpImp { get; set; }
        public string tpEmis { get; set; }
        public string cDV { get; set; }
        public string tpAmb { get; set; }
        public string finNFe { get; set; }
        public string indFinal { get; set; }
        public string indPres { get; set; }
        public string procEmi { get; set; }
        public string verProc { get; set; }
    }

    public class EnderEmit
    {
        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xBairro { get; set; }
        public string cMun { get; set; }
        public string xMun { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string cPais { get; set; }
        public string xPais { get; set; }
        public string fone { get; set; }
    }

    public class Emit
    {
        public string CNPJ { get; set; }
        public string xNome { get; set; }
        public string xFant { get; set; }
        public EnderEmit enderEmit { get; set; }
        public string IE { get; set; }
        public string CRT { get; set; }

        public string cStat { get; set; }

        public string xMotivo { get; set; }
    }

    public class EnderDest
    {
        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xBairro { get; set; }
        public string cMun { get; set; }
        public string xMun { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string cPais { get; set; }
        public string xPais { get; set; }
        public string fone { get; set; }
    }

    public class Dest
    {
        public string CNPJ { get; set; }
        public string xNome { get; set; }
        public EnderDest enderDest { get; set; }
        public string indIEDest { get; set; }
        public string IE { get; set; }
        public string email { get; set; }
    }

    public class Prod
    {
        public string nItemPed { get; set; }
        public string cProd { get; set; }
        public object cEAN { get; set; }
        public string xProd { get; set; }
        public string NCM { get; set; }
        public string CFOP { get; set; }
        public string uCom { get; set; }
        public string qCom { get; set; }
        public string vUnCom { get; set; }
        public string vProd { get; set; }
        public object cEANTrib { get; set; }
        public string uTrib { get; set; }
        public string qTrib { get; set; }
        public string vUnTrib { get; set; }
        public string indTot { get; set; }

        public string CEST { get; set; }

        public Comb comb { get; set; }
    }

    public class ICMSSN500
    {
        public string orig { get; set; }
        public string CSOSN { get; set; }

    }

    public class ICMS
    {
        public ICMSSN500 ICMSSN500 { get; set; }
        public ICMS00 ICMS00 { get; set; }
    }

    public class PISAliq
    {
        public string CST { get; set; }
        public string vBC { get; set; }
        public string pPIS { get; set; }
        public string vPIS { get; set; }
    }

    public class PIS
    {
        public PISAliq PISAliq { get; set; }
        public PISOutr PISOutr { get; set; }       

        
    }

    public class COFINSAliq
    {
        public string CST { get; set; }
        public string vBC { get; set; }
        public string pCOFINS { get; set; }
        public string vCOFINS { get; set; }
    }

    public class COFINS
    {
        public COFINSAliq COFINSAliq { get; set; }
        public COFINSOutr COFINSOutr { get; set; }
    }

    public class Imposto
    {
        public ICMS ICMS { get; set; }
        public PIS PIS { get; set; }
        public COFINS COFINS { get; set; }        
        public IPI IPI { get; set; }
        
        
    }

    public class Det
    {
        [JsonProperty("@nItem")]
        public string NItem { get; set; }
        public Prod prod { get; set; }
        public Imposto imposto { get; set; }

       // public string infAdProd { get; set; }
    }

    public class ICMSTot
    {
        public string vBC { get; set; }
        public string vICMS { get; set; }
        public string vICMSDeson { get; set; }
        public string vFCPUFDest { get; set; }
        public string vICMSUFDest { get; set; }
        public string vICMSUFRemet { get; set; }
        public string vFCP { get; set; }
        public string vBCST { get; set; }
        public string vST { get; set; }
        public string vFCPST { get; set; }
        public string vFCPSTRet { get; set; }
        public string vProd { get; set; }
        public string vFrete { get; set; }
        public string vSeg { get; set; }
        public string vDesc { get; set; }
        public string vII { get; set; }
        public string vIPI { get; set; }
        public string vIPIDevol { get; set; }
        public string vPIS { get; set; }
        public string vCOFINS { get; set; }
        public string vOutro { get; set; }
        public string vNF { get; set; }
    }

    public class Total
    {
        public ICMSTot ICMSTot { get; set; }
    }

    public class Transporta
    {
        public string CNPJ { get; set; }
        public string xNome { get; set; }
    }

    public class Transp
    {
        public string modFrete { get; set; }
        public Transporta transporta { get; set; }
    }

    public class InfAdic
    {
        public string infCpl { get; set; }
    }

    public class InfNFe
    {
        [JsonProperty("@versao")]
        public string Versao { get; set; }

        [JsonProperty("@Id")]
        public string Id { get; set; }
        public Ide ide { get; set; }
        public Emit emit { get; set; }
        public Dest dest { get; set; }
        public Det det { get; set; }
        public Total total { get; set; }
        public Transp transp { get; set; }
       // public AutXML autXML { get; set; }

        public Entrega entrega { get; set; }
        public InfAdic infAdic { get; set; }
    }

    public class InfNFeLista
    {
        [JsonProperty("@versao")]
        public string Versao { get; set; }

        [JsonProperty("@Id")]
        public string Id { get; set; }
        public Ide ide { get; set; }
        public Emit emit { get; set; }
        public Dest dest { get; set; }
        public List<Det> det { get; set; }
        public Total total { get; set; }
        public Transp transp { get; set; }
      // public AutXMLLista autXML { get; set; }

        public Entrega entrega { get; set; }
        public InfAdic infAdic { get; set; }
    }

    public class CanonicalizationMethod
    {
        [JsonProperty("@Algorithm")]
        public string Algorithm { get; set; }
    }

    public class SignatureMethod
    {
        [JsonProperty("@Algorithm")]
        public string Algorithm { get; set; }
    }

    public class Transform
    {
        [JsonProperty("@Algorithm")]
        public string Algorithm { get; set; }
    }

    public class Transforms
    {
        public List<Transform> Transform { get; set; }
    }

    public class DigestMethod
    {
        [JsonProperty("@Algorithm")]
        public string Algorithm { get; set; }
    }

    public class Reference
    {
        [JsonProperty("@URI")]
        public string URI { get; set; }
        public Transforms Transforms { get; set; }
        public DigestMethod DigestMethod { get; set; }
        public string DigestValue { get; set; }
    }

    public class SignedInfo
    {
        public CanonicalizationMethod CanonicalizationMethod { get; set; }
        public SignatureMethod SignatureMethod { get; set; }
        public Reference Reference { get; set; }
    }

    public class X509Data
    {
        public string X509Certificate { get; set; }
    }

    public class KeyInfo
    {
        public X509Data X509Data { get; set; }
    }

    public class Signature
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public SignedInfo SignedInfo { get; set; }
        public SignatureValue SignatureValue { get; set; }
        public KeyInfo KeyInfo { get; set; }
    }

    public class SignatureValue
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class NFe
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public InfNFe infNFe { get; set; }

        //public Signature Signature { get; set; }
    }

    public class NFeLista
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public InfNFeLista infNFe { get; set; }
       // public Signature Signature { get; set; }
    }

    public class InfProt
    {
        public string tpAmb { get; set; }
        public string verAplic { get; set; }
        public string chNFe { get; set; }
        public DateTime dhRecbto { get; set; }
        public string nProt { get; set; }
        public string digVal { get; set; }
        public string cStat { get; set; }
        public string xMotivo { get; set; }
    }

    public class ProtNFe
    {
        [JsonProperty("@versao")]
        public string Versao { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public InfProt infProt { get; set; }
    }

    public class NfeProc
    {
        [JsonProperty("@versao")]
        public string Versao { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public NFe NFe { get; set; }
        public ProtNFe protNFe { get; set; }
    }

    public class NfeProcLista
    {
        [JsonProperty("@versao")]
        public string Versao { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public NFeLista NFe { get; set; }
        public ProtNFe protNFe { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    //public class NFref
    //{
    //    public string refNFe { get; set; }
    //}

    //public class Ide
    //{
    //    public string cUF { get; set; }
    //    public string cNF { get; set; }
    //    public string natOp { get; set; }
    //    public string mod { get; set; }
    //    public string serie { get; set; }
    //    public string nNF { get; set; }
    //    public DateTime dhEmi { get; set; }
    //    public DateTime dhSaiEnt { get; set; }
    //    public string tpNF { get; set; }
    //    public string idDest { get; set; }
    //    public string cMunFG { get; set; }
    //    public string tpImp { get; set; }
    //    public string tpEmis { get; set; }
    //    public string cDV { get; set; }
    //    public string tpAmb { get; set; }
    //    public string finNFe { get; set; }
    //    public string indFinal { get; set; }
    //    public string indPres { get; set; }
    //    public string procEmi { get; set; }
    //    public string verProc { get; set; }
    //    public NFref NFref { get; set; }
    //}

    //public class EnderEmit
    //{
    //    public string xLgr { get; set; }
    //    public string nro { get; set; }
    //    public string xCpl { get; set; }
    //    public string xBairro { get; set; }
    //    public string cMun { get; set; }
    //    public string xMun { get; set; }
    //    public string UF { get; set; }
    //    public string CEP { get; set; }
    //    public string cPais { get; set; }
    //    public string xPais { get; set; }
    //    public string fone { get; set; }
    //}

    public class Entrega
    {
        public string CNPJ { get; set; }

        public string xNome { get; set; }
        public string fone { get; set; }

        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xCpl { get; set; }
        public string xBairro { get; set; }
        public string cMun { get; set; }
        public string xMun { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string cPais { get; set; }
        public string xPais { get; set; }
        public string IE { get; set; }
    }

    //public class Emit
    //{
    //    public string CNPJ { get; set; }
    //    public string xNome { get; set; }
    //    public EnderEmit enderEmit { get; set; }
    //    public string IE { get; set; }
    //    public string CRT { get; set; }
    //}

    //public class EnderDest
    //{
    //    public string xLgr { get; set; }
    //    public string nro { get; set; }
    //    public string xBairro { get; set; }
    //    public string cMun { get; set; }
    //    public string xMun { get; set; }
    //    public string UF { get; set; }
    //    public string CEP { get; set; }
    //    public string cPais { get; set; }
    //    public string xPais { get; set; }
    //    public string fone { get; set; }
    //}

    //public class Dest
    //{
    //    public string CNPJ { get; set; }
    //    public string xNome { get; set; }
    //    public EnderDest enderDest { get; set; }
    //    public string indIEDest { get; set; }
    //    public string IE { get; set; }

    //    public string email { get; set; }
    //}

    public class AutXMLLista
    {
        public string CPF { get; set; }
    }
    public class AutXML
    {
        public string CPF { get; set; }
    }

    public class Comb
    {
        public string cProdANP { get; set; }
        public string descANP { get; set; }
        public string CODIF { get; set; }
        public string UFCons { get; set; }
    }

    //public class Prod
    //{
    //    public string cProd { get; set; }
    //    [JsonIgnore]
    //    [JsonProperty("cEAN", NullValueHandling = NullValueHandling.Ignore)]
    //    public object cEAN { get; set; }
    //    public string xProd { get; set; }
    //    public string NCM { get; set; }
    //    public string CFOP { get; set; }
    //    public string uCom { get; set; }
    //    public string qCom { get; set; }
    //    public string vUnCom { get; set; }
    //    public string vProd { get; set; }
    //    [JsonIgnore]
    //    [JsonProperty("cEANTrib", NullValueHandling = NullValueHandling.Ignore)]
    //    public object cEANTrib { get; set; }
    //    public string uTrib { get; set; }
    //    public string qTrib { get; set; }
    //    public string vUnTrib { get; set; }
    //    public string indTot { get; set; }

    //    public string CEST { get; set; }

    //    public string nItemPed { get; set; }
    //    public Comb comb { get; set; }
    //}

    public class ICMS00
    {
        public string orig { get; set; }
        public string CST { get; set; }
        public string modBC { get; set; }
        public string vBC { get; set; }
        public string pICMS { get; set; }
        public string vICMS { get; set; }
    }

    //public class ICMS

    //{
    //    public ICMS00 ICMS00 { get; set; }
    //}

    public class IPINT
    {
        public string CST { get; set; }
    }

    public class IPI
    {
        public string cEnq { get; set; }
        public IPINT IPINT { get; set; }
    }

    public class PISOutr
    {
        public string CST { get; set; }
        public string vBC { get; set; }
        public string pPIS { get; set; }
        public string vPIS { get; set; }
    }

    //public class PIS
    //{
    //    public PISOutr PISOutr { get; set; }
    //}

    public class COFINSOutr
    {
        public string CST { get; set; }
        public string vBC { get; set; }
        public string pCOFINS { get; set; }
        public string vCOFINS { get; set; }
    }

    //public class COFINS
    //{
    //    public COFINSOutr COFINSOutr { get; set; }
    //}

    //public class Imposto
    //{
    //    public ICMS ICMS { get; set; }
    //    public IPI IPI { get; set; }
    //    public PIS PIS { get; set; }
    //    public COFINS COFINS { get; set; }
    //}

    //public class Det
    //{
    //    [JsonProperty("@nItem")]
    //    public string NItem { get; set; }
    //    public Prod prod { get; set; }
    //    public Imposto imposto { get; set; }

    //    [JsonIgnore]
    //    public string infAdProd { get; set; }
    //}

    //public class ICMSTot
    //{
    //    public string vBC { get; set; }
    //    public string vICMS { get; set; }
    //    public string vICMSDeson { get; set; }
    //    public string vFCPUFDest { get; set; }
    //    public string vICMSUFDest { get; set; }
    //    public string vICMSUFRemet { get; set; }
    //    public string vFCP { get; set; }
    //    public string vBCST { get; set; }
    //    public string vST { get; set; }
    //    public string vFCPST { get; set; }
    //    public string vFCPSTRet { get; set; }
    //    public string vProd { get; set; }
    //    public string vFrete { get; set; }
    //    public string vSeg { get; set; }
    //    public string vDesc { get; set; }
    //    public string vII { get; set; }
    //    public string vIPI { get; set; }
    //    public string vIPIDevol { get; set; }
    //    public string vPIS { get; set; }
    //    public string vCOFINS { get; set; }
    //    public string vOutro { get; set; }
    //    public string vNF { get; set; }
    //}

    //public class Total
    //{
    //    public ICMSTot ICMSTot { get; set; }
    //}

    //public class Transporta
    //{
    //    public string CNPJ { get; set; }
    //    public string xNome { get; set; }
    //    public string IE { get; set; }
    //    public string xEnder { get; set; }
    //    public string xMun { get; set; }
    //    public string UF { get; set; }
    //}

    //public class Vol
    //{
    //    public string qVol { get; set; }
    //    public string pesoL { get; set; }
    //    public string pesoB { get; set; }
    //}

    //public class Transp
    //{
    //    public string modFrete { get; set; }
    //    public Transporta transporta { get; set; }
    //    public Vol vol { get; set; }
    //}

    //public class DetPag
    //{
    //    public string tPag { get; set; }
    //    public string vPag { get; set; }
    //}

    //public class Pag
    //{
    //    public DetPag detPag { get; set; }
    //}

    //public class ObsCont
    //{
    //    [JsonProperty("@xCampo")]
    //    public string XCampo { get; set; }
    //    public string xTexto { get; set; }
    //}

    //public class InfAdic
    //{
    //    public string infAdFisco { get; set; }
    //    public string infCpl { get; set; }
    //    public List<ObsCont> obsCont { get; set; }
    //}

    //public class InfNFe
    //{
    //    [JsonProperty("@Id")]
    //    public string Id { get; set; }

    //    [JsonProperty("@versao")]
    //    public string Versao { get; set; }
    //    public Ide ide { get; set; }
    //    public Emit emit { get; set; }

    //    public Entrega entrega { get; set; }
    //    public Dest dest { get; set; }
    //    public AutXML autXML { get; set; }

    //    public Det det { get; set; }
    //    public Total total { get; set; }
    //    public Transp transp { get; set; }
    //    public Pag pag { get; set; }

    //    public Cobr cobr { get; set; }
    // //   public InfAdic infAdic { get; set; }
    //}

    //public class CanonicalizationMethod
    //{
    //    [JsonProperty("@Algorithm")]
    //    public string Algorithm { get; set; }
    //}

    //public class SignatureMethod
    //{
    //    [JsonProperty("@Algorithm")]
    //    public string Algorithm { get; set; }
    //}

    //public class Transform
    //{
    //    [JsonProperty("@Algorithm")]
    //    public string Algorithm { get; set; }
    //}

    //public class Transforms
    //{
    //    public List<Transform> Transform { get; set; }
    //}

    //public class DigestMethod
    //{
    //    [JsonProperty("@Algorithm")]
    //    public string Algorithm { get; set; }
    //}

    //public class Reference
    //{
    //    [JsonProperty("@URI")]
    //    public string URI { get; set; }
    //    public Transforms Transforms { get; set; }
    //    public DigestMethod DigestMethod { get; set; }
    //    public string DigestValue { get; set; }
    //}

    //public class SignedInfo
    //{
    //    public CanonicalizationMethod CanonicalizationMethod { get; set; }
    //    public SignatureMethod SignatureMethod { get; set; }
    //    public Reference Reference { get; set; }
    //}

    //public class X509Data
    //{
    //    public string X509Certificate { get; set; }
    //}

    //public class KeyInfo
    //{
    //    public X509Data X509Data { get; set; }
    //}

    //public class Signature
    //{
    //    [JsonProperty("@xmlns")]
    //    public string Xmlns { get; set; }
    //    public SignedInfo SignedInfo { get; set; }
    //    public string SignatureValue { get; set; }
    //    public KeyInfo KeyInfo { get; set; }
    //}

    //public class NFe
    //{
    //    [JsonProperty("@xmlns")]
    //    public string Xmlns { get; set; }
    //    public InfNFe infNFe { get; set; }
    //    public Signature Signature { get; set; }
    //}

    //public class InfProt
    //{
    //    [JsonProperty("@Id")]
    //    public string Id { get; set; }
    //    public string tpAmb { get; set; }
    //    public string verAplic { get; set; }
    //    public string chNFe { get; set; }
    //    public DateTime dhRecbto { get; set; }
    //    public string nProt { get; set; }
    //    public string digVal { get; set; }
    //    public string cStat { get; set; }
    //    public string xMotivo { get; set; }
    //}

    //public class ProtNFe
    //{
    //    [JsonProperty("@versao")]
    //    public string Versao { get; set; }

    //    [JsonProperty("@xmlns")]
    //    public string Xmlns { get; set; }
    //    public InfProt infProt { get; set; }
    //}

    //public class NfeProc
    //{
    //    [JsonProperty("@versao")]
    //    public string Versao { get; set; }

    //    [JsonProperty("@xmlns")]
    //    public string Xmlns { get; set; }
    //    public NFe NFe { get; set; }
    //    public ProtNFe protNFe { get; set; }
    //}

    //public class AvalaraInfoNotas
    //{
    //    public NfeProc nfeProc { get; set; }
    //}

    public class Dup
    {
    public string nDup { get; set; }
        public string dVenc { get; set; }
        public string vDup { get; set; }
    }

    public class Cobr
    {
        public Dup dup { get; set; }
    }

    public class AvalaraInfoNotas
    {
        public NfeProc nfeProc { get; set; }
    }

    public class AvalaraInfoNotasLista
    {
        public NfeProcLista nfeProc { get; set; }
    }





}
