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
            Ocupacion ocupacion = new Ocupacion();
            using (Stream stream = GenerateStreamFromString(file))
            {
                HtmlDocument doc = new HtmlDocument();
                doc.Load(stream);

                foreach (HtmlNode td in doc.DocumentNode.SelectNodes("//td"))
                {
                    string[] texto = td.InnerText.Split(':');

                    if (texto.Count() > 1)
                    {
                        byte[] bytes = Encoding.Default.GetBytes(texto[1]);
                        texto[1] = Encoding.UTF8.GetString(bytes);
                        texto[1] = texto[1].Trim();
                    }
                    switch (texto[0])
                    {
                        case "DUNA":
                            ocupacion.DUNA = texto[1];
                            break;
                        case "SP":
                            ocupacion.SP = texto[1];
                            break;
                        case "DG":
                            ocupacion.DG = texto[1];
                            break;
                        case "Provincia":
                            if (texto[1] == "Coruña, A")
                                ocupacion.IdProvincia = 32;
                            if (texto[1] == "Pontevedra")
                                ocupacion.IdProvincia = 45;
                            if (texto[1] == "Lugo")
                                ocupacion.IdProvincia = 25;
                            break;
                        case "TM":
                            ocupacion.Municipio = texto[1];
                            break;
                        case "X":
                            ocupacion.CoordenadaXOriginal = Convert.ToDouble(texto[1]);
                            break;
                        case "Y":
                            ocupacion.CoordenadaYOriginal = Convert.ToDouble(texto[1]);
                            break;
                        case "Huso":
                            ocupacion.Huso = texto[1];
                            break;
                        case "Datum":
                            ocupacion.Datum = texto[1];
                            break;
                        case "Uso":
                            ocupacion.Uso = texto[1];
                            break;
                        case "Tipo":
                            ocupacion.Tipo = texto[1];
                            break;
                        case "Titulo":
                            ocupacion.Titulo = texto[1];
                            break;
                        case "Situacion":
                            ocupacion.Situacion = texto[1];
                            break;
                        case "Exp. sancionador":
                            ocupacion.ExpSancionador = texto[1] == "Falso" ? false : true;
                            break;
                        case "Descripcion":
                            ocupacion.Descripcion = texto[1];
                            break;
                        default:
                            break;
                    }
                }

            }
            return ocupacion;
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
