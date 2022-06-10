using System.Text.Json;
using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;
public class PilotRepository : ICardRepository<PilotCard>
{
    private const string PilotPath = "..\\data\\pilots";
    public Dictionary<string, List<PilotCard>> Cards { get; } = new Dictionary<string, List<PilotCard>>();
    public List<string> Failures { get; } = new List<string>();
    private string[] Factions = new string[] { "rebel-alliance", "galactic-empire", "scum-and-villainy", "first-order", "resistance", "galactic-republic", "separatist-alliance" };

    public PilotRepository()
    {
        LoadCards();
    }
    private void LoadCards()
    {
        foreach (var dir in Directory.GetDirectories(PilotPath))
        {
            foreach (var file in Directory.GetFiles($"{PilotPath}\\{dir}"))
            {
                var shipType = Path.GetFileNameWithoutExtension(file);
                var json = File.ReadAllText(file);
                try {
                var ships = CardDeserializer.DeserializeCards<Ship>(json);
                foreach (var ship in ships)
                {
                    Cards.Add(dir!, ship.Pilots!.ToList());
                }
                } catch  {
                    Failures.Add(dir + " - " + shipType);
                }
            }
        }
    }
    public IEnumerable<PilotCard> GetFilteredCards(string filter)
    {
        if (FilterIsFactionName(filter))
            return Cards[filter];
        else 
            return Cards.SelectMany(x => x.Value).Where(v => v.XwsShip == filter).ToList();
    }
    private bool FilterIsFactionName(string filter)
    {
        return Factions.Contains(filter);
    }
}
