using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5.Domain
{
    [Table("PermissionTypes")]
    public sealed class PermissionType
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}