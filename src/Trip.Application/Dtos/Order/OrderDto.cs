using Trip.Application.Dtos.CartLineItem;

namespace Trip.Application.Dtos.Order;

/// <summary>
/// 订单DTO
/// </summary>
public class OrderDto
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ICollection<CartLineItemDto> OrderItems { get; set; } = new List<CartLineItemDto>();

    public string OrderState { get; set; } = string.Empty;

    public DateTime CreateDateUtc { get; set; }

    public string TransactionMetadata { get; set; } = string.Empty;
}