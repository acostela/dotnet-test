using Microsoft.AspNetCore.Mvc;
using TechTest.Data;
using TechTest.Domain;
using TechTest.Services;

namespace TechTest.Controllers;

[ApiController]
[Route("[controller]")]
public class RobotsController : ControllerBase
{
    private readonly IDataContext _context;
    private readonly IRepository _repository;

    public RobotsController(IDataContext context, IRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    [HttpGet("required_rooms")]
    public IActionResult GetRequiredRoomsForDay(DateTime date)
    {
        throw new NotImplementedException();
    }

    [HttpPost("available")]
    public IActionResult GetAvailable(string condition, DateTime? when = null)
    {

        var robots = _repository.GetRobots().Result;
        var robotResult = new List<Robot>();

        int i = 0;
        while(i < robots.Count)
        {
            if (robots[i].ConditionExpertise == condition)
            {
                robotResult.Add(robots[i]);
            }else if (robotResult.Count == 0 && robots.Count == i + 1)
                throw new IndexOutOfRangeException();

            ++i;
        }

        if(robotResult.Count > 0)
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
