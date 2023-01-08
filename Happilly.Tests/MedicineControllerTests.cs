using Happilly.Application.Configurations;
using Happilly.Application.Dtos;
using Happilly.Application.Interfaces;
using Happilly.Application.Services;
using Happilly.Domain.Entities;
using Happilly.Persistence.Database;
using Happilly.Persistence.Repositories;
using Happilly.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Happilly.Tests
{
    [TestClass]
    public class MedicineControllerTests
    {
        public MedicineController MedicineController { get; set; }

        public ServiceProvider InitializeServices()
        {
            //Recreating the (Program.cs) to test the controller in memory with Entity Framework.
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddOptions();
            serviceCollection.Configure<DbConfiguration>(dbConfig =>
            {
                dbConfig.UseLazyLoading = true;
                dbConfig.ConnectionString = "";
                //Empty = In memory database
                dbConfig.DbProvider = "";
            });

            //Logging needed for the DBFactory in case something is wrong.
            serviceCollection.AddLogging(p => p.AddConsole());

            //Get assembly where the mapping profiles are stored (Presentation layer/Mapping/Profiles)
            serviceCollection.AddAutoMapper(Assembly.GetAssembly(typeof(MedicineController)));

            //An instance of the database to use during a test from the DBFactory for in-memory usage.
            serviceCollection.AddSingleton<IFactory<HappillyDbContext>, HappillyDatabaseFactory>();
            serviceCollection.AddSingleton<HappillyDbContext>(p =>
            {
                HappillyDbContext happillyDbContext = p.GetRequiredService<IFactory<HappillyDbContext>>().Create();
                happillyDbContext.Database.EnsureCreated();
                return happillyDbContext;
            });

            //Repository/Service can be Transient because they are just Logic. (One time use)
            serviceCollection.AddTransient<IRepository<Medicine>, MedicineRepository>();
            serviceCollection.AddTransient<IService<MedicineDto>, MedicineService>();

            //Build the service provider.
            return serviceCollection.BuildServiceProvider();
        }

        [TestInitialize]
        public void Initialize()
        {
            ServiceProvider serviceProvider = InitializeServices();
            IService<MedicineDto> medicineService = serviceProvider.GetService<IService<MedicineDto>>();
            MedicineController = new MedicineController(medicineService);
        }

        [TestMethod]
        public async Task CreateMedicineParacetamolTest()
        {
            //Arrange
            MedicineDto medicineDto = new MedicineDto()
            {
                Id = Guid.NewGuid(),
                Name = "Paracetamol",
                Description = "Pain killer",
                Group = 2,
                Stock = 12
            };

            //Act
            ActionReturnObject returnObject = await MedicineController.CreateMedicineAsync(medicineDto);

            //Assert
            Assert.IsTrue(returnObject.Success);
        }

        [TestMethod]
        public async Task GetAllMedicineShouldReturnEmptyTest()
        {
            //Arrange
            
            //Act
            IEnumerable<MedicineDto> returnObject = await MedicineController.GetAllAsync();
            //Assert
            Assert.IsFalse(returnObject.Any());
        }

        [TestMethod]
        public async Task GetAllMedicineShouldReturnParacetemolTest()
        {
            //Arrange
            MedicineDto medicineDto = new MedicineDto()
            {
                Id = Guid.NewGuid(),
                Name = "Paracetamol",
                Description = "Pain killer",
                Group = 2,
                Stock = 12
            };
            await MedicineController.CreateMedicineAsync(medicineDto);
            //Act
            IEnumerable<MedicineDto> returnObject = await MedicineController.GetAllAsync();
            //Assert
            MedicineDto medicine = returnObject.First();
            Assert.AreEqual(medicine.Name, medicineDto.Name);
        }

        [TestMethod]
        public async Task DeleteMedicineShouldDeleteParacetamolTest()
        {
            //Arrange
            MedicineDto medicineDto = new MedicineDto()
            {
                Id = Guid.NewGuid(),
                Name = "Paracetamol",
                Description = "Pain killer",
                Group = 2,
                Stock = 12
            };
            await MedicineController.CreateMedicineAsync(medicineDto);
            //Act
            IActionResult returnObject = await MedicineController.DeleteMedicineAsync(medicineDto.Id);
            //Assert
            Assert.IsTrue(returnObject is OkResult);
        }

        [TestMethod]
        public async Task UpdateMedicineShouldUpdateParacetamolToIbuprofenTest()
        {
            //Arrange
            MedicineDto medicineDto = new MedicineDto()
            {
                Id = Guid.NewGuid(),
                Name = "Paracetamol",
                Description = "Pain killer",
                Group = 2,
                Stock = 12
            };
            await MedicineController.CreateMedicineAsync(medicineDto);
            //Act
            medicineDto.Name = "Ibuprofen";
            IActionResult returnObject = await MedicineController.UpdateMedicineAsync(medicineDto);
            medicineDto = await MedicineController.GetByIdAsync(medicineDto.Id);
            //Assert
            Assert.AreEqual(medicineDto.Name, "Ibuprofen");
        }
    }
}