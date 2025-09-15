using parking_manager.DTO;
using parking_manager.Entity;
using parking_manager.Interfaces;
namespace parking_manager.Services
{
    public class PriceService(IPriceRepository priceRepository) : IPriceService
    {
        private readonly IPriceRepository _priceRepository = priceRepository;

        /// Creates a new price entry if it doesn't already exist within the specified validity period.

        public async Task<PricesEntity> CreatePrice(PriceDTO price)
        {
            if (price.ValidFrom >= price.ValidTo)
            {
                throw new Exception("Invalid validity period");
            }
            var newPrice = new PricesEntity
            {
                ValidFrom = price.ValidFrom,
                ValidTo = price.ValidTo,
                PricePerHour = price.PricePerHour
            };

            var alreadyExists = await _priceRepository.GetPriceValidation(price.ValidFrom, price.ValidTo);
            if (alreadyExists != null)
            {
                throw new Exception("Price already exists");
            }

            await _priceRepository.CreatePrice(newPrice);
            return newPrice;

        }

        public async Task<PriceDTO?> GetPriceRange(DateTime validFrom, DateTime validTo)
        {
            var priceRange = await _priceRepository.GetPriceValidation(validFrom, validTo);

            if (priceRange == null) return null;

            return new PriceDTO
            {
                ValidFrom = priceRange.ValidFrom,
                ValidTo = priceRange.ValidTo,
                PricePerHour = priceRange.PricePerHour
            };
        }


    }
}