using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Web.Http;

namespace Sabio.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
            UnityContainer container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //  this line is needed so that the resolver can be used by api controllers 
            config.DependencyResolver = new Sabio.Web.Core.Injection.UnityResolver(container);

        }
    }
}