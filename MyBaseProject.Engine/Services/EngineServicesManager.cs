using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBaseProject.Engine.Engines;
using MyBaseProject.Engine.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBaseProject.Engine.Services
{
  public static class EngineServicesManager
  {
    /// <summary>
    /// manages dependency injection for engine classes
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddEngineServices(this IServiceCollection services, IConfiguration configuration)
    {
      ConnectionSettings connSettings =
        new ConnectionSettings(new Uri(configuration.GetValue<string>("Elastic:Url")));
      ElasticClient elasticClient = new ElasticClient(connSettings.BasicAuthentication(configuration.GetValue<string>("Elastic:Username"), configuration.GetValue<string>("Elastic:Password")));
      services.AddSingleton<IElasticClient>(elasticClient);

      services.AddTransient<CacheEngine, CacheEngine>();
      services.AddSingleton<ILogEngine>(new ESLogEngine(elasticClient));

      services.AddTransient<EngineBaseManager, EngineBaseManager>();
      return services;
    }
  }
}
