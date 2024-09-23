using System;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;
//DbContext is an object that represents a session with the database
//It enables CRUD into the db

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    //slightly modifies the database model by adding static or some default data into the databse
    // This happens right after runtime and the db is populated with the data below
    // overrding the Genre model (that creates the Genres table)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "racing"},
            new {Id = 2, Name = "fighting"},
            new {Id = 3, Name = "sports"},
            new {Id = 4, Name = "freeworld"},
            new {Id = 5, Name = "adventure"}
        );
    }

}


/*public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {

    }

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

}
*/


