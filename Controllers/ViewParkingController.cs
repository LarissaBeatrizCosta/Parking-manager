using Microsoft.AspNetCore.Mvc;
using parking_manager.Interfaces;

namespace parking_manager.Controllers
{
    [Route("[controller]")]
    public class ViewParking(IParkingSessionService parkingService) : Controller
    {
        public readonly IParkingSessionService _parkingService = parkingService;

        public async Task<IActionResult> Index()
        {
            var sessions = await _parkingService.GetParkingSessions();
            return View(sessions);
        }

    }
}