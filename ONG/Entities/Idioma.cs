using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{

    public class Idioma
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string abreviatura { get; set; }
        
    }
}