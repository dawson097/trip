using Mapster;
using Trip.Application.Dtos.CartLineItem;
using Trip.Application.Dtos.ShoppingCart;
using Trip.Domain.Entities;

namespace Trip.Application.Common.Mappers;

public class ShoppingCartMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ShoppingCart, ShoppingCartDto>();

        config.NewConfig<CartLineItem, CartLineItemDto>();
    }
}