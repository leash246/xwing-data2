namespace xwing_cards.Models;
  public class UpgradeCard : ICard
  {
    public string Name { get; set; }
    public int Limited { get; set; }
    public string Xws { get; set; }
    public Side[] Sides { get; set; }
    public Cost Cost { get; set; }
    public Restriction[] Restrictions { get; set; }
    public bool Standard { get; set; }
    public bool Extended { get; set; }
    public bool Epic { get; set; }
  }