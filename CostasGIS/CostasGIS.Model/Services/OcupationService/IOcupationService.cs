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
        long AddOcupation(long idProvincia, Ocupacion ocupacion);
    }
}
