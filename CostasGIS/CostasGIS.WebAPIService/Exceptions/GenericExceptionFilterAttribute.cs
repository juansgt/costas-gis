using StockEnShock.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CostasGIS.WebAPIService.Exceptions
{
    public class GenericExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpError error = new HttpError(ExceptionManagement.GetCompleteExceptionErrorMessage(context.Exception, "juansgt@gmail.com", "Error aplicación", null));

            context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, error);
        }
    }
}