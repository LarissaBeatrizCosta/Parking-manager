using Microsoft.EntityFrameworkCore;
using parking_manager.Data;
using parking_manager.Entity;
using parking_manager.Interfaces;

namespace parking_manager.Repository
{
    public class ParkingSessionsRepository(ApplicationDbContext context) : IParkingSessionsRepository
    {
        private readonly ApplicationDbContext _context = context;

        /// Create a new parking session in the database
        public async Task<ParkingSessionsEntity> CreateParkingSession(ParkingSessionsEntity parkingSession)
        {
            _context.Parking.Add(parkingSession);
            await _context.SaveChangesAsync();
            return parkingSession;
        }

        /// Get active parking session by plate
        public async Task<ParkingSessionsEntity?> GetActiveParkingSession(string plate)
        {
            var parkingSession = await _context.Parking.Where(session => session.VehicleId == plate && session.ExitDate == null).FirstOrDefaultAsync();
            return parkingSession;
        }

        ///Get parking session by id
        public async Task<ParkingSessionsEntity?> GetParkingSessionById(int id)
        {
            var parkingSession = await _context.Parking.Where(session => session.Id == id).FirstOrDefaultAsync();
            return parkingSession;
        }

        /// Update parking session
        public async Task<ParkingSessionsEntity> UpdateParkingSession(ParkingSessionsEntity parkingSession)
        {
            _context.Parking.Update(parkingSession);
            await _context.SaveChangesAsync();
            return parkingSession;
        }

        /// Get all parking sessions
        public async Task<IEnumerable<ParkingSessionsEntity>> GetParkingSessions()
        {
            return await _context.Parking.ToListAsync();
            
        }
    }
}