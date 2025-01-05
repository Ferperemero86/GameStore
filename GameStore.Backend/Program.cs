using GameStore.Backend.Dtos;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
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

// GET /games
app.MapGet("/games", () => games);

// GET /games/id
app.MapGet("/games/{id}", (int id) => 
{
  GameDto? game = games.Find(game => game.Id == id);

  return game is null ? Results.NotFound() : Results.Ok(game);
}) 
.WithName(GetGameEndpointName);

//POST /games
app.MapPost("/games", (CreateGameDto newGame) => 
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
app.MapPut("/games/{id}", (int id, UpdatedGameDto updatedGame) => {
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
app.MapDelete("games/{id}", (int id) => {
  games.RemoveAll(game => game.Id == id);

  Results.NoContent();
});


app.Run();
