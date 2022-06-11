using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;
public class UpgradeCardRepository : IRepository<UpgradeCard>
{
    private const string UpgradesPath = "\\upgrades";
    public Dictionary<string, List<UpgradeCard>> Cards { get; set; } = new Dictionary<string, List<UpgradeCard>>();
    public IConfiguration Configuration { get; }

    public UpgradeCardRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void LoadCards()
    {

        var cards = new Dictionary<string, List<UpgradeCard>>();
        foreach (var file in Directory.GetFiles(Configuration["DataPath"] + UpgradesPath))
        {
            var filename = Path.GetFileNameWithoutExtension(file);
            var json = File.ReadAllText(file);
            var upgrades = CardDeserializer.DeserializeList<UpgradeCard>(json);
            SetUpgradeHotacCost(upgrades, filename);
            cards.Add(filename, upgrades);
        }
        PushData(cards);
    }
    public void PushData(Dictionary<string, List<UpgradeCard>> cards)
    {
        Cards = cards;
    }

    public IEnumerable<UpgradeCard> GetFilteredCards(string filter)
    {
        return Cards[filter];
    }
    private static void SetUpgradeHotacCost(List<UpgradeCard> cards, string slot)
    {
        foreach (var card in cards)
        {
            card.Cost ??= new Cost() { Value = 0 };
            card.Cost.HotacValue = card.Cost.Value * multiplier();

            int multiplier() => slot switch
            {
                "talent" => 2,
                _ => 1
            };
        }
    }

}