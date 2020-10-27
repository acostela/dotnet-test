using System.ComponentModel.DataAnnotations;

namespace TechTest.Domain;
public class Appointment
{
    [Key]
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int RobotId { get; set; }
    public virtual Robot Robot { get; set; }
}
