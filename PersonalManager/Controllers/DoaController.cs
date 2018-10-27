using Microsoft.AspNet.Identity;
using PersonalManager.Models;
using PersonalManager.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PersonalManager.Controllers
{
    public class DoaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoaController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Doa
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult CreateDoa()
        {
            var viewModel = new DoaViewModel
            {
                Heading = "Create"
            };

            return View("DoaForm", viewModel);
        }

        [Authorize]
        public ActionResult UpdateDoa(int id)
        {
            var doa = _context.Doas.Single(d => d.Id == id);
            var doaViewModel = new DoaViewModel
            {
                Title = doa.Title,
                Id = doa.Id,
                Description = doa.Description,
                Heading = "Update"
            };
            return View("DoaForm", doaViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoa(DoaViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View("DoaForm", viewModel);
            }

            var doa = new Doa
            {
                Title = viewModel.Title,
                UserId = userId,
                Description = viewModel.Description,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            _context.Doas.Add(doa);
            _context.SaveChanges();

            return RedirectToAction("Index", "Dashboard");
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDoa(DoaViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View("CreateDoa", viewModel);
            }

            var doa = _context.Doas
                .Single(d => d.Id == viewModel.Id && d.UserId == userId);

            doa.Title = viewModel.Title;
            doa.ModifiedDate = DateTime.Now;
            doa.Description = viewModel.Description;

            _context.SaveChanges();

            return RedirectToAction("Index", "Dashboard");
        }
    }
}