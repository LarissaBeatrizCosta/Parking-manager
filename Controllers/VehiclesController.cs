using Microsoft.AspNetCore.Mvc;
using parking_manager.Data;
using parking_manager.Models;

namespace parking_manager.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController(ApplicationDbContext appDbContext) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehiclesEntity vehicle)
        {
            appDbContext.Vehicles.Add(vehicle);
            await appDbContext.SaveChangesAsync();
            return Ok(vehicle);
        }
    }
}