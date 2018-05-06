﻿using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Swashbuckle.Application;

namespace PT.MarketDataService.Application.Owin
{
    public class OwinStartup
    {
        string baseAddress = "http://localhost:9000/";
        private readonly Container _container;

        public OwinStartup(Container container)
        {
            _container = container;
        }

        public void Initialize()
        {
            var server = WebApp.Start(baseAddress, (appBuilder) =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    using (AsyncScopedLifestyle.BeginScope(_container))
                    {
                        await next();
                    }
                });
                // Configure Web API for self-host. 
                HttpConfiguration config = new HttpConfiguration();

                // Attribute routing.
                config.MapHttpAttributeRoutes();

                // Swashbuckle
                config.EnableSwagger(c => c.SingleApiVersion("v1", "MarketDataService API"))
                      .EnableSwaggerUi();

                // SimpleInjector
                config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);

                appBuilder.UseWebApi(config);
            });
        }
    }
}