using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayerAsync(string callsign);
        Task<Player[]> GetPlayersAsync();
        Task SavePlayerAsync(Player player);
    }
}
