using parking_manager.DTO;
using parking_manager.Interfaces;
using parking_manager.Entity;
using System.Text.RegularExpressions;

namespace parking_manager.Services
{

    public partial class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
    {
        [GeneratedRegex(@"^([A-Z]{3}-[0-9]{4}|[A-Z]{3}[0-9][A-Z][0-9]{2})$")]
        private static partial Regex plateRegex();
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        public async Task<VehicleDTO> CreateVehicle(VehicleDTO vehicle)
        {
            var plateUpper = vehicle.Plate.Trim().ToUpper();
            var regex = plateRegex();
            if (!regex.IsMatch(plateUpper))
            {
                throw new Exception("Invalid plate format");
            }

            var plateFormatted = plateUpper.Replace("-", "");

            var alreadyExists = await _vehicleRepository.GetVehicleById(plateFormatted);
            if (alreadyExists != null)
            {
                throw new Exception("Vehicle already exists");
            }

            var newVehicle = new VehiclesEntity
            {
                Plate = plateFormatted
            };

            await _vehicleRepository.CreateVehicle(newVehicle);

            return new VehicleDTO { Plate = plateFormatted };
        }

        public async Task<VehicleDTO?> GetVehicleById(string plate)
        {
            var plateUpper = plate.Trim().ToUpper();
            var regex = plateRegex();
            if (!regex.IsMatch(plateUpper))
            {
                throw new Exception("Invalid plate format");
            }

            var plateFormatted = plateUpper.Replace("-", "");

            var vehicle = await _vehicleRepository.GetVehicleById(plateFormatted);
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