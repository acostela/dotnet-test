using Microsoft.EntityFrameworkCore;
using TechTest.Domain;

namespace TechTest.Data;

public interface IDataContext
{
    DbSet<Robot> Robots { get; set; }
    DbSet<Appointment> Appointments { get; set; }
}

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }
    public DbSet<Robot> Robots { get; set; }

    public DbSet<Appointment> Appointments { get; set; }
}
