﻿namespace DreamGarden.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int flowerId, int qty);
        Task<int> RemoveItem(int flowerId);
        Task<ShoppingCart> GetUserCart();
        Task <int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);

        Task<bool> DoCheckout(CheckoutModel model);
    }
}
