using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData.Extensions;
using System.Web.OData.Builder;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
         //ODATA
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<ProductViewModel>("Products");
            builder.EntitySet<ProductCategoryViewModel>("ProductCategories");
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            //Enable QUERY ODATA
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            //// Web API routes
            config.Routes.MapHttpRoute(
               name: "MapByAction",
               routeTemplate: "api/{controller}/{action}/{id}", defaults: new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            // Web API configuration and services
        }
    }
}