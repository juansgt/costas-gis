using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess.Exceptions
{
    public class ParcelMismatchException : ModelException
    {
        public IEnumerable<long> IdsParcelsMisMatching { get; private set; }

        public ParcelMismatchException(string message, IEnumerable<long> idsParcelsMisMatching) : base(message)
        {
            this.IdsParcelsMisMatching = idsParcelsMisMatching;
        }
    }
}
