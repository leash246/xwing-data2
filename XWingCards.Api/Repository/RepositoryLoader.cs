using XWingCards.Api.Models;

namespace XWingCards.Api.Repositories;

public interface IRepositoryLoader
{
    void LoadRepositories();
}
public class RepositoryLoader : IRepositoryLoader
{
    private IRepository<UpgradeCard> UpgradeCardRepository { get; }
    private IRepository<PilotCard> PilotRepository { get; }
    private IRepository<Ship> ShipRepository { get; }
    public IPlayerRepository PlayerRepository { get; }

    public RepositoryLoader(IRepository<UpgradeCard> upgradeCardRepository,
        IRepository<PilotCard> pilotRepository,
        IRepository<Ship> shipRepository)
    {
        UpgradeCardRepository = upgradeCardRepository;
        PilotRepository = pilotRepository;
        ShipRepository = shipRepository;
    }
    public void LoadRepositories()
    {
        UpgradeCardRepository.LoadCards();
        PilotRepository.LoadCards();
        ShipRepository.LoadCards();
    }



}
