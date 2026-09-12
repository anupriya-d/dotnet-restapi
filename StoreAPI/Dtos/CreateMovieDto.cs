namespace StoreAPI.Dtos;

public record CreateMovieDto(string Title, string Director, int ReleaseYear, string Genre);