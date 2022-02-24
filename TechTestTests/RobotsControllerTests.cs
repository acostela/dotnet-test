using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTest.Controllers;
using TechTest.Data;
using TechTest.Domain;
using TechTestTests.Builders;

namespace TechTestTests;
[TestClass]
public class RobotsControllerTests
{
    [TestMethod]
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

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [TestMethod]
    public void AskingRobotsWithSpecificExpertise()
    {
        //Initialize empty in-memory db
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "techtestdb-test")
            .Options;
        var context = new DataContext(options);

        //Set up test data
        var robotBuilder = new RobotBuilder();
        Robot bloatyHeadRobot = robotBuilder.WithId(1).WithConditionExpertise("Bloaty Head").Build();
        context.Robots.Add(bloatyHeadRobot);
        context.Robots.Add(robotBuilder.WithId(2).WithConditionExpertise("Another").Build());
        context.SaveChanges();
            
        var controller = new RobotsController(context);

        //Call controller and get result
        var result = controller.GetAvailable("Bloaty Head");

        var expectedResult = new List<object>
        {
            new
            {
                Id = 1, 
                conditionExpertise = "Bloaty Head"
            }
        };

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        // Assert.AreEqual(expectedResult, ((OkObjectResult)result).Value);
        CollectionAssert.AreEqual(expectedResult, (List<object>)((OkObjectResult)result).Value);

        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
