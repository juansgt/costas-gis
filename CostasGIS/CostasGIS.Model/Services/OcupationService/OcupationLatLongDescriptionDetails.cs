using CostasGIS.Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostasGIS.Model.Services.OcupationService
{
    public class OcupationLatLongDescriptionDetails
    {
        public long IdOcupacion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Titulo { get; set; }
        public string Situacion { get; set; }
        public string Descripcion { get; set; }

        public OcupationLatLongDescriptionDetails() { }

        public OcupationLatLongDescriptionDetails(Ocupacion ocupacion)
        {
            this.IdOcupacion = ocupacion.IdOcupacion;
            this.Descripcion = ocupacion.Descripcion;
            this.Titulo = ocupacion.Titulo;
            this.Situacion = ocupacion.Situacion;
        }
    }
}
