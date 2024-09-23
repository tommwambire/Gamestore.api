using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Dtos;
public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    //creating a static class only containing the list of data items
    /*private static readonly List<GameSummaryDto> games = [
        new (
            1,
            "Street Fighter 1",
            "Fighting",
            9.78M,
            new DateOnly(1992, 7, 15)),
        new (
            2,
            "Mortal Kombat 1", 
            "Fighting",
            100.30M,
            new DateOnly(2020, 10, 20)),
        new (
            3,
            "Grand Theft Auto 5",
            "Freeworld",
            105.11M,
            new DateOnly(2014, 8, 20)),
        new (
            4,
            "Fifa 23",
            "Sports",
            88.88M,
            new DateOnly(2023, 9, 16)),

    ];*/

    /*
    creates a class that extends the global and locked class WebApplication and adds the MapGamesEndpoints functionallity
    Theres a difference between extending and inheriting
    Extending is mostly done to locked classes to add functionality
    Inheriting is done to classes that are not locked.
    Best practice is to only use extending where it is absolutely necessary else use inheriting
    */
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        /*
         creates a route group (This means we do not have to repear games in the routing eg)
         MapGet("games", ....)
         this means that we also have to change the app. sections into group and drop the "games" in the routing
         WebApplication after static also changes to RouteGroupBuilder
        */
        var group = app.MapGroup("games")
                    .WithParameterValidation();
        // parameter validation at group level

        //Get /games
        //Gets all games in database using dbcontext
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                //For each game include the Genre string
                .Include(game => game.Genre)

                //return each game in the GameSummaryDto format
                .Select(game => game.ToGameSummaryDto())

                //ASP.NET doesnt track each game. Saves on resources
                .AsNoTracking()
                
                //handles the output asynchronously and returns a list
                .ToListAsync());

        // Get/games/1
        // async await shows that the mapget can be handled asynchronously
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => 
        {
            Game? game = await dbContext.Games.FindAsync(id);

            //GameDto? game = games.Find(game => game.Id == id); // explicit data type GameDto? is nullable

            //checks whether var game is null and adjusts results accordingly    
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);


        // Post create game
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            //Instead of posting according to CreateGameDto which is linked to Games
            // This is done using the GameStoreContext dependecy injected in
            //Code for this is now under GameMapping to prevent the file looking to

            Game game = newGame.ToEntity();


            //dbContext will start keeping track of data to be added to database
            dbContext.Games.Add(game);

            // save changes into the database
            await dbContext.SaveChangesAsync();

            /*Due to the new format (Genre details from Genre Id), a new output format from the post
             response has to be created
             This means having to specify a new format as seen the gameDto instance below
            */

            
            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game.ToGameDetailsDto());

            /*
            // Old format where post was happening according to GameDto

            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);
            

            //games.Add(game);

            //Returns the route of the game created(endpointname/game name, id, game details)
            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
            */
        })
        // parameter validation from MinimalApis.Extensions
        .WithParameterValidation();

        // Put (update)
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            

            if (existingGame is null)
            {
                return Results.NotFound();
            }
            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));

            await dbContext.SaveChangesAsync();
        
            
            return Results.NoContent();    
        
        });

        // Delete/games/{id}
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            //batch delete- Deletes in one statment(finds matching id and deletes)
            await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();


            return Results.NoContent();
        });

        // return also changes from app to group
        return group;
    }

}

