using XWingCards.Api.Models;

namespace XWingCards.Api.Endpoints;

public static class EndpointMapper
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapUpgradesEndpoints();
        app.MapPilotsEndpoints();
        app.MapShipsEndpoints();
        app.MapPlayersEndpoints();
    }
    private static void MapPilotsEndpoints(this WebApplication app)
    {
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
    }
    private static void MapShipsEndpoints(this WebApplication app)
    {
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
    }
    private static void MapUpgradesEndpoints(this WebApplication app)
    {
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
    }
    private static void MapPlayersEndpoints(this WebApplication app)
    {
        app.MapGet("/players", (PlayersEndpoints endoints) =>
        {
            return endoints.AllPlayers();
        })
        .WithName("GetAllPlayers");

        app.MapGet("/players/{callsign}", (string callsign, PlayersEndpoints endoints) =>
        {
            return endoints.PlayerByCallsign(callsign);
        })
        .WithName("GetPlayer");

        app.MapPost("/player", (Player player, PlayersEndpoints endoints) =>
        {
            return endoints.SavePlayer(player);
        })
        .WithName("SavePlayer");
    }
}
