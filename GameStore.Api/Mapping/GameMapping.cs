using System;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;


public static class GameMapping
{
    //static class that uses injection to map game data into input format and output format
    //in GamesEndPoints.cs
    public static Game ToEntity(this CreateGameDto game)
    {
        //maps created games to  CreatedGameDto entity (database context)
        return new()
        {
            Name = game.Name,            
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        //maps created games to  CreatedGameDto entity (database context)
        return new()
        {
            Id = id,
            Name = game.Name,            
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        //maps games to Dto from the Game Entity (database)
        return new
        (
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        //maps games to Dto from the Game Entity (database)
        return new
        (
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }

}
