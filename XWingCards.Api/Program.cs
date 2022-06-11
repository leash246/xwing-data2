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

builder.Services.AddSingleton<IRepositoryLoader, RepositoryLoader>();
builder.Services.AddSingleton(ctx => new UpgradesEndpoints(repository: ctx.GetService<IRepository<UpgradeCard>>()!));
builder.Services.AddSingleton(ctx => new PilotsEndpoints(repository: ctx.GetService<IRepository<PilotCard>>()!));
builder.Services.AddSingleton(ctx => new ShipsEndpoints(repository: ctx.GetService<IRepository<Ship>>()!));

var app = builder.Build();

app.Services.GetService<IRepositoryLoader>()!.LoadRepositories();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/upgrades/{slot}", (string slot, UpgradesEndpoints endoints) =>
{
    return endoints.UpgradesBySlot(slot);
})
.WithName("GetUpgradesBySlot");
app.MapGet("/upgrades", (bool? standard, bool? extended, bool? epic, UpgradesEndpoints endoints) =>
{
    return endoints.AllUpgrades(standard ?? true, extended ?? true, epic ?? false);
})
.WithName("GetAllUpgrades");
app.MapGet("/pilots/{faction}", (string faction, string? ship, PilotsEndpoints endoints) =>
{
    if (string.IsNullOrEmpty(ship))
        return endoints.PilotsByFaction(faction);
    return endoints.PilotsByShip(faction, ship);
})
.WithName("GetPilotsByFaction");
app.MapGet("/pilots", (bool? standard, bool? extended, bool? epic, PilotsEndpoints endoints) =>
{
    return endoints.AllUpgrades(standard ?? true, extended ?? true, epic ?? false);
})
.WithName("GetAllPilots");
app.MapGet("/ships/{faction}", (string faction, ShipsEndpoints endoints) =>
{
    return endoints.ShipsByFaction(faction);
})
.WithName("GetShipsByFaction");
app.MapGet("/ships", (bool? standard, bool? extended, bool? epic, ShipsEndpoints endoints) =>
{
    return endoints.AllShips(standard ?? true, extended ?? true, epic ?? false);
})
.WithName("GetAllShips");
app.Run();
