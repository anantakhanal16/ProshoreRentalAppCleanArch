using ApplicationLayer.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Implementation
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

        public async Task<bool> RegisterAsync(ApplicationUser username, string password, string role)
            {
                var user = new ApplicationUser { UserName = username.Email, Email = username.Email }; 

                var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded && !string.IsNullOrEmpty(role))
            {
                bool roleExists = await _roleManager.RoleExistsAsync(role);
               
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                if (role.Equals("HouseSeeker"))
                {
                    await _userManager.AddToRoleAsync(user, "HouseSeeker");
                }
                else if (role.Equals("Broker"))
                {
                    await _userManager.AddToRoleAsync(user, "Broker");
                }
               
            }
            return result.Succeeded;

        }

        public async Task<bool> SignInAsync(string username, string password, bool rememberMe)
        {

            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task SignOutAsync()
            {
                await _signInManager.SignOutAsync();
            }
        
    }
}
