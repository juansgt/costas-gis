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

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("ocupations/")]
        //public IEnumerable<string> ImportOcupaciones()
        //{
        //    return ocupationService.ImportFromKml();
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/")]
        public IEnumerable<OcupacionLatLong> FindOcupacionesLatLong()
        {
            return ocupationService.FindOcupacionLatLong();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/provincia/{idProvincia:long}")]
        public IEnumerable<OcupacionLatLong> FindOcupacionesLatLong(long idProvincia)
        {
            return ocupationService.FindOcupacionLatLong(idProvincia);
        }
    }
}
