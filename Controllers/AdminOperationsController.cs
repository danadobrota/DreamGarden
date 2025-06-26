using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DreamGarden.Constants;
using Microsoft.AspNetCore.Mvc.Rendering; // For role definition

namespace DreamGarden.Controllers;

[Authorize(Roles =nameof(Roles.Admin))] //only admin can access this controller
public class AdminOperationsController : Controller
{
    private readonly IUserOrderRepository _userOrderRepository;

    // Constructor injection for IUserOrderRepository
    public AdminOperationsController(IUserOrderRepository userOrderRepository)
    {
        _userOrderRepository = userOrderRepository;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> AllOrders()
    {         
        // Fetch all orders from the repository
        var orders = await _userOrderRepository.UserOrders(true);
        
        // Return the view with the list of orders
        return View(orders);
    }

    public async Task <IActionResult> TogglePaymentStatus(int orderId)
    {
        try
        {
            await _userOrderRepository.TogglePaymentStatus(orderId);
        }
        catch(Exception ex)
        {
            //no expections are thrown in this method, so we can log the error if needed
        }
        // Redirect to the AllOrders view after toggling the payment status
        return RedirectToAction(nameof(AllOrders));
    }

    public async Task<IActionResult> UpdateOrderStatus(int orderId)
    {
        var order=await _userOrderRepository.GetOrderById(orderId); 
        if(order == null)
        {
            throw new InvalidOperationException($"Order with id: {orderId} not found");
        } 
        var orderStatusList=(await _userOrderRepository.GetOrderStatuses()).Select(orderStatus 
            =>
        {
            return new SelectListItem
            {
                Value = orderStatus.Id.ToString(),
                Text = orderStatus.StatusName,
                Selected = order.OrderStatusId == orderStatus.Id
            };// Set selected if it matches the current order status
            });

        var data= new UpdateOrderStatusModel
        {
            OrderId = orderId,
            OrderStatusId = order.OrderStatusId,
            OrderStatusList=orderStatusList
        };
        return View(data);

    }
    [HttpPost]

    public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusModel data)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                data.OrderStatusList = (await _userOrderRepository.GetOrderStatuses()).Select(orderStatus =>
                {
                    return new SelectListItem
                    {
                        Value = orderStatus.Id.ToString(),
                        Text = orderStatus.StatusName,
                        Selected = orderStatus.Id == data.OrderStatusId
                     };
                });
                return View(data);
            }
            await _userOrderRepository.ChangeOrderStatus(data);
            TempData["msg"] = "Updated successfully!!";
        }
        catch(Exception ex)
        {
            TempData["msg"] = $"Error: {ex.Message} Something went wrong";
            // Log the error if needed
        }
        return RedirectToAction(nameof(UpdateOrderStatus), new { orderId = data.OrderId });

    }
}
