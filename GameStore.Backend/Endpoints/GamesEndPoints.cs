using GameStore.Backend.Dtos;

namespace GameStore.Backend.Endpoints;

public static class GamesEndPoints
{
  const string GetGameEndpointName = "GetGame";

  public static List<GameDto> games = [
    new (
      3,
      "Street Fighter II",
      "Fighting",
      20.00M,
      new DateOnly(1992, 7, 15)
    ),
    new(
      1,
      "Final Fantasy",
      "RPG", 
      15.00M,
      new DateOnly(2000, 7, 15)
    ), 
    new(
      2,
      "Silent Hill 2",
      "Horror", 
      30.00M,
      new DateOnly(2024, 7, 15)
    )
  ];

  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {

    var group = app.MapGroup("games").WithParameterValidation();

    // GET /games
    group.MapGet("/", () => games);

    // GET /games/id
    group.MapGet("/{id}", (int id) => 
    {
      GameDto? game = games.Find(game => game.Id == id);
    
      return game is null ? Results.NotFound() : Results.Ok(game);
    }) 
    .WithName(GetGameEndpointName);

    //POST /games
    group.MapPost("/", (CreateGameDto newGame) => 
    {
      GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
      );

      games.Add(game);

      return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
    });
    

    // PUT /games
    group.MapPut("/{id}", (int id, UpdatedGameDto updatedGame) => {
       var index = games.FindIndex(game => game.Id == id);

       if (index == -1)
       {
        return Results.NotFound();
       }

       games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
       );

       return Results.NoContent();
    });

    // DELETE /games/1
    group.MapDelete("/{id}", (int id) => {
      games.RemoveAll(game => game.Id == id);

      Results.NoContent();
    });

    return group;
  }
}
