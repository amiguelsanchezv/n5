using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Domain
{
    [Table("Permissions")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
    }

    public class PermissionResponse : Permission
    {
        public string Permiso { get; set; }
    }
}
