
using TiendaAPI.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


using Amazon.Runtime.Internal.Settings;
using System.Configuration;
using Microsoft.Extensions.Options;
using TiendaAPI.Services;

namespace TiendaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // configuracion personalizada
            builder.Services.Configure<TiendaSettings>
                (builder.Configuration.GetSection(nameof(TiendaSettings)));

            builder.Services.AddSingleton<ITiendaSettings>
                (a => a.GetRequiredService<IOptions<TiendaSettings>>().Value);

            builder.Services.AddSingleton<ProductService>();

            builder.Services.AddControllers();
            
            // fin de config

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
