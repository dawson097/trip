using Microsoft.EntityFrameworkCore;
using Trip.Infrastructure.Persistence.DbContexts;
using Trip.Domain.Entities;
using Trip.Domain.Repositories;

namespace Trip.Infrastructure.Persistence.Repositories;

public class CartLineItemRepository(AppDbContext context)
    : CommonRepository<CartLineItem>(context), ICartLineItemRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<CartLineItem>> GetItemsByIdsAsync(IEnumerable<int> itemIds)
    {
        return await _context.CartLineItems.Where(item => itemIds.Contains(item.Id)).ToListAsync();
    }

    public async Task<CartLineItem> GetItemByIdAsync(int itemId)
    {
        return (await _context.CartLineItems.FirstOrDefaultAsync(item => item.Id == itemId))!;
    }

    public async Task CreateItemAsync(CartLineItem item)
    {
        await _context.CartLineItems.AddAsync(item);
    }

    public void DeleteItem(CartLineItem item)
    {
        _context.CartLineItems.Remove(item);
    }

    public void DeleteItems(IEnumerable<CartLineItem> items)
    {
        _context.CartLineItems.RemoveRange(items);
    }
}