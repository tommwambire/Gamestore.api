using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//connecting to the database
/*In order to connect to the database configuration and access details are required
Passing in the data in this file manually is not best practise
the web application library has an interface that can be used to access cnfiguration details using too much detail
This is the iConfiguration interface shown below
*/
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

// map crud enpoints to Games
app.MapGamesEndpoints();

//map endpoints to Genres
app.MapGenresEndPoints();

//Migrate Db on app run to ensure the most up to date version of db is available
await app.MigrateDbAsync();

app.Run();