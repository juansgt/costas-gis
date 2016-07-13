﻿using System;
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
using Model.DataAccess.Exceptions;
using CostasGIS.Model.DataAccess.MunicipioDao;
using System.Collections.Concurrent;

namespace CostasGIS.Model.Services.OcupationService
{
    public class OcupationServiceImpl : IOcupationService
    {
        private readonly int COORDINATE_SYSTEMID = 25829;
        private ObjectContainer.Container container = ObjectContainer.Container.Instance;
        private IOcupacionDao ocupationDao;
        private IMunicipioDao municipioDao;

        public OcupationServiceImpl()
        {
            ocupationDao = container.Resolve<IOcupacionDao>();
            municipioDao = container.Resolve<IMunicipioDao>();
        }

        public long AddOcupation(long idMunicipio, Ocupacion ocupacion)
        {
            using (TransactionScope ts = TransactionScopeBuilder.Instance.GetTransactionScope())
            {
                ocupacion.IdMunicipio = idMunicipio;
                ocupacion = ocupationDao.Create(ocupacion);
                ts.Complete();

                return ocupacion.IdOcupacion;
            }
        }

        public IEnumerable<string> ImportFromKml()
        {
            IHtmlParsing htmlParsing = new HtmlParsingImpl();
            Ocupacion ocupacion;
            Municipio municipio;
            Point point;
            List<string> names = new List<string>();
            // This will read a Kml file into memory.
            KmlFile file = KmlFile.Load(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/Documents/KMLImport/Ocupaciones.kml", FileMode.Open));
            
            Kml kml = file.Root as Kml;
            if (kml != null)
            {
                foreach (Folder folder in kml.Flatten().OfType<Folder>())
                {
                    if (folder.Name == "GALICIA")
                    {
                        foreach (Placemark placemark in folder.Flatten().OfType<Placemark>())
                        {
                            using (TransactionScope ts = TransactionScopeBuilder.Instance.GetTransactionScope())
                            {
                                municipio = htmlParsing.ParseMunicipio(placemark.Description.Text.Replace("<![CDATA[", "").Replace("]]>", ""));
                                try
                                {
                                    municipio = municipioDao.Find(municipio.Nombre);
                                }
                                catch(InstanceNotFoundException)
                                {
                                    municipio = municipioDao.Create(municipio);
                                }
                                ocupacion = htmlParsing.ParseOcupacion(placemark.Description.Text.Replace("<![CDATA[", "").Replace("]]>", ""));
                                point = placemark.Flatten().OfType<Point>().ElementAt(0);
                                ocupacion.Geometria = DbGeometry.PointFromText(ProjTransform.TransformToGeometry(point.Coordinate.Latitude, point.Coordinate.Longitude, 0, COORDINATE_SYSTEMID), COORDINATE_SYSTEMID);
                                ocupacion.IdMunicipio = municipio.IdMunicipio;
                                ocupationDao.Create(ocupacion);
                                ts.Complete();
                            }
                        }
                        break;
                    }
                }
            }
            return names;
        }

        public OcupacionLatLong FindOcupacionLatLong(long idOcupacion)
        {

            OcupacionLatLong ocupacionLatLong = new OcupacionLatLong();
            Ocupacion ocupacion;
            double[] latLong;
            try
            {
                ocupacion = ocupationDao.Find(idOcupacion);
                if (ocupacion.Geometria.XCoordinate.HasValue && ocupacion.Geometria.YCoordinate.HasValue)
                {
                    latLong = ProjTransform.TransformToLatLong(ocupacion.Geometria.XCoordinate.Value, ocupacion.Geometria.YCoordinate.Value, 0, COORDINATE_SYSTEMID);
                    ocupacionLatLong.Longitud = latLong[0];
                    ocupacionLatLong.Latitud = latLong[1];
                }
            }
            catch (InstanceNotFoundException)
            {
                throw;
            }

            return ocupacionLatLong;
        }

        public IEnumerable<OcupacionLatLong> FindOcupacionesLatLong()
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

        public IEnumerable<OcupacionLatLong> FindOcupacionesLatLongByProvincia(long idProvincia)
        {
            Object thisLock = new Object();
            List<OcupacionLatLong> lOcupaciones = new List<OcupacionLatLong>();
            IEnumerable<OcupacionLatLong> aux;
            
            Parallel.ForEach(municipioDao.FindMunicicpiosByProvincia(idProvincia), municipio =>
            {
                aux = this.FindOcupacionesLatLongByMunicipio(municipio.IdMunicipio);
                lock (thisLock)
                {
                    lOcupaciones.AddRange(aux);
                }
            });

            return lOcupaciones;
        }

        public IEnumerable<OcupacionLatLong> FindOcupacionesLatLongByMunicipio(long idMunicipio)
        {
            OcupacionLatLong ocupacionLatLong;
            ConcurrentQueue<OcupacionLatLong> concurrentQueue = new ConcurrentQueue<OcupacionLatLong>();
            double[] latLong;

            Parallel.ForEach(ocupationDao.FindOcupacionLatLong(idMunicipio), ocupacion =>
            {
                if (ocupacion.Geometria.XCoordinate.HasValue && ocupacion.Geometria.YCoordinate.HasValue)
                {
                    ocupacionLatLong = new OcupacionLatLong(ocupacion);
                    latLong = ProjTransform.TransformToLatLong(ocupacion.Geometria.XCoordinate.Value, ocupacion.Geometria.YCoordinate.Value, 0, COORDINATE_SYSTEMID);
                    ocupacionLatLong.Longitud = latLong[0];
                    ocupacionLatLong.Latitud = latLong[1];
                    concurrentQueue.Enqueue(ocupacionLatLong);
                }
            });

            return concurrentQueue;
        }
    }
}
