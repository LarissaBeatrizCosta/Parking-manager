using parking_manager.DTO;
using parking_manager.Interfaces;
using parking_manager.Models;
using parking_manager.Repositories;

namespace parking_manager.Services
{
    public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        public async Task<VehicleDTO> CreateVehicle(VehicleDTO vehicle)
        {
            var newVehicle = new VehiclesEntity
            {
                Plate = vehicle.Plate
            };
            await _vehicleRepository.CreateVehicle(newVehicle);
            return vehicle;
        }

        public async Task<IEnumerable<VehicleDTO>> GetVehicles()
        {
            var vehicle = await _vehicleRepository.GetVehicles();
            var vehicleDTOs = new List<VehicleDTO>();

            foreach (var v in vehicle)
            {
                var vehicleDTO = new VehicleDTO
                {
                    Plate = v.Plate
                };
                vehicleDTOs.Add(vehicleDTO);
            }
            return vehicleDTOs;

        }
    }
}