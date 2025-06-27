﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace DreamGarden.Repositories
{
    public class UserOrderRepository :IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UserOrderRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager,IHttpContextAccessor httpContextAccessor) //constructor
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task ChangeOrderStatus(UpdateOrderStatusModel data)
        {
            var order = await _db.Orders.FindAsync(data.OrderId);
            if(order==null)
            {
                throw new InvalidOperationException($"Order with id: {data.OrderId} not found");
            }
            order.OrderStatusId = data.OrderStatusId;
            await _db.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatuses()
        {
            return await _db.orderStatuses.ToListAsync();
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            var order= await _db.Orders.FindAsync(orderId);
            if(order==null)
            {
                throw new InvalidOperationException($"Order with id: {orderId} not found");
            }
            order.IsPaid = !order.IsPaid; //toggle the payment status
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
        {
            var orders = _db.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderDetail)
                .ThenInclude(x => x.Flower)
                .ThenInclude(x => x.Genre).AsQueryable();

            if(!getAll)
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");
                orders = orders.Where(a => a.UserId == userId);
                return await orders.ToListAsync();  
            }
            return await orders.ToListAsync();
        }

        //public async Task<IEnumerable<Order>> UserOrders()
        //{
        //    var userId = GetUserId();
        //    if (string.IsNullOrEmpty(userId))
        //        throw new Exception("User is not logged in");

        //    var orders = await _db.Orders
        //                .Include(x => x.OrderStatus)
        //                .Include(x=>x.OrderDetail)
        //                .ThenInclude(x => x.Flower)
        //                .ThenInclude(x => x.Genre)
        //                .Where(a => a.UserId==userId)
        //                .ToListAsync();

        //    return orders;
        //}

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
