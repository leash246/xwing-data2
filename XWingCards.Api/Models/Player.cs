namespace XWingCards.Api.Models;
public class Player
{
    public string Callsign { get; init; }
    public int Initiative { get; set; }
    public string ShipType { get; set; }
    public string[] AvailableFactions { get; set; }
    public int ExtraCharges { get; set; }
    public int EarnedXp { get; set; }
    public int SpentXp { get; set; }
    public UpgradeSlot[] UpgradeSlots { get; set; }
}