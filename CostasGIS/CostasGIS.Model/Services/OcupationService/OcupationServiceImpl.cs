using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CostasGIS.Model.DataAccess;
using ObjectContainer;
using CostasGIS.Model.DataAccess.OcupacionDao;
using System.Transactions;
using ObjectContainer.Transactions;
using SharpKml.Engine;
using System.IO;
using SharpKml.Dom;
using System.Data.Entity.Spatial;
using GeometryExtensions;
using CostasGIS.Model.Services.Util.HTML;

namespace CostasGIS.Model.Services.OcupationService
{
    public class OcupationServiceImpl : IOcupationService
    {
        private readonly int COORDINATE_SYSTEMID = 25829;
        private ObjectContainer.Container container = ObjectContainer.Container.Instance;
        private IOcupacionDao ocupationDao;

        public OcupationServiceImpl()
        {
            ocupationDao = container.Resolve<IOcupacionDao>();
        }

        public long AddOcupation(long idProvincia, Ocupacion ocupacion)
        {
            using (TransactionScope ts = TransactionScopeBuilder.Instance.GetTransactionScope())
            {
                ocupacion.IdProvincia = idProvincia;
                ocupacion = ocupationDao.Create(ocupacion);
                ts.Complete();

                return ocupacion.IdOcupacion;
            }
        }

        public IEnumerable<string> ImportFromKml()
        {
            IHtmlParsing htmlParsing = new HtmlParsingImpl();
            Ocupacion ocupacion;
            Point point;
            List<string> names = new List<string>();
            // This will read a Kml file into memory.
            KmlFile file = KmlFile.Load(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/Documents/KMLImport/Ocupaciones D.P.kml", FileMode.Open));

            Kml kml = file.Root as Kml;
            if (kml != null)
            {
                foreach (Folder folder in kml.Flatten().OfType<Folder>())
                {
                    if (folder.Name == "GALICIA")
                    {
                        foreach (Placemark placemark in folder.Flatten().OfType<Placemark>())
                        {
                            ocupacion = new Ocupacion();
                            point = placemark.Flatten().OfType<Point>().ElementAt(0);
                            ocupacion.Geometria = DbGeometry.PointFromText(ProjTransform.TransformToGeometry(point.Coordinate.Latitude, point.Coordinate.Longitude, 0, COORDINATE_SYSTEMID), COORDINATE_SYSTEMID);
                            ocupacion.Descripcion = htmlParsing.ParseOcupacion(placemark.Description.Text.Replace("<![CDATA[", "").Replace("]]>", "")).Descripcion;

                            ocupationDao.Create(ocupacion);
                        }
                        break;
                    }
                }
            }

            return names;
        }
    }
}
