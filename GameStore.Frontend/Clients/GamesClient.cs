
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
  private List<GameSummary> games = [
    new() {
      Id = 1,
      Name = "Street Fighter II",
      Genre = "Fighting",
      Price = 20.00M,
      ReleaseDate = new DateOnly(1992, 7, 15)
    },
    new() {
      Id = 1,
      Name = "Final Fantasy",
      Genre = "RPG", 
      Price = 15.00M,
      ReleaseDate = new DateOnly(2000, 7, 15)
    }, 
    new() {
      Id = 1,
      Name = "Silent Hill 2",
      Genre = "Horror", 
      Price = 30.00M,
      ReleaseDate = new DateOnly(2024, 7, 15)
    }
  ];

  private readonly Genre[] genres = new GenresClient().GetGenres();

  public GameSummary[] GetGames() => [.. games];

  public void AddGame(GameDetails game)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);

    var genre = genres.Single(genre => genre.Id == int.Parse(game.GenreId));

    var gameSummary = new GameSummary
    {
      Id = games.Count + 1,
      Name = game.Name,
      Genre = genre.Name,
      Price = game.Price,
      ReleaseDate = game.ReleaseDate
    };

    games.Add(gameSummary);
  }
}
