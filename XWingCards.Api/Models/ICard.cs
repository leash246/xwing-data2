namespace XWingCards.Api.Models
{
    public interface ICard
    {
        string? Name { get; set; }
        public string? Xws { get; set; }
        public bool Standard { get; set; }
        public bool Extended { get; set; }
        public bool Epic { get; set; }
    }
}