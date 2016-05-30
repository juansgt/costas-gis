using System.Collections.Generic;
using CostasGIS.Model.DataAccess;
using ObjectContainer;
using CostasGIS.Model.DataAccess.ProvinciaDao;

namespace CostasGIS.Model.Services.CatalogService
{
    public class CatalogServiceImpl : ICatalogService
    {
        private Container container = Container.Instance;
        private IProvinciaDao provinciaDao;

        public CatalogServiceImpl()
        {
            provinciaDao = container.Resolve<IProvinciaDao>();
        }

        public IEnumerable<Provincia> FindProvincias()
        {
            return provinciaDao.FindAll();
        }
    }
}
