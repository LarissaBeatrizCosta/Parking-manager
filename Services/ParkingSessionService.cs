using parking_manager.DTO;
using parking_manager.Entity;
using parking_manager.Interfaces;

namespace parking_manager.Services
{
    public class ParkingSessionService(IParkingSessionsRepository parkingRepository, IPriceRepository priceRepository, IVehicleRepository vehicleRepository) : IParkingSessionService
    {

        private readonly IParkingSessionsRepository _parkingRepository = parkingRepository;
        private readonly IPriceRepository _priceRepository = priceRepository;

        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;


        public async Task<ParkingSessionsDTO> CreateParkingSession(string plate)
        {
            var alreadyExists = await _parkingRepository.GetActiveParkingSession(plate);
            if (alreadyExists != null)
            {
                throw new Exception("There is already an active parking session for this vehicle");
            }

            var vehicleExists = await _vehicleRepository.GetVehicleById(plate);
            if (vehicleExists == null)
            {
                var newVehicle = new VehiclesEntity
                {
                    Plate = plate
                };
                await _vehicleRepository.CreateVehicle(newVehicle);
            }

            var newParkingSession = new ParkingSessionsEntity
            {
                VehicleId = plate,
                ExitDate = null,
                TotalPrice = null
            };
            await _parkingRepository.CreateParkingSession(newParkingSession);

            return new ParkingSessionsDTO
            {
                VehiclePlate = newParkingSession.VehicleId,
                EntryDate = newParkingSession.EntryDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ExitDate = newParkingSession.ExitDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                Duration = "0:00:00",
                HoursPaid = 0,
                PricePerHour = 0,
                TotalPrice = 0
            };
        }

        public async Task<ParkingSessionsDTO> FinalizeParkingSession(string plate)
        {
            var parkingSession = await _parkingRepository.GetActiveParkingSession(plate) ?? throw new Exception("There is no active parking session for this vehicle");

            parkingSession.ExitDate = DateTime.Now;
            parkingSession.UpdatedAt = DateTime.Now;


            var prices = await _priceRepository.GetPriceValidation(parkingSession.EntryDate, parkingSession.ExitDate ?? DateTime.Now) ?? throw new Exception("There is no price defined for this period");
            var pricePerHour = prices.PricePerHour;
            var priceFirstHour = prices.FirstHourPrice;


            var durationSession = CalculateDuration(parkingSession.EntryDate, parkingSession.ExitDate);
            var totalHours = CalculateHours(durationSession);

            parkingSession.TotalPrice = CalculateTotalPrice(durationSession, priceFirstHour, pricePerHour);

            await _parkingRepository.UpdateParkingSession(parkingSession);

            return new ParkingSessionsDTO
            {
                VehiclePlate = parkingSession.VehicleId,
                EntryDate = parkingSession.EntryDate.ToString("yyyy-MM-dd HH:mm:ss"),
                ExitDate = parkingSession.ExitDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                Duration = durationSession.ToString(@"hh\:mm\:ss"),
                HoursPaid = totalHours,
                PricePerHour = pricePerHour,
                TotalPrice = parkingSession.TotalPrice ?? 0
            };
        }

        public async Task<IEnumerable<ParkingSessionsDTO>> GetParkingSessions()
        {
            var parkingSessions = await _parkingRepository.GetParkingSessions();
            var parkingSessionsDTOs = new List<ParkingSessionsDTO>();

            foreach (var item in parkingSessions)
            {
                if (item.ExitDate == null)
                {
                    var parkingSessionDTO = new ParkingSessionsDTO
                    {
                        VehiclePlate = item.VehicleId,
                        EntryDate = item.EntryDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        ExitDate = item.ExitDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                        TotalPrice = 0,
                        HoursPaid = 0,
                        Duration = "0:00:00",
                        PricePerHour = 0

                    };
                    parkingSessionsDTOs.Add(parkingSessionDTO);
                }
                else
                {
                    var durationSession = CalculateDuration(item.EntryDate, item.ExitDate);
                    var totalHours = CalculateHours(durationSession);

                    var getPricePerHour = await _priceRepository.GetPriceValidation(item.EntryDate, item.ExitDate ?? DateTime.Now) ?? throw new Exception("There is no price defined for this period");
                    var pricePerHour = getPricePerHour.PricePerHour;

                    var parkingSessionDTO = new ParkingSessionsDTO
                    {
                        VehiclePlate = item.VehicleId,
                        EntryDate = item.EntryDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        ExitDate = item.ExitDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                        Duration = durationSession.ToString(@"hh\:mm\:ss"),
                        HoursPaid = totalHours,
                        PricePerHour = pricePerHour,
                        TotalPrice = item.TotalPrice ?? 0
                    };
                    parkingSessionsDTOs.Add(parkingSessionDTO);
                }


            }
            return parkingSessionsDTOs;
        }


        /// Calculate total price based on duration and price per hour
        private static double CalculateTotalPrice(TimeSpan duration, double firstHourPrice, double pricePerHour)
        {
            if (duration.TotalMinutes <= 30)
                return firstHourPrice / 2;

            double totalPrice = firstHourPrice;
            var remainingMinutes = duration.TotalMinutes - 60;

            if (remainingMinutes > 0)
            {
                double additionalHours = remainingMinutes / 60;
                double minutes = remainingMinutes % 60;

                if (minutes > 10)
                {
                    additionalHours = additionalHours - (minutes / 60) + 1;
                }
                else
                {
                    additionalHours -= minutes / 60;
                }


                totalPrice += additionalHours * pricePerHour;
            }
            return totalPrice;

        }


        public static int CalculateHours(TimeSpan duration)
        {
            if (duration.TotalMinutes <= 30)
            {
                return 0;
            }

            var hoursPaid = Math.Ceiling(duration.TotalMinutes / 70);
            return Convert.ToInt32(hoursPaid);
        }

        public static TimeSpan CalculateDuration(DateTime entryDate, DateTime? exitDate)
        {
            if (exitDate < entryDate || exitDate == entryDate || exitDate == null)
            {
                throw new Exception("Invalid dates provided");
            }

            return exitDate.Value - entryDate;
        }


    }
}

