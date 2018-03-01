using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{

    public class Gasto
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public string identificador { get; set; }
        [Required]
        public string uuid_ctacontable { get; set; }
        [Required]
        public string uuid_tipo { get; set; }
        [Required]
        public string nivel { get; set; }
        
        
    }
}