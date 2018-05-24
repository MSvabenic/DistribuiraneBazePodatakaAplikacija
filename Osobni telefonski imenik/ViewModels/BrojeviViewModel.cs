using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Osobni_telefonski_imenik.Models;

namespace Osobni_telefonski_imenik.ViewModels
{
    public class BrojeviViewModel
    {
        public Guid BrojID { get; set; }

        [DisplayName("Osoba")]
        [Required(ErrorMessage = "Obvezno je odabrati osobu!")]
        public Guid OsobaID { get; set; }

        [DisplayName("Tip broja telefona")]
        [Required(ErrorMessage = "Obvezno je odabrati tip broja!")]
        public Guid BrojTipID { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Dozvoljen je samo unos brojeva!")]
        [Required(ErrorMessage = "Obvezno je unijeti broj!")]
        public string Broj { get; set; }

        [DisplayName("Opis broja")]
        public string OpisBroja { get; set; }

        public IEnumerable<Osoba> Osoba { get; set; }

        public IEnumerable<BrojTip> BrojTip { get; set; }
    }
}