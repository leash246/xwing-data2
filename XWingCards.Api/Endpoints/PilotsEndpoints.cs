using XWingCards.Api.Models;
using XWingCards.Api.Repositories;

namespace XWingCards.Api.Endpoints;

public class PilotsEndpoints
{
    private readonly ICardRepository<PilotCard> _repository;

    public PilotsEndpoints(ICardRepository<PilotCard> repository)
    {
        this._repository = repository;
    }
    public List<string> FailedShips() {
        return _repository.Failures;
    }
    public List<PilotCard> PilotsByFaction(string faction)
    {
        if (string.IsNullOrWhiteSpace(faction))
            new List<PilotCard>();
        return _repository.GetFilteredCards(faction).ToList();
    }
    public List<PilotCard> PilotsByShip(string faction, string shipXws)
    {
        if (string.IsNullOrWhiteSpace(faction) || string.IsNullOrWhiteSpace(shipXws))
            new List<PilotCard>();
        return _repository.GetFilteredCards(faction).Where(c => c.XwsShip == shipXws).ToList();
    }
    public List<PilotCard> AllUpgrades(bool standard, bool extended, bool epic)
    {
        var upgrades = new List<PilotCard>();
        foreach (var cardSet in _repository.Cards.Values)
            upgrades.AddRange(cardSet.Where(c => (standard && c.Standard) || (extended && c.Extended) || (epic && c.Epic)));
        return upgrades;
    }
}
