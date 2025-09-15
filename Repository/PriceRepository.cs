using Microsoft.EntityFrameworkCore;
using parking_manager.Data;
using parking_manager.Entity;
using parking_manager.Interfaces;
namespace parking_manager.Repository
{
    public class PriceRepository(ApplicationDbContext context) : IPriceRepository
    {
        private readonly ApplicationDbContext _context = context;

        /// Create a new price entry in the database

        public async Task<PricesEntity> CreatePrice(PricesEntity price)
        {
            _context.Prices.Add(price);
            await _context.SaveChangesAsync();
            return price;

        }

        /// Get price valildation for date range, the range cannot be overlapped and validFrom must be less than validTo

        public async Task<PricesEntity?> GetPriceValidation(DateTime validFrom, DateTime validTo)
        {
            var priceRange = await _context.Prices.Where(data => data.ValidFrom <= validTo && data.ValidTo >= validFrom).FirstOrDefaultAsync();
            return priceRange;
        }
    }
}