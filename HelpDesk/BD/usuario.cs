//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelpDesk.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class usuario
    {
        public int id { get; set; }
        public string username { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public Nullable<int> idPerfil { get; set; }
    
        public virtual perfil perfil { get; set; }
    }
}
