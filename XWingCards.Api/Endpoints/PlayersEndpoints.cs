using XWingCards.Api.Models;
using XWingCards.Api.Repositories;

namespace XWingCards.Api.Endpoints;

public class PlayersEndpoints
{
    private readonly IPlayerRepository _repository;

    public PlayersEndpoints(IPlayerRepository repository)
    {
        this._repository = repository;
    }
    public async Task<List<Player>> AllPlayers()
    {
        return (await _repository.GetPlayersAsync()).ToList();
    }
    public async Task<Player> PlayerByCallsign(string callsign)
    {
        if (string.IsNullOrWhiteSpace(callsign))
            new Player();
        return await _repository.GetPlayerAsync(callsign);
    }
    public async Task SavePlayer(Player player)
    {
        await _repository.SavePlayerAsync(player);
    }
}
