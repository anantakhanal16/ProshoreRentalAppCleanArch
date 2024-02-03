using Core.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(ApplicationUser user, string password,string role);
        Task<bool> SignInAsync(string username, string password, bool rememberMe);
        Task SignOutAsync();
    }
}
