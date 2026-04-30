using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Trip.Infrastructure.Persistence.DbContexts;
using Trip.Domain.Repositories;

namespace Trip.Infrastructure.Persistence.Repositories;

public class CommonRepository<T>(AppDbContext context) : ICommonRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<bool> CheckExitsAsync(Expression<Func<T, bool>> entityId)
    {
        return await _dbSet.AnyAsync(entityId);
    }

    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() >= 0;
    }
}