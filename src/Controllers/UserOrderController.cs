using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DreamGarden.Controllers
{
    [Authorize] // Only authenticated users can access this controller
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository _userOrderRepo;
        public UserOrderController(IUserOrderRepository userOrderRepo) // Constructor injection for IUserOrderRepository
        {
            _userOrderRepo = userOrderRepo;
        }
                   
        
        public async Task<IActionResult> UserOrders()
        {
            var orders= await _userOrderRepo.UserOrders(); // Fetch user orders asynchronously
            return View(orders);
        }
    }
}
