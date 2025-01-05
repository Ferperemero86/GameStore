namespace GameStore.Backend.Dtos;

public record class UpdatedGameDto(
  string Name, 
  string Genre, 
  decimal Price, 
  DateOnly ReleaseDate
);
