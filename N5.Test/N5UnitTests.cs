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
        private readonly string _permissionUri = "/api/permission";
        private readonly string _permissionTypeUri = "/api/permissionType";

        public N5UnitTests(WebApplicationFactory<Startup> application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task CreatePermissionType_ShouldReturnOk_WhenValidData()
        {
            // Arrange
            var permissionType = new PermissionType { Description = "Standard User Role" };
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(permissionType)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await _client.PostAsync(_permissionTypeUri, byteContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var createdPermissionType = JsonConvert.DeserializeObject<PermissionType>(responseContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            createdPermissionType.ShouldNotBeNull();
            createdPermissionType.Id.ShouldBeGreaterThan(0);
            createdPermissionType.Description.ShouldBe("Standard User Role");
        }

        [Fact]
        public async Task CreatePermission_ShouldReturnOk_WhenValidData()
        {
            // Arrange
            var permission = new Permission
            {
                EmployeeName = "John",
                EmployeeLastName = "Doe",
                PermissionType = 1,
                PermissionDate = DateTime.Now.Date
            };
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(permission)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await _client.PostAsync(_permissionUri, byteContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var createdPermission = JsonConvert.DeserializeObject<Permission>(responseContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            createdPermission.ShouldNotBeNull();
            createdPermission.Id.ShouldBeGreaterThan(0);
            createdPermission.EmployeeName.ShouldBe("John");
            createdPermission.EmployeeLastName.ShouldBe("Doe");
            createdPermission.PermissionType.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreatePermission_ShouldReturnInternalServerError_WhenInvalidPermissionType()
        {
            // Arrange
            var permission = new Permission
            {
                EmployeeName = "Jane",
                EmployeeLastName = "Smith",
                PermissionType = -1, // Invalid permission type
                PermissionDate = DateTime.Now.Date
            };
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(permission)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await _client.PostAsync(_permissionUri, byteContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
            responseContent.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task UpdatePermission_ShouldReturnOk_WhenValidData()
        {
            // Arrange
            // First, create a permission to update
            var createPermission = new Permission
            {
                EmployeeName = "Test",
                EmployeeLastName = "User",
                PermissionType = 1,
                PermissionDate = DateTime.Now.Date
            };
            var createContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(createPermission)));
            createContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var createResponse = await _client.PostAsync(_permissionUri, createContent);
            var createdPermission = JsonConvert.DeserializeObject<Permission>(await createResponse.Content.ReadAsStringAsync());

            // Update the permission
            var updatePermission = new Permission
            {
                Id = createdPermission.Id,
                EmployeeName = createdPermission.EmployeeName,
                EmployeeLastName = createdPermission.EmployeeLastName,
                PermissionType = 2,
                PermissionDate = createdPermission.PermissionDate
            };
            var updateContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(updatePermission)));
            updateContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await _client.PutAsync(_permissionUri, updateContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedPermission = JsonConvert.DeserializeObject<Permission>(responseContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            updatedPermission.ShouldNotBeNull();
            updatedPermission.PermissionType.ShouldBe(2);
        }

        [Fact]
        public async Task UpdatePermission_ShouldReturnInternalServerError_WhenInvalidPermissionType()
        {
            // Arrange
            var permission = new Permission
            {
                Id = 1,
                PermissionType = -1 // Invalid permission type
            };
            var byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(permission)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            var response = await _client.PutAsync(_permissionUri, byteContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
            responseContent.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetAllPermissions_ShouldReturnOk_WithPermissionList()
        {
            // Act
            var response = await _client.GetAsync(_permissionUri);
            var responseContent = await response.Content.ReadAsStringAsync();
            var permissions = JsonConvert.DeserializeObject<List<PermissionResponse>>(responseContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            permissions.ShouldNotBeNull();
            permissions.Count.ShouldBeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task GetAllPermissionTypes_ShouldReturnOk_WithPermissionTypeList()
        {
            // Act
            var response = await _client.GetAsync(_permissionTypeUri);
            var responseContent = await response.Content.ReadAsStringAsync();
            var permissionTypes = JsonConvert.DeserializeObject<List<PermissionType>>(responseContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            permissionTypes.ShouldNotBeNull();
            permissionTypes.Count.ShouldBeGreaterThan(0);
        }
    }
}
