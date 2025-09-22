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
            var initalDate = DateTime.Parse(price.ValidFrom);
            var finalDate = DateTime.Parse(price.ValidTo);

            if (initalDate >= finalDate)
            {
                throw new Exception("Período de vigência inválido");
            }

            if (price.PricePerHour <= 0 || price.FirstHourPrice <= 0)
            {
                throw new Exception("Preço por hora ou preço de primeira hora inválido");
            }
            var newPrice = new PricesEntity
            {
                ValidFrom = initalDate,
                ValidTo = finalDate,
                PricePerHour = price.PricePerHour,
                FirstHourPrice = price.FirstHourPrice
            };

            var alreadyExists = await _priceRepository.GetPriceValidation(initalDate, finalDate);
            if (alreadyExists != null)
            {
                throw new Exception("Preço já cadastrado");
            }

            await _priceRepository.CreatePrice(newPrice);
            return newPrice;

        }

        /// Verify if a price and return it if the period already exists

        public async Task<PriceDTO?> GetPriceRange(DateTime validFrom, DateTime validTo)
        {
            var priceRange = await _priceRepository.GetPriceValidation(validFrom, validTo);

            if (priceRange == null) return null;

            var initalDate = priceRange.ValidFrom.ToString("yyyy-MM-dd HH:mm:ss");
            var finalDate = priceRange.ValidTo.ToString("yyyy-MM-dd HH:mm:ss");

            return new PriceDTO
            {
                ValidFrom = initalDate,
                ValidTo = finalDate,
                PricePerHour = priceRange.PricePerHour,
                FirstHourPrice = priceRange.FirstHourPrice
            };
        }


    }
}