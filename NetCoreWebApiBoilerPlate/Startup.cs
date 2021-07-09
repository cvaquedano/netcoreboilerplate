using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Services;
using NetCoreWebApiBoilerPlate.Data.UnitsOfWork;
using System;
using Polly;

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
            services.AddCors();
            //services.AddHttpCacheHeaders();

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            services.Configure<ServiceSetting>(Configuration.GetSection(nameof(ServiceSetting)));

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExampleMasterService, ExampleMasterService>();
            services.AddScoped<IMasterStatusService, MasterStatusService>();
            services.AddScoped<IMasterDetailService, MasterDetailService>();
          
            services.AddScoped<IUnitOfWork, UnitOfWork>();
          

            services.AddDbContext<Data.Context>(options =>
            {
                // this configuration is store use secret manager.
                options.UseSqlServer(Configuration.GetConnectionString("BoilerPlateDBConnection"),                   

                    // Connection resiliency automatically retries failed database commands.
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    }
                    );
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetCoreWebApiBoilerPlate", Version = "v1" });
            });

            services.AddHttpClient<ExternalServiceClient>()
                .AddTransientHttpErrorPolicy( builder => builder.WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
                .AddTransientHttpErrorPolicy( builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(15)));

            services.AddHealthChecks()
                .AddCheck<ExternalEndpointHealthCheck>("OpenWeather");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
               
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCoreWebApiBoilerPlate v1"));

            app.UseHttpsRedirection();

            //Use to generate Etags on controllers response headers. 
            // use it with If-Match header in order to handling Concurrency
            //app.UseHttpCacheHeaders(); 

            app.UseRouting();

          

            //app.UseAuthorization();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
