using Model.DataAccess.GenericDao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostasGIS.Model.DataAccess.OcupacionDao
{
    internal class OcupacionDaoPocoEF : GenericDaoPocoEntityFramework<CostasGISEntities, Ocupacion, long>, IOcupacionDao
    {
        public IEnumerable<Ocupacion> FindOcupacionLatLong(long idProvincia)
        {
            using (CostasGISEntities context = (CostasGISEntities)this.getDbContext())
            {
                return context.Ocupacion.Where(ent => ent.IdProvincia == idProvincia).ToList();
            }
        }
    }
}
