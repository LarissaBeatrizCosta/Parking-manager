using parking_manager.DTO;
using parking_manager.Models;

namespace parking_manager.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleDTO> CreateVehicle(VehicleDTO vehicle);
        Task<IEnumerable<VehicleDTO>> GetVehicles();

    }
}