using Microsoft.AspNetCore.Mvc;
using parking_manager.DTO;
using parking_manager.Interfaces;

namespace parking_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController(IVehicleService vehicleService) : ControllerBase
    {
        private readonly IVehicleService _vehicleService = vehicleService;

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehicleDTO vehicle)
        {
            try
            {
                var newVehicle = await _vehicleService.CreateVehicle(vehicle);
                return Ok(newVehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                var vehicles = await _vehicleService.GetVehicles();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{plate}")]
        public async Task<IActionResult> GetVehicleById(string plate)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(plate);
                if (vehicle == null)
                    return NotFound("Vehicle not found");

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
    }
}