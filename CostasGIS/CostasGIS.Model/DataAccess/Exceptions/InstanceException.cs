using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.DataAccess.Exceptions
{
    public class InstanceException : ModelException
    {
        protected InstanceException(String specificMessage, Object key,
            String className)
            : base(specificMessage + " (key = '" + key +
                                    "' - className = '" + className + "')")
        {

            this.Key = key;
            this.ClassName = className;

        }

        #region Properties

        public Object Key { get; private set; }

        public string ClassName { get; private set; }

        #endregion
    }
}
