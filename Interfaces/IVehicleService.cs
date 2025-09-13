using parking_manager.DTO;

namespace parking_manager.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleDTO> CreateVehicle(VehicleDTO vehicle);
        Task<IEnumerable<VehicleDTO>> GetVehicles();

        Task<VehicleDTO?> GetVehicleById(string plate);

    }
}