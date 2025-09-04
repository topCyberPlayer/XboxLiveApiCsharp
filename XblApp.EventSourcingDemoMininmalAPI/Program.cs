using XblApp.EventSourcingDemoMininmalAPI.Entities;
using XblApp.EventSourcingDemoMininmalAPI.Events;
using XblApp.EventSourcingDemoMininmalAPI.Events.Handler;

namespace XblApp.EventSourcingDemoMininmalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddScoped<DomainEventDispatcher>();
            builder.Services.AddScoped<IDomainEventHandler<GamerCreatedEvent>, GamerCreatedEventHandler>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast");

            app.MapPost("/gamers", async (CreateGamerRequest request, DomainEventDispatcher dispatcher) =>
            {
                Gamer gamer = new(request.Gamertag);

                await dispatcher.DispatchAsync(gamer.DomainEvents);
                gamer.ClearDomainEvents();
            });

            app.Run();
        }
    }
}
