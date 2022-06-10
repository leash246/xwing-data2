using System.Text.Json;
using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;
public class UpgradeCardRepository : ICardRepository<UpgradeCard>
{
    public Dictionary<string, List<UpgradeCard>> Cards { get; set; } = new Dictionary<string, List<UpgradeCard>>();


    public List<string> Failures => new List<string>();

    public UpgradeCardRepository() {
        LoadCards();
    }
    public IEnumerable<UpgradeCard> GetFilteredCards(string filter)
    {
        if (!Cards.Any())
            LoadCards();
        return Cards[filter];
    }
    private void LoadCards()
    {
        foreach (var file in Directory.GetFiles("..\\data\\upgrades"))
        {
            var filename = Path.GetFileNameWithoutExtension(file);
            var json = File.ReadAllText(file);
            var upgrades = CardDeserializer.DeserializeCards<UpgradeCard>(json);
            SetHotacCost(upgrades, filename);
            Cards.Add(filename, upgrades);
        }
    }
    private static void SetHotacCost(List<UpgradeCard> cards, string slot)
    {
        foreach (var card in cards)
        {
            card.Cost ??= new Cost() { Value = 0 };
            card.Cost.HotacValue = card.Cost.Value * multiplier();

            int multiplier() => slot switch
            {
                "talent" => 2,
                "pilot" => 3 + card.Sides!.First().Force?.Value ?? 0,
                _ => 1
            };
        }
    }

}