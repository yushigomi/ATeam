﻿using Microsoft.Owin;
using Owin;
// ********** FYI: System Generated File

[assembly: OwinStartupAttribute(typeof(Sabio.Web.Startup))]
namespace Sabio.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
