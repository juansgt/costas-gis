using CostasGIS.Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostasGIS.Model.Services.OcupationService
{
    public class OcupacionLatLong : Ocupacion
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public OcupacionLatLong() { }

        public OcupacionLatLong(Ocupacion ocupacion)
        {
            this.CoordenadaXOriginal = ocupacion.CoordenadaXOriginal;
            this.CoordenadaYOriginal = ocupacion.CoordenadaYOriginal;
            this.Datum = ocupacion.Datum;
            this.Descripcion = ocupacion.Descripcion;
            this.DG = ocupacion.DG;
            this.DUNA = ocupacion.DUNA;
            this.ExpSancionador = ocupacion.ExpSancionador;
            this.FechaDenegacion = ocupacion.FechaDenegacion;
            this.FechaExtincion = ocupacion.FechaExtincion;
            this.FechaOtorgamiento = ocupacion.FechaOtorgamiento;
            this.Geometria = ocupacion.Geometria;
            this.Huso = ocupacion.Huso;
            this.IdOcupacion = ocupacion.IdOcupacion;
            this.IdProvincia = ocupacion.IdProvincia;
            this.Municipio = ocupacion.Municipio;
            this.Situacion = ocupacion.Situacion;
            this.SP = ocupacion.SP;
            this.Tipo = ocupacion.Tipo;
            this.Titulo = ocupacion.Titulo;
            this.Uso = ocupacion.Uso;
        }
    }
}
