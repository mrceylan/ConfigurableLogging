using MyBaseProject.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBaseProject.Web.Managers
{
  /// <summary>
  /// base manager 
  /// </summary>
  public class WebBaseManager
  {
    public readonly EngineBaseManager engineBaseManager;

    public WebBaseManager(EngineBaseManager engineBaseManager)
    {
      this.engineBaseManager = engineBaseManager;
    }
  }
}
