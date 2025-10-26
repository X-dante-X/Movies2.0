using Models.DTO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace MovieService.Helpers;

/// <summary>
/// Helper class for extracting and normalizing file names for movies, persons, and production companies.
/// Ensures file names are safe, unique, and consistent for storage and retrieval.
/// </summary>
public static class ExtractFileName
{
    /// <summary>
    /// Generates a normalized and unique file name for a movie.
    /// Combines the movie title and release date (yyyyMMdd) with a unique suffix.
    /// </summary>
    /// <param name="movieDTO">Movie data transfer object containing title and release date.</param>
    /// <returns>Normalized unique file name string for the movie.</returns>
    public static string ExtractMovieFileName(MovieDTO movieDTO)
    {
        var releaseDate = movieDTO.ReleaseDate ?? DateTime.Now;
        string datePart = releaseDate.ToString("yyyyMMdd");

        string titlePart = NormalizeFileName(movieDTO.Title);

        return $"{titlePart}-{datePart}";
    }

    /// <summary>
    /// Generates a normalized and unique file name for a person's photo.
    /// Combines the person's name and date of birth (yyyyMMdd) with a unique suffix.
    /// </summary>
    /// <param name="personDTO">Person data transfer object containing name and date of birth.</param>
    /// <returns>Normalized unique file name string for the person's photo.</returns>
    public static string ExtractPhotoFileName(PersonDTO personDTO)
    {
        string datePart = personDTO.DateOfBirth.ToString("yyyyMMdd");

        string personNamePart = NormalizeFileName(personDTO.PersonName);

        return $"{personNamePart}-{datePart}";
    }

    /// <summary>
    /// Generates a normalized and unique file name for a production company's logo.
    /// Uses the company name as the base and appends a unique suffix.
    /// </summary>
    /// <param name="productionCompanyDTO">Production company DTO containing company name.</param>
    /// <returns>Normalized unique file name string for the logo.</returns>
    public static string ExtractLogoFileName(ProductionCompanyDTO productionCompanyDTO)
    {
        return NormalizeFileName(productionCompanyDTO.CompanyName);
    }

    /// <summary>
    /// Normalizes an input string for safe file naming:
    /// - Removes diacritics
    /// - Replaces non-alphanumeric characters with hyphens
    /// - Trims extra hyphens
    /// - Appends a short unique GUID suffix
    /// </summary>
    /// <param name="input">Input string to normalize.</param>
    /// <returns>Normalized, unique, filesystem-safe string.</returns>
    private static string NormalizeFileName(string input)
    {
        // Remove diacritics
        string normalized = input.Normalize(NormalizationForm.FormD);
        normalized = new string(normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());

        // Replace non-alphanumeric characters with '-'
        normalized = Regex.Replace(normalized, "[^a-zA-Z0-9]+", "-");

        // Append a short unique GUID
        string uniquePart = Guid.NewGuid().ToString("N").Substring(0, 6);

        return $"{normalized.Trim('-')}-{uniquePart}";
    }
}
