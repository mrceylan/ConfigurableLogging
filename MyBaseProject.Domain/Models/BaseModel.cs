using System;
using System.Collections.Generic;
using System.Text;

namespace MyBaseProject.Domain.Models
{
  public abstract class BaseModel
  {
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
  }
}
