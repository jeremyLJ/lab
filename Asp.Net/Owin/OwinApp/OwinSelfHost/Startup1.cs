using Microsoft.Owin;
using Owin;
using OwinSelfHost;

[assembly: OwinStartup(typeof(Startup1))]

namespace OwinSelfHost
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello, World via self host");
            });
        }
    }
}
