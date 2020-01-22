using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBaseProject.Web.Models
{
  /// <summary>
  /// datatables viewmodel for listing requests
  /// </summary>
  public class DatatableRequestModel
  {
    public int draw { get; set; }
    public int start { get; set; }
    public int length { get; set; }
    public string sortOrder
    {
      get
      {
        if (this.order != null && this.order.Count > 0)
        {
          return this.order.First().dir;
        }
        else
          return null;
      }
    }
    public string sortColumn
    {
      get
      {
        if (this.order != null && this.order.Count > 0)
        {
          return this.columns[this.order.First().column].name;
        }
        else
          return null;
      }
    }

    public List<Column> columns { get; set; }
    public Search search { get; set; }
    public List<Order> order { get; set; }
  }

  public class Column
  {
    public string data { get; set; }
    public string name { get; set; }
    public bool searchable { get; set; }
    public bool orderable { get; set; }
    public Search search { get; set; }
  }

  public class Search
  {
    public string value { get; set; }
    public string regex { get; set; }
  }

  public class Order
  {
    public int column { get; set; }
    public string dir { get; set; }
  }
}
