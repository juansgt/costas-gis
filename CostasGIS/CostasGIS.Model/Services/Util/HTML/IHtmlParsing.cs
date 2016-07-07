using CostasGIS.Model.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostasGIS.Model.Services.Util.HTML
{
    internal interface IHtmlParsing
    {
        Ocupacion ParseOcupacion(string file);
        Municipio ParseMunicipio(string file);
    }
}
