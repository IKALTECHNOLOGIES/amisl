//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ONG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class idioma
    {
        public idioma()
        {
            this.etiqueta = new HashSet<etiqueta>();
        }
    
        public string UUID { get; set; }
        public string NOMBRE { get; set; }
        public string ABREVIATURA { get; set; }
    
        public virtual ICollection<etiqueta> etiqueta { get; set; }
    }
}
