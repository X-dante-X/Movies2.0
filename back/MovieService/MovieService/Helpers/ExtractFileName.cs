using Models.DTO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace MovieService.Helpers;

public static class ExtractFileName
{
    public static string ExtractMovieFileName(MovieDTO movieDTO)
    {
        var releaseDate = movieDTO.ReleaseDate ?? DateTime.Now;
        string datePart = releaseDate.ToString("yyyyMMdd");

        string titlePart = NormalizeFileName(movieDTO.Title);

        return $"{titlePart}-{datePart}";
    }

    public static string ExtractPhotoFileName(PersonDTO personDTO)
    {
        string datePart = personDTO.DateOfBirth.ToString("yyyyMMdd");

        string personNamePart = NormalizeFileName(personDTO.PersonName);

        return $"{personNamePart}-{datePart}";
    }

    public static string ExtractLogoFileName(ProductionCompanyDTO productionCompanyDTO)
    {
        return NormalizeFileName(productionCompanyDTO.CompanyName);
    }

    private static string NormalizeFileName(string input)
    {
        string normalized = input.Normalize(NormalizationForm.FormD);
        normalized = new string(normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());

        normalized = Regex.Replace(normalized, "[^a-zA-Z0-9]+", "-");

        string uniquePart = Guid.NewGuid().ToString("N").Substring(0, 6);

        return $"{normalized.Trim('-')}-{uniquePart}";
    }
}
