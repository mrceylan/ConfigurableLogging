using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBaseProject.Web.Managers;
using Microsoft.AspNetCore.Mvc;

namespace MyBaseProject.Web.Controllers
{
  public class BaseController : Controller
  {
    public readonly WebBaseManager webBaseManager;

    public BaseController(WebBaseManager webBaseManager)
    {
      this.webBaseManager = webBaseManager;
    }
  }
}