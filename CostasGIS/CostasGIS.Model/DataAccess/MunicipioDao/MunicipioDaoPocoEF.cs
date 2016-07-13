using Model.DataAccess.Exceptions;
using Model.DataAccess.GenericDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostasGIS.Model.DataAccess.MunicipioDao
{
    internal class MunicipioDaoPocoEF : GenericDaoPocoEntityFramework<CostasGISEntities, Municipio, long>, IMunicipioDao
    {
        public Municipio Find(string nombre)
        {
            using (CostasGISEntities context = (CostasGISEntities)this.getDbContext())
            {
                Municipio result = context.Municipio.Where(ent => ent.Nombre == nombre).FirstOrDefault();

                if (result == null)
                {
                    throw new InstanceNotFoundException(nombre, typeof(Municipio).FullName);
                }
                else
                {
                    return result;
                }
            }
        }

        public IEnumerable<Municipio> FindMunicicpiosByProvincia(long idProvincia)
        {
            using (CostasGISEntities context = (CostasGISEntities)this.getDbContext())
            {
                IEnumerable<Municipio> municipios = context.Municipio.Where(ent => ent.IdProvincia == idProvincia).ToList();
                return context.Municipio.Where(ent => ent.IdProvincia == idProvincia).ToList();
            }
        }
    }
}
