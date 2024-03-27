using Donut.Api.App;
using Library.WebApi.Services.DonutRepository;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;


namespace Donut.Api {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Donut.Api", Version = "v1" });
            });

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            builder.Services.AddDonutServices(builder.Configuration.GetSection("AppSettings").Get<AppSettings>());

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}