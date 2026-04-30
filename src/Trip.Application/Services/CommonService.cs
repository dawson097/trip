using System.Linq.Expressions;
using Trip.Domain.Repositories;
using Trip.Application.Interfaces;

namespace Trip.Application.Services;

public class CommonService<T>(ICommonRepository<T> commonRepository) : ICommonService<T> where T : class
{
    public async Task<bool> CheckExitsAsync(Expression<Func<T, bool>> entityId)
    {
        return await commonRepository.CheckExitsAsync(entityId);
    }

    public async Task<bool> SaveAsync()
    {
        return await commonRepository.SaveAsync();
    }
}