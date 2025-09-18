using parking_manager.Entity;

namespace parking_manager.Interfaces
{
    public interface IParkingSessionsRepository
    {
        Task<ParkingSessionsEntity> CreateParkingSession(ParkingSessionsEntity parkingSession);
        Task<ParkingSessionsEntity?> GetActiveParkingSession(string plate);

        Task<ParkingSessionsEntity?> GetParkingSessionById(int id);

        Task<IEnumerable<ParkingSessionsEntity>> GetParkingSessions();

        Task<ParkingSessionsEntity> UpdateParkingSession(ParkingSessionsEntity parkingSession);


    }
}