using Microsoft.AspNetCore.Mvc;
using parking_manager.Interfaces;
using parking_manager.Models;

namespace parking_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController(IVehicleService vehicleService) : ControllerBase
    {
        private readonly IVehicleService _vehicleService = vehicleService;

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehiclesEntity vehicle)
        {
            try
            {
                await _vehicleService.CreateVehicleAsync(vehicle);
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}