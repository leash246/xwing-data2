using XwingCards.Api.Models;
using XwingCards.Api.Repositories;
using XWingCards.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICardRepository<UpgradeCard>, UpgradeCardRepository>();
builder.Services.AddSingleton(ctx => new UpgradesEndpoints(repository: ctx.GetService<ICardRepository<UpgradeCard>>()));

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

app.Run();
