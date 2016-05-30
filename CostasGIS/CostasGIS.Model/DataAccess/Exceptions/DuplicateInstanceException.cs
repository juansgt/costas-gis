using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.DataAccess.Exceptions
{
    public class DuplicateInstanceException : InstanceException
    {

        public DuplicateInstanceException(Object key, String className)
            : base("Duplicate instance", key, className) { }


        #region Test Code Region. Uncomment for testing.

        //public static void Main(String[] args)
        //{

        //    try
        //    {
        //        throw new DuplicateInstanceException("object", 
        //            "DuplicateInstanceException");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Message: " + e.Message);
        //        Console.WriteLine("Stack Trace: " + e.StackTrace);
        //        Console.ReadLine();
        //    }
        //}

        #endregion

    }
}
