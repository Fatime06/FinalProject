using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Email;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRatingRepository _ratingRepo;
        private readonly IEmailService _emailService;

        public OrderService(
            IBasketService basketService,
            IProductService productService,
            IOrderRepository orderRepo,
            IHttpContextAccessor http,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IProductRatingRepository ratingRepo,
            IEmailService emailService)
        {
            _basketService = basketService;
            _productService = productService;
            _orderRepo = orderRepo;
            _http = http;
            _mapper = mapper;
            _userManager = userManager;
            _ratingRepo = ratingRepo;
            _emailService = emailService;
        }

        public async Task<bool> CreateOrderAsync(CheckoutVM vm, ModelStateDictionary modelState)
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

            var user = await _userManager.FindByIdAsync(userId);

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
                Name = user.Name,
                Surname = user.Surname,
                Phone = vm.Phone,
                Address = vm.Address,
                TotalPrice = basket.TotalPrice,
                OrderItems = basket.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.ProductCount,
                    Price = i.ProductPrice
                }).ToList(),
                CreatedDate = DateTime.Now
            };

            await _orderRepo.AddAsync(order);

            foreach (var item in basket.Items)
            {
                var product = products[item.ProductId];
                product.Quantity -= item.ProductCount;
                product.InStock = product.Quantity > 0;
            }
            user.IsVerifiedPurchase = true;
            await _basketService.ClearDbBasketAsync();

            await _orderRepo.SaveChangesAsync();

            return true;
        }
        public async Task<List<OrderVM>> GetAllAsync()
        {
            var userId = _http.HttpContext.User
               .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new CustomException(401, "User not authenticated");
            var user = await _userManager.FindByIdAsync(userId);

            var orders = await _orderRepo.GetAll()
                .Include(o => o.AppUser)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return orders.Select(o => new OrderVM
            {
                Id = o.Id,
                Name = o.Name,
                Surname = o.Surname,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                CustomerNumber = user.CustomerNumber,
                CreatedDate = o.CreatedDate,
                Items = o.OrderItems.Select(i => new OrderItemVM
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).ToList();
        }

        public async Task MarkDeliveredAsync(int orderId)
        {
            var order = await _orderRepo.Find(orderId)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new CustomException(404, "Order not found");

            order.Status = OrderStatus.Delivered;
            await _orderRepo.SaveChangesAsync();
        }
        public async Task<OrderVM> GetAsync(int orderId)
        {
            var order = await _orderRepo.Find(orderId)
                .Include(o => o.AppUser)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync();
            if (order == null)
                throw new CustomException(404, "Order not found");
            return new OrderVM
            {
                Id = order.Id,
                Name = order.Name,
                Surname = order.Surname,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CreatedDate = order.CreatedDate,
                Items = order.OrderItems.Select(i => new OrderItemVM
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }
        public async Task UpdateStatusAsync(int id, OrderStatus status)
        {
            var userId = _http.HttpContext.User
               .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new CustomException(401, "User not authenticated");
            var user = await _userManager.FindByIdAsync(userId);

            var order = await _orderRepo.GetAll()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new CustomException(404, "Order not found");

            order.Status = status;

            await _orderRepo.SaveChangesAsync();

            var body = $"Your order status is now: {status}";
            EmailSendVM emailVm = new()
            {
                Subject = "Order Status",
                Body = body,
                To = user.Email
            };
            _emailService.SendEmailAsync(emailVm);
        }
        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepo.GetAll()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new CustomException(404, "Order not found");

            _orderRepo.Delete(order);
            await _orderRepo.SaveChangesAsync();
        }
        public async Task<List<OrderVM>> GetUserOrdersAsync()
        {
            var userId = _http.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new CustomException(401, "User not authenticated");

            return await _orderRepo.GetAll()
                .Where(o => o.AppUserId == userId)
                .OrderByDescending(o => o.CreatedDate)
                .Select(o => new OrderVM
                {
                    Id = o.Id,
                    Name = o.Name,
                    Surname = o.Surname,
                    TotalPrice = o.TotalPrice,
                    Status = o.Status,
                    CreatedDate = o.CreatedDate
                }).ToListAsync();
        }
        public async Task<OrderVM> GetUserOrderDetailAsync(int orderId)
        {
            var userId = _http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new CustomException(401, "User not authenticated");

            var order = await _orderRepo.GetAll()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.AppUserId == userId);

            if (order == null)
                throw new CustomException(404, "Order not found");

            var ratedPairs = await _ratingRepo.GetAll()
                .Where(r => r.AppUserId == userId)
                .Select(r => new { r.ProductId, r.OrderId })
                .ToListAsync();

            return new OrderVM
            {
                Id = order.Id,
                Name = order.Name,
                Surname = order.Surname,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                CreatedDate = order.CreatedDate,
                Items = order.OrderItems.Select(i => new OrderItemVM
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    CanRate = order.Status == OrderStatus.Delivered && !ratedPairs.Any(x =>
                    x.ProductId == i.ProductId &&
                    x.OrderId == order.Id)
                }).ToList()
            };
        }

    }
}
