using Microsoft.EntityFrameworkCore;
using Trip.Application.Dtos.Pagination;

namespace Trip.Application.Common.Extensions;

public static class PaginationExtensions<T>
{
    /// <summary>
    /// 创建分页
    /// </summary>
    /// <param name="currentPage">当前页数</param>
    /// <param name="pageSize">分页显示数据量</param>
    /// <param name="queryRes">分页查询数据</param>
    /// <returns></returns>
    public static async Task<PaginationDto<T>> CreatePaginationAsync(int currentPage, int pageSize,
        IQueryable<T> queryRes)
    {
        // 获取真实的总页数
        var totalCount = await queryRes.CountAsync();

        // 获取跳过的页数
        var skipPage = (currentPage > 0 ? currentPage - 1 : 0) * pageSize;
        queryRes = queryRes.Skip(skipPage); // 跳过前面已显示过的记录条数
        queryRes = queryRes.Take(pageSize); // 从当前位置开始截取指定数量的数据（即当前页的数据）

        // 将当前页获取到的真实记录条数以集合的形式呈现
        var pageItems = await queryRes.ToListAsync();

        return new PaginationDto<T>(totalCount, currentPage, pageSize, pageItems);
    } 
}