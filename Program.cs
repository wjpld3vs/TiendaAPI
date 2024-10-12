
using TiendaAPI.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


using Amazon.Runtime.Internal.Settings;
using System.Configuration;
using Microsoft.Extensions.Options;
using TiendaAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TiendaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            ////////// Configuracion personalizada Singleton

            var key = builder.Configuration
                    .GetSection(nameof(TiendaSettings))
                    .GetSection("Token").Value;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "https://localhost",
                            ValidAudience = "https://localhost",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                        };
                    }
                );

            builder.Services.Configure<TiendaSettings>
                (builder.Configuration.GetSection(nameof(TiendaSettings)));

            builder.Services.AddSingleton<ITiendaSettings>
                (a => a.GetRequiredService<IOptions<TiendaSettings>>().Value);

            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<CustomerService>();
            builder.Services.AddSingleton<SupplierService>();
            builder.Services.AddSingleton<ProductCategoryService>();
            builder.Services.AddSingleton<UserService>();

            builder.Services.AddControllers();

            ////////// Fin de Configuracion personalizada Singleton

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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
