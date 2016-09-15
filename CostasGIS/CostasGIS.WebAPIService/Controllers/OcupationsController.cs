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
using Utils.CustomAttributes;

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

        [HttpGet]
        [AllowAnonymous]
        [Route("ocupations/descriptiondetails/municipio/{idMunicipio:long}/estado/{estado}")]
        public IEnumerable<OcupationLatLongDescriptionDetails> FindOcupacionesLatLongDescDetByMunicipioEstado(long idMunicipio, string estado)
        {
            Ocupacion.EstadoOcupacion estadoOcupacion = Ocupacion.EstadoOcupacion.INDETERMINADO;

            if (estado == StringEnum.GetStringValue(Ocupacion.EstadoOcupacion.SIN_DATOS))
            {
                estadoOcupacion = Ocupacion.EstadoOcupacion.SIN_DATOS;
            }
            else if (estado == StringEnum.GetStringValue(Ocupacion.EstadoOcupacion.CADUCADA_DENEGADA))
            {
                estadoOcupacion = Ocupacion.EstadoOcupacion.CADUCADA_DENEGADA;
            }
            else if (estado == StringEnum.GetStringValue(Ocupacion.EstadoOcupacion.SIN_INICIAR))
            {
                estadoOcupacion = Ocupacion.EstadoOcupacion.SIN_INICIAR;
            }
            else if (estado == StringEnum.GetStringValue(Ocupacion.EstadoOcupacion.EN_TRAMITE))
            {
                estadoOcupacion = Ocupacion.EstadoOcupacion.EN_TRAMITE;
            }
            else if (estado == StringEnum.GetStringValue(Ocupacion.EstadoOcupacion.OTORGADA))
            {
                estadoOcupacion = Ocupacion.EstadoOcupacion.OTORGADA;
            }
            else if (estado == StringEnum.GetStringValue(Ocupacion.EstadoOcupacion.INDETERMINADO))
            {
                estadoOcupacion = Ocupacion.EstadoOcupacion.INDETERMINADO;
            }
            return ocupationService.FindOcupacionesLatLongDescDetByMunicipioEstado(idMunicipio, estadoOcupacion);
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
            return ocupationService.UpdateOcupationLatLongDescriptionDetails(idOcupation, ocupacion);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("ocupationsLatLong/{idOcupation:long}")]
        public long UpdateOcupation(long idOcupation, OcupacionLatLong ocupacionLatLong)
        {
            return ocupationService.UpdateOcupationLatLong(idOcupation, ocupacionLatLong);
        }
    }
}
