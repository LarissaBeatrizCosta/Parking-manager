using Microsoft.AspNetCore.Mvc;
using parking_manager.Data;
using parking_manager.Models;
using parking_manager.Repositories;

namespace parking_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController(IVehicleRepository vehicleRepository) : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehiclesEntity vehicle)
        {
            try
            {
                await _vehicleRepository.CreateVehicleAsync(vehicle);
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}