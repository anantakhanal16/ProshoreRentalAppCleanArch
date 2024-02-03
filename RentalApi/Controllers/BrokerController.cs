using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentalApi.Controllers
{
    [Authorize(Roles = "Broker")]
    [Route("api/broker")]
    public class BrokerController : ControllerBase
    {
     
    }
}
