using Microsoft.EntityFrameworkCore;
using parking_manager.Models;

namespace parking_manager.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<VehiclesEntity> Vehicles { get; set; }
    }
}
