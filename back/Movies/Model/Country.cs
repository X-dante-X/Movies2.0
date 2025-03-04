namespace Models;

public class Country
{
    public int CountryId { get; set; }
    public string CountryIsoCode { get; set; } = null!;
    public string CountryName { get; set; } = null!;
    public List<Movie>? Movies { get; set; }
    public List<ProductionCompany>? ProductionCompanys { get; set; }
}