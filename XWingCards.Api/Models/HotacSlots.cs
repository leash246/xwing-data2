namespace XWingCards.Api.Models;

public static class HotacSlots
{
    public static UpgradeSlot[] GetUpgradeSlots(string shipXws)
    {
        return shipXws switch
        {
            "btla4ywing" => GenerateSlotsInOrder(new InitiativeSlot(1, "Astromech"), new InitiativeSlot(1, "Device"), new InitiativeSlot(1, "Turret"), new InitiativeSlot(1, "Torpedo"), new InitiativeSlot(1, "Modification"), new InitiativeSlot(1, "Gunner"), new InitiativeSlot(3, "Talent"), new InitiativeSlot(4, "Modification", "Sensor"), new InitiativeSlot(4, "Talent", "Force Power"), new InitiativeSlot(5, "Modification", "Tech"), new InitiativeSlot(5, "Talent", "Force Power"), new InitiativeSlot(6, "Modification", "Illicit"), new InitiativeSlot(6, "Talent", "Force Power")),
            "t65xwing" => GenerateSlotsInOrder(new InitiativeSlot(1, "Configuration"), new InitiativeSlot(1, "Astromech"), new InitiativeSlot(1, "Modification"), new InitiativeSlot(1, "Torpedo"), new InitiativeSlot(3, "Talent"), new InitiativeSlot(4, "Modification", "Sensor"), new InitiativeSlot(4, "Talent", "Force Power"), new InitiativeSlot(5, "Modification", "Tech"), new InitiativeSlot(5, "Talent", "Force Power"), new InitiativeSlot(6, "Modification", "Illicit"), new InitiativeSlot(6, "Talent", "Force Power")),
            _ => new UpgradeSlot[] { },
        };
    }
    private static UpgradeSlot[] GenerateSlotsInOrder(params InitiativeSlot[] slots)
    {
        var slotsInOrder = new List<UpgradeSlot>();
        for (var i = 1; i <= slots.Length; i++)
        {
            slotsInOrder.Add(new UpgradeSlot(i, slots[i-1].Initiative, slots[i-1].Slots, null, false));
        }
        return slotsInOrder.ToArray();
    }
    private class InitiativeSlot
    {
        public InitiativeSlot(int initiative, params string[] slots)
        {
            Initiative = initiative;
            Slots = slots;
        }

        public int Initiative { get; set; }
        public string[] Slots { get; set; }
    }
}

public class UpgradeSlot
{
    public UpgradeSlot(int number, int initiativeRequirement, string[] slot, string xws, bool isPilot)
    {
        Number = number;
        InitiativeRequirement = initiativeRequirement;
        Slot = slot;
        Xws = xws;
        IsPilot = isPilot;
    }

    public int Number { get; set; }
    public string? Xws { get; set; }
    public string[] Slot { get; set; }
    public int InitiativeRequirement { get; set; }
    public bool IsPilot { get; set; }

}