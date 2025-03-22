using MoviesCore.Models;

namespace MoviesCore.Interfaces;
public interface IAdminService
{
    Task<Movie> AddMovie(Movie movie);
    Task<Movie> UpdateMovie(Movie movie);
    Task DeleteMovie(int movieId);


    Task<Person> AddPerson(Person person);
    Task<Person> UpdatePerson(Person person);
    Task DeletePerson(int personId);

    Task<ProductionCompany> AddProductionCompany(ProductionCompany productionCompany);
    Task<ProductionCompany> UpdateProductionCompany(ProductionCompany productionCompany);
    Task DeleteProductionCompany(int productionCompanyId);

    Task SetKeywordsToMovie(int movieId, int[] keywordsId);
    Task SetGenresToMovie(int movieId, int[] genresId);
    Task SetProductionCountriesToMovie(int movieId, int[] productionCountriesId);
    Task SetMoviesToProdactionCompany(int prodactionCompanyId, int[] moviesId);

}
