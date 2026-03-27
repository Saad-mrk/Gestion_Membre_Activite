using Gestion_des_membres_et_activités_d_un_club.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_des_membres_et_activités_d_un_club.Controllers
{
    public class ActivitesController : Controller
    {
        private readonly ClubContext _context;

        public ActivitesController(ClubContext context)
        {
            _context = context;
        }

        // GET: Activites
        public IActionResult Index()
        {
            var activites = _context.Activites.ToList();
            return View(activites);
        }

        // GET: Activites/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Activite activite)
        {
            // On ignore l'Id (généré par SQL) et la liste des inscriptions (vide au départ)
            ModelState.Remove("Id");
            ModelState.Remove("Inscriptions");

            if (ModelState.IsValid)
            {
                activite.DateActivite = DateTime.SpecifyKind(
                    activite.DateActivite,
                    DateTimeKind.Utc
                );
                _context.Activites.Add(activite);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirige vers la liste des activités
            }

            return View(activite);
        }

        // GET: Activites/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var activite = _context.Activites.Find(id);
            if (activite == null) return NotFound();
            return View(activite);
        }

        // POST: Activites/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Activite activite)
        {
            if (id != activite.Id) return NotFound();
            ModelState.Remove("Inscriptions");
            if (ModelState.IsValid)
            {
                activite.DateActivite = DateTime.SpecifyKind(
                    activite.DateActivite,
                    DateTimeKind.Utc
                );
                _context.Update(activite);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(activite);
        }

        // POST: Activites/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var activite = _context.Activites.Find(id);
            if (activite != null)
            {
                _context.Activites.Remove(activite);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }

}