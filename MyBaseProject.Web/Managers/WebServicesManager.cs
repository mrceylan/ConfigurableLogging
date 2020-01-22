using MyBaseProject.Engine.Services;
using MyBaseProject.Dal.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MyBaseProject.Web.Managers
{
  public static class WebServicesManager
  {
    /// <summary>
    /// managing dependency injection
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddEngineServices(configuration);
      services.AddTransient<WebBaseManager, WebBaseManager>();
      return services;
    }

    /// <summary>
    /// updates migrations to database 
    /// </summary>
    /// <param name="app"></param>
    internal static void UpdateDatabase(this IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices
          .GetRequiredService<IServiceScopeFactory>()
          .CreateScope())
      {
        using (var context = serviceScope.ServiceProvider.GetService<ProjectDbContext>())
        {
          context.Database.Migrate();
        }
      }
    }
  }
}
