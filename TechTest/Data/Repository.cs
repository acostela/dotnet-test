using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TechTest.Domain;

namespace TechTest.Data;

public interface IRepository
{
    Task<List<Robot>> GetRobots();
    Task<List<Robot>> GetRobots(Expression<Func<Robot, bool>> conditions);
}

/// <summary>
/// This class emulates the database access
/// </summary>
public class Repository : IRepository
{
    private readonly IDataContext _context;

    public Repository(IDataContext context)
    {
        _context = context;
    }

    public Task<List<Robot>> GetRobots()
    {
        return GetRobots(x => true);
    }

    public async Task<List<Robot>> GetRobots(Expression<Func<Robot, bool>> conditions)
    {
        return await _context.Robots.Where(conditions).Include(x => x.Appointments).ToListAsync();
    }
}
