
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
using NBA.Players.Charts.Hubs;
using Microsoft.AspNetCore.Identity;

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
            builder.Services.AddSignalR();

            builder.Services.AddHttpClient("MyHttpClient")
                .AddPolicyHandler(ServiceExtension.GetRetryPolicy())
                .AddPolicyHandler(ServiceExtension.GetCircuitBreakerPolicy());
            var cnStr = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(cnStr!);
            });

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddHostedService<PlayerStatsBackgroundService>();
            builder.Services.AddHostedService<GameStatsBackgroundService>();
            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://localhost:5087/") // Allow specific origin
                          .AllowAnyHeader()                    // Allow any headers
                          .AllowAnyMethod()                  // Allow any HTTP methods
                        .AllowCredentials();
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(x => x
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

            }

          
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

            app.MapHub<GameStatsHub>("/gamestatsHub");
            app.MapIdentityApi<IdentityUser>();
            app.Run();
        }
    }
}
