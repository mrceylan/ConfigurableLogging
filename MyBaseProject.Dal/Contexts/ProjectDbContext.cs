using MyBaseProject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyBaseProject.Engine.Engines;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MyBaseProject.Domain.DTOs;
using MyBaseProject.Engine.Interfaces;
using MyBaseProject.Engine.Services;

namespace MyBaseProject.Dal.Contexts
{
  public class ProjectDbContext : DbContext
  {
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly EngineBaseManager engineBaseManager;

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IHttpContextAccessor httpContextAccessor, EngineBaseManager engineBaseManager) : base(options)
    {
      this.httpContextAccessor = httpContextAccessor;
      this.engineBaseManager = engineBaseManager;
    }

    public virtual DbSet<LogField> LogFields { get; set; }

    public override int SaveChanges()
    {
      try
      {
        LoggingProcess().Wait();
      }
      catch { }
      return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
      try
      {
        SaveLogs().Wait();
      }
      catch { }
      return (await base.SaveChangesAsync(true, cancellationToken));
    }






    private async Task<Dictionary<string, HashSet<string>>> GetLoggedFields()
    {
      var logConfs = engineBaseManager.cacheEngine.TryGet<Dictionary<string, HashSet<string>>>("LogFields", null);
      if (logConfs == null)
      {
        logConfs = await LogFields
          .Where(l => !l.IsDeleted)
          .GroupBy(l => l.TableName)
          .ToDictionaryAsync(g => g.First().TableName, g => g.Select(l => l.ColumnName).ToHashSet());
        engineBaseManager.cacheEngine.Add("LogFields", logConfs);
      }
      return logConfs;
    }

    private IEnumerable<LogObjectVm> CollectChanges(Dictionary<string, HashSet<string>> logs)
    {
      var timestamp = DateTime.UtcNow;
      string userId;
      string userName;

      try
      {
        userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        userName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
      }
      catch (Exception)
      {
        userId = null;
        userName = null;
      }

      var modifiedEntities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();

      foreach (var change in modifiedEntities)
      {
        var tableName = change.Metadata.Relational().TableName;
        if (!logs.ContainsKey(tableName))
          continue;
        var watchedColumns = logs[tableName];

        var rowId = change.OriginalValues[change.Metadata.FindPrimaryKey().Properties.First()] ?? 0;

        foreach (var item in change.Properties.Where(x => x.IsModified))
        {
          var columnName = item.Metadata.Relational().ColumnName;
          if (!watchedColumns.Contains(columnName))
            continue;

          var oldValue = item.OriginalValue == null ? "" : item.OriginalValue.ToString();
          var newValue = item.CurrentValue == null ? "" : item.CurrentValue.ToString();

          if (oldValue != newValue)
            yield return new LogObjectVm
            {
              UserId = userId,
              UserName = userName,
              TableName = tableName,
              RowId = rowId,
              ColumnName = columnName,
              OldValue = oldValue,
              NewValue = newValue,
              Timestamp = timestamp
            };
        }
      }

    }

    private async Task LoggingProcess()
    {
      var logConfs = await GetLoggedFields();
      if (logConfs.Count == 0)
        return;
      var logs = CollectChanges(logConfs).ToList();
      await engineBaseManager.logEngine.LogAll(logs);
    }


  }
}
