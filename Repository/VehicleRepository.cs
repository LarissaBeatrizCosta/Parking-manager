using System.Collections;
using Microsoft.EntityFrameworkCore;
using parking_manager.Data;
using parking_manager.Models;
using parking_manager.Repositories;

namespace parking_manager.Repository
{
    public class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<VehiclesEntity> CreateVehicle(VehiclesEntity vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<IEnumerable<VehiclesEntity>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }


        public async Task<VehiclesEntity?> GetVehicleById(string plate)
        {
            var vehicle = await _context.Vehicles.Where(v => v.Plate == plate).FirstOrDefaultAsync();
            return vehicle;
        }


    }
}