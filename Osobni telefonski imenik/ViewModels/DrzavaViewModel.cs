using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.ViewModels
{
    public class DrzavaViewModel
    {
        [Required(ErrorMessage = "Obvezno je unijeti naziv države!")]
        public string Naziv { get; set; }
    }
}