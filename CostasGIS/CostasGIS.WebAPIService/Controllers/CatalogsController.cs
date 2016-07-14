using CostasGIS.Model.DataAccess;
using CostasGIS.Model.Services.CatalogService;
using CostasGIS.WebAPIService.Exceptions;
using ObjectContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CostasGIS.WebAPIService.Controllers
{
    [GenericExceptionFilterAttribute]
    public class CatalogsController : ApiController
    {
        private Container container = Container.Instance;
        private ICatalogService catalogService;

        public CatalogsController()
        {
            catalogService = container.Resolve<ICatalogService>();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("catalogs/provincias/")]
        public IEnumerable<Provincia> FindProvincias()
        {
            return catalogService.FindProvincias();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("catalogs/municipios/provincia/{idProvincia:long}")]
        public IEnumerable<Municipio> FindMunicipiosByProvincia(long idProvincia)
        {
            return catalogService.FindMunicipiosByProvincia(idProvincia);
        }
    }
}
