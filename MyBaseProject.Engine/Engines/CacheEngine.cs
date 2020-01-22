using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBaseProject.Engine.Engines
{
  public class CacheEngine
  {
    private readonly IMemoryCache memoryCache;

    public CacheEngine(IMemoryCache memoryCache)
    {
      this.memoryCache = memoryCache;
    }

    /// <summary>
    /// Add or Update Cache
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="minutesToRemain"></param>
    public void Add(string key, object value, int minutesToRemain = 60)
    {
      if (this.memoryCache == null) return;
      object c = new object();
      if (!this.memoryCache.TryGetValue(key, out c))
      {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
          AbsoluteExpirationRelativeToNow = new TimeSpan(0, minutesToRemain, 0),
          SlidingExpiration = null,
          Priority = CacheItemPriority.High
        };

        this.memoryCache.Set(key, value, cacheEntryOptions);
      }
      else
      {
        this.memoryCache.Set(key, value);
      }
    }

    /// <summary>
    /// Remove Cache
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key)
    {
      if (this.memoryCache == null) return;
      this.memoryCache.Remove(key);
    }

    /// <summary>
    /// Get Cache, If Not Exists throw (ArgumentNullException)
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public dynamic Get(string key)
    {
      if (this.memoryCache == null) return "";

      var c = new object();
      if (!this.memoryCache.TryGetValue(key, out c))
        throw new ArgumentNullException("key: " + key, key + " could not be located in cache");

      return c;
    }

    /// <summary>
    /// Get Cache, If Not Exists Return Default Value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public T TryGet<T>(string key, T defaultValue)
    {
      if (this.memoryCache == null) return defaultValue;
      try
      {
        var c = new object();
        if (this.memoryCache.TryGetValue(key, out c))
        {
          return (T)c;
        }
        else
        {
          return (T)defaultValue;
        }
      }
      catch { return (T)defaultValue; }
    }
  }
}
