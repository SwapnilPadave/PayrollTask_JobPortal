using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobApplication.Api.ExFilter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode httpStatus = HttpStatusCode.InternalServerError;
            string errormessage = string.Empty;
            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                errormessage = " Unauthorized Acess";
                httpStatus = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                errormessage = "Data Not Found ";
                httpStatus = HttpStatusCode.NotFound;
            }            
            else
            {
                errormessage = "Internal Server Error ";
                httpStatus = HttpStatusCode.InternalServerError;
            }
            var response = new HttpResponseMessage(httpStatus)
            {
                Content = new StringContent(errormessage),

            };
            context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
