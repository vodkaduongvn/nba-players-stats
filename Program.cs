
using Microsoft.EntityFrameworkCore;
using NBA.Players.Charts.Jobs;
using NBA.Players.Charts.Models;
using NBA.Players.Charts.PlayerDbContext;
using NBA.Players.Charts.Services;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using NBA.Players.Charts.Extensions;

namespace NBA.Players.Charts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
          
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddHttpClient("MyHttpClient")
                .AddPolicyHandler(ServiceExtension.GetRetryPolicy())
                .AddPolicyHandler(ServiceExtension.GetCircuitBreakerPolicy());

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddHostedService<PlayerStatsBackgroundService>();
            builder.Services.AddHostedService<GameStatsBackgroundService>();
            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // Allow specific origin
                          .AllowAnyHeader()                    // Allow any headers
                          .AllowAnyMethod();                   // Allow any HTTP methods
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Use CORS
            app.UseCors("AllowSpecificOrigins");

            app.MapControllers();
            //app.MapGet("/api/players-stats", async (HttpClient httpClient) =>
            //{
            //    var response = await httpClient.GetAsync("https://vn.global.nba.com/stats2/player/stats.json?playerCode=trae_young&ds=profile&locale=vn");

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var content = await response.Content.ReadAsStringAsync();
            //        var playerStats = JsonSerializer.Deserialize<PlayerStats>(content);

            //        return Results.Ok(new[] { playerStats });
            //    }

            //    return Results.StatusCode(StatusCodes.Status500InternalServerError);
            //});


            app.Run();
        }
    }
}
