using Microsoft.EntityFrameworkCore;
using parking_manager.Entity;

namespace parking_manager.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<VehiclesEntity> Vehicles { get; set; }
        public DbSet<PricesEntity> Prices { get; set; }
        public DbSet<ParkingSessionsEntity> Parking { get; set; }

    }
}
