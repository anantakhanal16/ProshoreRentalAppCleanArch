using ApplicationLayer.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ApplicationLayer.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterAsync(ApplicationUser appUser, string password, string role)
        {
            var user = new ApplicationUser { UserName = appUser.Email, Email = appUser.Email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded && !string.IsNullOrEmpty(role))
            {
                bool roleExists = await _roleManager.RoleExistsAsync(role);

                if (!roleExists)
                    await _roleManager.CreateAsync(new IdentityRole(role));

                await _userManager.AddToRoleAsync(user, role);
            }

            return result.Succeeded;
        }

        public async Task<bool> SignInAsync(string username, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task SignOutAsync() => await _signInManager.SignOutAsync();

        public async Task<ApplicationUser> GetUserDetails()
        {
            var user = await _userManager.GetUserAsync(_signInManager.Context.User);

            return user;
        }

        
    }
}
