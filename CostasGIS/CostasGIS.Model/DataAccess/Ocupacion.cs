//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CostasGIS.Model.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ocupacion
    {
        public long IdOcupacion { get; set; }
        public string DUNA { get; set; }
        public string SP { get; set; }
        public string DG { get; set; }
        public Nullable<long> IdProvincia { get; set; }
        public System.Data.Entity.Spatial.DbGeometry Geometria { get; set; }
        public string Huso { get; set; }
        public string Datum { get; set; }
        public string Uso { get; set; }
        public string Tipo { get; set; }
        public string Titulo { get; set; }
        public string Situación { get; set; }
        public Nullable<System.DateTime> FechaOtorgamiento { get; set; }
        public Nullable<System.DateTime> FechaExtincion { get; set; }
        public Nullable<System.DateTime> FechaDenegacion { get; set; }
        public Nullable<bool> ExpSancionador { get; set; }
        public string Descripcion { get; set; }
        public string Municipio { get; set; }
    
        public virtual Provincia Provincia { get; set; }
    }
}
