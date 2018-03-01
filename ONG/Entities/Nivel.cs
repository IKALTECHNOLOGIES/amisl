using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{
    public class Nivel
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public string nivel1 { get; set; }
        [Required]
        public string identificador { get; set; }
        [Required]
        public string superior { get; set; }
    }
}