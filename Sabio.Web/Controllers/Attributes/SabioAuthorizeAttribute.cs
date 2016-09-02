using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;


namespace Sabio.Web.Controllers.Attributes
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SabioAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Authorize(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {            
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "access has been randomly denied for demonstration");

            return;
        }

        private bool Authorize(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                Random gen = new Random();

                return (gen.NextDouble() > 0.5); 
            }
            catch (Exception)
            {
                return false;
            }
        } 
    }
}     