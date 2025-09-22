using Microsoft.AspNetCore.Mvc;
using parking_manager.DTO;
using parking_manager.Interfaces;

namespace parking_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController(IPriceService priceService) : ControllerBase
    {
        private readonly IPriceService _priceService = priceService;

        /// Create a new price

        [HttpPost]
        public async Task<IActionResult> CreatePrice(PriceDTO price)
        {
            try
            {
                var newPrice = await _priceService.CreatePrice(price);
                return Ok(newPrice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// Return a price range


        [HttpGet("{validFrom}/{validTo}")]
        public async Task<IActionResult> GetPriceRange(DateTime validFrom, DateTime validTo)
        {
            try
            {
                var priceRange = await _priceService.GetPriceRange(validFrom, validTo);
                if (priceRange == null)
                    return NotFound("Preço não encontrado");

                return Ok(priceRange);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}