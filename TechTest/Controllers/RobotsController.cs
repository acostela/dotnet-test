using Microsoft.AspNetCore.Mvc;
using TechTest.Data;
using TechTest.Domain;
using TechTest.Services;

namespace TechTest.Controllers;

[ApiController]
[Route("[controller]")]
public class RobotsController : ControllerBase
{
    private readonly DataContext _context;

    public RobotsController(DataContext context)
    {
        this._context = context;
    }

    [HttpGet("required_rooms")]
    public IActionResult GetRequiredRoomsForDay(DateTime date)
    {
        throw new NotImplementedException();
    }

    [HttpPost("available")]
    public IActionResult GetAvailable(string condition)
    {
        var repository = new Repository(_context);

        var robots = repository.GetRobots().Result;
        var robotResult = robots.Where(robot => robot.ConditionExpertise == condition).ToList();

        if (robotResult.Count == 0 && robots.Count > 0)
            throw new IndexOutOfRangeException();

        if (robotResult.Count > 0)
        {
            int j = 0;
            while (j < robotResult.Count)
            {
                (new EngineeringNotificationService()).NotifyRobotSelected(robotResult[j].Id);
                (new CustomerNotificationService()).NotifyRobotSelected(robotResult[j].Id);
                (new InvoicingNotificationService()).NotifyRobotSelected(robotResult[j].Id);
                j++;
            }
        }

        var response = new List<object>();
        for (int j = 0; j < robotResult.Count; j++)
        {
            response.Add(new { id = robotResult[j].Id, conditionExpertise = robotResult[j].ConditionExpertise });
        }

        return base.Ok(response);
    }
}
