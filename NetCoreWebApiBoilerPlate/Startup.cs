using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Repositories;
using NetCoreWebApiBoilerPlate.Services;
using System;

namespace NetCoreWebApiBoilerPlate
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
            services.AddHttpCacheHeaders();
            services.AddCors();
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExampleMasterService, ExampleMasterService>();
            services.AddScoped<IMasterStatusService, MasterStatusService>();
            services.AddScoped<IMasterDetailService, MasterDetailService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExampleMasterRepository, ExampleMasterRepository>();
            services.AddScoped<IMasterStatusRepository, MasterStatusRepository>();
            services.AddScoped<IMasterDetailRepository, MasterDetailRepository>();

            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(
                    @"Server=localhost\SQLEXPRESS;Database=NetCoreWebApiBoilerPlate;Trusted_Connection=True;");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetCoreWebApiBoilerPlate", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCoreWebApiBoilerPlate v1"));
            }

            app.UseHttpsRedirection();

            //Use to generate Etags on controllers response headers. 
            // use it with If-Match header in order to handling Concurrency
            app.UseHttpCacheHeaders(); 

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            //app.UseAuthorization();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
