using AutoMapper;
using MyBaseProject.Dal.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBaseProject.Core.Services
{
  public static class CoreServicesManager
  {
    /// <summary>
    /// manages dependency injection for core classes
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
      services.AddTransient<CoreBaseManager, CoreBaseManager>();
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      return services;
    }
  }
}
