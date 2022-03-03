using System.Collections.Generic;
using TechTest.Domain;

namespace TechTestTests.Builders;
public class RobotBuilder
{
    private string _conditionExpertise = "Kidney Beans";
    private int _id = 1;
    private ICollection<Appointment> _appointments = new List<Appointment>();
        
    public Robot Build()
    {
        return new Robot
        {
            Id = _id,
            ConditionExpertise = _conditionExpertise,
            Appointments = _appointments
        };
    }

    public RobotBuilder WithId(int id)
    {
        _id = id;

        return this;
    }
        
    public RobotBuilder WithConditionExpertise(string conditionExpertise)
    {
        _conditionExpertise = conditionExpertise;

        return this;
    }
        
    public RobotBuilder WithAppointments(ICollection<Appointment> appointments)
    {
        _appointments = appointments;

        return this;
    }
}