using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using N5.Application;
using N5.Infrastructure;

namespace N5.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var db = Configuration.GetConnectionString("N5DB");
            var createPermission = typeof(CreatePermission).Assembly;
            var createPermissionType = typeof(CreatePermissionType).Assembly;
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.AddApplication();
            services.AddInfrastructure();
            services.AddScoped<IElasticSearchService, ElasticSearchService>();
            services.AddScoped<IKafkaService, KafkaService>();
            services.AddScoped<IOperations, Operations>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
            services.AddDbContext<PermissionDbContext>(p => p.UseSqlServer(db));
            services.AddDbContext<PermissionTypeDbContext>(p => p.UseSqlServer(db));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(createPermission));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(createPermissionType));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "N5.WebApi", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "N5.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
