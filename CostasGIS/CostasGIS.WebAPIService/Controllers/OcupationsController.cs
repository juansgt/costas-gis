using CostasGIS.Model.Services.OcupationService;
using GeoJSON.Net.Feature;
using GeoJSON.Net;
using GeoJSON.Net.Converters;
using ObjectContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GeoJSON.Net.Geometry;
using CostasGIS.WebAPIService.Exceptions;
using CostasGIS.Model.DataAccess;

namespace CostasGIS.WebAPIService.Controllers
{
    [GenericExceptionFilterAttribute]
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
        [Route("ocupations/importkml")]
        public IEnumerable<string> ImportOcupaciones()
        {
            return ocupationService.ImportFromKml();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/")]
        public IEnumerable<OcupacionLatLong> FindOcupacionesLatLong()
        {
            return ocupationService.FindOcupacionesLatLong();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/{idOcupacion:long}")]
        public OcupacionLatLong FindOcupacionLatLong(long idOcupacion)
        {
            return ocupationService.FindOcupacionLatLong(idOcupacion);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/provincia/{idProvincia:long}")]
        public IEnumerable<OcupacionLatLong> FindOcupacionesLatLongByProvincia(long idProvincia)
        {
            return ocupationService.FindOcupacionesLatLongByProvincia(idProvincia);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/municipio/{idMunicipio:long}")]
        public IEnumerable<OcupacionLatLong> FindOcupacionesLatLongByMunicipio(long idMunicipio)
        {
            return ocupationService.FindOcupacionesLatLongByMunicipio(idMunicipio);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/descriptiondetails/provincia/{idProvincia:long}")]
        public IEnumerable<OcupationLatLongDescriptionDetails> FindOcupacionesLatLongDescDetByProvincia(long idProvincia)
        {
            return ocupationService.FindOcupacionesLatLongDescDetByProvincia(idProvincia);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/descriptiondetails/municipio/{idMunicipio:long}")]
        public IEnumerable<OcupationLatLongDescriptionDetails> FindOcupacionesLatLongDescDetByMunicipio(long idMunicipio)
        {
            return ocupationService.FindOcupacionesLatLongDescDetByMunicipio(idMunicipio);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("ocupations/{idOcupation:long}")]
        public long UpdateOcupation(long idOcupation, Ocupacion ocupacion)
        {
            return ocupationService.UpdateOcupation(idOcupation, ocupacion);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("ocupations/descriptiondetails/{idOcupation:long}")]
        public long UpdateOcupation(long idOcupation, OcupationLatLongDescriptionDetails ocupacion)
        {
            return ocupationService.UpdateOcupation(idOcupation, ocupacion);
        }
    }
}
