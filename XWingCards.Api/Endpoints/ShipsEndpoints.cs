using XWingCards.Api.Models;
using XWingCards.Api.Repositories;

namespace XWingCards.Api.Endpoints;

public class ShipsEndpoints
{
    private readonly IRepository<Ship> _repository;

    public ShipsEndpoints(IRepository<Ship> repository)
    {
        this._repository = repository;
    }
    public List<Ship> ShipsByFaction(string faction)
    {
        if (string.IsNullOrWhiteSpace(faction))
            new List<Ship>();
        return _repository.GetFilteredCards(faction).ToList();
    }
    public List<Ship> AllShips(bool standard, bool extended, bool epic)
    {
        var ships = new List<Ship>();
        foreach (var cardSet in _repository.Cards.Values)
            ships.AddRange(cardSet.Where(c => (standard && c.Standard) || (extended && c.Extended) || (epic && c.Epic)));
        return ships;
    }
}
