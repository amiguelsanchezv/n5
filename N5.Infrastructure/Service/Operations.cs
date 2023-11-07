using MediatR;
using N5.Application;
using N5.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5.Infrastructure
{
    public class Operations : IOperations
    {
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IKafkaService _kafkaService;
        public Operations(IElasticSearchService elasticSearchService, IKafkaService kafkaService)
        {
            _elasticSearchService = elasticSearchService;
            _kafkaService = kafkaService;
        }

        public async Task<ICollection<PermissionResponse>> GetPermissions(IMediator mediator)
        {
            await _kafkaService.WriteKafka("get");
            await _elasticSearchService.CheckIndex();
            var permissionsResponse = new List<PermissionResponse>();
            var permissions = await mediator.Send(new GetAllPermissions());
            var permissionTypes = await mediator.Send(new GetAllPermissionTypes());
            Parallel.ForEach(permissions, p =>
            {
                permissionsResponse.Add(new PermissionResponse() { Id = p.Id, NombreEmpleado = p.NombreEmpleado, ApellidoEmpleado = p.NombreEmpleado, FechaPermiso = p.FechaPermiso, TipoPermiso = p.TipoPermiso, Permiso = permissionTypes.FirstOrDefault(pt => pt.Id.Equals(p.TipoPermiso)).Descripcion });
            });

            if (permissions.Count > 0)
            {
                await _elasticSearchService.InsertBulkDouments(permissions);
            }
            return permissionsResponse;
        }

        public async Task<ICollection<PermissionType>> GetPermissionTypes(IMediator mediator)
        {
            return await mediator.Send(new GetAllPermissionTypes()); ;
        }

        public async Task<Permission> AddPermission(IMediator mediator, Permission permission)
        {
            await _kafkaService.WriteKafka("request");
            await _elasticSearchService.CheckIndex();
            var permissionType = await mediator.Send(new GetPermissionTypeById { Id = permission.TipoPermiso });
            if (permissionType == null)
            {
                throw new Exception("El tipo de permiso no está parametrizado.", new Exception("No se ha creado el tipo de permiso."));
            }
            permission = await mediator.Send(new CreatePermission { NombreEmpleado = permission.NombreEmpleado, ApellidoEmpleado = permission.ApellidoEmpleado, TipoPermiso = permission.TipoPermiso, FechaPermiso = permission.FechaPermiso });
            await _elasticSearchService.InsertDocument(permission);
            return permission;
        }
        public async Task<Permission> ModifyPermission(IMediator mediator, Permission permission)
        {
            await _kafkaService.WriteKafka("modify");
            await _elasticSearchService.CheckIndex();
            var permissionType = await mediator.Send(new GetPermissionTypeById { Id = permission.TipoPermiso });
            if (permissionType == null)
            {
                throw new Exception("El tipo de permiso no está parametrizado.", new Exception("No se ha creado el tipo de permiso."));
            }
            permission = await mediator.Send(new UpdatePermission { Id = permission.Id, TipoPermiso = permission.TipoPermiso, FechaPermiso = permission.FechaPermiso });
            await _elasticSearchService.InsertDocument(permission);
            return permission;
        }
        public async Task<PermissionType> AddPermissioType(IMediator mediator, PermissionType permissionType)
        {
            permissionType = await mediator.Send(new CreatePermissionType { Descripcion = permissionType.Descripcion });
            return permissionType;
        }
    }
}
