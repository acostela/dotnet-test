using System;
using TechTest.Domain;

namespace TechTestTests.Builders;
public class AppointmentBuilder
{
    private DateTime _startDate = DateTime.Now.Date;
    private DateTime _endDate = DateTime.Now.Date.AddMinutes(30);
    private Robot _robot = new RobotBuilder().Build();
    private int _id = 1;
    public Appointment Build()
    {
        return new Appointment
        {
            Id = _id,
            StartDate = _startDate,
            EndDate = _endDate,
            Robot = _robot,
            RobotId = _robot.Id
        };
    }

    public AppointmentBuilder WithStartAndEnd(DateTime startDateTime, DateTime endDateTime)
    {
        _startDate = startDateTime;
        _endDate = endDateTime;

        return this;
    }
        
    public AppointmentBuilder WithStartAndDuration(DateTime startDateTime, int durationInMinutes)
    {
        _startDate = startDateTime;
        _endDate = startDateTime.AddMinutes(durationInMinutes);

        return this;
    }
        
    public AppointmentBuilder WithId(int id)
    {
        _id = id;

        return this;
    }
        
    public AppointmentBuilder WithRobot(Robot robot)
    {
        _robot = robot;

        return this;
    }
}