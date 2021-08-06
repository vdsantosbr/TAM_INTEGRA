using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace TAMIntegra.Helpers
{
    public class Utils
    {
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string ResultXML(string xml)
        {
            var docc = XDocument.Parse(xml);
            var emptyElements = from descendant in docc.Descendants() where descendant.IsEmpty || string.IsNullOrWhiteSpace(descendant.Value) select descendant; emptyElements.Remove();

            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string json = JsonConvert.SerializeXmlNode(doc);
            string json1 =  "";
            json1 = JsonConvert.SerializeXNode(docc);

            return json;

           // return  JsonConvert.SerializeObject(docc);


        }


    }
}