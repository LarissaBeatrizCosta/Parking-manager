using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using parking_manager.Data;
using parking_manager.Models;
using parking_manager.Repositories;

namespace parking_manager.Repository
{
    public class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<VehiclesEntity> CreateVehicleAsync(VehiclesEntity vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }
    }
}