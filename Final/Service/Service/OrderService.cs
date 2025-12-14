using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Order;
using System.Security.Claims;

namespace Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepo;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;

        public OrderService(
            IBasketService basketService,
            IProductService productService,
            IOrderRepository orderRepo,
            IHttpContextAccessor http,
            IMapper mapper)
        {
            _basketService = basketService;
            _productService = productService;
            _orderRepo = orderRepo;
            _http = http;
            _mapper = mapper;
        }

        public async Task<bool> CreateOrderAsync(CheckoutVM vm,ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            var basket = await _basketService.GetBasketFromDbAsync();
            if (!basket.Items.Any())
                throw new CustomException(400, "Basket is empty");

            var userId = _http.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new CustomException(401, "User not authenticated");

            var products = new Dictionary<int, Product>();

            foreach (var item in basket.Items)
            {
                if (!products.ContainsKey(item.ProductId))
                {
                    var product = await _productService.GetAsyncWithoutMapping(item.ProductId);

                    if (product == null)
                        throw new CustomException(404, "Product not found");

                    products.Add(item.ProductId, product); 
                }
            }

            foreach (var item in basket.Items)
            {
                var product = products[item.ProductId];

                if (product.Quantity < item.ProductCount)
                    throw new CustomException(
                        400,
                        $"{product.Name} stock not enough");
            }

            var order = new Order
            {
                AppUserId = userId,
                Name = vm.Name,
                Surname = vm.Surname,
                Phone = vm.Phone,
                Address = vm.Address,
                TotalPrice = basket.TotalPrice,
                OrderItems = basket.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.ProductCount,
                    Price = i.ProductPrice
                }).ToList()
            };

            await _orderRepo.AddAsync(order);

            foreach (var item in basket.Items)
            {
                var product = products[item.ProductId];
                product.Quantity -= item.ProductCount;
                product.InStock = product.Quantity > 0;
            }

            await _basketService.ClearDbBasketAsync();

            await _orderRepo.SaveChangesAsync();

            return true;
        }

    }
}
