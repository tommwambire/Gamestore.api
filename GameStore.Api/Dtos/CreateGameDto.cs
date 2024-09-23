using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    /* data annotaions at the beginning to specify incoming data format requirements
    to acctually do the validation you need endpoint filters. One from nuget

    To do the actual validation, a package (MinimalApis.Extensions) is imported
    */

    [Required][StringLength(50)]string Name,
    int GenreId,
    [Range(1, 250)]decimal Price,
    DateOnly ReleaseDate
);
