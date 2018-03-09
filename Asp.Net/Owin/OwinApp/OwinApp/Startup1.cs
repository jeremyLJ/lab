using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OwinApp.Startup1))]

namespace OwinApp
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.Run(context =>
            {
                if (context.Request.Path.Equals(new PathString("/fail")))
                {
                    throw new Exception("Dummy exception");
                }
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello, World");
            });
        }
    }
}
