using MyBaseProject.Engine.Engines;
using MyBaseProject.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBaseProject.Engine.Services
{
  public class EngineBaseManager
  {
    public readonly ILogEngine logEngine;
    public readonly CacheEngine cacheEngine;

    public EngineBaseManager(ILogEngine logEngine, CacheEngine cacheEngine)
    {
      this.logEngine = logEngine;
      this.cacheEngine = cacheEngine;
    }
  }
}
