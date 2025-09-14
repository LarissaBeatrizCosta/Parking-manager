using parking_manager.Entity;

namespace parking_manager.Interfaces
{
    public interface IPriceRepository
    {
        Task<PricesEntity?> GetPriceValidation(DateTime validFrom, DateTime validTo);
        Task<PricesEntity> CreatePrice(PricesEntity price);
    }
}