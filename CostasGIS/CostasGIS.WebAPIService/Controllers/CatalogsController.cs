using CostasGIS.Model.DataAccess;
using CostasGIS.Model.Services.CatalogService;
using ObjectContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CostasGIS.WebAPIService.Controllers
{
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
    }
}
