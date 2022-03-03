using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TechTest.Controllers;
using TechTest.Data;
using TechTestTests.Builders;

namespace NunitTechTestTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckItWorks()
        {
            //Initialize empty in-memory db
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "techtestdb-test")
                .Options;
            var context = new DataContext(options);

            //Set up test data
            context.Robots.Add(new RobotBuilder().WithConditionExpertise("Bloaty Head").Build());
            context.SaveChanges();

            var controller = new RobotsController(context);

            //Call controller and get result
            var result = controller.GetAvailable("Bloaty Head");

            result.Should().BeOfType<OkObjectResult>();

            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}