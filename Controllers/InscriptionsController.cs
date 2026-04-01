using Gestion_des_membres_et_activités_d_un_club.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gestion_des_membres_et_activités_d_un_club.Controllers
{
    public class InscriptionsController : Controller
    {
        private readonly ClubContext _context;

        public InscriptionsController(ClubContext context)
        {
            _context = context;
        }

        // GET: Inscriptions
        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var inscriptions = _context.Inscriptions
                .Include(i => i.Membre)
                .Include(i => i.Activite)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                inscriptions = inscriptions.Where(s => s.Membre.Nom.Contains(searchString) 
                                                 || s.Membre.Prenom.Contains(searchString) 
                                                 || s.Activite.Nom.Contains(searchString));
            }

            return View(inscriptions.ToList());
        }

        // GET: Inscriptions/Create
        public IActionResult Create()
        {
            // Prépare les listes déroulantes pour la vue
            ViewBag.MembreId = new SelectList(_context.Membres.ToList(), "Id", "Nom");
            ViewBag.ActiviteId = new SelectList(_context.Activites.ToList(), "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inscription inscription)
        {
            // On ignore l'Id et les objets de navigation
            ModelState.Remove("Id");
            ModelState.Remove("Membre");
            ModelState.Remove("Activite");

            // Si vous n'avez pas mis de champ DateInscription dans le formulaire,
            // on peut l'ajouter automatiquement ici au moment de la création :
            if (inscription.DateInscription == default)
            {
                inscription.DateInscription = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                _context.Inscriptions.Add(inscription);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirige vers la liste des inscriptions
            }

            // Si la validation échoue, il faut recharger les listes déroulantes (ViewBag)
            // ViewBag.MembreId = new SelectList(_context.Membres, "Id", "Nom", inscription.MembreId);
        // ViewBag.ActiviteId = new SelectList(_context.Activites, "Id", "Nom", inscription.ActiviteId);

        return View(inscription);
    }

    // GET: Inscriptions/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var inscription = _context.Inscriptions.Find(id);
        if (inscription == null) return NotFound();
        
        ViewBag.MembreId = new SelectList(_context.Membres.ToList(), "Id", "Nom", inscription.MembreId);
        ViewBag.ActiviteId = new SelectList(_context.Activites.ToList(), "Id", "Nom", inscription.ActiviteId);
        return View(inscription);
    }

    // POST: Inscriptions/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Inscription inscription)
    {
        if (id != inscription.Id) return NotFound();
        ModelState.Remove("Membre");
        ModelState.Remove("Activite");
        
        if (ModelState.IsValid)
        {
            _context.Update(inscription);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.MembreId = new SelectList(_context.Membres.ToList(), "Id", "Nom", inscription.MembreId);
        ViewBag.ActiviteId = new SelectList(_context.Activites.ToList(), "Id", "Nom", inscription.ActiviteId);
        return View(inscription);
    }

    // POST: Inscriptions/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var inscription = _context.Inscriptions.Find(id);
        if (inscription != null)
        {
            _context.Inscriptions.Remove(inscription);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}

}