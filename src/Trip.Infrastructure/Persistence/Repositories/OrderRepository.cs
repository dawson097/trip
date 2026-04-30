using Microsoft.EntityFrameworkCore;
using Trip.Infrastructure.Persistence.DbContexts;
using Trip.Domain.Entities;
using Trip.Domain.Repositories;

namespace Trip.Infrastructure.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : CommonRepository<Order>(context), IOrderRepository
{
    private readonly AppDbContext _context = context;

    public IQueryable<Order> GetAllOrdersWithQuery(string userId, int pageSize, int pageNumber)
    {
        return _context.Orders.Where(order => order.UserId == userId);
    }

    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        return (await _context.Orders.Include(order => order.OrderItems)!
            .ThenInclude(item => item.TouristRoute)
            .FirstOrDefaultAsync(order => order.Id == orderId))!;
    }

    public async Task CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}