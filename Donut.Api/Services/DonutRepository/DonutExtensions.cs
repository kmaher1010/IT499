using Donut.Api.App;
using Microsoft.EntityFrameworkCore;

namespace Library.WebApi.Services.DonutRepository {
    public static class DonutExtensions {
        public static void AddDonutServices(this IServiceCollection services, AppSettings settings) {

            services.AddDbContext<DonutDbContext>(optionsAction => {
                var connectionString = settings.DonutsConnectionString;
                optionsAction.UseSqlServer(connectionString);
                optionsAction.LogTo(Console.WriteLine, LogLevel.Information);
            });

            services.AddScoped<IDonutRepository,DonutRepository>();
        }
    }
}
