using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CostasGIS.Model.DataAccess;
using ObjectContainer;
using CostasGIS.Model.DataAccess.OcupacionDao;
using System.Transactions;
using ObjectContainer.Transactions;

namespace CostasGIS.Model.Services.OcupationService
{
    public class OcupationServiceImpl : IOcupationService
    {
        private Container container = Container.Instance;
        private IOcupacionDao ocupationDao;

        public OcupationServiceImpl()
        {
            ocupationDao = container.Resolve<IOcupacionDao>();
        }

        public long AddOcupation(long idProvincia, Ocupacion ocupacion)
        {
            using (TransactionScope ts = TransactionScopeBuilder.Instance.GetTransactionScope())
            {
                ocupacion.IdProvincia = idProvincia;
                ocupacion = ocupationDao.Create(ocupacion);
                ts.Complete();

                return ocupacion.IdOcupacion;
            }
        }
    }
}
