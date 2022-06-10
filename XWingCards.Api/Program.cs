using XWingCards.Api.Models;
using XWingCards.Api.Repositories;
using XWingCards.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICardRepository<UpgradeCard>, UpgradeCardRepository>();
builder.Services.AddSingleton<ICardRepository<PilotCard>, PilotRepository>();
builder.Services.AddSingleton(ctx => new UpgradesEndpoints(repository: ctx.GetService<ICardRepository<UpgradeCard>>()!));
builder.Services.AddSingleton(ctx => new PilotsEndpoints(repository: ctx.GetService<ICardRepository<PilotCard>>()!));

var app = builder.Build();

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
app.MapGet("/pilots/failed", (PilotsEndpoints endoints) =>
{
    return endoints.FailedShips();
})
.WithName("GetFailedShips");
app.Run();
