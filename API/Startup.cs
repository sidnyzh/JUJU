using API.Middlewares.GlobalExceptionMiddleware;
using Application.DTO.FluentValidations;
using Application.Interfaces;
using Application.Main;
using Application.Mapping;
using AutoMapper;
using Domain.Core;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Data;
using Repository.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace ProjectAPI.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Servicios                      
            services.AddScoped<ICustomerDomain, CustomerDomain>();
            services.AddScoped<IGenericRepositoriy<Customer>, GenericRepository<Customer>>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerApplication, CustomerApplication>();

            services.AddScoped<IPostDomain, PostDomain>();
            services.AddScoped<IGenericRepositoriy<Post>, GenericRepository<Post>>();
            services.AddScoped<IPostApplication, PostApplication>();

            services.AddAutoMapper(typeof(MappingProfile));



            //Agregar cadena de conexion al contexto
            services.AddDbContext<JujuTestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Development")));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePostRequestValidator>())
                ;


            // ======== CONFIGURACIÓN DE SWAGGER =========
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TestAPI", Version = "v1" });
            });
            services.AddHttpContextAccessor();
            services.AddSession();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>(); 

            // ======== CONFIGURACIÓN DE SWAGGER =========
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "TestAPI v1");
            });

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();


            app.UseSession();
            app.UseMvc();
        }
    }
}