using parking_manager.DTO;
using parking_manager.Entity;

namespace parking_manager.Interfaces
{
    public interface IPriceService
    {
        Task<PricesEntity> CreatePrice(PriceDTO price);

        Task<PriceDTO?> GetPriceRange(DateTime validFrom, DateTime validTo);
    }
}