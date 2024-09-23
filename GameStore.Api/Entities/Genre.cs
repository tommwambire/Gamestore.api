using System;

namespace GameStore.Api.Entities;

public class Genre
{
    public int Id {get; set;}

    //Value for name has to be provided
    public required string Name {get; set;}

}
