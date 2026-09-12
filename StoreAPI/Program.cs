using StoreAPI.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();





List<MovieDto> movies = 
    [
        new MovieDto(1, "Inception", "Christopher Nolan", 2010, "Sci-Fi"),
        new MovieDto(2, "The Dark Knight", "Christopher Nolan", 2008, "Action"),
        new MovieDto(3, "Interstellar", "Christopher Nolan", 2014, "Sci-Fi")
    ];

//GET /movies

app.MapGet("/movies", () =>movies);

//GET /movies/{id}
app.MapGet("/movies/{id}", (int id) => movies.Find(movie => movie.Id == id) );

//POST /movies

app.MapPost("/movies", (CreateMovieDto newMovie) => {

    MovieDto movie = new (movies.Count + 1, newMovie.Title, newMovie.Director, newMovie.ReleaseYear, newMovie.Genre);
    movies.Add(movie);

    return Results.Created($"/movies/{movie.Id}", movie);


});


//PUT /movies/{id}
app.MapPut("/movies/{id}", (int id, UpdateMovieDto updatedMovie) => {

    var index = movies.FindIndex(movie => movie.Id == id);

    movies[index] = new MovieDto(id, updatedMovie.Title, updatedMovie.Director, updatedMovie.ReleaseYear, updatedMovie.Genre);
    return Results.Ok(movies[index]);

});

//DELETE /movies/{id}
app.MapDelete("/movies/{id}", (int id) => {

    movies.RemoveAll(movie => movie.Id == id);

    return Results.NoContent();

});


app.MapGet("/", () => "Movie Store API is running. Use /movies endpoint to interact with the API.");

app.Run();
