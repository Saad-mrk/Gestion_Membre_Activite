using System.ComponentModel.DataAnnotations;

namespace Gestion_des_membres_et_activités_d_un_club.Models
{
    public class Inscription
    {
        public int Id { get; set; }

        [Required]
        public int MembreId { get; set; }

        [Required]
        public int ActiviteId { get; set; }

        public DateTime DateInscription { get; set; } = DateTime.Now;

        public virtual Membre Membre { get; set; }
        public virtual Activite Activite { get; set; }
    }
}