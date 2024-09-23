using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    // extends the WebApplication Class as app
    //MigrateDb method migrates the database everytime on app run
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        //creating a scoped Service using .Services for dependecy injection
        //A scope service is an action or a service created once per requestor operation
        //Scoped services are disposed of as soon as they are fullfiled (keyword `using`)
        using var scope = app.Services.CreateScope();

        // instance within the scope that resolves instance of the GameStoreContext 
        //GameStoreContext is this projects dbContext (runs everytime on app run)

        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        //using the instance of the properties of the dbContext run the migrate functionality
        await dbContext.Database.MigrateAsync();
    }

}
