using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONG.Entities
{
    public class Usuario
    {
        [Key]
        public string uuid  { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellidos { get; set; }
        [Required]
        public string usuario1 { get; set; }
        [Required]
        public string tel1 { get; set; }
        
        public string tel2 { get; set; }
        
        public string tel3 { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string dir1 { get; set; }
        
        public string dir2 { get; set; }
        
        public string dir3 { get; set; }
        [Required]
        public string nivel { get; set; }
        [Required]
        public string clave { get; set; }
    }
}