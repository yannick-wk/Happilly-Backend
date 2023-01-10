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
    public class ReminderControllerTests
    {
        public ReminderController ReminderController { get; set; }

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
            serviceCollection.AddAutoMapper(Assembly.GetAssembly(typeof(ReminderController)));

            //An instance of the database to use during a test from the DBFactory for in-memory usage.
            serviceCollection.AddSingleton<IFactory<HappillyDbContext>, HappillyDatabaseFactory>();
            serviceCollection.AddSingleton<HappillyDbContext>(p =>
            {
                HappillyDbContext happillyDbContext = p.GetRequiredService<IFactory<HappillyDbContext>>().Create();
                happillyDbContext.Database.EnsureCreated();
                return happillyDbContext;
            });

            //Repository/Service can be Transient because they are just Logic. (One time use)
            serviceCollection.AddTransient<IRepository<Reminder>, ReminderRepository>();
            serviceCollection.AddTransient<IService<ReminderDto>, ReminderService>();

            //Build the service provider.
            return serviceCollection.BuildServiceProvider();
        }

        [TestInitialize]
        public void Initialize()
        {
            ServiceProvider serviceProvider = InitializeServices();
            IService<ReminderDto> reminderService = serviceProvider.GetService<IService<ReminderDto>>();
            ReminderController = new ReminderController(reminderService);
        }

        [TestMethod]
        public async Task CreateReminderTest()
        {
            //Arrange
            ReminderDto reminderDto = new ReminderDto()
            {
                Id = Guid.NewGuid(),
                Name = "Reminder1",
                UserId = 1,
            };

            //Act
            ActionReturnObject returnObject = await ReminderController.CreateReminderAsync(reminderDto);

            //Assert
            Assert.IsTrue(returnObject.Success);
        }

        [TestMethod]
        public async Task GetAllRemindersShouldReturnEmptyTest()
        {
            //Arrange

            //Act
            IEnumerable<ReminderDto> returnObject = await ReminderController.GetAllAsync();
            //Assert
            Assert.IsFalse(returnObject.Any());
        }

        [TestMethod]
        public async Task GetAllRemindersShouldReturnReminder1Test()
        {
            //Arrange
            ReminderDto reminderDto = new ReminderDto()
            {
                Id = Guid.NewGuid(),
                Name = "Reminder1",
                UserId = 1,
            };
            await ReminderController.CreateReminderAsync(reminderDto);
            //Act
            IEnumerable<ReminderDto> returnObject = await ReminderController.GetAllAsync();
            //Assert
            ReminderDto reminder = returnObject.First();
            Assert.AreEqual(reminder.Name, reminderDto.Name);
        }

        [TestMethod]
        public async Task DeleteReminderShouldDeleteReminder1Test()
        {
            //Arrange
            ReminderDto reminderDto = new ReminderDto()
            {
                Id = Guid.NewGuid(),
                Name = "Reminder1",
                UserId = 1,
            };
            await ReminderController.CreateReminderAsync(reminderDto);
            //Act
            IActionResult returnObject = await ReminderController.DeleteReminderAsync(reminderDto.Id);
            //Assert
            Assert.IsTrue(returnObject is OkResult);
        }

        [TestMethod]
        public async Task UpdateReminderShouldUpdateReminder1ToReminder2Test()
        {
            //Arrange
            ReminderDto reminderDto = new ReminderDto()
            {
                Id = Guid.NewGuid(),
                Name = "Reminder1",
                UserId = 1,
            };
            await ReminderController.CreateReminderAsync(reminderDto);
            //Act
            reminderDto.Name = "Reminder2";
            IActionResult returnObject = await ReminderController.UpdateReminderAsync(reminderDto);
            reminderDto = await ReminderController.GetByIdAsync(reminderDto.Id);
            //Assert
            Assert.AreEqual(reminderDto.Name, "Reminder2");
        }
    }
}