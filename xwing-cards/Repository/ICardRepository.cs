using xwing_cards.Models;

namespace xwing_cards.Repositories;
public interface ICardRepository<T> where T : ICard
{
    Dictionary<string, List<T>> Cards { get; }
    IEnumerable<T> GetFilteredCards(string filter);
}