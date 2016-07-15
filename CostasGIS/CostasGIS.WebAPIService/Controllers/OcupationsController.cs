﻿using CostasGIS.Model.Services.OcupationService;
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

        [HttpPut]
        [AllowAnonymous]
        [Route("trafficsigns/senalesverticales/{idOcupation:long}")]
        public long UpdateOcupation(long idOcupation, Ocupacion ocupacion)
        {
            return ocupationService.UpdateOcupation(idOcupation, ocupacion);
        }
    }
}
