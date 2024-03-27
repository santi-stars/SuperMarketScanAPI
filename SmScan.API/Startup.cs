using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SmScan.API.AppDbContext.Productos;
using SmScan.API.AppDbContext.Usuarios;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmScanAPI", Version = "v1" }));

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

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
