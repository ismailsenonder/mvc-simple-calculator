using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebCalculator.Helpers
{
    public class XmlHelper
    {
        public DataSet ReturnXmlContent(string xmlPath)
        {
            string xmlData = HttpContext.Current.Server.MapPath(xmlPath);
            DataSet xmlDataSet = new DataSet();
            xmlDataSet.ReadXml(xmlData);
            return xmlDataSet;
        }
    }
}