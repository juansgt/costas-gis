using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Projections;
using System.Data.Entity.Spatial;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;
using GeoAPI.Geometries;

namespace GeometryExtensions
{
    public class ProjTransform
    {
        public static string TransformToLatLongWKT(double XCoordinate, double YCoordinate, double ZCoordinate, int coordinateSystemId)
        {
            WKTWriter wktWriter = new WKTWriter();
            WKTReader wktReader = new WKTReader();

            ProjectionInfo projectInfoUTM = ProjectionInfo.FromEpsgCode(coordinateSystemId);
            ProjectionInfo projectInfoGeograficas = ProjectionInfo.FromEpsgCode(4326);
            List<double> auxXY = new List<double>();
            List<double> auxZ = new List<double>();
            double[] xy;
            double[] z;

            auxXY.Add(XCoordinate);
            auxXY.Add(YCoordinate);
            auxZ.Add(ZCoordinate);

            xy = auxXY.ToArray<double>();
            z = auxZ.ToArray<double>();

            Reproject.ReprojectPoints(xy, z, projectInfoUTM, projectInfoGeograficas, 0, 1);

            return wktWriter.Write(new Point(new Coordinate(xy[0], xy[1])));
        }

        public static double[] TransformToLatLong(double XCoordinate, double YCoordinate, double ZCoordinate, int coordinateSystemId)
        {
            WKTWriter wktWriter = new WKTWriter();
            WKTReader wktReader = new WKTReader();

            ProjectionInfo projectInfoUTM = ProjectionInfo.FromEpsgCode(coordinateSystemId);
            ProjectionInfo projectInfoGeograficas = ProjectionInfo.FromEpsgCode(4326);
            List<double> auxXY = new List<double>();
            List<double> auxZ = new List<double>();
            double[] xy;
            double[] z;

            auxXY.Add(XCoordinate);
            auxXY.Add(YCoordinate);
            auxZ.Add(ZCoordinate);

            xy = auxXY.ToArray<double>();
            z = auxZ.ToArray<double>();

            Reproject.ReprojectPoints(xy, z, projectInfoUTM, projectInfoGeograficas, 0, 1);

            return xy;
        }

        public static string TransformToGeometry(double latitude, double longitude, double altitude, int coordinateSystemId)
        {
            WKTWriter wktWriter = new WKTWriter();
            WKTReader wktReader = new WKTReader();

            ProjectionInfo projectInfoUTM = ProjectionInfo.FromEpsgCode(coordinateSystemId);
            ProjectionInfo projectInfoGeograficas = ProjectionInfo.FromEpsgCode(4326);
            List<double> auxXY = new List<double>();
            List<double> auxZ = new List<double>();
            double[] xy;
            double[] z;

            auxXY.Add(longitude);
            auxXY.Add(latitude);
            auxZ.Add(altitude);

            xy = auxXY.ToArray<double>();
            z = auxZ.ToArray<double>();

            Reproject.ReprojectPoints(xy, z, projectInfoGeograficas, projectInfoUTM, 0, 1);

            return wktWriter.Write(new Point(new Coordinate(xy[0], xy[1])));
        }
    }
}
