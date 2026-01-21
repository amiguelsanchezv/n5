using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;

namespace N5.Domain
{
    public static class Mapping
    {
        public static TypeMappingDescriptor<Permission> PermissionsMapping(this TypeMappingDescriptor<Permission> descriptor)
        {
            return descriptor
                .Properties(properties => properties
                    .Keyword("Id")
                    .Text("EmployeeName")
                    .Text("EmployeeLastName")
                    .Date("PermissionDate")
                );
        }
    }
}
