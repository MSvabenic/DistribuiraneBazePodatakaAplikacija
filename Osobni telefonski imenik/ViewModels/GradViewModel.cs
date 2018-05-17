using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Osobni_telefonski_imenik.Models;

namespace Osobni_telefonski_imenik.ViewModels
{
    public class GradViewModel
    {
        [Required(ErrorMessage = "Obvezno je unijeti naziv grada!")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Obvezno je odabrati državu!")]
        [DisplayName("Država")]
        public Guid DrzavaID { get; set; }

        public IEnumerable<Drzava>  Drzava { get; set; }
    }
}