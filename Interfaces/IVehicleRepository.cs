using parking_manager.Models;

namespace parking_manager.Repositories
{
    public interface IVehicleRepository
    {
        Task<VehiclesEntity> CreateVehicleAsync(VehiclesEntity vehicle);
    }
}