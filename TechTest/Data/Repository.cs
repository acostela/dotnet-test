using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TechTest.Domain;

namespace TechTest.Data;
/// <summary>
/// This class emulates the database access
/// </summary>
public class Repository
{
    private readonly DataContext _context;

    public Repository(DataContext context)
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
