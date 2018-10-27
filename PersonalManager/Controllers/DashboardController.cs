using Microsoft.AspNet.Identity;
using PersonalManager.Models;
using System.Linq;
using System.Web.Mvc;

namespace PersonalManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var doa = _context.Doas
                   .Where(d => d.UserId == userId)
                   .ToList();
            return View(doa);
        }
    }
}