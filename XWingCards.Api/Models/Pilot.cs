namespace XWingCards.Api.Models;
    public class PilotCard : ICard
    {
        public string? Name { get; set; }
        public string? Caption { get; set; }
        public string? Xws { get; set; }
        public int? Ffg { get; set; }
        public int Initiative { get; set; }
        public int Cost { get; set; }
        public int HotacCost { get; set; }
        public int Loadout { get; set; }
        public int Limited { get; set; }
        public string? Artwork { get; set; }
        public string? Ability { get; set; }
        public string? Image { get; set; }
        public Force? Force { get; set; }
        public Charge? Charge { get; set; }
        public string[]? Keywords { get; set; }
        public ShipAbility? ShipAbility { get; set; }
        public string? Text { get; set; }
        public bool Standard { get; set; }
        public bool Extended { get; set; }
        public bool Epic { get; set; }
        public int? Engagement { get; set; }
        public string? XwsShip { get; set; }
        public string[]? Slots { get; set; }
    }