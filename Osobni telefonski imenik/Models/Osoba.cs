using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.Models
{
    public class Osoba
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OsobaID { get; set; }

        public string UserID { get; set; }

        [DisplayName("Grad")]
        [Required(ErrorMessage = "Obvezno je unijeti grad kontakta!")]
        public Guid GradID { get; set; }

        [Required(ErrorMessage = "Obvezno je unijeti ime kontakta!")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Obvezno je unijeti prezime kontakta!")]
        public string Prezime { get; set; }

        public string Opis { get; set; }

        public virtual Grad Grad{ get; set; }
    }
}