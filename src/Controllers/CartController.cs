using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DreamGarden.Controllers
{
    [Authorize] //only authenticated users can access this controller

    
    public class CartController : Controller
    {

        private readonly ICartRepository _cartRepo;
        //constructor
        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int flowerId, int qty=1,int redirect=0)
        {
            var cartCount = await _cartRepo.AddItem(flowerId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            //else
            return RedirectToAction("GetUserCart");
           
        }

        public async Task<IActionResult> RemoveItem(int flowerId)
        {
            var cartCount = await _cartRepo.RemoveItem(flowerId);
            return RedirectToAction("GetUserCart");
            
        }

        public async Task<IActionResult> GetUserCart()
        {
           var cart = await _cartRepo.GetUserCart();
           return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem=await _cartRepo.GetCartItemCount();
            //return View();
            return Ok(cartItem);
        }

        public IActionResult Checkout()
        {
            return View(); // Return the view for checkout process
        }

        [HttpPost]

        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
           if(!ModelState.IsValid)
           {
               //if model state is not valid, return the view with the model
               return View(model);
           }   
            bool isCheckedOut = await _cartRepo.DoCheckout(model);
                //if checkout is not successful, throw an exception
                if (!isCheckedOut)
                    return RedirectToAction(nameof(OrderFailure));  
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {             
                return View(); // Return the view for checkout process
        }


        public IActionResult OrderFailure()
        {
            return View(); // Return the view for checkout process
        }


    }
}
