using Notes.Models;

namespace Notes.Repository
{
    public interface IRepository
    {
        List<Game> GetAllGames();
    }
}
