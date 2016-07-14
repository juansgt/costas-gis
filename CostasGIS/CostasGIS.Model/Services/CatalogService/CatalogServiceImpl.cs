using System.Collections.Generic;
using CostasGIS.Model.DataAccess;
using ObjectContainer;
using CostasGIS.Model.DataAccess.ProvinciaDao;
using System;
using CostasGIS.Model.DataAccess.MunicipioDao;

namespace CostasGIS.Model.Services.CatalogService
{
    public class CatalogServiceImpl : ICatalogService
    {
        private Container container = Container.Instance;
        private IProvinciaDao provinciaDao;
        private IMunicipioDao municipioDao;

        public CatalogServiceImpl()
        {
            provinciaDao = container.Resolve<IProvinciaDao>();
            municipioDao = container.Resolve<IMunicipioDao>();
        }

        public IEnumerable<Provincia> FindProvincias()
        {
            return provinciaDao.FindAll();
        }

        public IEnumerable<Municipio> FindMunicipiosByProvincia(long idProvincia)
        {
            return municipioDao.FindMunicicpiosByProvincia(idProvincia);
        }
    }
}
