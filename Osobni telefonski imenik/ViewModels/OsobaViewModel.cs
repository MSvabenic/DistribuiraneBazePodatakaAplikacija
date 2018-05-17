using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Osobni_telefonski_imenik.Models;

namespace Osobni_telefonski_imenik.ViewModels
{
    public class OsobaViewModel
    {
        [Required(ErrorMessage = "Obvezno je unijeti ime kontakta!")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obvezno je unijeti prezime kontakta!")]
        public string Prezime { get; set; }

        [DisplayName("Grad")]
        [Required(ErrorMessage = "Obvezno je unijeti grad kontakta!")]
        public Guid GradID { get; set; }

        public string Opis { get; set; }

        public IEnumerable<Grad> Grad { get; set; }
    }
}