using Microsoft.AspNetCore.Mvc.Testing;
using N5.Domain;
using N5.WebApi;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace N5.Test
{
    public class N5UnitTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly string uri = "/api/permission";
        private readonly string uriType = "/api/permissionType";

        public N5UnitTests(WebApplicationFactory<Startup> application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task Test_0_RequestPermissionType_OK()
        {
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new PermissionType() { Descripcion = "Rol Usuario Normal" })));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync(uriType, byteContent);
            var permissionType = JsonConvert.DeserializeObject<PermissionType>(await response.Content.ReadAsStringAsync());
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            permissionType.Id.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Test_1_RequestPermission_OK()
        {
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Permission() { NombreEmpleado = "Miguel", ApellidoEmpleado = "Sánchez", TipoPermiso = 1, FechaPermiso = DateTime.Now })));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync(uri, byteContent);
            var permission = JsonConvert.DeserializeObject<Permission>(await response.Content.ReadAsStringAsync());
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            permission.TipoPermiso.ShouldBeLessThan(5);
        }

        [Fact]
        public async Task Test_2_RequestPermission_Failed()
        {
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Permission() { NombreEmpleado = "Miguel", ApellidoEmpleado = "Sánchez", TipoPermiso = -1, FechaPermiso = DateTime.Now })));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync(uri, byteContent);
            var permission = JsonConvert.DeserializeObject<Permission>(await response.Content.ReadAsStringAsync());
            response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
            permission.NombreEmpleado.ShouldBeNull();
        }

        [Fact]
        public async Task Test_3_ModifyPermission_OK()
        {
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Permission() { Id = 1, TipoPermiso = 2 })));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PutAsync(uri, byteContent);
            var permission = JsonConvert.DeserializeObject<Permission>(await response.Content.ReadAsStringAsync());
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            permission.TipoPermiso.ShouldBeLessThan(5);
        }

        [Fact]
        public async Task Test_4_ModifyPermission_Failed()
        {
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Permission() { Id = 1, TipoPermiso = -1 })));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PutAsync(uri, byteContent);
            var permission = JsonConvert.DeserializeObject<Permission>(await response.Content.ReadAsStringAsync());
            response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
            permission.NombreEmpleado.ShouldBeNull();
        }

        [Fact]
        public async Task Test_5_GetPermissions_OK()
        {
            var response = await _client.GetAsync(uri);
            var permissions = JsonConvert.DeserializeObject<List<Permission>>(await response.Content.ReadAsStringAsync());
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            permissions.Count.ShouldBeGreaterThan(0);
        }
    }
}
