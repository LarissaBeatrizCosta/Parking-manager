using parking_manager.DTO;
using parking_manager.Interfaces;
using parking_manager.Entity;

namespace parking_manager.Services
{
    public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        public async Task<VehicleDTO> CreateVehicle(VehicleDTO vehicle)
        {
            var alreadyExists = await _vehicleRepository.GetVehicleById(vehicle.Plate);
            if (alreadyExists != null)
            {
                throw new Exception("Vehicle already exists");
            }

            var newVehicle = new VehiclesEntity
            {
                Plate = vehicle.Plate
            };
            await _vehicleRepository.CreateVehicle(newVehicle);
            return vehicle;
        }

        public async Task<VehicleDTO?> GetVehicleById(string plate)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(plate);
            if (vehicle == null) return null;

            return new VehicleDTO
            {
                Plate = vehicle.Plate
            };
        }

        public async Task<IEnumerable<VehicleDTO>> GetVehicles()
        {
            var vehicles = await _vehicleRepository.GetVehicles();
            var vehicleDTOs = new List<VehicleDTO>();

            foreach (var item in vehicles)
            {
                var vehicleDTO = new VehicleDTO
                {
                    Plate = item.Plate
                };
                vehicleDTOs.Add(vehicleDTO);
            }
            return vehicleDTOs;

        }


    }
}