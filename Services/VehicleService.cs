using parking_manager.Interfaces;
using parking_manager.Models;
using parking_manager.Repositories;

namespace parking_manager.Services
{
    public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        public async Task<VehiclesEntity> CreateVehicleAsync(VehiclesEntity vehicle)
        {
        return await _vehicleRepository.CreateVehicleAsync(vehicle);
        }
    }
}