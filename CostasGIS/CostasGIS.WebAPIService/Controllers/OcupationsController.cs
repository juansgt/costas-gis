using CostasGIS.Model.Services.OcupationService;
using ObjectContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CostasGIS.WebAPIService.Controllers
{
    public class OcupationsController : ApiController
    {
        private Container container = Container.Instance;
        private IOcupationService ocupationService;

        public OcupationsController()
        {
            ocupationService = container.Resolve<IOcupationService>();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/names/")]
        public IEnumerable<string> FindNames()
        {
            return ocupationService.ImportFromKml();
        }
    }
}
