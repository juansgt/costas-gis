using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CostasGIS.Model.DataAccess;
using HtmlAgilityPack;
using System.IO;

namespace CostasGIS.Model.Services.Util.HTML
{
    internal class HtmlParsingImpl : IHtmlParsing
    {
        public Ocupacion ParseOcupacion(string file)
        {
            using (Stream stream = GenerateStreamFromString(file))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.Load(stream);

                foreach (HtmlNode td in doc.DocumentNode.SelectNodes("//td"))
                {
                    string t = td.InnerText;

                }

            }
            return null;
        }

        protected Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
