using CostasGIS.Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostasGIS.Model.Services.OcupationService
{
    public interface IOcupationService
    {
        long AddOcupation(long idMunicipio, Ocupacion ocupacion);
        OcupacionLatLong FindOcupacionLatLong(long idOcupacion);
        IEnumerable<string> ImportFromKml();
        IEnumerable<OcupacionLatLong> FindOcupacionesLatLong();
        IEnumerable<OcupacionLatLong> FindOcupacionesLatLongByProvincia(long idProvincia);
        IEnumerable<OcupacionLatLong> FindOcupacionesLatLongByMunicipio(long idMunicipio);
        IEnumerable<OcupationLatLongDescriptionDetails> FindOcupacionesLatLongDescDetByProvincia(long idProvincia);
        IEnumerable<OcupationLatLongDescriptionDetails> FindOcupacionesLatLongDescDetByMunicipio(long idMunicipio);
        long UpdateOcupation(long idOcupation, Ocupacion ocupacion);
        long UpdateOcupation(long idOcupation, OcupationLatLongDescriptionDetails ocupacion);

    }
}
