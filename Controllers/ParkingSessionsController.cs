using Microsoft.AspNetCore.Mvc;
using parking_manager.DTO;
using parking_manager.Interfaces;

namespace parking_manager.Controllers
{
    [Route("api/[controller]")]
    public class ParkingSessionsController(IParkingSessionService parkingService) : ControllerBase
    {
        private readonly IParkingSessionService _parkingService = parkingService;

        /// Create a new parking session for a vehicle

        [HttpPost("{plate}")]
        public async Task<ActionResult<ParkingSessionsDTO>> CreateParkingSession(string plate)
        {
            try
            {
                var newSession = await _parkingService.CreateParkingSession(plate);
                return Ok(newSession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// Finalize a parking session

        [HttpPut("{plate}/finalize")]
        public async Task<ActionResult<ParkingSessionsDTO>> FinalizeParkingSession(string plate)
        {
            try
            {
                var finalizedSession = await _parkingService.FinalizeParkingSession(plate);
                return Ok(finalizedSession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// Get all parking sessions

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingSessionsDTO>>> GetParkingSessions()
        {
            try
            {
                var sessions = await _parkingService.GetParkingSessions();
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}