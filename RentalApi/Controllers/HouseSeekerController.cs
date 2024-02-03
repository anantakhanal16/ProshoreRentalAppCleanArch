using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentalApi.Controllers
{
    [Authorize(Roles = "HouseSeeker")]
    [Route("api/house-seeker")]
    public class HouseSeekerController : ControllerBase
    {
  
    }
}
