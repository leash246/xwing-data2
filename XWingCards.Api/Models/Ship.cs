namespace XWingCards.Api.Models;
public class Ship : ICard
{
    public string? Name { get; set; }
    public string? Xws { get; set; }
    public int Ffg { get; set; }
    public string? Faction { get; set; }
    public string? Icon { get; set; }
    public string? Size { get; set; }
    public string[]? Dial { get; set; }
    public string[]? DialCodes { get; set; }
    public Stat[]? Stats { get; set; }
    public Action[]? Actions { get; set; }
    public PilotCard[]? Pilots { get; set; }
    public bool Standard { get; set; }
    public bool Extended { get; set; }
    public bool Epic { get; set; }
}