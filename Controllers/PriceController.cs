using Microsoft.AspNetCore.Mvc;
using parking_manager.Interfaces;

namespace parking_manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController(IPriceService priceService) : ControllerBase
    {
        private readonly IPriceService _priceService = priceService;

    }
}