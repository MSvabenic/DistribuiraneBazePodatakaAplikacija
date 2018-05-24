using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Osobni_telefonski_imenik.Models
{
    public class Drzava
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DrzavaID { get; set; }

        [Required(ErrorMessage = "Obvezno je unijeti naziv države!")]
        public string Naziv { get; set; }
    }
}