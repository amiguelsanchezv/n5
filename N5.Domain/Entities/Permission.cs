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
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
    }

    public class PermissionResponse : Permission
    {
        public string Permission { get; set; }
    }
}
