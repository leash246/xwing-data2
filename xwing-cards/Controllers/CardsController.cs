using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using xwing_cards.Models;
using xwing_cards.Repositories;

namespace xwing_cards.Controllers;

[ApiController]
[Route("[controller]")]
public class CardsController : ControllerBase
{
    private readonly ILogger<CardsController> _logger;
    private readonly ICardRepository<UpgradeCard> _upgradeCardRepository;

    public CardsController(ILogger<CardsController> logger, ICardRepository<UpgradeCard> upgradeCardRepository)
    {
        _logger = logger;
        _upgradeCardRepository = upgradeCardRepository;
    }

    [HttpGet("Upgrades/{slot}")]
    public IActionResult Get([FromRoute] string slot)
    {
        if (string.IsNullOrWhiteSpace(slot))
            return BadRequest();
        var cards = _upgradeCardRepository.GetFilteredCards(slot);
        if (!cards.Any())
            return BadRequest("Unknown slot");
        return Ok(cards);
    }
    [HttpGet("Upgrades")]
    public IActionResult Get([FromQuery] bool epic = false, [FromQuery] bool standard = true, [FromQuery] bool extended = true)
    {
        var cards = _upgradeCardRepository.Cards.Where(
            c => c.Value.Any(u => u.Epic == epic && u.Standard == standard && u.Extended == extended));
        if (!cards.Any())
            return BadRequest("No cards found");
        return Ok(cards);
    }
    
}
