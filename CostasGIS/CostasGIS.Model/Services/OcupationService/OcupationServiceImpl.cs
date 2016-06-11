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

namespace CostasGIS.Model.Services.OcupationService
{
    public class OcupationServiceImpl : IOcupationService
    {
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
            List<string> names = new List<string>();
            // This will read a Kml file into memory.
            KmlFile file = KmlFile.Load(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/Documents/KMLImport/Ocupaciones D.P.kml", FileMode.Open));

            Kml kml = file.Root as Kml;
            if (kml != null)
            {
                foreach (var placemark in kml.Flatten().OfType<Placemark>())
                {
                    names.Add(placemark.Name);
                }
            }

            return names;
        }
    }
}
