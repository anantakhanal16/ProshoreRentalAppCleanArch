using ApplicationLayer.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using RentalApi.DTOs;

namespace RentalApi.Controllers
{

    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("Register-house-seeker")]
        public async Task<IActionResult> RegisterHouseSeeker([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserRole = "HouseSeeker"
                };

                var result = await _authService.RegisterAsync(user, model.Password,user.UserRole);

                if (result)
                {

                    return Ok(new { Message = "House seeker registered successfully" });
                }
            }

            return BadRequest(new { Message = "Invalid registration data" });
        }


        [HttpPost("Register-as-broker")]
        public async Task<IActionResult> RegisterAsBroker([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserRole = "Broker"
                };

                var result = await _authService.RegisterAsync(user, model.Password, user.UserRole);

                if (result)
                {

                    return Ok(new { Message = " Registered successfully as broker" });
                }
                else 
                {
                    return BadRequest(new { Message = "Error while registering" });
                }
            }

            return BadRequest(new { Message = "Invalid registration data" });
        }
       
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var signInResult = await _authService.SignInAsync(model.Email, model.Password, model.RememberMe);

            if (signInResult)
            {
                
                return Ok("Login successful");
            }

            return BadRequest("Invalid login attempt");
        }


        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
         

                 await _authService.SignOutAsync();

                return Ok("Signout successful");
            

        }
    }

}



