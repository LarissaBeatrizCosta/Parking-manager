using Microsoft.AspNetCore.Mvc;
using parking_manager.Data;

namespace parking_manager.Controllers
{
    [Route("[vehicles]")]
    public class VehiclesController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;
    }
}