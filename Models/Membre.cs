using System.ComponentModel.DataAnnotations;

namespace Gestion_des_membres_et_activités_d_un_club.Models
{
    public class Membre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        public string Email { get; set; }

        // Relation : Un membre peut avoir plusieurs inscriptions
        public virtual ICollection<Inscription> Inscriptions { get; set; }
    }
}