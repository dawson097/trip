using Mapster;
using Trip.Application.Dtos.Order;
using Trip.Domain.Entities;

namespace Trip.Application.Common.Mappers;

public class OrderMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>()
            .Map(dest => dest.OrderState, src => src.OrderState.ToString());
    }
}