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
    
    public partial class nivel_usuario
    {
        public string UUID { get; set; }
        public string UUID_NIVEL { get; set; }
        public string UUID_USUARIO { get; set; }
    
        public virtual nivel nivel { get; set; }
    }
}
