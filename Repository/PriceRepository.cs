using parking_manager.Data;
using parking_manager.Entity;
using parking_manager.Interfaces;
namespace parking_manager.Repository
{
    public class PriceRepository(ApplicationDbContext context) : IPriceRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<PricesEntity> CreatePrice(PricesEntity price)
        {
            _context.Prices.Add(price);
            await _context.SaveChangesAsync();
            return price;

        }

        public Task<PricesEntity?> GetPriceValidation(DateTime validFrom, DateTime validTo)
        {
            throw new NotImplementedException();
        }
    }
}