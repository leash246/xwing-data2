namespace XwingCards.Api.Models;
public class Restriction
{
    public string[] Factions { get; set; }
    public string[] Sizes { get; set; }
    public string[] Ships { get; set; }
    public string[] Keywords { get; set; }
    public string[] Names { get; set; }
    public Action Action { get; set; }
    public bool Standardized { get; set; }
    public bool NonLimited { get; set; }
    public string[] ShipAbility { get; set; }
}