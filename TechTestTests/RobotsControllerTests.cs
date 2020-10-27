using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTest.Controllers;
using TechTest.Data;
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
}
