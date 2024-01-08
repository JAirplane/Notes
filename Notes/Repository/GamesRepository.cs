using Notes.Models;

namespace Notes.Repository
{
    public class GamesRepository : IRepository
    {
        private List<Game> gamesCollection;
        public GamesRepository()
        {
            gamesCollection =
            [
                new()
                {
                    Name = "Cities SkyLine",
                    Genre = "Simulator",
                    ReleaseDate = new DateTime(2013, 10, 15)
                },
                new()
                {
                    Name = "The Witcher 3",
                    Genre = "Action/RPG",
                    ReleaseDate = new DateTime(2016, 05, 18)
                },
                new()
                {
                    Name = "They are billion!",
                    Genre = "Strategy",
                    ReleaseDate = new DateTime(2019, 03, 10)
                }
            ];

        }
        public List<Game> GetAllGames()
        {
            return gamesCollection;
        }
    }
}
