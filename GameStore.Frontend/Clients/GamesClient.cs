
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
  private List<GameSummary> games = [
    new() {
      Id = 3,
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
      Id = 2,
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
    Genre genre = GetGenreById(game.GenreId);

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

  public GameDetails GetGame(int id)
  {
    GameSummary game = GetGameSummaryById(id);

    var genre = genres.SingleOrDefault(genre => string.Equals(genre.Name, game.Genre, StringComparison.OrdinalIgnoreCase));

    return new GameDetails
    {
        Id = game.Id,
        Name = game.Name,
        GenreId = genre?.Id.ToString(),
        Price = game.Price,
        ReleaseDate = game.ReleaseDate
    };

  }

  public void UpdateGame(GameDetails updatedGame)
  {
    var genre = GetGenreById(updatedGame.GenreId);
    GameSummary existingGame = GetGameSummaryById(updatedGame.Id);

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = genre.Name;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;
  }

  private GameSummary GetGameSummaryById(int id)
  {
    GameSummary? game = games.Find(game => game.Id == id);
    ArgumentNullException.ThrowIfNull(game);

    return game;
  }

  private Genre GetGenreById(string? id)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(id);

    return genres.Single(genre => genre.Id == int.Parse(id));
  }
}
