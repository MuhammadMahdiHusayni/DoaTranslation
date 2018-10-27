using Microsoft.AspNet.Identity;
using PersonalManager.Models;
using PersonalManager.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PersonalManager.Controllers
{
    public class DoaTextController : Controller
    {

        private readonly ApplicationDbContext _context;

        public DoaTextController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult ViewText(int id)
        {
            var userId = User.Identity.GetUserId();
            var doa = _context.Doas
                .Include(d => d.DoaTexts)
                .Single(d => d.Id == id && d.UserId == userId);

            return View(doa);
        }

        [Authorize]
        public ActionResult AddText(int id)
        {
            var userId = User.Identity.GetUserId();

            var doa = _context.Doas
                .Single(d => d.Id == id && d.UserId == userId);

            var text = _context.DoaTexts
                .Where(dt => dt.DoaId == doa.Id)
                .Select(t => new DoaTextViewModel
                {
                    Id = t.Id,
                    TempId = Guid.NewGuid(),
                    Arabic = t.Arabic,
                    MalayTranslation = t.MalayTranslation
                })
                .ToList();

            var doaTextForm = new DoaTextFormViewModel
            {
                DoaId = doa.Id,
                DoaTitle = doa.Title,
                DoaTexts = text
            };

            var deleteDoaTextFormViewModel = new DeleteDoaTextFormViewModel
            {
                doaTextFormViewModel = doaTextForm
            };

            return View("DoaTextForm", deleteDoaTextFormViewModel);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddText(DeleteDoaTextFormViewModel viewModel, string answer)
        {
            if (answer == "Submit")
            {
                foreach (var item in viewModel.doaTextFormViewModel.DoaTexts)
                {
                    if (item.Id.HasValue)
                    {
                        var doaText = _context.DoaTexts.FirstOrDefault(dt => dt.Id == item.Id);
                        doaText.Arabic = item.Arabic;
                        doaText.MalayTranslation = item.MalayTranslation;
                    }
                    else
                    {
                        var dt = new DoaText
                        {
                            DoaId = viewModel.doaTextFormViewModel.DoaId,
                            Arabic = item.Arabic,
                            MalayTranslation = item.MalayTranslation
                        };
                        var context = _context.DoaTexts.Add(dt);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction("ViewText", new { id = viewModel.doaTextFormViewModel.DoaId });

                // return RedirectToAction("Index", "Dashboard");
            }

            if (answer == "Add New Line")
            {
                if (viewModel.doaTextFormViewModel.DoaTexts == null)
                {
                    viewModel.doaTextFormViewModel.DoaTexts = new System.Collections.Generic.List<DoaTextViewModel>();
                }

                viewModel.doaTextFormViewModel.DoaTexts.Add(new DoaTextViewModel
                {
                    TempId = Guid.NewGuid(),
                    Arabic = "",
                    MalayTranslation = ""
                });

                return View("DoaTextForm", viewModel);
            }

            // TODO: why delete no 1 then delete no 2 still no 1 is there?
            if (answer.Substring(0, 6) == "Remove")
            {
                var doaTextItem = viewModel.doaTextFormViewModel.DoaTexts
                    .FirstOrDefault(d => d.TempId.ToString() == answer.Substring(7, 36));

                if (doaTextItem.Id.HasValue)
                {
                    var doaText = _context.DoaTexts.Find(doaTextItem.Id);
                    viewModel.doaTextFormViewModel.DoaTexts.Remove(doaTextItem);
                    _context.DoaTexts.Remove(doaText);
                }
                else
                {
                    viewModel.doaTextFormViewModel.DoaTexts.Remove(doaTextItem);
                    // viewModel.doaTextFormViewModel.DoaTexts.RemoveAll(d => d.TempId.ToString() == answer.Substring(7, 36));
                }
                _context.SaveChanges();
                return View("DoaTextForm", viewModel);
            }

            return View("DoaTextForm", viewModel);

        }
    }
}