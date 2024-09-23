 # Gamestore.api
This is a .NET 8 web api to handle CRUD operations and update the database as appropriately as possible. It contains different files that are important in its functions. Files that have simmilar functionality or handle simmilar operations are stored in the same folder.

The following is the file structure and what each directory contains or stores;

- Data - Stores the DbContext files and stores the various migrations to Database files.
- Dtos(Data Transfer Objects/ Contracts) - Handles the format of data between user inputs and the data models.
- Endpoints - Stores the various API endpoints (Put, Get, Post, Delete)
- Entities - Contains the API models and the various properties. Also forms the basis for database table creations.
- Mapping - Maps user input data or requests to API data and Api data to user output
-  Properties - Stores configuration details about the project
