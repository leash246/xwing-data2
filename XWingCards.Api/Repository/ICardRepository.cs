using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;
public interface IRepository<T> where T : ICard
{
    Dictionary<string, List<T>> Cards { get; }
    void LoadCards();
    void PushData(Dictionary<string, List<T>> cards);
    IEnumerable<T> GetFilteredCards(string filter);
}