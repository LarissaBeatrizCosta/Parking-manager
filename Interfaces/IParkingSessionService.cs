using parking_manager.DTO;

namespace parking_manager.Interfaces
{
    public interface IParkingSessionService
    {
        Task<ParkingSessionsDTO> CreateParkingSession(string plate);

        Task<ParkingSessionsDTO> FinalizeParkingSession(string plate);

        Task<IEnumerable<ParkingSessionsDTO>> GetParkingSessions();
    }

}