using Gestion_des_membres_et_activités_d_un_club.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_des_membres_et_activités_d_un_club.Controllers
{
    public class MembresController : Controller
    {
        private readonly ClubContext _context;

        // Injection du contexte de base de données
        public MembresController(ClubContext context)
        {
            _context = context;
        }

        // GET: Membres
        public IActionResult Index()
        {
            var membres = _context.Membres.ToList();
            return View(membres);
        }

        // GET: Membres/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Membre membre)
        {
            // On dit au système d'arrêter de chercher des erreurs sur l'Id et les Inscriptions
            // car c'est normal qu'ils soient vides à la création !
            ModelState.Remove("Id");
            ModelState.Remove("Inscriptions");

            // 1. Maintenant, la validation devrait passer au vert !
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Membres.Add(membre);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    string erreurSql = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    ModelState.AddModelError("", "🛑 ERREUR SQL SERVER : " + erreurSql);
                }
            }
            else
            {
                // CE CODE VA AFFICHER EXACTEMENT LE CHAMP QUI BLOQUE
                var erreurs = ModelState.Where(x => x.Value.Errors.Count > 0).ToList();
                foreach (var erreur in erreurs)
                {
                    ModelState.AddModelError("", $"❌ Le champ '{erreur.Key}' bloque : {erreur.Value.Errors.First().ErrorMessage}");
                }
            }

        return View(); // ou return View(membre); / return View(inscription);
    }

    // GET: Membres/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null) return NotFound();
        var membre = _context.Membres.Find(id);
        if (membre == null) return NotFound();
        return View(membre);
    }

    // POST: Membres/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Membre membre)
    {
        if (id != membre.Id) return NotFound();
        ModelState.Remove("Inscriptions");
        if (ModelState.IsValid)
        {
            _context.Update(membre);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(membre);
    }

    // POST: Membres/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var membre = _context.Membres.Find(id);
        if (membre != null)
        {
            _context.Membres.Remove(membre);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}

}