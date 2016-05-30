using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.DataAccess.Exceptions
{
    public class InstanceNotFoundException : InstanceException
    {

        public InstanceNotFoundException(Object key, String className)
            : base("Instance not found", key, className) { }

    }
}
