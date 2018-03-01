using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{

    public class Etiqueta
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public string valor { get; set; }
        [Required]
        public string uuid_idioma { get; set; }
        [Required]
        public string uuid_registro { get; set; }
        [Required]
        public string identificador { get; set; }
        
    }
}