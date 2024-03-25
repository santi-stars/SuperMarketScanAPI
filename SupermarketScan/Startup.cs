using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SupermarketScanAPI.AppDbContext.Products;

namespace SupermarketScanAPI
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SupermarketScanAPI", Version = "v1" }));

            services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("DBConfiguration__SupermarketScan_Products__cn")))
                    //.AddDbContext<UsersDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("DBConfiguration__SupermarketScan_Users__cn")))
                    ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SupermarketScanAPI v1"));
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
