using FakeItEasy;
using FluentAssertions;
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
    private IDataContext _dataContext;
    private RobotsController _controller;
    private IRepository _repository;

    [TestInitialize]
    public void Initialize()
    {
        _dataContext = A.Fake<IDataContext>();
        _repository = A.Fake<IRepository>();

        _controller = new RobotsController(_dataContext, _repository);
    }


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
            
        var controller = new RobotsController(context, new Repository(context));

        //Call controller and get result
        var result = controller.GetAvailable("Bloaty Head");

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));

        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [TestMethod]
    public void GiveAnAvailableRobot_GetAvailable_ShouldReturnIt()
    {
        var robotBuilder = new RobotBuilder();
        var appointmentBuilder = new AppointmentBuilder();
        var condition = "flu";
        var availableRobot = robotBuilder
            .WithId(0)
            .WithConditionExpertise(condition)
            .Build();
        var notOverlappingAppointment = appointmentBuilder
            .WithId(0)
            .WithRobot(availableRobot)
            .WithStartAndEnd(new DateTime(2022, 01, 01, 13, 0, 0), new DateTime(2022, 01, 01, 14, 0, 0))
            .Build();
        availableRobot.Appointments = new List<Appointment> { notOverlappingAppointment };
        
        var notAvailableRobot = robotBuilder
            .WithId(1)
            .WithConditionExpertise(condition)
            .Build();
        var overlappingAppointment = appointmentBuilder
            .WithId(1)
            .WithRobot(notAvailableRobot)
            .WithStartAndEnd(new DateTime(2022, 01, 01, 15, 0, 0), new DateTime(2022, 01, 01, 16, 0, 0))
            .Build();
        notAvailableRobot.Appointments = new List<Appointment> { overlappingAppointment };
        
        
        A.CallTo(() => _repository.GetRobots()).Returns(Task.FromResult(new List<Robot> { availableRobot, notAvailableRobot }));

        var availableRobotsResponse = _controller.GetAvailable(condition, new DateTime(2022, 01, 01, 15, 0, 0));

        availableRobotsResponse.Result.Should().BeOfType<OkObjectResult>();
        var response = availableRobotsResponse.Result.As<OkObjectResult>();
        var result = response.Value.As<List<RobotDto>>();
        result.Should().HaveCount(1);
        result.First().Id.Should().Be(availableRobot.Id);
        result.First().ConditionExpertise.Should().Be(availableRobot.ConditionExpertise);

    }
}
