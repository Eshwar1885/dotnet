using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Interfaces
{
    public interface ICartService
    {
        bool AddToCart(CartDTO cartDTO);
        bool RemoveFromCart(CartDTO cartDTO);
    }
}