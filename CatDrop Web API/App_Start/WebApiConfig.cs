using CatDrop.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CatDrop.WebApi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {

      //Unity config
      var _container = UnityConfig.Register(config);



      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "swagger",
          routeTemplate: "",
          defaults: null,
          constraints: null,
          handler: new Swashbuckle.Application.RedirectHandler((message => message.RequestUri.ToString()), "swagger")
);

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "{controller}/{action}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
    }
  }
}
