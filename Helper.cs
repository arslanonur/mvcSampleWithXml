using ProjSaki.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace ProjSaki
{
    public class Helper
    {
        public static string getXmlPath()
        {

            return "~/App_Data/XmlDb.xml";
        }

        public static List<UnhealtyFeatures> getDataModelFromXML(string tableName, string whereClause)
        {

            try
            {
                List<UnhealtyFeatures> uhfList = new List<UnhealtyFeatures>();
                string path = HostingEnvironment.MapPath(getXmlPath());
                if (System.IO.File.Exists(path))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(path);
                    DataRow[] dr = ds.Tables[tableName].Select(whereClause);

                    foreach (DataRow row in dr)
                    {
                        UnhealtyFeatures uhf = new UnhealtyFeatures();
                        DateTime newDateTime = new DateTime();
                        uhf.date = DateTime.ParseExact(row["date"].ToString(), "dd.mm.yyyy", CultureInfo.CurrentCulture);
                        uhf.name = row["name"].ToString();
                        uhf.company = row["company"].ToString();
                        uhf.content = row["content"].ToString();
                        uhf.serialNumber = row["serialNumber"].ToString();
                        uhfList.Add(uhf);
                    }

                }

                return uhfList;
            }
            catch (Exception ex)
            {

                return null;
            }




        }

        public void GenerateXML(UnhealtyFeatures model)
        {
            string path = HostingEnvironment.MapPath(getXmlPath());
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode root = doc.DocumentElement;            
            XmlElement elem = doc.CreateElement("food");
            //elem.InnerText = "19.95";
            elem.InnerXml = "<date>"+DateTime.Now.ToString("dd.mm.yyyy")+"</date>" +
            "<name>"+model.name+"</name>" +
            "<company>"+model.company+"</company>" +
            "<content>"+model.content+"</content>" +
            "<serialNumber>"+model.serialNumber+"</serialNumber>";
  
            root.InsertAfter(elem, root.FirstChild);
            doc.Save(path);
        }
    }
}