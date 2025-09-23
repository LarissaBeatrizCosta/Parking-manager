using parking_manager.DTO;
using parking_manager.Entity;
using parking_manager.Interfaces;

namespace parking_manager.Services
{
    public class ParkingSessionService(IParkingSessionsRepository parkingRepository, IPriceRepository priceRepository, IVehicleService vehicleService) : IParkingSessionService
    {

        private readonly IParkingSessionsRepository _parkingRepository = parkingRepository;
        private readonly IPriceRepository _priceRepository = priceRepository;

        private readonly IVehicleService _vehicleService = vehicleService;

        /// Create a new parking session with parameters plate, exit date and total price with value null

        public async Task<ParkingSessionsDTO> CreateParkingSession(string plate)
        {
            var plateFormatted = plate.Trim().ToUpper().Replace("-", "");

            var alreadyExists = await _parkingRepository.GetActiveParkingSession(plateFormatted);
            if (alreadyExists != null)
            {
                throw new Exception("Não pode haver mais de uma sessão ativa para o mesmo veículo");
            }

            var vehicleExists = await _vehicleService.GetVehicleById(plateFormatted);
            if (vehicleExists == null)
            {
                await _vehicleService.CreateVehicle(new VehicleDTO { Plate = plateFormatted });
            }


            var newParkingSession = new ParkingSessionsEntity
            {
                VehicleId = plateFormatted,
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

        /// Finalize a parking session and calculate the total price, hours paid and duration
        public async Task<ParkingSessionsDTO> FinalizeParkingSession(string plate)
        {
            var parkingSession = await _parkingRepository.GetActiveParkingSession(plate) ?? throw new Exception("Não existe sessão ativa para o veículo");

            parkingSession.ExitDate = DateTime.Now;
            parkingSession.UpdatedAt = DateTime.Now;


            var prices = await _priceRepository.GetPriceValidation(parkingSession.EntryDate, parkingSession.ExitDate ?? DateTime.Now) ?? throw new Exception("Não existe preço definido para essa data");
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

        /// Get all parking sessions


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

                    var getPricePerHour = await _priceRepository.GetPriceValidation(item.EntryDate, item.ExitDate ?? DateTime.Now) ?? throw new Exception("Não existe preço definido para essa data");
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

        /// Calculate hours based on duration


        public static int CalculateHours(TimeSpan duration)
        {
            if (duration.TotalMinutes <= 30)
            {
                return 0;
            }

            var hoursPaid = Math.Ceiling(duration.TotalMinutes / 70);
            return Convert.ToInt32(hoursPaid);
        }

        /// Calculate duration between entry date and exit date

        public static TimeSpan CalculateDuration(DateTime entryDate, DateTime? exitDate)
        {
            if (exitDate < entryDate || exitDate == entryDate || exitDate == null)
            {
                throw new Exception("Data de saída deve ser maior que a data de entrada.");
            }

            return exitDate.Value - entryDate;
        }


    }
}

