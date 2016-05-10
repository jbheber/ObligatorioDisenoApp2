using System;
using System.Web.Http;

namespace Stockapp.Portal
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        public void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            Console.WriteLine(ex.Message);
        }
    }
}
