using Model.DataAccess.GenericDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostasGIS.Model.DataAccess.ProvinciaDao
{
    internal class ProvincaiDaoPocoEF : GenericDaoPocoEntityFramework<Provincia, long>, IProvinciaDao
    {
    }
}
