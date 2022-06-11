using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;
public class ShipRepository : IRepository<Ship>
{
    private const string PilotPath = "\\pilots";
    public Dictionary<string, List<Ship>> Cards { get; set; } = new Dictionary<string, List<Ship>>();
    public List<string> Failures { get; } = new List<string>();
    private static readonly string[] Factions = new string[] { "rebel-alliance", "galactic-empire", "scum-and-villainy", "first-order", "resistance", "galactic-republic", "separatist-alliance" };

    public IConfiguration Configuration { get; }

    public ShipRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void LoadCards()
    {
        foreach (var dir in Directory.GetDirectories(Configuration["DataPath"] + PilotPath))
        {
            var faction = dir.Split("\\").Last();
            var factionShips = new List<Ship>();
            foreach (var file in Directory.GetFiles(dir))
            {
                var json = File.ReadAllText(file);
                var ship = CardDeserializer.Deserialize<Ship>(json);
                StripPilots(ship!);
                factionShips.Add(ship!);
            }
            Cards.Add(faction, factionShips);
        }
    }
    public void PushData(Dictionary<string, List<Ship>> cards)
    {
        Cards = cards;
    }
    public IEnumerable<Ship> GetFilteredCards(string filter)
    {
        if (FilterIsFactionName(filter))
            return Cards[filter];
        else
            return Cards.SelectMany(x => x.Value).Where(v => v.Xws == filter).ToList();
    }
    private static void StripPilots(Ship ship)
    {
        if (ship.Pilots is not null)
        {
            ship.Standard = ship.Pilots.Any(p => p.Standard);
            ship.Extended = ship.Pilots.Any(p => p.Extended);
            ship.Epic = ship.Pilots.Any(p => p.Epic);
            ship.Pilots = null;
        }
    }
    private static bool FilterIsFactionName(string filter)
    {
        return Factions.Contains(filter);
    }
}
