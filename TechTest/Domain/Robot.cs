using System.ComponentModel.DataAnnotations;

namespace TechTest.Domain;
public class Robot
{
    [Key]
    public int Id { get; set; }
    public string ConditionExpertise { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
