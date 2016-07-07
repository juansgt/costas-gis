using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Mail;

namespace StockEnShock.Utils.Exceptions
{
    public static class ExceptionManagement
    {
        /// <summary>
        /// Obtiene los mensajes de error de una excepción incluyendo todas las InnerException.
        /// </summary>
        /// <param name="exception">La excepción</param>
        /// <returns>String con la composición de los mesajes de error excepción, null si la excepción recibida es null o no tiene mensajes</returns>
        public static string GetCompleteExceptionErrorMessage(Exception errorException)
        {
            string mensaje = null;

            while (errorException != null)
            {
                mensaje = mensaje + errorException.Message + " ***** StackTrace ***** " + errorException.StackTrace + " ****** ";
                errorException = errorException.InnerException;
            }

            return string.IsNullOrEmpty(mensaje) ? null : mensaje;
        }

        /// <summary>
        /// Obtiene los mensajes de error de una excepción incluyendo todas las InnerException. Ademas envia los mensajes de la excepción por correo electronico.
        /// </summary>
        /// <param name="exception">La excepción</param>
        /// <param name="eMailAddress"></param>
        /// <param name="eMailsubject"></param>
        /// <param name="eMailBody"></param>
        /// <returns></returns>
        public static string GetCompleteExceptionErrorMessage(Exception exception, string eMailAddress, string eMailsubject, string eMailBody)
        {
            string mensaje = GetCompleteExceptionErrorMessage(exception);
            MailManagement.SendMailToUser(eMailAddress, eMailsubject, eMailBody + "<p></p>" + "Application_Error" + mensaje);

            return mensaje;
        }
    }
}
