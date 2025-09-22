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

        /// Create a new vehicle

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
                return StatusCode(500, ex.Message);
            }
        }

        /// Get all vehicles


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
                return StatusCode(500, ex.Message);
            }
        }

        /// Get vehicle by id

        [HttpGet("{plate}")]
        public async Task<IActionResult> GetVehicleById(string plate)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(plate);
                if (vehicle == null)
                    return NotFound("Veículo não encontrado");

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}