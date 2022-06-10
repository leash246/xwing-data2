using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;
public interface ICardRepository<T> where T : ICard
{
    List<string> Failures {get;}
    Dictionary<string, List<T>> Cards { get; }
    IEnumerable<T> GetFilteredCards(string filter);
}