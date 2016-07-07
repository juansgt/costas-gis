using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockEnShock.Utils.Validations
{
    public static class StringValidation
    {
        public static bool AreAllNotNullOrEmpty(params string[] parametros)
        {
            foreach (string param in parametros)
            {
                if (string.IsNullOrEmpty(param))
                    return false;
            }

            return true;
        }

        public static bool AreSomeNotNullOrEmpty(params string[] parametros)
        {
            foreach (string param in parametros)
            {
                if (!string.IsNullOrEmpty(param))
                    return true;
            }

            return false;
        }

        public static bool AreAllNumbers(string cadena)
        {
            return Regex.IsMatch(cadena, "^[0-9]+$");
        }
    }
}
