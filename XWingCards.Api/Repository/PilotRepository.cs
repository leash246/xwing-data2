using System.Text.Json;
using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;
public class PilotRepository : IRepository<PilotCard>
{
    private const string PilotPath = "pilots";
    public Dictionary<string, List<PilotCard>> Cards { get; set; } = new Dictionary<string, List<PilotCard>>();
    private static readonly string[] Factions = new string[] { "rebel-alliance", "galactic-empire", "scum-and-villainy", "first-order", "resistance", "galactic-republic", "separatist-alliance" };

    public IConfiguration Configuration { get; }

    public PilotRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void LoadCards()
    {
        foreach (var dir in Directory.GetDirectories(Configuration["DataPath"] + PilotPath))
        {
            var faction = dir.Split("\\").Last();
            var factionPilots = new List<PilotCard>();
            foreach (var file in Directory.GetFiles(dir))
            {
                var json = File.ReadAllText(file);
                var ship = CardDeserializer.Deserialize<Ship>(json);
                UpdatePilotShips(ship!);
                factionPilots.AddRange(ship!.Pilots!.ToList());
            }
            Cards.Add(faction, factionPilots);
        }
    }
    public void PushData(Dictionary<string, List<PilotCard>> cards)
    {
        Cards = cards;
    }
    public IEnumerable<PilotCard> GetFilteredCards(string filter)
    {
        if (FilterIsFactionName(filter))
            return Cards[filter];
        else
            return Cards.SelectMany(x => x.Value).Where(v => v.XwsShip == filter).ToList();
    }
    private static bool FilterIsFactionName(string filter)
    {
        return Factions.Contains(filter);
    }
    private static void UpdatePilotShips(Ship ship)
    {
        foreach (var pilot in ship.Pilots!)
        {
            pilot.XwsShip = ship.Xws;
            SetHotacCost(pilot);
        }
    }
    private static void SetHotacCost(PilotCard pilot)
    {
        pilot.HotacCost = pilot.Initiative * multiplier();

        int multiplier() => pilot.Force is null ? 2 : pilot.Force.Value + 3;
    }
}
