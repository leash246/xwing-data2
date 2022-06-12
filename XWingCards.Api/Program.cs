using XWingCards.Api.Models;
using XWingCards.Api.Repositories;
using XWingCards.Api.Endpoints;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.PropertyNameCaseInsensitive = true; // JSON is case-insensitive
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // JSON is camelCase
    options.SerializerOptions.WriteIndented = true; // JSON is indented
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddSingleton<IRepository<UpgradeCard>, UpgradeCardRepository>();
builder.Services.AddSingleton<IRepository<PilotCard>, PilotRepository>();
builder.Services.AddSingleton<IRepository<Ship>, ShipRepository>();
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();

builder.Services.AddSingleton<IRepositoryLoader, RepositoryLoader>();
builder.Services.AddSingleton(ctx => new UpgradesEndpoints(repository: ctx.GetService<IRepository<UpgradeCard>>()!));
builder.Services.AddSingleton(ctx => new PilotsEndpoints(repository: ctx.GetService<IRepository<PilotCard>>()!));
builder.Services.AddSingleton(ctx => new ShipsEndpoints(repository: ctx.GetService<IRepository<Ship>>()!));
builder.Services.AddSingleton(ctx => new PlayersEndpoints(repository: ctx.GetService<IPlayerRepository>()!));

var app = builder.Build();

app.Services.GetService<IRepositoryLoader>()!.LoadRepositories();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
