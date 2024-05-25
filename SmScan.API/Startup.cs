using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using SmScan.API.Abstractions.Behaviors;
using SmScan.API.AppDbContext.Productos;
using SmScan.API.AppDbContext.Usuarios;
using SmScan.API.Filters;
using SmScan.API.Infrastructure;
using System.Text.Json.Serialization;

namespace SmScan.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IHostBuilder builder)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(FilterException));
            }).AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmScanAPI", Version = "v1" }));

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<ProductosDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("DBConfiguration__SupermarketScan_Productos__cn")))
                    .AddDbContext<UsuariosDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("DBConfiguration__SupermarketScan_Usuarios__cn")))
                    ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmScanAPI v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
