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
                            ocupacion = htmlParsing.ParseOcupacion(placemark.Description.Text.Replace("<![CDATA[", "").Replace("]]>", ""));
                            point = placemark.Flatten().OfType<Point>().ElementAt(0);
                            ocupacion.Geometria = DbGeometry.PointFromText(ProjTransform.TransformToGeometry(point.Coordinate.Latitude, point.Coordinate.Longitude, 0, COORDINATE_SYSTEMID), COORDINATE_SYSTEMID);
                            ocupationDao.Create(ocupacion);
                        }
                        break;
                    }
                }
            }

            return names;
        }

        public IEnumerable<OcupacionLatLong> FindOcupacionLatLong()
        {
            OcupacionLatLong ocupacionLatLong;
            List<OcupacionLatLong> lOcupacionLatLong = new List<OcupacionLatLong>();
            double[] latLong;

            foreach (Ocupacion ocupacion in ocupationDao.FindAll())
            {
                if (ocupacion.Geometria.XCoordinate.HasValue && ocupacion.Geometria.YCoordinate.HasValue)
                {
                    ocupacionLatLong = new OcupacionLatLong(ocupacion);
                    latLong = ProjTransform.TransformToLatLong(ocupacion.Geometria.XCoordinate.Value, ocupacion.Geometria.YCoordinate.Value, 0, COORDINATE_SYSTEMID);
                    ocupacionLatLong.Longitud = latLong[0];
                    ocupacionLatLong.Latitud = latLong[1];
                    lOcupacionLatLong.Add(ocupacionLatLong);
                }
            }

            return lOcupacionLatLong;
        }

        public IEnumerable<OcupacionLatLong> FindOcupacionLatLong(long idProvincia)
        {
            OcupacionLatLong ocupacionLatLong;
            List<OcupacionLatLong> lOcupacionLatLong = new List<OcupacionLatLong>();
            double[] latLong;

            foreach (Ocupacion ocupacion in ocupationDao.FindAll())
            {
                if (ocupacion.Geometria.XCoordinate.HasValue && ocupacion.Geometria.YCoordinate.HasValue)
                {
                    ocupacionLatLong = new OcupacionLatLong(ocupacion);
                    latLong = ProjTransform.TransformToLatLong(ocupacion.Geometria.XCoordinate.Value, ocupacion.Geometria.YCoordinate.Value, 0, COORDINATE_SYSTEMID);
                    ocupacionLatLong.Longitud = latLong[0];
                    ocupacionLatLong.Latitud = latLong[1];
                    lOcupacionLatLong.Add(ocupacionLatLong);
                }
            }

            return lOcupacionLatLong;
        }
    }
}
