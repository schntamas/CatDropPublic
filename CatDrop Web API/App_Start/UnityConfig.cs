using CatDrop.Interfaces;
using CatDrop.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CatDrop.WebApi
{
  public class UnityConfig
  {
    public static UnityContainer Register(HttpConfiguration config)
    {
      var container = new UnityContainer();

      container.RegisterType<IUserService, UserService>();
      container.RegisterType<ICatService, CatService>();
      container.RegisterType<ICatHttpClient, CatHttpClient>();
      container.RegisterType<ILogger, Logger>();

      config.DependencyResolver = new UnityResolver(container);

      var providers = config.Services.GetFilterProviders().ToList();
      var defaultProvider = providers.Where(x => x is ActionDescriptorFilterProvider).FirstOrDefault();
      config.Services.Remove(typeof(IFilterProvider), defaultProvider);
      config.Services.Add(typeof(IFilterProvider), new UnityFilterProvider(container));

      return container;
    }
  }
}