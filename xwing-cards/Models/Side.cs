namespace xwing_cards.Models;

    //    "force": { "value": 1, "recovers": 1 },
    //        "conditions": ["youdbettermeanbusiness", "youshouldthankme"],

public class Side
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string Ability { get; set; }
    public string[] Slots { get; set; }
    public Charge Charges { get; set; }
    public Attack Attack { get; set; }
    public Grants[] Grants { get; set; }
    public Action[] Actions { get; set; }
    public Force Force { get; set; }
    public string[] Conditions { get; set; }
    public string Image { get; set; }
    public int Ffg { get; set; }
    public string Artwork { get; set; }
    public ShipAbility ShipAbility { get; set; }
}

public class Force
{
    public int Value { get; set; }
    public int Recovers { get; set; }
}

public class ShipAbility
{
    public string Name { get; set; }
    public string Text { get; set; }
}

public class Charge
{
    public int Value { get; set; }
    public int Recovers { get; set; }
}

public class Attack
{
    public string Arc { get; set; }
    public int Value { get; set; }
    public int MinRange { get; set; }
    public int MaxRange { get; set; }
    public bool Ordnance { get; set; }
}
public class Grants
{
    public string Type { get; set; }
    public GrantValue Value { get; set; }
}
public class GrantValue
{
    public string Type { get; set; }
    public Action Linked { get; set; }
    public int Amount { get; set; }
    public Attack Attack { get; set; }
    public string Difficulty { get; set; }
    public string[] Side { get; set; }
}

public class Action
{
    public string Type { get; set; }
    public string Difficulty { get; set; }
    public Action Linked { get; set; }
}