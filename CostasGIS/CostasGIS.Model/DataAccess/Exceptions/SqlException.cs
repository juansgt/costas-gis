using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Model.DataAccess.Exceptions
{
    public class SqlException : DbException
    {

        public SqlException(string msg)
            : base(msg) { }

    }
}
