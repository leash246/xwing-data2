using System.Text.Json;
using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;
public class PlayerRepository : IPlayerRepository
{
    private JsonSerializerOptions _options;
    private string PlayersPath = "";
    public PlayerRepository(IConfiguration configuration)
    {
        PlayersPath = configuration["PlayersPath"];
        if (!Directory.Exists(PlayersPath))
        {
            Directory.CreateDirectory(PlayersPath);
        }
        _options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<Player> GetPlayerAsync(string callsign)
    {
        if (File.Exists(PlayersPath + callsign + ".json"))
        {
            var data = await File.ReadAllTextAsync(PlayersPath + callsign + ".json");
            return JsonSerializer.Deserialize<Player>(data, _options) ?? new Player();
        }
        return new Player();
    }
    public async Task<Player[]> GetPlayersAsync()
    {
        var players = new List<Player>();
        foreach (var player in Directory.GetFiles(PlayersPath))
        {
            var data = await File.ReadAllTextAsync(player);
            var playerObject = JsonSerializer.Deserialize<Player>(data, _options);
            if (playerObject != null)
            {
                players.Add(playerObject);
            }
        }
        return players.ToArray();
    }
    public async Task SavePlayerAsync(Player player)
    {
        var contents = JsonSerializer.Serialize(player, _options);
        await File.WriteAllTextAsync(PlayersPath + player.Callsign + ".json", contents);
    }
}
