using System;
using Microsoft.AspNetCore.Authentication;

namespace GameStore.Api.Entities;

public class Game
{
    // Games class contains game details including name, Id, Genre, Price and ReleaseDate
    //this class specifies a one to one relationship with the Genre class
    // this means that the genreId needs to be present but the genre info can remain null
    public int Id {get; set;}

    //Value for name has to be provided
    public required string Name {get; set;}

    //To link data in the games with the genre, genre id needs to be addedd to the game class
    //A nullable variable(instance) of Genre also needs to be made in the games class
    public int GenreId {get; set;}

    public Genre? Genre { get; set;}

    public decimal Price {get; set;}

    public DateOnly ReleaseDate {get; set;}

}
