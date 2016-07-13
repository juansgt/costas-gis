using Model.DataAccess.GenericDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostasGIS.Model.DataAccess.MunicipioDao
{
    internal interface IMunicipioDao : IGenericDao<Municipio, long>
    {
        Municipio Find(string nombre);
        IEnumerable<Municipio> FindMunicicpiosByProvincia(long idProvincia);
    }
}
