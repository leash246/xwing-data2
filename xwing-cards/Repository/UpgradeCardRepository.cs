using System.Text.Json;
using xwing_cards.Models;

namespace xwing_cards.Repositories;
public class UpgradeCardRepository : ICardRepository<UpgradeCard>
{
    public Dictionary<string, List<UpgradeCard>> Cards { get; set; }

    public UpgradeCardRepository() {
        Cards = new Dictionary<string, List<UpgradeCard>>();
        LoadCards();
    }
    public IEnumerable<UpgradeCard> GetFilteredCards(string filter)
    {
        if (Cards is null || !Cards.Any())
            LoadCards();
        return Cards[filter];
    }
    private void LoadCards()
    {
        foreach (var file in Directory.GetFiles("..\\data\\upgrades"))
        {
            var filename = Path.GetFileNameWithoutExtension(file);
            var json = System.IO.File.ReadAllText(file);
            var upgrades = DeserializeUpgradeCards(json);
            Cards.Add(filename, upgrades);
        }
    }
    private List<UpgradeCard> DeserializeUpgradeCards(string json)
    {
        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true, // JSON is case-insensitive
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // JSON is camelCase
            WriteIndented = true, // JSON is indented
        };
        json = json.Replace("non-limited", "nonLimited");
        if (string.IsNullOrWhiteSpace(json))
            return new List<UpgradeCard>();
        return JsonSerializer.Deserialize<List<UpgradeCard>>(json, jsonSerializerOptions);
    }
}