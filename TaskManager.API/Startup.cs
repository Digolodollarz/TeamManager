using AngularJSAuthentication.API.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Buffers;
//using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartup(typeof(AngularJSAuthentication.API.Startup))]

namespace AngularJSAuthentication.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);
            //Database.SetInitializer(new ProjectContextInitializer());
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            //InitDummies.Initialise();

        }

        public void ConfigureOAuth(IAppBuilder app)
        {

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions() {
            
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };
            
            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc(options =>
        //    {
        //        options.OutputFormatters.Clear();
        //        options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings()
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //        }, ArrayPool<char>.Shared));
        //    });
        //}


    }
}