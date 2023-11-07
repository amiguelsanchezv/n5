using Nest;

namespace N5.Domain
{
    public static class Mapping
    {
        public static CreateIndexDescriptor PermissionsMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<Permission>(p => p.Properties(pp => pp
                .Keyword(k => k.Name(p => p.Id))
                .Text(t => t.Name(p => p.NombreEmpleado))
                .Text(t => t.Name(p => p.ApellidoEmpleado))
                .Number(n => n.Name(p => p.TipoPermiso))
                .Date(d => d.Name(p => p.FechaPermiso))
            ));
        }
    }
}
