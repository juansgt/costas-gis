using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.CustomAttributes;

namespace CostasGIS.Model.DataAccess
{
    public partial class Ocupacion
    {
        public enum EstadoOcupacion
        {
            [StringValue("Sin Iniciar")]
            SIN_INICIAR,
            [StringValue("En trámite")]
            EN_TRAMITE,
            [StringValue("")]
            SIN_DATOS,
            [StringValue("Extinguido")]
            CADUCADA_DENEGADA,
            [StringValue("En vigor")]
            OTORGADA,
            [StringValue("Indeterminado")]
            INDETERMINADO
        }

        private EstadoOcupacion estado;

        public EstadoOcupacion Estado
        {
            get
            {
                if (string.IsNullOrEmpty(this.Situacion) && string.IsNullOrEmpty(this.Titulo))
                {
                    return EstadoOcupacion.SIN_DATOS;
                }
                else if (this.Titulo == "Extinguido")
                {
                    return EstadoOcupacion.CADUCADA_DENEGADA;
                }
                else if (this.Situacion == "Sin Iniciar")
                {
                    return EstadoOcupacion.SIN_INICIAR;
                }
                else if (this.Situacion == "En trámite")
                {
                    return EstadoOcupacion.EN_TRAMITE;
                }
                else if (this.Titulo == "En vigor")
                {
                    return EstadoOcupacion.OTORGADA;
                }
                return EstadoOcupacion.INDETERMINADO;
            }
        }
    }
}
