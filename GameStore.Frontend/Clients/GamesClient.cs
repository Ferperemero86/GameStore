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

  public GameSummary[] GetGames() => [.. games];
}
