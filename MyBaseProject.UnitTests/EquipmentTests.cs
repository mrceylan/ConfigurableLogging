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
  public class EquipmentTests
  {
    ProjectDbContext context;
    IUnitOfWork unitOfWork;
    ClinicManager clinicManager;
    EquipmentManager equipmentManager;
    IMapper map;

    public EquipmentTests()
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
    public async System.Threading.Tasks.Task Can_Create_Equipment()
    {
      var _clinicResult = await clinicManager.Edit(TestClinicModel);
      TestEquipmentModel.ClinicID = _clinicResult.Data.ClinicID;
      var result = await equipmentManager.Edit(TestEquipmentModel);
      Assert.False(result.Error);
      Assert.True(result.Data.EquipmentID > 0);
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_Get_Equipment()
    {
      var _clinicResult = await clinicManager.Edit(TestClinicModel);
      TestEquipmentModel.ClinicID = _clinicResult.Data.ClinicID;
      var _create = await equipmentManager.Edit(TestEquipmentModel);
      var result = await equipmentManager.Detail(_create.Data.EquipmentID);
      Assert.False(result.Error);
      Assert.True(result.Data.EquipmentID > 0);
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_List_Equipments()
    {
      var _clinicResult = await clinicManager.Edit(TestClinicModel);
      TestEquipmentModel.ClinicID = _clinicResult.Data.ClinicID;
      var _create = await equipmentManager.Edit(TestEquipmentModel);
      var result = await equipmentManager.ListAllAsync();
      Assert.False(result.Error, "List Error");
      Assert.True(result.Data.Count() > 0, "List count not greater than 0");
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_Delete_Equipment()
    {
      var _clinicResult = await clinicManager.Edit(TestClinicModel);
      TestEquipmentModel.ClinicID = _clinicResult.Data.ClinicID;
      var _create = await equipmentManager.Edit(TestEquipmentModel);
      var result = await equipmentManager.Delete(_create.Data.EquipmentID);
      Assert.False(result.Error);
    }

    [Fact]
    public async System.Threading.Tasks.Task Can_Edit_Equipment()
    {
      var _clinicResult = await clinicManager.Edit(TestClinicModel);
      TestEquipmentModel.ClinicID = _clinicResult.Data.ClinicID;
      var _create = await equipmentManager.Edit(TestEquipmentModel);
      _create.Data.Name = "Test Equipment 2";
      var result = await equipmentManager.Edit(_create.Data);
      Assert.False(result.Error);
    }

    private readonly ClinicVM TestClinicModel = new ClinicVM { ClinicName = "Test Clinic" };
    private readonly EquipmentVM TestEquipmentModel = new EquipmentVM { Name = "Test Equipment", Price = 100, PurchaseDate = DateTime.UtcNow, Quantity = 2, UseRate = 90 };

  }
}
