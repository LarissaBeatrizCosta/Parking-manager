using parking_manager.Entity;

namespace parking_manager.Interfaces
{
    public interface IVehicleRepository
    {
        Task<VehiclesEntity> CreateVehicle(VehiclesEntity vehicle);
        Task<IEnumerable<VehiclesEntity>> GetVehicles();

        Task<VehiclesEntity?> GetVehicleById(string plate);
    }
}