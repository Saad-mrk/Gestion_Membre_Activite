using System.ComponentModel.DataAnnotations;

namespace Gestion_des_membres_et_activités_d_un_club.Models
{
    public class Activite
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de l'activité est obligatoire.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Veuillez fournir une description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La date est obligatoire.")]
        [DataType(DataType.Date)]
        public DateTime DateActivite { get; set; }

        // Relation : Une activité peut avoir plusieurs inscriptions
        public virtual ICollection<Inscription> Inscriptions { get; set; }
    }
}
