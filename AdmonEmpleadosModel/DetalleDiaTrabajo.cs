//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdmonEmpleadosModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleDiaTrabajo
    {
        public int id { get; set; }
        public int idEmpleado { get; set; }
        public int idDiaTrabajo { get; set; }
    
        public virtual DiaTrabajo DiaTrabajo { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
