using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SimpleODataApiWithEf.Models;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace SimpleODataApiWithEf
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.MessageHandlers.Add(new SimpleMessageHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            // New code:
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");
            builder.EntitySet<ProductCategory>("ProductCategories");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "api/odata",
                model: builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
