using MapsterMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Trip.Application.Common.Extensions;
using Trip.Application.Common.Helpers;
using Trip.Application.Dtos.Order;
using Trip.Domain.Entities;
using Trip.Domain.Repositories;
using Trip.Application.Common.ResourceParameters;
using Trip.Application.Interfaces;

namespace Trip.Application.Services;

public class OrderService(
    ICommonRepository<Order> commonRepository,
    IOrderRepository orderRepository,
    IMapper mapper,
    UrlHelper urlHelper,
    IHttpClientFactory httpClientFactory)
    : CommonService<Order>(commonRepository), IOrderService
{
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(string userId,
        PaginationResourceParameters paginationParams)
    {
        var queryRes =
            orderRepository.GetAllOrdersWithQuery(userId, paginationParams.PageSize, paginationParams.PageNumber);
        var ordersFromRepo =
            await PaginationExtensions<Order>.CreatePaginationAsync(paginationParams.PageNumber,
                paginationParams.PageSize, queryRes);

        return mapper.Map<IEnumerable<OrderDto>>(ordersFromRepo);
    }

    public async Task<OrderDto> GetOrderAsync(Guid orderId)
    {
        var order = await orderRepository.GetOrderByIdAsync(orderId);

        return mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> PlaceOrderAsync(Guid orderId)
    {
        var orderFromRepo = await orderRepository.GetOrderByIdAsync(orderId);

        orderFromRepo.PaymentProcessing();
        await orderRepository.SaveAsync();

        var httpClient = httpClientFactory.CreateClient("OrderApi");
        var url = @"http://localhost:8080/api/payment-process?&orderNumber={0}&returnFault={1}";

        var resp = await httpClient.PostAsync(string.Format(url, orderFromRepo.Id, false), null);

        // 提取支付结果
        var isApproved = false;
        var transactionMetadata = "";

        if (resp.IsSuccessStatusCode)
        {
            transactionMetadata = await resp.Content.ReadAsStringAsync();
            var jsonObj = (JObject)JsonConvert.DeserializeObject(transactionMetadata)!;
            isApproved = jsonObj["approved"]!.Value<bool>();
        }

        if (isApproved)
        {
            // 支付成功，完成订单
            orderFromRepo.PaymentApproved();
        }
        else
        {
            // 支付失败
            orderFromRepo.PaymentRejected();
        }

        orderFromRepo.TransactionMetadata = transactionMetadata;
        await orderRepository.SaveAsync();

        return mapper.Map<OrderDto>(orderFromRepo);
    }
}