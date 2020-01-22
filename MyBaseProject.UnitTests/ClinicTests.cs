using AutoMapper;
using MyBaseProject.Core.Managers;
using MyBaseProject.Dal.Contexts;
using MyBaseProject.Dal.UnitOfWork;
using MyBaseProject.Domain.VMs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace MyBaseProject.UnitTests
{
  public class ClinicTests
  {
    ProjectDbContext context;
    IUnitOfWork unitOfWork;
    ClinicManager clinicManager;
    EquipmentManager equipmentManager;
    IMapper map;
    public ClinicTests()
    {
      var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
      context = new ProjectDbContext(options);
      unitOfWork = new UnitOfWork(context);
      var mapConfig = new MapperConfiguration(c => c.AddMaps("MyBaseProject.Core"));
      clinicManager = new ClinicManager(unitOfWork, mapConfig.CreateMapper());
      equipmentManager = new EquipmentManager(unitOfWork, mapConfig.CreateMapper());
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_Create_Clinic()
    {
      var result = await clinicManager.Edit(TestModel);
      Assert.False(result.Error);
      Assert.True(result.Data.ClinicID > 0);
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_Get_Clinic()
    {
      var _create = await clinicManager.Edit(TestModel);
      var result = await clinicManager.Detail(_create.Data.ClinicID);
      Assert.False(result.Error);
      Assert.True(result.Data.ClinicID > 0);
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_List_Clinics()
    {
      await clinicManager.Edit(TestModel);
      var result = await clinicManager.ListAllAsync();
      Assert.False(result.Error, "List Error");
      Assert.True(result.Data.Count() > 0, "List count not greater than 0");
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_Delete_Clinic()
    {
      var _create = await clinicManager.Edit(TestModel);
      var result = await clinicManager.Delete(_create.Data.ClinicID);
      Assert.False(result.Error);
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_Edit_Clinic()
    {
      var _create = await clinicManager.Edit(TestModel);
      _create.Data.ClinicName = "Test Clinic 2";
      var result = await clinicManager.Edit(_create.Data);
      Assert.False(result.Error);
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_Search_Clinic()
    {
      var _create = await clinicManager.Edit(TestModel);
      var result = await clinicManager.Search("Test");
      Assert.False(result.Error);
      Assert.True(result.Data.Count() > 0, "List count not greater than 0");
    }

    private readonly ClinicVM TestModel = new ClinicVM { ClinicName = "Test Clinic" };

  }
}
