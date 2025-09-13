using parking_manager.Models;

namespace parking_manager.Interfaces
{
    public interface IVehicleService
    {
        Task<VehiclesEntity> CreateVehicleAsync(VehiclesEntity vehicle);

    }
}