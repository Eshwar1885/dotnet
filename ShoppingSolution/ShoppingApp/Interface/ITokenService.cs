using ShoppingApp.Models.DTOs;

namespace ShoppingApp.Interface
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}
