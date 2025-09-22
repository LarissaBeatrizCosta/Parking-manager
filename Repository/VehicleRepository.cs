using Microsoft.EntityFrameworkCore;
using parking_manager.Data;
using parking_manager.Entity;
using parking_manager.Interfaces;

namespace parking_manager.Repository
{
    public class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
    {
        private readonly ApplicationDbContext _context = context;

        /// Create a new vehicle in the database
        public async Task<VehiclesEntity> CreateVehicle(VehiclesEntity vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        /// Get all vehicles in the database

        public async Task<IEnumerable<VehiclesEntity>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        /// Get vehicle by id in the database
        public async Task<VehiclesEntity?> GetVehicleById(string plate)
        {
            var vehicle = await _context.Vehicles.Where(v => v.Plate == plate).FirstOrDefaultAsync();
            return vehicle;
        }


    }
}