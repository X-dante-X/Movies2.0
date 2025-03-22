namespace Models.DTO;

public class ProductionCompanyDTO
{
    public string CompanyName { get; set; } = null!;
    public IFile Logo { get; set; } = null!;
    public int CountryId { get; set; }
}
