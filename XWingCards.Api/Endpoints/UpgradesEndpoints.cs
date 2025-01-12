﻿using XWingCards.Api.Models;
using XWingCards.Api.Repositories;

namespace XWingCards.Api.Endpoints;

public class UpgradesEndpoints
{
    private readonly IRepository<UpgradeCard> _repository;

    public UpgradesEndpoints(IRepository<UpgradeCard> repository)
    {
        this._repository = repository;
    }
    public List<UpgradeCard> UpgradesBySlot(string slot)
    {
        if (string.IsNullOrWhiteSpace(slot))
            new List<UpgradeCard>();
        return _repository.GetFilteredCards(slot).ToList();
    }
    public List<UpgradeCard> AllUpgrades(bool standard, bool extended, bool epic)
    {
        var upgrades = new List<UpgradeCard>();
        foreach (var cardSet in _repository.Cards.Values)
            upgrades.AddRange(cardSet.Where(c => (standard && c.Standard) || (extended && c.Extended) || (epic && c.Epic)));
        return upgrades;
    }
}
