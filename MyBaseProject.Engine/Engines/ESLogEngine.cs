using Elasticsearch.Net;
using MyBaseProject.Domain.DTOs;
using MyBaseProject.Engine.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBaseProject.Engine.Engines
{
  public class ESLogEngine : ILogEngine
  {
    private readonly IElasticClient client;

    public ESLogEngine(IElasticClient elasticClient)
    {
      this.client = elasticClient;
    }

    public async Task LogAll(IEnumerable<LogObjectVm> logs)
    {
      if (!logs.Any())
      {
        return;
      }

      var asyncBulkIndexResponse = await client.BulkAsync(b => b
        .Index("fieldLogs")
        .IndexMany(logs)
      );
    }
  }
}
